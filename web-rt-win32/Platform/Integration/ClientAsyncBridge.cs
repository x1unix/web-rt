using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    /// <summary>
    /// A bridge between web view client and host runtime to execute VM calls
    /// </summary>
    class ClientAsyncBridge
    {
        private readonly ApplicationProcess CurrentProcess;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="process">Application process</param>
        public ClientAsyncBridge(ApplicationProcess process)
        {
            CurrentProcess = process;
        }


        /// <summary>
        /// Invoke call
        /// </summary>
        /// <param name="controller">Controller name</param>
        /// <param name="action">Action name</param>
        /// <param name="args">Serialized JSON arguments</param>
        /// <returns></returns>
        public string Invoke(string controller, string action, string args)
        {
            return RequestDispatcherProvider.GetInstance()
                .GetMainRequestDispatcher()
                .InvokeRequest(CurrentProcess, controller, action, args);
        }

        /// <summary>
        /// Get list of controllers
        /// </summary>
        /// <returns></returns>
        public string[] GetHandlersList()
        {
            return RequestDispatcherProvider.GetInstance()
                .GetMainRequestDispatcher()
                .GetHandlersList();
        }
    }
}
