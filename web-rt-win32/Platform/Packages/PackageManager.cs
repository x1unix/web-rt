using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Host;

namespace WebRT.Platform.Packages
{
    class PackageManager
    {
        private static PackageManager Instance;

        public static PackageManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PackageManager();
            }

            return Instance;
        }

        public PackagesRepository GlobalPackages;
        public PackagesRepository SystemPackages;
        public PackagesRepository LocalPackages;
        public HostConfiguration Configuration;


        public PackageManager()
        {
            Configuration = HostConfigurationProvider.GetInstance().GetConfiguration();

            // Init package sources
            GlobalPackages = new PackagesRepository(Configuration.GlobalPackagesLocation);
            LocalPackages = new PackagesRepository(Configuration.LocalPackagesLocation);
            SystemPackages = new PackagesRepository(Configuration.SystemPackagesLocation);
        }


        public AppManifest GetPackage(string packageDomain)
        {
            if (SystemPackages.HasPackage(packageDomain))
            {
                return SystemPackages.GetPackage(packageDomain);
            }

            //if (await GlobalPackages.HasPackage(packageDomain))
            //{
            //    return await GlobalPackages.GetPackage(packageDomain);
            //}

            //if (await LocalPackages.HasPackage(packageDomain))
            //{
            //    return await LocalPackages.GetPackage(packageDomain);
            //}

            return null;
        }
    }
}
