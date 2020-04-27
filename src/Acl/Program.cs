using System;
using System.Threading;
using System.Threading.Tasks;

namespace Acl
{
    class Program
    {
        private const string LegacyDatabaseConnectionString = @"Server=L110042\SQLEXPRESS;Database=PackageDelivery;Trusted_Connection=true;";
        private const string BubbleDatabaseConnectionString = @"Server=L110042\SQLEXPRESS;Database=PackageDeliveryNew;Trusted_Connection=true;";

        private static readonly TimeSpan IntervalBetweenDeliverySyncs = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan IntervalBetweenProductSyncs = TimeSpan.FromHours(1);

        private static Task _deliverySyncTask;
        private static DeliverySyncronizer _deliverySyncronizer;
        private static Task _productSyncTask;
        private static ProductSyncronizer _productSyncronizer;
        private static CancellationTokenSource _cancellationTokenSource;

        static void Main(string[] args)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _deliverySyncronizer = new DeliverySyncronizer(LegacyDatabaseConnectionString, BubbleDatabaseConnectionString);
            _deliverySyncTask = new Task(
                () => Sync(_deliverySyncronizer.Sync, IntervalBetweenDeliverySyncs), 
                TaskCreationOptions.LongRunning);
            _deliverySyncTask.Start();

            _productSyncronizer = new ProductSyncronizer(LegacyDatabaseConnectionString, BubbleDatabaseConnectionString);
            _productSyncTask = new Task(
                () => Sync(_productSyncronizer.Sync, IntervalBetweenProductSyncs), 
                TaskCreationOptions.LongRunning);
            _productSyncTask.Start();

            Console.WriteLine("[Press any key to stop]");
            Console.ReadKey();

            _cancellationTokenSource.Cancel();
            _deliverySyncTask.Wait();
            _productSyncTask.Wait();
        }

        private static async void Sync(Action doSync, TimeSpan intervalBetweenSyncs)
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    doSync();
                    await Task.Delay(intervalBetweenSyncs, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException ex)
                {
                }
                catch (Exception ex)
                {
                    Log(ex);
                    throw;
                }
            }
        }

        private static void Log(Exception ex)
        {
            // Log the exception
        }
    }
}
