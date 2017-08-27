using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Package name (ex: 1.0.0-beta.1)
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Application description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Application background color
        /// </summary>
        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Theme color
        /// </summary>
        [JsonProperty("themeColor")]
        public string ThemeColor { get; set; }

        /// <summary>
        /// Screen orientation
        /// </summary>
        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("iconSmall")]
        public string IconSmall { get; set; }

        [JsonProperty("iconLarge")]
        public string IconLarge { get; set; }

        [JsonProperty("iconColor")]
        public string IconColor { get; set; }

        /// <summary>
        /// Location of the main page
        /// </summary>
        [JsonProperty("mainPage")]
        public string MainPage { get; set; }

        [JsonProperty("window", DefaultValueHandling = DefaultValueHandling.Populate)]
        public AppWindowDefinition Window { get; set; }
        /// <summary>
        /// List of required permissions
        /// </summary>
        [JsonProperty("permissions")]
        public string[] RequiredPermissions { get; set; }

        [JsonProperty("useModules")]
        public string[] RequiredModules { get; set; }

        public string Location { get; set; }

        public AppManifest()
        {
            MainPage = "index.html";
            Window = new AppWindowDefinition();
            IconColor = "#666";
        }

        public static readonly string FileName = "application.json";

    }
}
