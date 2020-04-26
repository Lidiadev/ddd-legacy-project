using Dapper;
using PackageDeliveryNew.Deliveries;
using PackageDeliveryNew.Utils;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace PackageDeliveryNew.Acl
{
    public class DeliveryRepository
    {
        public Delivery GetById(int id)
        {
            DeliveryLegacy deliveryLegacy = GetLegacyDelivery(id);

            return MapLegacyDelivery(deliveryLegacy);
        }

        private Delivery MapLegacyDelivery(DeliveryLegacy deliveryLegacy)
        {
            if (string.IsNullOrEmpty(deliveryLegacy.CT_ST) || !deliveryLegacy.CT_ST.Contains(' '))
                throw new Exception("Invalid city and state.");

            string[] cityAndState = deliveryLegacy.CT_ST.Split(' ');

            var address = new Address(
                (deliveryLegacy.STR ?? string.Empty).Trim(),
                cityAndState[0].Trim(),
                cityAndState[1].Trim(),
                (deliveryLegacy.ZP ?? string.Empty).Trim());

            return new Delivery(deliveryLegacy.NMB_CLM, address);
        }

        private DeliveryLegacy GetLegacyDelivery(int id)
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = @"
                    SELECT d.NMB_CLM, a.*
                    FROM [dbo].[DLVR_TBL] d
                    INNER JOIN [dbo].[ADDR_TBL] a ON a.DLVR = d.NMB_CLM
                    WHERE d.NMB_CLM = @ID";

                return connection
                    .Query<DeliveryLegacy>(query, new { ID = id })
                    .FirstOrDefault();
            }
        }

        private class DeliveryLegacy
        {
            public int NMB_CLM { get; set; }

            public string STR { get; set; }

            public string CT_ST { get; set; }

            public string ZP { get; set; }
        }
    }
}
