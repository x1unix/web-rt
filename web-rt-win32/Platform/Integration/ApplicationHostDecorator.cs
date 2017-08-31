using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    /// <summary>
    /// Web view preparation helper
    /// </summary>
    class ApplicationHostDecorator
    {
        /// <summary>
        /// Create a new client-host bridge and bind it to the web view
        /// </summary>
        /// <param name="process">Application process</param>
        /// <param name="webView">Client web view</param>
        public static void PrepareEnvironment(ApplicationProcess process, ChromiumWebBrowser webView)
        {
            webView.RegisterJsObject("application", new WindowApplication(process), BindingOptions.DefaultBinder);
            webView.RegisterAsyncJsObject("runtime", new ClientAsyncBridge(process), BindingOptions.DefaultBinder);
        }
    }
}
