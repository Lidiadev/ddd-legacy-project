﻿using PackageDeliveryNew.Common;
using System.Collections.Generic;

namespace PackageDeliveryNew.Deliveries
{
    public class ProductLine : ValueObject<ProductLine>
    {
        public Product Product { get; }

        public int Amount { get; }

        public ProductLine(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Product;
            yield return Amount;
        }
    }
}
