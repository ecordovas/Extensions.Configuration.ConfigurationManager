using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Extensions.Configuration {
    public class ConfigurationManagerProvider : IConfigurationProvider {
        private readonly ConfigurationReloadToken _reloadToken = new ConfigurationReloadToken();

        public ConfigurationManagerProvider(ConfigurationManagerSource source) {
            Source = source;
        }

        protected IDictionary<string, string> Data { get; set; }
        protected ConfigurationManagerSource Source { get; }

        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath) {
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

        public IChangeToken GetReloadToken() => _reloadToken;

        public void Load() {
            var appSettings = ConfigurationManager.AppSettings;
            foreach (var key in appSettings.AllKeys) {
                Data.Add(key, appSettings[key]);
            }

            var connectionStrings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings connectionString in connectionStrings) {
                Data.Add($"ConnectionStrings{ConfigurationPath.KeyDelimiter}{connectionString.Name}", connectionString.ConnectionString);
            }
        }

        public virtual void Set(string key, string value) => Data[key] = value;

        public virtual bool TryGet(string key, out string value) => Data.TryGetValue(key, out value);

        public override string ToString() => $"{GetType().Name}";
    }
}