using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebRT.Platform.Host;
using WebRT.Foundation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WebRT.Platform.Packages
{
    /// <summary>
    /// Package manifests cache manager
    /// </summary>
    class PackageCache: Loggable
    {
        private static readonly string CacheFileName = ".pkg-cache";
        private static PackageCache Instance;

        public static PackageCache GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PackageCache();
            }

            return Instance;
        }

        private HostConfiguration Configuration;

        public PackageCache()
        {
            Configuration = HostConfigurationProvider.GetInstance().GetConfiguration();
        }

        /// <summary>
        /// Get cache file path for specific directory
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        private string GetCacheFilePath(string directoryPath)
        {
            return FSHelper.NormalizeLocation($"{directoryPath}\\{CacheFileName}");
        }

        /// <summary>
        /// Write sync packages to cache at specific directory
        /// </summary>
        /// <param name="dirPath">Directory full path</param>
        /// <param name="items">Packages</param>
        /// <returns></returns>
        private bool WriteCacheAtDirectory(string dirPath, Dictionary<string, AppManifest> items)
        {
            if (!Directory.Exists(dirPath))
            {
                LogError($"Cache directory doesn't exists: {dirPath}, a new one will be created");
                Directory.CreateDirectory(dirPath);
            }

            string cacheFile = GetCacheFilePath(dirPath);
            bool success = false;

            FileStream fs = new FileStream(cacheFile, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, items);
                LogInfo($"{items.Count} items were cached to '{cacheFile}'");
                success = true;
            } catch (SerializationException ex)
            {
                LogError($"Failed to serialize items for cache at '{cacheFile}': {ex.Message}");
            } catch (Exception ex)
            {
                LogError($"Failed to create packages cache at '{cacheFile}': {ex.Message}");
            } finally
            {
                fs.Close();
            }

            return success;
        }

        /// <summary>
        /// Write packages cache async to file
        /// </summary>
        /// <param name="dirPath">Directory Path</param>
        /// <param name="items">Packages</param>
        /// <returns></returns>
        public async Task<bool> WriteCacheAtDirectoryAsync(string dirPath, Dictionary<string, AppManifest> items)
        {
            return await Task<bool>.Factory.StartNew(() => {
                return WriteCacheAtDirectory(dirPath, items);
            });
        }

        /// <summary>
        /// Cache list of local packages
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<bool> CacheLocalPackages(Dictionary<string, AppManifest> items)
        {
            return await WriteCacheAtDirectoryAsync(Configuration.LocalPackagesLocation, items);
        }


        /// <summary>
        /// Cache list of global packages
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<bool> CacheGlobalPackages(Dictionary<string, AppManifest> items)
        {
            return await WriteCacheAtDirectoryAsync(Configuration.GlobalPackagesLocation, items);
        }


        /// <summary>
        /// Cache list of system packages
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<bool> CacheSystemPackages(Dictionary<string, AppManifest> items)
        {
            return await WriteCacheAtDirectoryAsync(Configuration.SystemPackagesLocation, items);
        }

        private Dictionary<string, AppManifest> ReadCacheFromDirectory(string dir)
        {
            string cacheFile = GetCacheFilePath(dir);

            if (!File.Exists(cacheFile))
            {
                Log($"Cache file not exists: {cacheFile}");
                return null;
            }

            FileStream fs = new FileStream(cacheFile, FileMode.Open);

            if (fs.Length == 0)
            {
                Log($"Cache file '{cacheFile}' is empty, skipping...");
            }

            Dictionary<string, AppManifest> items = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                items = (Dictionary<string, AppManifest>) formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                LogError($"Failed to deserialize items from cache at '{cacheFile}': {ex.Message}");
            }
            catch (Exception ex)
            {
                LogError($"Failed to read packages cache at '{cacheFile}': {ex.Message}");
            }
            finally
            {
                fs.Close();
            }

            return items;
        }

        /// <summary>
        /// Read packages from cache
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> ReadCacheFromDirectoryAsync(string dir)
        {
            return await Task<Dictionary<string, AppManifest>>.Factory.StartNew(() => {
                return ReadCacheFromDirectory(dir);
            });
        }

        /// <summary>
        /// Get cached local packages
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetLocalPackages()
        {
            return await ReadCacheFromDirectoryAsync(Configuration.LocalPackagesLocation);
        }

        /// <summary>
        /// Get cached global packages
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetGlobalPackages()
        {
            return await ReadCacheFromDirectoryAsync(Configuration.GlobalPackagesLocation);
        }

        /// <summary>
        /// Get cached system packages
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, AppManifest>> GetSystemPackages()
        {
            return await ReadCacheFromDirectoryAsync(Configuration.SystemPackagesLocation);
        }
    }
}
