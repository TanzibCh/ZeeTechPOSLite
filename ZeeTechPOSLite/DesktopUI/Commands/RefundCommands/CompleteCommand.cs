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
        private readonly ReturnStore _returnStore;
        private readonly RefundViewModel _refundViewModel;

        public CompleteCommand(INavigationService navigationService,
            ReturnStore returnStore, RefundViewModel refundViewModel)
        {
            _navigationService = navigationService;
            _returnStore = returnStore;
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _returnStore.SaleId = _refundViewModel.SaleId;
            _returnStore.Amount = _refundViewModel.RefundTotal;
            _navigationService.Navigate();
        }
    }
}