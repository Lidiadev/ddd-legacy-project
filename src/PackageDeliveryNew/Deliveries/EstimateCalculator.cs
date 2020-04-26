using PackageDeliveryNew.Acl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PackageDeliveryNew.Deliveries
{
    public class EstimateCalculator
    {
        private readonly DeliveryRepository _deliveryRepository;
        private readonly ProductRepository _productRepository;
        private readonly AddressResolver _addressResolver;

        public EstimateCalculator()
        {
            _deliveryRepository = new DeliveryRepository();
            _productRepository = new ProductRepository();
            _addressResolver = new AddressResolver();
        }

        public decimal Calculate(int deliveryId,
            int? productId1, int amount1,
            int? productId2, int amount2,
            int? productId3, int amount3,
            int? productId4, int amount4)
        {
            if (productId1 == null && productId2 == null && productId3 == null && productId4 == null)
                throw new Exception("Must provide at least one product.");

            Delivery delivery = _deliveryRepository.GetById(deliveryId);
            if (delivery == null)
                throw new Exception($"Delivery is not found for id {deliveryId}.");

            double? distance = _addressResolver.GetDistance(delivery.Address);
            if (distance == null)
                throw new Exception($"Address is not found for delivery {deliveryId}.");

            List<ProductLine> productLines = new List<(int? productId, int amount)>
                {
                    (productId1, amount1),
                    (productId2, amount2),
                    (productId3, amount3),
                    (productId4, amount4)
                }
                .Where(x => x.productId != null)
                .Select(x => new ProductLine(_productRepository.GetById(x.productId.Value), x.amount))
                .ToList();

            if (productLines.Any(x => x.Product == null))
                throw new Exception($"One of the products is not found for delivery {deliveryId}.");

            return delivery.GetEstimate(distance.Value, productLines);
        }
    }

    public class AddressResolver
    {
        public double? GetDistance(Address address)
        {
            /* Call to an external API */
            return 15;
        }
    }
}
