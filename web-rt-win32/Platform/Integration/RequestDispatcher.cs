using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Foundation;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    /// <summary>
    /// Request router and dispatcher
    /// </summary>
    class RequestDispatcher
    {
        private Dictionary<string, RequestController> Controllers = new Dictionary<string, RequestController>();


        /// <summary>
        /// Register a new controller
        /// </summary>
        /// <param name="controllerInstance"></param>
        /// <returns></returns>
        public RequestDispatcher AddController(RequestController controllerInstance)
        {
            Controllers.Add(controllerInstance.GetControllerName(), controllerInstance);

            return this;
        }


        /// <summary>
        /// Add a range of controllers
        /// </summary>
        /// <param name="controllers"></param>
        /// <returns></returns>
        public RequestDispatcher AddControllers(RequestController[] controllers)
        {
            foreach (RequestController controller in controllers)
            {
                Controllers.Add(controller.GetControllerName(), controller);
            }

            return this;
        }

        /// <summary>
        /// Execute request
        /// </summary>
        /// <param name="process">Application process</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="actionName">Action name</param>
        /// <param name="data">JSON data</param>
        /// <returns></returns>
        public string InvokeRequest(ApplicationProcess process, string controllerName, string actionName, string data)
        {
            if (!Controllers.ContainsKey(controllerName))
            {
                throw new ArgumentOutOfRangeException($"Requested request handler not found: {controllerName}");
            }

            RequestController ctrl = Controllers[controllerName];

            if (!ctrl.HasAction(actionName))
            {
                throw new ArgumentOutOfRangeException($"Requested action '{actionName}' not available in {controllerName}");
            }

            ActionHandler handler = ctrl.GetActionHandler(actionName);

            return handler(process, data);
        }

        /// <summary>
        /// Get list of available controllers
        /// </summary>
        /// <returns></returns>
        public string[] GetHandlersList()
        {
            return Controllers.Keys.ToArray();
        }

    }
}
