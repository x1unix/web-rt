using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebRT.Platform.Integration
{
    class RequestController
    {
        private string DefinedReceiverName;

        private Dictionary<string, ActionHandler> ActionHandlers;

        private bool HandlersCollected = false;

        public string Name
        {
            get
            {
                return DefinedReceiverName ?? this.GetType().ToString();
            }
        }

        public RequestController() { }

        public RequestController(string receiverName)
        {
            DefinedReceiverName = receiverName;
        }

        protected string Response(object[] data)
        {
            return JsonConvert.SerializeObject(data);
        }

        protected string Response(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        protected object[] ExtractArray(string source)
        {
            return JsonConvert.DeserializeObject<object[]>(source);
        }

        protected object Extract(string source)
        {
            return JsonConvert.DeserializeObject(source);
        }

        private void CollectActionHandlers()
        {
            ActionHandlers = new Dictionary<string, ActionHandler>();

            IEnumerable<MethodInfo> methods = GetType().GetMethods().Where(methodInfo => methodInfo.GetCustomAttributes(typeof(BindAction), true).Length > 0);

            foreach (MethodInfo method in methods)
            {
                var attr = method.GetCustomAttributes(true).OfType<BindAction>().FirstOrDefault();
                if (attr != null)
                {
                    string actionName = attr.Name ?? method.Name;
                    ActionHandlers.Add(actionName, (ActionHandler) Delegate.CreateDelegate(typeof(ActionHandler), this, method));
                }
            }

            HandlersCollected = true;
        }

        public string GetControllerName()
        {
            BindController[] attrs = (BindController[]) this.GetType().GetCustomAttributes(typeof(BindController), true);

            return attrs[0].Name ?? this.GetType().ToString();
        }

        public bool HasAction(string actionName)
        {
            if (!HandlersCollected)
            {
                CollectActionHandlers();
            }

            return ActionHandlers.ContainsKey(actionName);
        }

        public ActionHandler GetActionHandler(string actionName)
        {
            if (!HandlersCollected)
            {
                CollectActionHandlers();
            }

            return ActionHandlers[actionName];
        }

        public Dictionary<string, ActionHandler> GetActions()
        {
            if (!HandlersCollected)
            {
                CollectActionHandlers();
            }

            return ActionHandlers;
        }

    }
}
