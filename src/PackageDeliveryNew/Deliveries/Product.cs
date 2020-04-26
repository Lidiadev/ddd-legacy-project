using PackageDeliveryNew.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class Product : Entity
    {
        public string Name { get; }
        public double WeightInPounds { get; }

        public Product(int id, string name, double weightInPounds) 
            : base(id)
        {
            Contracts.Require(id >= 0);
            Contracts.Require(!string.IsNullOrEmpty(name), "Name must be provided.");
            Contracts.Require(weightInPounds > 0, "Weight must be greater than 0.");

            Name = name;
            WeightInPounds = weightInPounds;
        }
    }
}
