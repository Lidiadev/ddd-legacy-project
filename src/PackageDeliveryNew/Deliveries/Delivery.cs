using PackageDeliveryNew.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PackageDeliveryNew.Deliveries
{
    public class Delivery : Entity
    {
        private const double PricePerMilePerPound = 0.04;
        private const double NonConditionalCharge = 0.04;

        public Address Address { get; }

        public Delivery(int id, Address address)
            : base(id)
        {
            Contracts.Require(id >= 0);
            Contracts.Require(address != null);

            Address = address;
        }

        public decimal GetEstimate(double distanceInMiles, List<ProductLine> productLines)
        {
            Contracts.Require(distanceInMiles >= 0, "Invalid distance.");
            Contracts.Require(productLines.Count > 0 && productLines.Count <= 4, "Invalid product line count.");

            double totalWeightInPounds = productLines.Sum(x => x.Amount * x.Product.WeightInPounds);

            double estimate = totalWeightInPounds * distanceInMiles * PricePerMilePerPound * NonConditionalCharge;

            return decimal.Round((decimal)estimate, 2);
        }
    }
}
