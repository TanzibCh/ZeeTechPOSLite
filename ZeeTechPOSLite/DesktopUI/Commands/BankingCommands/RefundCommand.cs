using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System.Windows;

namespace DesktopUI.Commands.BankingCommands
{
    public class RefundCommand : CommandBase
    {
        private readonly BankingViewModel _bankingViewModel;
        private readonly INavigationService _navigationService;
        private readonly SaleStore _saleStore;

        public RefundCommand(INavigationService navigationService,
            BankingViewModel bankingViewModel, SaleStore saleStore)
        {
            _navigationService = navigationService;
            _bankingViewModel = bankingViewModel;
            _saleStore = saleStore;
        }

        public override void Execute(object parameter)
        {
            _bankingViewModel.SetupSaleForRefund();

            if (_saleStore.SelectedSale == null)
            {
                MessageBox.Show("Please select a sale first.");
                return;
            }
            else
            {
                _navigationService.Navigate();
            }
        }
    }
}