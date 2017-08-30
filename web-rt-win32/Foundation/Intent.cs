using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Foundation
{
    class Intent
    {
        public string Target;
        public string Action;
        public Uri Data;
        public string Category;
        public string Type;
        public Bundle extras;
    }
}
