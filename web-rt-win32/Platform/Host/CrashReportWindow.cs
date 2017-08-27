using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WebRT.Platform.Host
{
    public partial class CrashReportWindow : Form
    {
        public string Source;

        public CrashReportWindow()
        {
            InitializeComponent();
            ErrorIcon.Image = System.Drawing.SystemIcons.Error.ToBitmap();
        }

        private void CrashReportWindow_Load(object sender, EventArgs e)
        {
            LogText.Text = Source;
        }

        public static CrashReportWindow CreateFromFile(string file)
        {
            return new CrashReportWindow()
            {
                Source = File.ReadAllText(file)
            };
        }

        public static CrashReportWindow CreateFromLog()
        {
            return CreateFromFile(HostConfigurationProvider.GetInstance().GetConfiguration().LogFile);
        }

        private void CloseAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CrashReportWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
