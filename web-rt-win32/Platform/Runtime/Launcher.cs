using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebRT.Platform.Packages;
using WebRT.Foundation;

namespace WebRT.Platform.Runtime
{
    class Launcher
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
            Task<AppManifest> task = Task.Run<AppManifest>(async () => await PackageManager.GetInstance().GetPackage(packageName));

            try
            {
                task.Wait();

                AppManifest app = task.Result;

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

            if (!File.Exists(viewLocation))
            {
                throw new LauncherException($"Failed to start application '{manifest.Name}' ({manifest.Domain}). View not found.");
            }

            ApplicationProcess proc = ProcessManager.GetInstance().CreateProcess();
            proc.Name = manifest.Name;
            proc.Domain = manifest.Domain;
            proc.DomainPath = manifest.Location;

            proc.Host.Text = manifest.Name;

            // TODO: Add icon loader
        
            proc.Host.ViewLocation = viewLocation;

            proc.Start();

            return proc;
        }
    }
}
