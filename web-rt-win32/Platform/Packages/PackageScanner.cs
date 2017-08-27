
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebRT.Platform.Host;
using System.IO;
using Newtonsoft.Json;
using WebRT.Foundation;

namespace WebRT.Platform.Packages
{
    class PackageScanner: Loggable
    {
        private static PackageScanner Instance;

        public static PackageScanner GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PackageScanner();
            }

            return Instance;
        }


        /// <summary>
        /// Host configuration
        /// </summary>
        private HostConfiguration Configuration;

        public PackageScanner() {
            Configuration = HostConfigurationProvider.GetInstance().GetConfiguration();
        }

        /// <summary>
        /// Get list of system packages
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetSystemPackages()
        {
            return await GetPackagesFromPath(Configuration.SystemPackagesLocation);
        }

        /// <summary>
        /// Get list of global packages
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetGlobalPackages()
        {
            return await GetPackagesFromPath(Configuration.GlobalPackagesLocation);
        }


        /// <summary>
        /// Get list of local packages
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetLocalPackages()
        {
            return await GetPackagesFromPath(Configuration.LocalPackagesLocation);
        }


        /// <summary>
        /// Get list of applications from specific path
        /// </summary>
        /// <param name="path">Source path</param>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetPackagesFromPath(string path)
        {
            if (!Directory.Exists(path))
            {
                LogWarn($"Packages path source not exists: '{path}'");
                return null;
            }

            string[] directories = Directory.GetDirectories(path);

            if (directories.Length == 0)
            {
                return null;
            }

            Dictionary<string, AppManifest> packages = new Dictionary<string, AppManifest>();

            foreach (string dir in directories)
            {
                string manifestFile = dir + @"\" + AppManifest.FileName;

                if (!File.Exists(manifestFile))
                {
                    continue;
                }

                AppManifest manifest = await ReadManifestAsync(manifestFile);

                if (manifest != null)
                {
                    packages.Add(manifest.Id, manifest);
                }
            }

            return packages;
        }

        /// <summary>
        /// Read manifest file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        protected async Task<AppManifest> ReadManifestAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                try
                {
                    string manifestJson = sb.ToString();
                    AppManifest manifest = JsonConvert.DeserializeObject<AppManifest>(manifestJson);
                    return manifest;
                } catch (Exception ex)
                {
                    LogError($"Corrupted app manifest at '${filePath}'. Error: ${ex.Message}");
                    return null;
                }
            }
        }
    }
}
