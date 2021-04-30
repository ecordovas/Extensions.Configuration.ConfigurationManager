using System.Collections.Generic;

namespace Extensions.Configuration.ConfigurationManager.Tests {
    public static class TestValues {
        public static IReadOnlyDictionary<string, string> Empty => new Dictionary<string, string>();

        public static IReadOnlyDictionary<string, string> PersonalInformation => new Dictionary<string, string> {
            {"firstname", "test"},
            {"test.last.name", "last.name"},
            {"residential.address:STREET.name", "Something street"},
            {"residential.address:zipcode", "12345"}
        };

        public static IReadOnlyDictionary<string, string> PersonalInformationSection => new Dictionary<string, string> {
            {"personal:firstname", "test"},
            {"personal:lastName", "last.name"},
            {"personal:address:STREET:name", "Something street"},
            {"personal:address:zipcode", "12345"}
        };

        public static IReadOnlyDictionary<string, string> IpsArray => new Dictionary<string, string> {
            {"ip:0", "15.16.17.18"},
            {"ip:1", "7.8.9.10"},
            {"ip:2", "11.12.13.14"}
        };
    }
}