using PackageDelivery.Delivery;
using PackageDeliveryNew.Utils;

namespace PackageDelivery
{
    public partial class App
    {
        public App()
        {
            string legacyDatabaseDonnectionString = @"Server=L110042\SQLEXPRESS;Database=PackageDelivery;Trusted_Connection=true;";
            string bubbleDatabaseDonnectionString = @"Server=L110042\SQLEXPRESS;Database=PackageDeliveryNew;Trusted_Connection=true;";

            DBHelper.Init(legacyDatabaseDonnectionString);
            Settings.Init(bubbleDatabaseDonnectionString);
        }
    }
}
