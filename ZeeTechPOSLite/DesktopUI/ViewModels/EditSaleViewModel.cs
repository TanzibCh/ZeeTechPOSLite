using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Helpers;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class EditSaleViewModel : INotifyPropertyChanged
    {
        #region Private properties

        private SalesData _salesData = new SalesData();
        private SaleProductData _saleProductData = new SaleProductData();

        private CurrencyHelper _currencyHelper = new CurrencyHelper();

        private int _saleId;

        #endregion

        #region Sales info Display properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _invoiceNo;

        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set
            {
                _invoiceNo = value;
                OnPropertyChanged(nameof(InvoiceNo));
            }
        }

        private string _invoiceDate;

        public string InvoiceDate
        {
            get { return _invoiceDate; }
            set
            {
                _invoiceDate = value;
                OnPropertyChanged(nameof(InvoiceDate));
            }
        }

        private string _invoiceTime;

        public string InvoiceTime
        {
            get { return _invoiceTime; }
            set
            {
                _invoiceTime = value;
                OnPropertyChanged(nameof(InvoiceTime));
            }
        }

        private string _totalCost;

        public string TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        private string _totalProfit;

        public string TotalProfit
        {
            get { return _totalProfit; }
            set
            {
                _totalProfit = value;
                OnPropertyChanged(nameof(TotalProfit));
            }
        }

        private string _saleTotal;

        public string SaleTotal
        {
            get { return _saleTotal; }
            set
            {
                _saleTotal = value;
                OnPropertyChanged(nameof(SaleTotal));
            }
        }

        private string _Card;

        public string Card
        {
            get { return _Card; }
            set 
            { 
                _Card = value;
                OnPropertyChanged(nameof(Card));
            }
        }

        private string _cash;

        public string Cash
        {
            get { return _cash; }
            set 
            {
                _cash = value;
                OnPropertyChanged(nameof(Cash));
            }
        }

        private string _credit;

        public string Credit
        {
            get { return _credit; }
            set 
            {
                _credit = value;
                OnPropertyChanged(nameof(Credit));
            }
        }


        #endregion

        #region Sales Products Display properties

        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _quantity;

        public string Quantity
        {
            get { return _quantity; }
            set 
            { 
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                CalculateProductTotal();
            }
        }

        private string _Cost;

        public string Cost
        {
            get { return _Cost; }
            set
            {
                _Cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private string _salePrice;

        public string SalePrice
        {
            get { return _salePrice; }
            set 
            { 
                _salePrice = value;
                OnPropertyChanged(nameof(SalePrice));
                CalculateProductTotal();
            }
        }

        public string Total { get; set; }

        private SaleProductDisplayModel _selectedProduct;

        public SaleProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        #endregion

        #region List Properties

        private BindingList<SaleProductDisplayModel> _saleProducts;

        public BindingList<SaleProductDisplayModel> SaleProducts
        {
            get { return _saleProducts; }
            set
            {
                _saleProducts = value;
                OnPropertyChanged(nameof(SaleProducts));
            }
        }

        #endregion

        #region Constructors

        // Default Constructor
        public EditSaleViewModel(int saleId)
        {
            _saleId = saleId;

            LoadSale(_saleId);
        }

        #endregion

        #region Methods

        private void CalculateProductTotal()
        {
            decimal quantity = Convert.ToDecimal(SelectedProduct.QuantitySold);
            decimal salePrice = _currencyHelper.ConvertCurrencyStringToDecimal(SelectedProduct.SalePrice);

           Total = _currencyHelper.ConvertDecimalToCurrencyString(quantity * salePrice);
        }

        private void LoadSale(int id)
        {
            SaleModel sale = _salesData.GetSaleById(id);

            // insert data to all the fields
            InvoiceNo = _currencyHelper.ConvertIntToCurrencyString(sale.InvoiceNo);
            InvoiceDate = sale.SaleDate;
            InvoiceTime = sale.SaleTime;
            TotalCost = _currencyHelper.ConvertIntToCurrencyString(sale.TotalCost);
            TotalProfit = _currencyHelper.ConvertIntToCurrencyString(sale.SaleTotal - sale.TotalCost);
            SaleTotal = _currencyHelper.ConvertIntToCurrencyString(sale.SaleTotal);
            Card = _currencyHelper.ConvertIntToCurrencyString(sale.Card);
            Cash = _currencyHelper.ConvertIntToCurrencyString(sale.Cash);
            Credit = _currencyHelper.ConvertIntToCurrencyString(sale.Credit);
        }
        #endregion

        #region INotifyPropertyChanged implimentation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
