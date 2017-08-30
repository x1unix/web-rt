using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebRT.Platform.Packages;
using WebRT.Foundation;
using System.Windows.Forms;
using WebRT.Platform.Integration;

namespace WebRT.Platform.Runtime
{
    class Launcher: Loggable
    {
        private static Launcher Instance;

        public static Launcher GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Launcher();
            }

            return Instance;
        }

        public ApplicationProcess StartApplication(string packageName)
        {
            try
            {
                AppManifest app = PackageManager.GetInstance().GetPackage(packageName);

                if (app == null)
                {
                    throw new LauncherException($"Failed to start application '{packageName}'. Application not found");
                }

                return StartApplication(app);
            } catch(Exception ex)
            {
                throw new LauncherException($"Failed to start application '{packageName}'. {ex.Message}");
            }
        }

        public ApplicationProcess StartApplication(AppManifest manifest)
        {
            string viewLocation = FSHelper.NormalizeLocation($"{manifest.Location}\\{manifest.MainPage}");

            Log($"Starting application from package '{manifest.Domain}'");

            if (!File.Exists(viewLocation))
            {
                throw new LauncherException($"Failed to start application '{manifest.Name}' ({manifest.Domain}). View not found.");
            }

            ApplicationProcess proc = ProcessManager.GetInstance().CreateProcess();
            proc.Name = manifest.Name;
            proc.Domain = manifest.Domain;
            proc.DomainPath = manifest.Location;

            // TODO: Add icon loader
            proc.Host.ViewName = manifest.MainPage;
            proc.Host.Styles = manifest.Window;
            proc.Host.Label = manifest.Name;


            proc.Start();
            proc.Host.Show();

            return proc;
        }
    }
}
