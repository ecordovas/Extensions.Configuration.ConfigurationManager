using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration {
    public class ConfigurationManagerSource : IConfigurationSource {
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new ConfigurationManagerProvider(this);
    }
}