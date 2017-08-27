using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Packages;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    class PackageManagerBinder: Injectable
    {
        public PackageManagerBinder()
        {
            Name = "PackageManager";
            Async = true;
        }

        
        public string GetPackages()
        {
            AppManifest[] items = PackageManager.GetInstance().GetPackages();
            return JsonConvert.SerializeObject(items);
        }
        
        public AppManifest[] echo()
        {
            AppManifest[] test = { new AppManifest() };

            return test;
        }
    }
}
