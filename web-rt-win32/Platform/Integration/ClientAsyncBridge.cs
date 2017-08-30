using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    class ClientAsyncBridge
    {
        private readonly List<string> LoadedModules;
        private readonly ApplicationProcess CurrentProcess;

        public ClientAsyncBridge(ApplicationProcess process)
        {
            CurrentProcess = process;
        }

        public string InvokeRuntimeCall(string controller, string action, string args)
        {
            return RequestDispatcherProvider.GetInstance()
                .GetMainRequestDispatcher()
                .InvokeRequest(CurrentProcess, controller, action, args);
        }
    }
}
