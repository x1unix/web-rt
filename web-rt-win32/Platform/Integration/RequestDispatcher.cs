using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Foundation;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    class RequestDispatcher
    {
        private Dictionary<string, RequestController> Controllers = new Dictionary<string, RequestController>();

        public RequestDispatcher AddController(RequestController controllerInstance)
        {
            Controllers.Add(controllerInstance.GetControllerName(), controllerInstance);

            return this;
        }

        public RequestDispatcher AddControllers(RequestController[] controllers)
        {
            foreach (RequestController controller in controllers)
            {
                Controllers.Add(controller.GetControllerName(), controller);
            }

            return this;
        }

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

        public string[] GetHandlersList()
        {
            return Controllers.Keys.ToArray();
        }

    }
}
