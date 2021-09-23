using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class CompleteCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly CreditStore _creditStore;
        private readonly RefundViewModel _refundViewModel;

        public CompleteCommand(INavigationService navigationService,
            CreditStore creditStore, RefundViewModel refundViewModel)
        {
            _navigationService = navigationService;
            _creditStore = creditStore;
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _creditStore.SaleId = _refundViewModel.SaleId;
            _creditStore.Amount = _refundViewModel.RefundTotal;
            _navigationService.Navigate();
        }
    }
}