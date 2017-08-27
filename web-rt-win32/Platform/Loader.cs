using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Integration;

namespace WebRT.Platform
{
    class Loader
    {
        public static void BootstrapEnvironment()
        {
            ClientInjector.DefineDefaultDependencies();
        }
    }
}
