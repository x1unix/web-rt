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

namespace WebRT
{

    public partial class Form1 : Form
    {
        private ChromiumWebBrowser WebView;
        protected bool DebugMode = true;

        public Form1()
        {
            InitializeComponent();
            InitializeWebView();
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
