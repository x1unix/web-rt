using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Runtime
{
    class Permissions
    {
        public HashSet<string> Provided = new HashSet<string>();
        public HashSet<string> Required = new HashSet<string>();
    }
}
