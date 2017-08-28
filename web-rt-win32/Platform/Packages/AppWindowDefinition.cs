using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Packages
{
    [Serializable]
    class AppWindowDefinition
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("resizable")]
        public bool Resizable { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("maximizable")]
        public bool Maximizable { get; set; }

        public AppWindowDefinition()
        {
            Height = 768;
            Width = 1024;
            Resizable = true;
            Title = null;
            Maximizable = true;
        }
    }
}
