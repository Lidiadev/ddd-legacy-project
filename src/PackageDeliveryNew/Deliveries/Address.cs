using PackageDeliveryNew.Common;
using System.Collections.Generic;

namespace PackageDeliveryNew.Deliveries
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; }

        public string City { get; }

        public string State { get; }

        public string ZipCode { get; }

        public Address(string street, string city, string state, string zipCode)
        {
            Contracts.Require(!string.IsNullOrEmpty(street));
            Contracts.Require(!string.IsNullOrEmpty(city));
            Contracts.Require(!string.IsNullOrEmpty(state));
            Contracts.Require(!string.IsNullOrEmpty(zipCode));

            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return ZipCode;
        }
    }
}
