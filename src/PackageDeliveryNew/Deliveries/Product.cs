using PackageDeliveryNew.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class Product : Entity
    {
        public double WeightInPounds { get; }

        public Product(int id, double weightInPounds) 
            : base(id)
        {
            WeightInPounds = weightInPounds;
        }
    }
}
