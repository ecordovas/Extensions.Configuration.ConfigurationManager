using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration {
    public class ConfigurationManagerSource : FileConfigurationSource {
        public override IConfigurationProvider Build(IConfigurationBuilder builder) => new ConfigurationManagerProvider(this);
    }
}