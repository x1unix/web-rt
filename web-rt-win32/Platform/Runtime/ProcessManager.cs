using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Foundation;

namespace WebRT.Platform.Runtime
{
    /// <summary>
    /// Runtime process manager
    /// </summary>
    class ProcessManager: Loggable
    {
        private static ProcessManager Instance;

        public delegate void RootThreadTask();

        /// <summary>
        /// Get current instance
        /// </summary>
        /// <returns></returns>
        public static ProcessManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ProcessManager();
            }

            return Instance;
        }

        private int LastProcessId = 0;

        /// <summary>
        /// Process list storage
        /// </summary>
        private Dictionary<int, ApplicationProcess> Processes = new Dictionary<int, ApplicationProcess>();

        /// <summary>
        /// Create and register a new empty process
        /// </summary>
        /// <returns></returns>
        public ApplicationProcess CreateProcess()
        {
            ApplicationProcess process = new ApplicationProcess(this)
            {
                Id = LastProcessId + 1,
                State = ApplicationProcess.ProcessState.Idle
            };

            process.Host = new ApplicationHost(process);

            Processes.Add(process.Id, process);

            LastProcessId = process.Id + 0;

            Log($"Created a new process with PID: {process.Id}");

            return process;
        }

        /// <summary>
        /// Kill process
        /// </summary>
        /// <param name="processId">Process ID</param>
        public void KillProcess(int processId)
        {
            Processes[processId].Kill(true);
            Processes.Remove(processId);
            Log($"Process ({processId}) was killed");
        }
         
    }
}
