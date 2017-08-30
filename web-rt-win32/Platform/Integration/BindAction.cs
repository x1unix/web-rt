using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Integration
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    class BindAction: Attribute
    {
        private string ActionName;

        public string Name
        {
            get
            {
                return ActionName;
            }
        }

        public BindAction() { }

        public BindAction(string name)
        {
            ActionName = name;
        }
    }
}
