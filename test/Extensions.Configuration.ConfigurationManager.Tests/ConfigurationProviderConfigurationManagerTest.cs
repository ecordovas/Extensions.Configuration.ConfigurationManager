using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Test;
using Xunit;

namespace Extensions.Configuration.ConfigurationManager.Tests {
    [Collection("ConfigurationManagerConfiguration")]
    public class ConfigurationProviderConfigurationManagerTest : ConfigurationProviderTestBase {
        [Fact]
        public override void Load_from_single_provider_with_duplicates_throws() {
            // Configuration Manager provider doesn't throw for duplicate values because it will load the last key value pair
            AssertConfig(BuildConfigRoot(LoadThroughProvider(TestSection.DuplicatesTestConfig)));
        }

        [Fact]
        public override void Load_from_single_provider_with_differing_case_duplicates_throws() {
            // Configuration Manager provider doesn't throw for duplicate values because it will load the last key value pair
            AssertConfig(BuildConfigRoot(LoadThroughProvider(TestSection.DuplicatesDifferentCaseTestConfig)));
        }

        protected override (IConfigurationProvider Provider, Action Initializer) LoadThroughProvider(TestSection testConfig) {
            var values = new List<KeyValuePair<string, string>>();
            SectionToValues(testConfig, "", values);

            ConfigurationFileUpdater.Update(values);

            var provider = new ConfigurationManagerProvider(new ConfigurationManagerSource());
            return (provider, () => provider.Load());
        }
    }
}