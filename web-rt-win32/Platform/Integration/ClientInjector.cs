using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    class ClientInjector
    {
        public static Dictionary<string, Type> Injectables = new Dictionary<string, Type>();

        public static void DefineDefaultDependencies()
        {
            Injectables.Add("PackageManager", typeof (PackageManagerBinder));
            Injectables.Add("Launcher", typeof(LauncherBinder));
        }

        public static void ProvideDependencies(string[] dependencies, ApplicationProcess proc) {
            if (dependencies == null)
            {
                return;
            }

            List<Injectable> items = new List<Injectable>();

            foreach (string dependency in dependencies)
            {
                if (!Injectables.ContainsKey(dependency))
                {
                    throw new RuntimeException($"Application requested undefined dependency: {dependency}");
                }

                Type type = Injectables[dependency];
                items.Add((Injectable)Activator.CreateInstance(type));
            }

            proc.Host.DefineDependencies(items.ToArray());
        }
    }
}
