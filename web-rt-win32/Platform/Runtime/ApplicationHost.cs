using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using WebRT.Foundation;
using WebRT.Platform.Packages;
using WebRT.Platform.Integration;
using WebRT.Platform.Host;

namespace WebRT.Platform.Runtime
{

    partial class ApplicationHost : Form
    {
        private ChromiumWebBrowser WebView;

        protected bool DebugMode = true;

        public ApplicationProcess AppProcess;

        public Bitmap PreviewImage = null;

        public string ViewName;

        public AppWindowDefinition Styles;

        public string Label;
        
        private bool WasClosed = false;

        private Injectable[] InjectedItems;

        public ApplicationHost(ApplicationProcess process)
        {
            AppProcess = process;
            AppProcess.ProcessKill += new ApplicationProcess.ProcessEventHandler(OnProcessDie);
            AppProcess.ProcessCreate += new ApplicationProcess.ProcessEventHandler(OnProcessCreated);
        }

        protected string GetResourceFullPath(string resourceName)
        {
            return FSHelper.NormalizeLocation($"{AppProcess.DomainPath}\\{resourceName}");
        }

        protected void InitializeWebView()
        {


            // Create a browser component
            WebView = new ChromiumWebBrowser(GetResourceFullPath(ViewName)) {
                Dock = DockStyle.Fill
            };

            ApplyDependencies();

            WebView.IsBrowserInitializedChanged += OnWebViewInitialised;

            // Add it to the form and fill it to the form window.
            this.Controls.Add(WebView);

        }

        private void ApplyDependencies()
        {
            foreach (Injectable dependency in InjectedItems)
            {
                // RegisterJsObject
                if (dependency.Async)
                {
                    WebView.RegisterAsyncJsObject(dependency.Name, dependency, BindingOptions.DefaultBinder);
                } else
                {
                    WebView.RegisterJsObject(dependency.Name, dependency, BindingOptions.DefaultBinder);
                }
            }
        }

        public void DefineDependencies(Injectable[] injectables)
        {
            if (InjectedItems == null)
            {
                InjectedItems = injectables;
            }
        }

        private void OnProcessCreated(ApplicationProcess process, EventArgs e)
        {
            InitializeComponent();
            InitializeWebView();
            Show();

            ApplyStyles();
            
        }

        private void OnWebViewInitialised(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            //if (HostConfigurationProvider.GetInstance().GetConfiguration().DebugModeEnabled)
            //{
            //    WebView.ShowDevTools();
            //}
        }

        private void ApplyStyles()
        {
            if (Styles != null)
            {
                Height = Styles.Height;
                Width = Styles.Width;

                FormBorderStyle = Styles.Resizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;

                Text = Styles.Title ?? Label;

                MaximizeBox = Styles.Maximizable;
            }
        }

        private void OnProcessDie(ApplicationProcess process, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!WasClosed)
            {
                WasClosed = true;
                AppProcess.Kill();
            }
        }
    }
}
