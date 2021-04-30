using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Extensions.Configuration.ConfigurationManager.Tests {
    public static class ConfigurationFileUpdater {
        public static void Update(IEnumerable<KeyValuePair<string, string>> values) {
            var valuesList = values.ToList();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.ConnectionStrings.ConnectionStrings.Clear();

            var settings = config.AppSettings.Settings;
            foreach (var valueKvp in valuesList) {
                if (settings[valueKvp.Key] == null) {
                    settings.Add(valueKvp.Key, valueKvp.Value);
                }
                else {
                    settings[valueKvp.Key].Value = valueKvp.Value;
                }
            }

            config.Save(ConfigurationSaveMode.Modified);

            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}