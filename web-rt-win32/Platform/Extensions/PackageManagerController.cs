using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Foundation;
using WebRT.Platform.Integration;
using WebRT.Platform.Packages;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Extensions
{
    [BindController("com.webrt.pm")]
    class PackageManagerController: RequestController
    {
        [BindAction()]
        public string GetPackagesList(ApplicationProcess invoker, string args)
        {
            return Response(PackageManager.GetInstance().GetPackages());
        } 
    }
}
