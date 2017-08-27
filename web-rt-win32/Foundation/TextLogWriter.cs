using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebRT.Platform.Host;

namespace WebRT.Foundation
{
    class TextLogWriter: LogWriter
    {
        private string FileName;

        public TextLogWriter()
        {
            FileName = HostConfigurationProvider.GetInstance().GetConfiguration().LogFile;
        }

        public override void Write(string message)
        {
            using (StreamWriter sw = File.AppendText(FileName))
            {
                sw.WriteLine(message);
            }
        }

        public override void Clear()
        {
            File.Delete(FileName);
        }
    }
}
