using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Integration;

namespace WebRT.Platform
{
    class Loader
    {
        private static bool Initialized = false;

        public static void BootstrapEnvironment()
        {
            ClientInjector.DefineDefaultDependencies();

            CefSettings settings = new CefSettings();

            //settings.RegisterScheme(new CefCustomScheme
            //{
            //    SchemeName = AppSchemeHandlerFactory.SchemeName,
            //    SchemeHandlerFactory = new AppSchemeHandlerFactory(AppProcess.DomainPath)
            //});

            // Initialize cef with the provided settings

            Cef.Initialize(settings);
        }

        public static bool IsInitialized()
        {
            return Initialized;
        }

        public static void Shutdown()
        {
            Cef.Shutdown();
        }
    }
}
