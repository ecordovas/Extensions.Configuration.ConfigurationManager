using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration {
    public static class ConfigurationManagerExtensions {
        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder)
            => AddConfigurationManager(builder, true, false);

        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder, bool optional)
            => AddConfigurationManager(builder, optional, false);

        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder, bool optional, bool reloadOnChange) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddConfigurationManager(source => {
                source.Path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
                source.Optional = optional;
                source.ReloadOnChange = reloadOnChange;
                source.ResolveFileProvider();
            });
        }

        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder, Action<ConfigurationManagerSource> configureSource)
            => builder.Add(configureSource);
    }
}