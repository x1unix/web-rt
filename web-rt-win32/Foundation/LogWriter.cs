using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Foundation
{
    abstract class LogWriter
    {
        public abstract void Write(string message);

        public abstract void Clear();
    }
}
