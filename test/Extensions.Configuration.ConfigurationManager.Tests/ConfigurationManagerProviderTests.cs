using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Test;
using Xunit;

namespace Extensions.Configuration.ConfigurationManager.Tests {
    [Collection("ConfigurationManagerConfiguration")]
    public class ConfigurationManagerProviderTests {
        private ConfigurationManagerProvider LoadProvider(IEnumerable<KeyValuePair<string, string>> values) {
            ConfigurationFileUpdater.Update(values);
            var p = new ConfigurationManagerProvider(new ConfigurationManagerSource());
            p.Load();
            return p;
        }

        [Fact]
        public void CanLoadValidSettingsFromConfigurationFile() {
            var kvps = new Dictionary<string, string> {
                { "Setting1", "May 5, 2014"},
                { "Setting2", "May 6, 2014"}
            };

            var provider = LoadProvider(kvps);

            var value1 = provider.Get("Setting1");
            var value2 = provider.Get("Setting2");

            Assert.Equal("May 5, 2014", value1);
            Assert.Equal("May 6, 2014", value2);
        }

        [Fact]
        public void LoadKeyValuePairsFromConfigurationFile() {
            var provider = LoadProvider(TestValues.PersonalInformation);

            Assert.Equal("test", provider.Get("firstname"));
            Assert.Equal("last.name", provider.Get("test.last.name"));
            Assert.Equal("Something street", provider.Get("residential.address:STREET.name"));
            Assert.Equal("12345", provider.Get("residential.address:zipcode"));
        }
    }
}