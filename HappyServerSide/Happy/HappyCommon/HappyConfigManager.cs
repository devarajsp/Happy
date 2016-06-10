using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCommon
{
    public class HappyConfigManager : IConfigManager
    {
        readonly Dictionary<string, string> configuration = new Dictionary<string, string>();
        bool _disposed = false;

        public string GetConfigurationSetting(string configurationSettingName)
        {
            return this.GetConfigurationSetting(configurationSettingName, string.Empty);
        }

        public string GetConfigurationSetting(string configurationSettingName, string defaultValue)
        {
            try
            {
                if (!this.configuration.ContainsKey(configurationSettingName))
                {
                    try
                    {
                        string configValue = string.Empty;
                        configValue = ConfigurationManager.AppSettings[configurationSettingName];
                        this.configuration.Add(configurationSettingName, configValue);
                    }
                    catch (ArgumentException)
                    {
                        // at this point, this key has already been added on a different
                        // thread, so we're fine to continue
                    }
                }
            }
            catch (ConfigurationException)
            {
                if (string.IsNullOrEmpty(defaultValue))
                    throw;

                this.configuration.Add(configurationSettingName, defaultValue);
            }

            return this.configuration[configurationSettingName];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }

        ~HappyConfigManager()
        {
            Dispose(false);
        }

    }
}
