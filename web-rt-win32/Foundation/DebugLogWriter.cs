using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebRT.Foundation
{
    class DebugLogWriter: LogWriter
    {
        public override void Write(string message)
        {
            Debug.WriteLine(message);
        }

        public override void Clear()
        {
            
        }
    }
}
