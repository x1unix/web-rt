using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    class LauncherBinder: Injectable
    {
        public LauncherBinder()
        {
            Name = "Launcher";
            Async = false;
        }

        public void CallActivity(string activityName)
        {
            Launcher.GetInstance().StartApplication(activityName);
        }
    }
}
