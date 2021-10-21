using DataAccessLibrary.DataAccess.Queries;
using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Stores;
using DesktopUI.Services;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.CompleteReturnCommands
{
    public class CompleteReturnCommand : CommandBase
    {
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();
        private readonly CreditData _creditData = new CreditData();
        private readonly RefundData _refundData = new RefundData();
        private readonly StockData _stockData = new StockData();
        private readonly ReturnCompleteViewModel _returnCompleteViewModel;
        private readonly ReturnStore _returnStore;
        private readonly LocationStore _locationStore;
        private readonly CloseAllModalNavigationService _navigationService;

        // Constructor
        public CompleteReturnCommand(ReturnCompleteViewModel returnCompleteViewModel,
            ReturnStore returnStore, LocationStore locationStore, CloseAllModalNavigationService navigationService)
        {
            _returnCompleteViewModel = returnCompleteViewModel;
            _returnStore = returnStore;
            _locationStore = locationStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            SaveReturn();

            _navigationService.Navigate();
        }

        private void SaveReturn()
        {
            // Save a new Crdit to Database
            if (_returnCompleteViewModel.CreditIsExpanded == true)
            {
                // Creats a new Credit and adds the stock back
                // if the product is in the database
                SaveCredit();

            }
            else if (_returnCompleteViewModel.RefundIsExpanded == true)
            {
                // Creats a new Refund and adds the stock back
                // if the product is in the database
                SaveRefund();
            }
            else
            {
                throw new Exception();
            }
        }

        private void SaveCredit()
        {
            CreditModel credit = new CreditModel
            {
                SaleId = _returnStore.SaleId,
                Comments = _returnCompleteViewModel.CreditNote,
                ValidTill = _returnCompleteViewModel.ValidTill.Date.ToString(),
                Amount = _cHelper.ConvertDecimalToInt(_returnCompleteViewModel.Amount)
            };

            _creditData.SaveNewCredit(credit);

            CreditModel newCredit = _creditData.GetLatestCredit();

            AddStock(newCredit.Id, "Credit");
        }

        private void SaveRefund()
        {
            int card = 0;
            int cash = 0;

            if (_returnCompleteViewModel.Card == true &&
                _returnCompleteViewModel.Cash == false)
            {
                card = 1;
                cash = 0;
            }
            else if (_returnCompleteViewModel.Card == false &&
                     _returnCompleteViewModel.Cash == true)
            {
                card = 0;
                cash = 1;
            }    

            RefundModel refund = new RefundModel
            {
                SaleId = _returnStore.SaleId,
                Comments = _returnCompleteViewModel.RefundNote,
                Amount = _cHelper.ConvertDecimalToInt(_returnCompleteViewModel.Amount),
                Card = card,
                Cash = cash
            };

            _refundData.SaveRefund(refund);

            RefundModel newRefund = _refundData.GetLatestRefund();

            AddStock(newRefund.Id, "Refund");
        }

        private void AddStock(int returnId, string returnType)
        {
            // Get list of products that has a valid ProductId for adding to stock from ReturnStore
            List<SaleProductDisplayModel> returnProducts = _returnStore.ReturnProducts.FindAll(p => p.ProductId != -1);

            // takes the ReturnId and ReturnType and creats the comment
            string comment = $"Returned For {returnType}, {returnType} no: {returnId}";

            if (returnProducts != null)
            {
                //ToDo: Add products in the returnProducts list to the stock
                foreach (var product in returnProducts)
                {
                    var stock = _stockData.GetStockByProductAndLocation(product.Id, _locationStore.Id);

                    if (stock != null)
                    {
                        _stockData.AddStock(_locationStore.Id, product.Id, product.QuantitySold, comment);
                    }
                    else
                    {
                        _stockData.AddNewItemToStock(_locationStore.Id, product.Id, product.QuantitySold, comment);
                    }
                }
            }
        }
    }
}