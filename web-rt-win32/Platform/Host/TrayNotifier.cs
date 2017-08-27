using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebRT.Foundation;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Host
{
    class TrayNotifier: Loggable
    {
        private NotifyIcon Icon;
        private ContextMenu ContentMenu;
        private System.ComponentModel.IContainer Components;
        private static TrayNotifier TrayInstance;
        private const string LauncherAppName = "com.webrt.launcher";

        public static TrayNotifier GetInstance()
        {
            if (TrayInstance == null)
            {
                TrayInstance = new TrayNotifier();
            }

            return TrayInstance;
        }

        public TrayNotifier()
        {
            Components = new System.ComponentModel.Container();
            ContentMenu = new ContextMenu();
        }

        private void InitializeComponent()
        {
            Logger log = Logger.GetInstance();

            log.Clear();
            log.WriteRaw($" === WebRT Started, Version {Application.ProductVersion} === ");

            MenuItem allAppsItem = new MenuItem("Show All Applications");
            allAppsItem.Click += new EventHandler(OnMenuClick);

            MenuItem exitItem = new MenuItem("Exit");
            exitItem.Click += new EventHandler(OnExitClick);

            MenuItem[] items = {
                allAppsItem,
                exitItem
            };

            ContentMenu.MenuItems.AddRange(items);

            Icon = new NotifyIcon(Components)
            {
                Icon = Properties.Resources.AppIconXS,
                ContextMenu = ContentMenu,
                Text = "Web Runtime",
                Visible = true
            };

            StartLauncher();
            
        }

        private void StartLauncher()
        {
            try
            {
                Launcher.GetInstance().StartApplication(LauncherAppName);
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                CrashReportWindow.CreateFromLog().ShowDialog();
            }
        }

        private void OnMenuClick(object Sender, EventArgs e)
        {
            StartLauncher();
        }

        private void OnExitClick(object Sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public void Inject()
        {
            InitializeComponent();

        }
    }
}
