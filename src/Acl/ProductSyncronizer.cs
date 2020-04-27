using System;

namespace Acl
{
    public class ProductSyncronizer
    {
        private string _legacyConnectionString;
        private string _bubbleConnectionString;

        public ProductSyncronizer(string legacyConnectionString, string bubbleConnectionString)
        {
            _legacyConnectionString = legacyConnectionString;
            _bubbleConnectionString = bubbleConnectionString;
        }

        public void Sync()
        {
            Console.WriteLine("Sync products");
        }
    }
}
