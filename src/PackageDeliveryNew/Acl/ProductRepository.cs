using Dapper;
using PackageDeliveryNew.Deliveries;
using PackageDeliveryNew.Utils;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace PackageDeliveryNew.Acl
{
    public class ProductRepository
    {
        private const double PoundsInKilogram = 2.20462;
        public Product GetById(int id)
        {
            ProductLegacy productLegacy = GetLegacyProduct(id);

            return MapLegacyProduct(productLegacy);
        }

        private Product MapLegacyProduct(ProductLegacy productLegacy)
        {
            if (productLegacy.WT == null && productLegacy.WT_KG == null)
                throw new Exception($"Invalid weight for product {productLegacy.NMB_CM}.");

            double wightInPounds = productLegacy.WT ?? productLegacy.WT_KG.Value * PoundsInKilogram;

            return null; // new Product(productLegacy.NMB_CM, wightInPounds);
        }

        public ProductLegacy GetLegacyProduct(int id)
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                var query = @"
                SELECT NMB_CM, WT, WT_KG
                FROM [dbo].[PRD_TBL]
                WHERE NMB_CM = @ID";

                return connection
                    .Query<ProductLegacy>(query, new { ID = id })
                    .FirstOrDefault();
            }
        }

        public class ProductLegacy
        {
            public int NMB_CM { get; set; }

            public double? WT { get; set; }

            public double? WT_KG { get; set; }
        }
    }
}
