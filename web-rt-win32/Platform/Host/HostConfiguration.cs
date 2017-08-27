using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Host
{
    class HostConfiguration
    {
        /// <summary>
        /// Location of globally installed packages
        /// </summary>
        public string GlobalPackagesLocation { get; set; }
        public string LocalPackagesLocation { get; set; }
        public string SystemPackagesLocation { get; set; }
        public bool DebugModeEnabled { get; set; }

        public static readonly string LocalRootDirectoryName = ".webrt";
        public static readonly string PackagesDirectoryName = "Applications";

        public HostConfiguration()
        {
            this.SystemPackagesLocation = FormatPath(AppDomain.CurrentDomain.BaseDirectory, PackagesDirectoryName);
            this.GlobalPackagesLocation = FormatPath(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), PackagesDirectoryName);
            this.LocalPackagesLocation = FormatPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), PackagesDirectoryName);
        }

        /// <summary>
        /// Get location of WebRT folder for specific directory root
        /// </summary>
        /// <param name="root">Root directory</param>
        /// <param name="path">Path</param>
        /// <returns></returns>
        private static string FormatPath(string root, string path)
        {
            return $"{root}\\{LocalRootDirectoryName}\\${path}";
        }
    }
}
