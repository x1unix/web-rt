using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRT.Platform.Host
{
    /// <summary>
    /// Provides runtime host configuration
    /// </summary>
    class HostConfigurationProvider
    {
        /// <summary>
        /// Saved instance
        /// </summary>
        private static HostConfigurationProvider Instance;

        /// <summary>
        /// Get provider's instance
        /// </summary>
        /// <returns></returns>
        private static HostConfigurationProvider GetInstance()
        {
            if (Instance == null)
            {
                Instance = new HostConfigurationProvider();
            }

            return Instance;
        }

        /// <summary>
        /// Saved configuration instance
        /// </summary>
        private HostConfiguration Configuration;

        /// <summary>
        /// Get host configuration
        /// </summary>
        /// <returns></returns>
        public HostConfiguration GetConfiguration()
        {
            if (Configuration == null)
            {
                Configuration = new HostConfiguration();
            }

            return Configuration;
        }
    }
}
