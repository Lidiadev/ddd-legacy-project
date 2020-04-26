using PackageDelivery.Delivery;
using PackageDeliveryNew.Utils;

namespace PackageDelivery
{
    public partial class App
    {
        public App()
        {
            string connectionString = @"Server=L110042\SQLEXPRESS;Database=PackageDelivery;Trusted_Connection=true;";

            DBHelper.Init(connectionString);
            Settings.Init(connectionString);
        }
    }
}
