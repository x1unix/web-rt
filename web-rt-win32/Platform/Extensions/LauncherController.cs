using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Integration;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Extensions
{
    [BindController("WebRT.Platform.Launcher")]
    class LauncherController: RequestController
    {
        [BindAction()]
        public string StartApplication(ApplicationProcess invoker, string appName)
        {
            if (invoker.Domain != appName)
            {
                Launcher.GetInstance().StartApplication(appName);
            }

            return null;
        }
    }
}
