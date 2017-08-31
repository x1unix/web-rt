using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Integration
{
    /// <summary>
    /// Controller binder decorator
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    class BindController: Attribute
    {
        private string ReceiverName;

        public string Name
        {
            get
            {
                return ReceiverName;
            }
        }

        public BindController(string name)
        {
            ReceiverName = name;
        }
    }
}
