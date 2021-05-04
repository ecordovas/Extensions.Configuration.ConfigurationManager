using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration {
    public class ConfigurationManagerProvider : FileConfigurationProvider {
        public ConfigurationManagerProvider(ConfigurationManagerSource source) : base(source) { }

        public override void Load(Stream _) {
            var appSettings = ConfigurationManager.AppSettings;
            foreach (var key in appSettings.AllKeys) {
                Data.Add(key, appSettings[key]);
            }

            var connectionStrings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings connectionString in connectionStrings) {
                Data.Add($"ConnectionStrings{ConfigurationPath.KeyDelimiter}{connectionString.Name}", connectionString.ConnectionString);
            }
        }
    }
}