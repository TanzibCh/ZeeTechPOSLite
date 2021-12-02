using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopUI.Commands.PaymentCompleteCommands
{
    public class CompleteCommand : CommandBase
    {
        private readonly PaymentVewModel _paymentVM;
        private readonly SaleStore _saleStore;
        private readonly SalesData _salesData = new SalesData();
        private readonly INavigationService _navigationService;
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();

        public CompleteCommand(PaymentVewModel paymentVM, SaleStore saleStore,
            INavigationService navigationService)
        {
            _paymentVM = paymentVM;
            _saleStore = saleStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            CompleteSale();
        }

        // pay Button : Completes the sale, saves the sale into the DB
        public void CompleteSale()
        {
            _paymentVM.PaymentSum = _paymentVM.CardPayment + _paymentVM.CashPayment + _paymentVM.CreditPayment;

            // variable for checking any difference in payment entered and Total 
            decimal balance  = 0;

            // Check if sum of payment methods == to CartTotal
            if (_paymentVM.Total > _paymentVM.PaymentSum)
            {
                // Show MessageBox asaking to enter payment method
                //MessageBox.Show("Full payment was not taken. Please enter correct Card, Cash or Credit amounts");
                _paymentVM.ErrorMessage = "Full payment was not taken.";

                return;
            }
            // check if there should be any change given
            else if (_saleStore.Total < _paymentVM.PaymentSum)
            {
                decimal cash = _paymentVM.CashPayment;
                decimal card = _paymentVM.CardPayment;
                decimal credit = _paymentVM.CreditPayment;

                decimal paymentReceived = cash + card + credit;
                balance = paymentReceived - _saleStore.Total;

                _paymentVM.CashChange = Math.Round(balance, 2);
            }

            // Save the sale to the database if all info is properly enterd
            if (MessageBox.Show("Do you want to complete the sale?",
               "Compleate Sale", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _paymentVM.CashPayment -= balance;

                _salesData.SaveSale(CreateSaleForDB(), _saleStore.SaleProductsForDB);
            }

            // TODO: Remove quantity from the current location stock
            // if the ProductId of any product in the Cart is != -1

            _navigationService.Navigate();

        }

        // Creates SaleModel object to save in the database
        private SaleModel CreateSaleForDB()
        {
            // Set CashIn as int to save in database
            int cashOnly = 0;
            if (_paymentVM.CashOnlyIsChecked)
            {
                cashOnly = 1;
            }
            else
            {
                cashOnly = 0;
            }

            SaleModel sale = new SaleModel
            {
                Card = _cHelper.ConvertDecimalToInt(_paymentVM.CardPayment),
                Cash = _cHelper.ConvertDecimalToInt(_paymentVM.CashPayment),
                Credit = _cHelper.ConvertDecimalToInt(_paymentVM.CreditPayment),
                SaleTotal = _cHelper.ConvertDecimalToInt(_paymentVM.Total),
                Tax = _cHelper.ConvertDecimalToInt(_paymentVM.Tax),
                TotalCost = _cHelper.ConvertDecimalToInt(_saleStore.TotalCost),
                Profit = _cHelper.ConvertDecimalToInt(_saleStore.TotalPtofit),
                CashOnly = cashOnly
            };

            return sale;
        }
    }
}
