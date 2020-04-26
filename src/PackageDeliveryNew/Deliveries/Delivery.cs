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

        public Address Destination { get; }

        public decimal? CostEstimate { get; private set; }

        private IList<ProductLine> _lines;
        public IReadOnlyList<ProductLine> Lines => _lines.ToList();

        public Delivery(int id, Address destination, decimal? costEstimate, IReadOnlyList<ProductLine> lines)
            : base(id)
        {
            Contracts.Require(id >= 0);
            Contracts.Require(destination != null);
            Contracts.Require(costEstimate >= 0);
            Contracts.Require(lines != null);

            Destination = destination;
            CostEstimate = costEstimate;
            _lines = lines.ToList();
        }

        public void RecalculateCostEstimate(double distanceInMiles)
        {
            Contracts.Require(distanceInMiles >= 0, "Invalid distance.");
            Contracts.Require(Lines.Count > 0, "Need at least one product line.");

            double totalWeightInPounds = Lines.Sum(x => x.Amount * x.Product.WeightInPounds);

            double estimate = totalWeightInPounds * distanceInMiles * PricePerMilePerPound * NonConditionalCharge;

            CostEstimate = decimal.Round((decimal)estimate, 2);
        }

        public void DeleteLine(ProductLine productLine)
        {
            Contracts.Require(productLine != null);

            _lines.Remove(productLine);
        }

        public void AddProduct(Product product, int amount)
        {
            Contracts.Require(product != null);
            Contracts.Require(amount >=0, "Amount must be greater than 0.");

            _lines.Add(new ProductLine(product, amount));
        }
    }
}
