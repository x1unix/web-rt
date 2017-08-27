using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebRT.Foundation;

namespace WebRT.Platform.Packages
{
    class PackagesRepository: Loggable
    {
        protected string Source;

        private bool CheckPerformed = false;

        protected Dictionary<string, AppManifest> PackagesStore;

        public PackagesRepository(string source)
        {
            Source = source;
        }

        private void CheckRepository()
        {
            if (CheckPerformed)
            {
                return;
            }

            if (!Directory.Exists(Source))
            {
                LogWarn($"Packages repository not exists: '{Source}'. A new one will be created");
                Directory.CreateDirectory(Source);
            }

            CheckPerformed = true;
        }

        public async Task<Dictionary<string, AppManifest>> GetPackages()
        {
            if (PackagesStore == null)
            {
                CheckRepository();

                var cached = await PackageCache.GetInstance().ReadCacheFromDirectoryAsync(Source);

                if ((cached == null) || (cached.Count == 0))
                {
                    PackagesStore = await PackageScanner.GetInstance().GetPackagesFromPath(Source);
                    await PackageCache.GetInstance().WriteCacheAtDirectoryAsync(Source, PackagesStore);
                }
                else
                {
                    PackagesStore = cached;
                }

            }

            return PackagesStore;
        }

        public async Task<AppManifest> GetPackage(string packageDomain)
        {
            Dictionary<string, AppManifest> packages = await GetPackages();

            return packages.ContainsKey(packageDomain) ? packages[packageDomain] : null;
        }

        public async Task<bool> HasPackage(string packageDomain)
        {
            Dictionary<string, AppManifest> packages = await GetPackages();

            return packages.ContainsKey(packageDomain);
        }
    }
}
