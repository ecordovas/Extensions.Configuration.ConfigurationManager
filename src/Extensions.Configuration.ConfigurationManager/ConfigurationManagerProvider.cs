using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration {
    public class ConfigurationManagerProvider : FileConfigurationProvider {
        public ConfigurationManagerProvider(ConfigurationManagerSource source) : base(source) { }

        public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath) {
            var prefix = parentPath == null ? string.Empty : parentPath + ConfigurationPath.KeyDelimiter;

            return Data
                .Where(kv => kv.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Select(kv => Segment(kv.Key, prefix.Length))
                .Concat(earlierKeys)
                .OrderBy(k => k, ConfigurationKeyComparer.Instance);
        }

        private static string Segment(string key, int prefixLength) {
            var indexOf = key.IndexOf(ConfigurationPath.KeyDelimiter, prefixLength, StringComparison.OrdinalIgnoreCase);
            return indexOf < 0 ? key.Substring(prefixLength) : key.Substring(prefixLength, indexOf - prefixLength);
        }

        public override void Load(Stream _) {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var appSettings = ConfigurationManager.AppSettings;
            foreach (var key in appSettings.AllKeys) {
                Data.Add(key, appSettings[key]);
            }

            var connectionStrings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings connectionString in connectionStrings) {
                Data.Add($"ConnectionStrings{ConfigurationPath.KeyDelimiter}{connectionString.Name}", connectionString.ConnectionString);
            }
        }

        public override string ToString() => $"{GetType().Name}";
    }
}