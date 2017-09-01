using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Integration;
using WebRT.Platform.Runtime;
using System.IO;

namespace WebRT.Platform.Extensions
{
    [BindController("WebRT.Platform.Storage")]
    class StorageController : RequestController
    {
        [BindAction()]
        public string GetExternalDrives(ApplicationProcess process, string args)
        {
            string systemDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            string[] drives = Array.FindAll(Directory.GetLogicalDrives(), c => c != systemDrive);

            return Response(drives);
        }

        [BindAction()]
        public string GetUserDirectory(ApplicationProcess process, string args)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        [BindAction()]
        public string GetDirectories(ApplicationProcess process, string args)
        {
            string path = args;
            return Response(Directory.GetDirectories(path));
        }

        [BindAction()]
        public string GetFiles(ApplicationProcess process, string args)
        {
            string path = args;
            return Response(Directory.GetFiles(path));
        }
    }
}
