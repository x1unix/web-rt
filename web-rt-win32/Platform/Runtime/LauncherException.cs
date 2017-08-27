using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Runtime
{
    [Serializable()]
    class LauncherException: RuntimeException
    {
        public LauncherException() : base() { }
        public LauncherException(string message) : base(message) { }
        public LauncherException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected LauncherException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
