using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using WebRT.Platform.Runtime;

namespace WebRT.Platform.Integration
{
    /// <summary>
    /// Runtime client request handler
    /// </summary>
    class RequestController
    {
        /// <summary>
        /// Controller name
        /// </summary>
        private string DefinedReceiverName;


        /// <summary>
        /// Actions cache
        /// </summary>
        private Dictionary<string, ActionHandler> ActionHandlers;

        private bool HandlersCollected = false;

        /// <summary>
        /// Controller name
        /// </summary>
        public string Name
        {
            get
            {
                return DefinedReceiverName ?? this.GetType().ToString();
            }
        }

        /// <summary>
        /// Controller contstructor
        /// </summary>
        public RequestController() { }

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="receiverName">Controller</param>
        public RequestController(string receiverName)
        {
            DefinedReceiverName = receiverName;
        }

        /// <summary>
        /// Execute action on root UI thread
        /// </summary>
        /// <param name="process">Current application process</param>
        /// <param name="action">Lambda action</param>
        protected void RunOnUIThread(ApplicationProcess process, Action action)
        {
            ProcessManager.RootThreadTask task = new ProcessManager.RootThreadTask(action);
            process.Host.Invoke(task);
        }
      
        /// <summary>
        /// Prepare array as response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string Response(object[] data)
        {
            return JsonConvert.SerializeObject(data);
        }


        /// <summary>
        /// Prepare object as response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string Response(object data)
        {
            return JsonConvert.SerializeObject(data);
        }


        /// <summary>
        /// Extract array from serialized request
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected object[] ExtractArray(string source)
        {
            return JsonConvert.DeserializeObject<object[]>(source);
        }


        /// <summary>
        /// Extract object from request
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected object Extract(string source)
        {
            return JsonConvert.DeserializeObject(source);
        }


        /// <summary>
        /// Collect and cache action handlers
        /// </summary>
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


        /// <summary>
        /// Get controller name
        /// </summary>
        /// <returns></returns>
        public string GetControllerName()
        {
            BindController[] attrs = (BindController[]) this.GetType().GetCustomAttributes(typeof(BindController), true);

            return attrs[0].Name ?? this.GetType().ToString();
        }

        /// <summary>
        /// Check if the controller has action handler
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <returns></returns>
        public bool HasAction(string actionName)
        {
            if (!HandlersCollected)
            {
                CollectActionHandlers();
            }

            return ActionHandlers.ContainsKey(actionName);
        }

        /// <summary>
        /// Get action handler
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <returns></returns>
        public ActionHandler GetActionHandler(string actionName)
        {
            if (!HandlersCollected)
            {
                CollectActionHandlers();
            }

            return ActionHandlers[actionName];
        }

        /// <summary>
        /// Get list of actions
        /// </summary>
        /// <returns></returns>
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
