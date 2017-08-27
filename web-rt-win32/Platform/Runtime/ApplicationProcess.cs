using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Packages;

namespace WebRT.Platform.Runtime
{
    class ApplicationProcess
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public string DomainPath { get; set; }

        public HashSet<int> Children { get; set; }

        public Permissions Permissions { get; set; }

        public event ProcessEventHandler ProcessKill;

        public event ProcessEventHandler ProcessCreate;

        private EventArgs e = null;

        public ApplicationHost Host;

        public enum ProcessState { Idle, Working, Dead }

        public ProcessState State = ProcessState.Idle;

        public delegate void ProcessEventHandler(ApplicationProcess p, EventArgs e);

        private ProcessManager Manager;

        public ApplicationProcess(ProcessManager manager)
        {
            Manager = manager;
        }

        public void Kill(bool fromParent = false)
        {
            if (!fromParent)
            {
                Manager.KillProcess(Id);
            }

            ProcessKill(this, e);
        }

        public void Start()
        {
            ProcessCreate(this, e);
        }
    }
}
