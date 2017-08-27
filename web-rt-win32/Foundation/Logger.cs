using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebRT.Foundation
{
    class Logger
    {
        private static Logger Instance;

        public static Logger GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Logger();
            }

            return Instance;
        }

        public enum LogLevels { Error, Warn, Info, Debug }

        public LogLevels LogLevel;

        public Logger()
        {
            LogLevel = LogLevels.Debug;
        }

        public void Write(LogLevels level, string source, string message)
        {
            if (level <= LogLevel)
            {
                Debug.WriteLine($"<{source}> [{Enum.GetName(typeof(LogLevels), level)}]: {message}");
            }
        }

        public void Log(string source, string message)
        {
            Write(LogLevels.Debug, source, message);
        }

        public void Info(string source, string message)
        {
            Write(LogLevels.Info, source, message);
        }

        public void Warn(string source, string message)
        {
            Write(LogLevels.Warn, source, message);
        }

        public void Error(string source, string message)
        {
            Write(LogLevels.Error, source, message);
        }
    }
}
