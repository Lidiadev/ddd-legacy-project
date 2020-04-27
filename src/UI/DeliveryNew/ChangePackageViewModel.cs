using PackageDelivery.Common;
using PackageDeliveryNew.Deliveries;
using System.Collections.Generic;

namespace PackageDelivery.DeliveryNew
{
    public class ChangePackageViewModel : ViewModel
    {
        private readonly PackageDeliveryNew.Deliveries.Delivery _delivery;
        public IReadOnlyList<ProductLine> ProductLines => _delivery.Lines;
        public decimal CostEstimate => _delivery.CostEstimate ?? 0;

        private readonly AddressResolver _addressResolver;
        private readonly DeliveryRepository _deliveryRepository;

        public Command NewProductLineCommand { get; }
        public Command<ProductLine> DeleteProductLineCommand { get; }
        public Command RecalculateCostCommand { get; }

        public Command OkCommand { get; }
        public Command CancelCommand { get; }

        public override string Caption => "Edit Package";
        public override double Height => 410;
        public override double Width => 400;

        public ChangePackageViewModel(int deliveryId)
        {
            _addressResolver = new AddressResolver();
            _deliveryRepository = new DeliveryRepository();
            _delivery = _deliveryRepository.GetById(deliveryId);

            RecalculateCostCommand = new Command(RecalculateCost);
            NewProductLineCommand = new Command(NewProductLine);
            DeleteProductLineCommand = new Command<ProductLine>(x => x != null, DeleteProductLine);
            OkCommand = new Command(Save);
            CancelCommand = new Command(() => DialogResult = false);
        }

        private void Save()
        {
            _deliveryRepository.Save(_delivery);
            DialogResult = true;
        }

        private void NewProductLine()
        {
            var viewModel = new AddProductLineViewModel();

            if(_dialogService.ShowDialog(viewModel) == true)
            {
                _delivery.AddProduct(viewModel.Product, viewModel.Amount);

                Notify(nameof(ProductLines));
            }
        }

        private void DeleteProductLine(ProductLine productLine)
        {
            _delivery.DeleteLine(productLine);

            Notify(nameof(ProductLines));
        }

        private void RecalculateCost()
        {
            if (ProductLines.Count == 0)
            {
                CustomMessageBox.ShowError("Please, specify at least one product line");
                return;
            }

            double? distance = _addressResolver.GetDistanceTo(_delivery.Destination);
            if (distance == null)
            {
                CustomMessageBox.ShowError("Address is not found");
                return;
            }

            _delivery.RecalculateCostEstimate(distance.Value);

            Notify(nameof(CostEstimate));
        }
    }
}
