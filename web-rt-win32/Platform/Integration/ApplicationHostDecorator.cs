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
    class ApplicationHostDecorator
    {
        public static void PrepareEnvironment(ApplicationProcess process, ChromiumWebBrowser webView)
        {
            webView.RegisterJsObject("currentProcess", new EnvironmentInformation(process), BindingOptions.DefaultBinder);
            webView.RegisterAsyncJsObject("runtimeBridge", new ClientAsyncBridge(process), BindingOptions.DefaultBinder);
        }
    }
}
