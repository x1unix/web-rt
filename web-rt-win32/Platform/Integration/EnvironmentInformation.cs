using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Runtime;
using System.Windows.Forms;

namespace WebRT.Platform.Integration
{
    class EnvironmentInformation
    {
        public readonly ApplicationProcess CurrentProcess;

        public readonly string Version;

        public EnvironmentInformation(ApplicationProcess process)
        {
            CurrentProcess = process;
            Version = Application.ProductVersion;
        }

        public string GetRuntimeVersion()
        {
            return Version;
        }

        public string GetPackageName()
        {
            return CurrentProcess.Domain;
        }

        public bool RequirePermission(string name)
        {
            return false;
        }
        
    }
}
