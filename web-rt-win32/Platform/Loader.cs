using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Foundation;
using WebRT.Platform.Integration;

namespace WebRT.Platform
{
    class Loader
    {
        private static bool Initialized = false;

        public static void BootstrapEnvironment()
        {

            CefSettings settings = new CefSettings();

            //settings.RegisterScheme(new CefCustomScheme
            //{
            //    SchemeName = AppSchemeHandlerFactory.SchemeName,
            //    SchemeHandlerFactory = new AppSchemeHandlerFactory(AppProcess.DomainPath)
            //});

            // Initialize cef with the provided settings

            Cef.Initialize(settings);

            Logger.GetInstance().Log("Loader", "CEF settings initialized");
            Logger.GetInstance().Info("Loader", "Environment initialized");
        }

        public static bool IsInitialized()
        {
            return Initialized;
        }

        public static void Shutdown()
        {
            Cef.Shutdown();
            Logger.GetInstance().Log("Loader", "CEF settings initialized");
            Logger.GetInstance().Info("Loader", "Environment was shut down gracefully");
        }
    }
}
