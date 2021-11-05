using DesktopUI.Helpers;
using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class PayCommand : CommandBase
    {
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();
        private readonly ManualSaleViewModel _manualSaleVM;
        private readonly INavigationService _navigationService;
        private readonly SaleStore _saleStore;

        public PayCommand(ManualSaleViewModel manualSaleVM,
            INavigationService navigationService, SaleStore saleStore)
        {
            _manualSaleVM = manualSaleVM;
            _navigationService = navigationService;
            _saleStore = saleStore;
        }

        public override void Execute(object parameter)
        {
            //_manualSaleVM.CompleteSale();
            OpenPaymentView();
        }

        private void OpenPaymentView()
        {
            _saleStore.SubTotal = _cHelper.ConvertStringToDecimal(_manualSaleVM.Subtotal);
            _saleStore.Tax = _cHelper.ConvertStringToDecimal(_manualSaleVM.Tax);
            _saleStore.Total = _cHelper.ConvertStringToDecimal(_manualSaleVM.CartTotal);

            _navigationService.Navigate();
        }


    }
}
