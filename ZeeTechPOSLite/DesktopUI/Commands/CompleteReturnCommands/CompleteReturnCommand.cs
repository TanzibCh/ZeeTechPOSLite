using DataAccessLibrary.DataAccess.CreditQueries;
using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Stores;
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
        private readonly ReturnCompleteViewModel _returnCompleteViewModel;
        private readonly ReturnStore _returnStore;

        // Constructor
        public CompleteReturnCommand(ReturnCompleteViewModel returnCompleteViewModel,
            ReturnStore returnStore)
        {
            _returnCompleteViewModel = returnCompleteViewModel;
            _returnStore = returnStore;
        }

        public override void Execute(object parameter)
        {
            SaveReturn();
        }

        private void SaveReturn()
        {
            // Save a new Crdit to Database
            if (_returnCompleteViewModel.CreditIsExpanded == true)
            {
                SaveCredit();
            }
            else if (_returnCompleteViewModel.RefundIsExpanded == true)
            {
                //Logic for saving Refund
                //ToDo: Add products back to stock
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
                CreditNote = _returnCompleteViewModel.CreditNote,
                ValidTill = _returnCompleteViewModel.ValidTill.Date.ToString(),
                Amount = _cHelper.ConvertDecimalToInt(_returnCompleteViewModel.Amount)
            };

            _creditData.SaveCredit(credit);

            // Get list of products for adding to stock from ReturnStore
            List<SaleProductDisplayModel> returnProducts = _returnStore.ReturnProducts.FindAll(p => p.ProductId != -1);

            if (returnProducts != null)
            {
                //ToDo: Add products in the returnProducts list to the stock
                foreach (var product in returnProducts)
                {
                    //ToDo: Get LocationId from LocationStore,
                    //get quantity sold from product and get productId from product.
                    //Save to database
                }
            }
        }
    }
}