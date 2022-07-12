using Domain.Orders;

namespace UnitTest.Builders
{
    public class AddressBuilder
    {
        private Address _address;
        public string TestCity => "Babol";
        public string TestState => "Shariati";
        public string TestZipCode => "123456789";
        public string TestPostalAddress => "Yas Tower";

        public AddressBuilder()
        {
            _address = WithDefaultValue();
        }

        private Address WithDefaultValue()
        {
            _address = new Address(TestState, TestCity, TestZipCode, TestPostalAddress, null);
            return _address;
        }
        public Address Build()
        {
            return _address;
        }
    }
}