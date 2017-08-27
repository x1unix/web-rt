using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebRT.Foundation;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Packages
{
    class PackagesRepository: Loggable
    {
        protected string Source;

        private bool CheckPerformed = false;
        private bool Cached = false;

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

        public void CachePackages()
        {
            Task<Dictionary<string, AppManifest>> task = Task.Run<Dictionary<string, AppManifest>>(async () => await CachePackagesAsync());

            try
            {
                task.Wait();
            }
            catch (Exception ex)
            {
                string msg = $"Failed to cache packages: {ex.Message}";
                LogError(msg);
                throw new RuntimeException(msg);
            }
        }

        public Dictionary<string, AppManifest> GetPackages()
        {
            if (!Cached)
            {
                CachePackages();
            }

            return PackagesStore;
        }

        public bool HasPackage(string name)
        {
            if (!Cached)
            {
                CachePackages();
            }

            return PackagesStore.ContainsKey(name);
        }

        public AppManifest GetPackage(string name)
        {
            if (!Cached)
            {
                CachePackages();
            }

            return PackagesStore[name];
        }

        public async Task<Dictionary<string, AppManifest>> CachePackagesAsync()
        {
            if (Cached)
            {
                return PackagesStore;
            }

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
                    LogInfo($"Fetched {cached.Count} items from cache");
                    PackagesStore = cached;
                }

            }

            Cached = true;
            return PackagesStore;
        }

        public async Task<AppManifest> GetPackageAsync(string packageDomain)
        {
            Dictionary<string, AppManifest> packages = await CachePackagesAsync();

            return packages.ContainsKey(packageDomain) ? packages[packageDomain] : null;
        }

        public async Task<bool> HasPackageAsync(string packageDomain)
        {
            Dictionary<string, AppManifest> packages = await CachePackagesAsync();

            return packages.ContainsKey(packageDomain);
        }
    }
}
