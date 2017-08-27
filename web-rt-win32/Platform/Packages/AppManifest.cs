using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Packages
{
    /// <summary>
    /// Application manifest definition
    /// </summary>
    [Serializable]
    class AppManifest
    {
        /// <summary>
        /// Appllication name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Package name (ex: 1.0.0-beta.1)
        /// </summary>
        public string Version { get; set; }

        public string Domain { get; set; }

        /// <summary>
        /// Application description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Package name (ex: com.company.name)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Application background color
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Theme color
        /// </summary>
        public string ThemeColor { get; set; }

        /// <summary>
        /// Screen orientation
        /// </summary>
        public string Orientation { get; set; }

        public string IconSmall { get; set; }

        public string IconLarge { get; set; }

        /// <summary>
        /// Location of the main page
        /// </summary>
        public string MainPage { get; set; }

        /// <summary>
        /// List of required permissions
        /// </summary>
        public string[] RequirePermissions { get; set; }

        public string Location { get; set; }

        public AppManifest()
        {
            MainPage = "index.html";
        }

        public static readonly string FileName = "application.json";

    }
}
