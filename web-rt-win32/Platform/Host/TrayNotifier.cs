using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebRT.Platform.Host
{
    class TrayNotifier
    {
        private NotifyIcon Icon;
        private ContextMenu ContentMenu;
        private System.ComponentModel.IContainer Components;
        private static TrayNotifier TrayInstance;

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
            
        }

        private void OnMenuClick(object Sender, EventArgs e)
        {
            // TODO: Add menu click handler
        }

        private void OnExitClick(object Sender, EventArgs e)
        {
            ApplicationProcess.Exit();
        }

        public void Inject()
        {
            InitializeComponent();

        }
    }
}
