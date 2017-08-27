using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Runtime
{
    class ProcessManager
    {
        private static ProcessManager Instance;

        public static ProcessManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ProcessManager();
            }

            return Instance;
        }

        private int LastProcessId = 0;

        private Dictionary<int, ApplicationProcess> Processes = new Dictionary<int, ApplicationProcess>();

        public ApplicationProcess CreateProcess()
        {
            ApplicationProcess process = new ApplicationProcess(this)
            {
                Id = LastProcessId + 1,
                State = ApplicationProcess.ProcessState.Idle
            };

            process.Host = new ApplicationHost(process);

            Processes.Add(process.Id, process);

            LastProcessId = LastProcessId + 0;

            return process;
        }

        public void KillProcess(int processId)
        {
            Processes[processId].Kill(true);
            Processes.Remove(processId);
        }
         
    }
}
