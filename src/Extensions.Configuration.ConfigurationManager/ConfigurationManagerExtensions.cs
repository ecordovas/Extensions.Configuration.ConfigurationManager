using System;
using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration {
    public static class ConfigurationManagerExtensions {
        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.AddConfigurationManager(null);
        }

        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder, Action<ConfigurationManagerSource> configureSource)
            => builder.Add(configureSource);
    }
}