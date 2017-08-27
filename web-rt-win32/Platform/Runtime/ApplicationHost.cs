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

namespace WebRT.Platform.Runtime
{

    partial class ApplicationHost : Form
    {
        private ChromiumWebBrowser WebView;

        protected bool DebugMode = true;

        public ApplicationProcess Process;

        public Bitmap PreviewImage;

        private string ViewURL;

        public string ViewLocation
        {
            get
            {
                return ViewURL;
            }

            set
            {
                ViewURL = new System.Uri(FSHelper.NormalizeLocation($"{Process.DomainPath}\\{value}")).AbsoluteUri;
            }
        }

        private bool WasClosed = false;

        public ApplicationHost(ApplicationProcess process)
        {
            Process = process;
            Process.ProcessKill += new ApplicationProcess.ProcessEventHandler(OnProcessDie);
            Process.ProcessCreate += new ApplicationProcess.ProcessEventHandler(OnProcessCreated);
        }

        protected void InitializeWebView()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings

            Cef.Initialize(settings);
            // Create a browser component
            WebView = new ChromiumWebBrowser("http://google.com") {
                Dock = DockStyle.Fill
            };

            // Add it to the form and fill it to the form window.
            this.Controls.Add(WebView);

        }

        private void OnProcessCreated(ApplicationProcess process, EventArgs e)
        {
            InitializeComponent();
            InitializeWebView();
            Show();
        }

        private void OnProcessDie(ApplicationProcess process, EventArgs e)
        {
            if (!WasClosed)
            {
                Close();
            }

            Cef.Shutdown();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!WasClosed)
            {
                WasClosed = true;
                Process.Kill();
            }
        }
    }
}
