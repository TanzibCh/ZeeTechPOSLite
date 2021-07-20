using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;

namespace DesktopUI.Commands.BankingCommands
{
    public class EditSaleCommand : CommandBase
    {
        private readonly SaleStore _saleStore;
        private readonly BankingViewModel _bankingViewModel;
        private readonly INavigationService _navigationService;

        public EditSaleCommand(INavigationService navigationService, SaleStore saleStore,
            BankingViewModel bankingViewModel)
        {
            _navigationService = navigationService;
            _saleStore = saleStore;
            _bankingViewModel = bankingViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_bankingViewModel.SelectedSale == null)
            {
                MessageBox.Show("Select a Sale to edit first.");
            }
            else
            {
                var selectedSale = _bankingViewModel.SelectedSale;
                _saleStore.SelectedSale = selectedSale;

                _navigationService.Navigate();
            }
            
        }
    }
}
