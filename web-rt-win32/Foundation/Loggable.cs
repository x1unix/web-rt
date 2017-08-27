using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Foundation
{
    abstract class Loggable
    {
        protected void Log(string message)
        {
            Logger.GetInstance().Log(this.GetType().Name, message);
        }

        protected void LogInfo(string message)
        {
            Logger.GetInstance().Info(this.GetType().Name, message);
        }

        protected void LogWarn(string message)
        {
            Logger.GetInstance().Warn(this.GetType().Name, message);
        }

        protected void LogError(string message)
        {
            Logger.GetInstance().Error(this.GetType().Name, message);
        }
    }
}
