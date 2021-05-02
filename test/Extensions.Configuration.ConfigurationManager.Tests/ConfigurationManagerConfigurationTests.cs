using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Extensions.Configuration.ConfigurationManager.Tests {
    [Collection("ConfigurationManagerConfiguration")]
    public class ConfigurationManagerConfigurationTests {
        private const int Retries = 100;
        private const int DelayInMilliseconds = 200;

        [Fact]
        public void GetValue_NonexistentKey_ReturnsNull() {
            ConfigurationFileUpdater.Update(TestValues.Empty);
            var config = new ConfigurationBuilder().AddConfigurationManager().Build();

            var value = config.GetValue<string>("Nonexistent");

            Assert.Null(value);
        }

        [Fact]
        public void GetSection_WithDotInSectionName_ReturnsSectionValues() {
            ConfigurationFileUpdater.Update(TestValues.PersonalInformation);
            var config = new ConfigurationBuilder().AddConfigurationManager().Build();

            var addressSection = config.GetSection("residential.address");

            Assert.Equal("Something street", addressSection["STREET.name"]);
            Assert.Equal("12345", addressSection["zipcode"]);
        }

        [Fact]
        public void Bind_ObjectIsBound() {
            ConfigurationFileUpdater.Update(TestValues.PersonalInformationSection);
            var config = new ConfigurationBuilder().AddConfigurationManager().Build();

            var personalInformation = new PersonalInformation();

            var personalSection = config.GetSection("personal");
            personalSection.Bind(personalInformation);

            Assert.Equal("test", personalInformation.FirstName);
            Assert.Equal("last.name", personalInformation.LastName);
            Assert.Equal("Something street", personalInformation.Address.Street.Name);
            Assert.Equal("12345", personalInformation.Address.Zipcode);
        }

        [Fact]
        public void ToArray_ReturnsArrayOfSection() {
            ConfigurationFileUpdater.Update(TestValues.IpsArray);
            var config = new ConfigurationBuilder().AddConfigurationManager().Build();

            var configuredIps = config.GetSection("ip").GetChildren().ToArray();
            Assert.Equal(3, configuredIps.Length);
            Assert.Equal("15.16.17.18", configuredIps[0].Value);
            Assert.Equal("7.8.9.10", configuredIps[1].Value);
            Assert.Equal("11.12.13.14", configuredIps[2].Value);
        }

        [Fact]
        public async Task UpdatedFile_ReturnsReloadedValues() {
            ConfigurationFileUpdater.Update(TestValues.PersonalInformationSection);
            var config = new ConfigurationBuilder().AddConfigurationManager(false, true).Build();

            var personalInformation = new PersonalInformation();

            var personalSection = config.GetSection("personal");
            personalSection.Bind(personalInformation);

            Assert.Equal("test", personalInformation.FirstName);
            Assert.Equal("last.name", personalInformation.LastName);
            Assert.Equal("Something street", personalInformation.Address.Street.Name);
            Assert.Equal("12345", personalInformation.Address.Zipcode);

            ConfigurationFileUpdater.Update(TestValues.PersonalInformation);
            await WaitForChange(() => config["residential.address:STREET.name"] == "Something street",
                                "Reload failed after files created.");

            var addressSection = config.GetSection("residential.address");

            Assert.Equal("Something street", addressSection["STREET.name"]);
            Assert.Equal("12345", addressSection["zipcode"]);
        }

        private async Task WaitForChange(Func<bool> test, string failureMessage, int multiplier = 1) {
            var i = 0;
            while (!test()) {
                if (++i >= Retries * multiplier) {
                    throw new Exception(failureMessage);
                }

                await Task.Delay(DelayInMilliseconds);
            }
        }
    }
}
