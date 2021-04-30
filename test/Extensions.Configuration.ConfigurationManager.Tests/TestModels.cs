namespace Extensions.Configuration.ConfigurationManager.Tests {
    public class PersonalInformation {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address {
        public Street Street { get; set; }
        public string Zipcode { get; set; }
    }

    public class Street {
        public string Name { get; set; }
    }
}