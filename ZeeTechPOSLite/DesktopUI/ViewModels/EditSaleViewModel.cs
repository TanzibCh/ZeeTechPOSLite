using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class EditSaleViewModel : ViewModelBase
    {
        #region Private properties

        private SalesData _salesData = new SalesData();
        private SaleProductData _saleProductData = new SaleProductData();

        private CurrencyHelper _currencyHelper = new CurrencyHelper();

        private int _saleId;
        private readonly SaleStore _saleStore;

        #endregion

        #region Sales info Display properties

        public int Id => _saleStore.SelectedSale.Id;

        public int InvoiceNo => _saleStore.SelectedSale.InvoiceNo;

        public string InvoiceDate => _saleStore.SelectedSale.SaleDate;

        public string InvoiceTime => _saleStore.SelectedSale.SaleTime;

        private string _totalCost;

        public string TotalCost
        {
            get { return _saleStore.SelectedSale.TotalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged(nameof(TotalCost));
            }
        }


        private string _totalProfit;

        public string TotalProfit
        {
            get
            {
                decimal total = _currencyHelper.ConvertCurrencyStringToDecimal(_saleStore.SelectedSale.SaleTotal);
                decimal totalCost = _currencyHelper.ConvertCurrencyStringToDecimal(_saleStore.SelectedSale.TotalCost);
                return _currencyHelper.ConvertDecimalToCurrencyString(total - totalCost);
            }
            set
            {
                _totalProfit = value;
                OnPropertyChanged(nameof(TotalProfit));
            }
        }


        private string _saleTotal;

        public string SaleTotal
        {
            get { return _saleStore.SelectedSale.SaleTotal; }
            set 
            { 
                _saleTotal = value;
                OnPropertyChanged(nameof(SaleTotal));
            }
        }


        private string _card;

        public string Card
        {
            get { return _card; }
            set
            {
                _card = value;
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

        #region

        public ICommand CloseCommand { get; }

        #endregion

        #region Constructors

        // Default Constructor
        public EditSaleViewModel(INavigationService closeModalNavigationService, SaleStore saleStore)
        {
            _saleStore = saleStore;

            CloseCommand = new CloseModalCommand(closeModalNavigationService);

            _saleStore.SelectedSaleChanged += OnSelectedSaleChanged;

            _card = _saleStore.SelectedSale.Card;
            _cash = _saleStore.SelectedSale.Cash;
            _credit = _saleStore.SelectedSale.Credit;

            //LoadSale(_id);
        }

        private void OnSelectedSaleChanged()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(InvoiceNo));
            OnPropertyChanged(nameof(InvoiceDate));
            OnPropertyChanged(nameof(InvoiceTime));
            OnPropertyChanged(nameof(TotalCost));
            OnPropertyChanged(nameof(SaleTotal));

            OnPropertyChanged(nameof(Card));
        }

        #endregion

        #region Methods 

        private void CalculateProductTotal()
        {
            decimal quantity = Convert.ToDecimal(SelectedProduct.QuantitySold);
            decimal salePrice = _currencyHelper.ConvertCurrencyStringToDecimal(SelectedProduct.SalePrice);

           Total = _currencyHelper.ConvertDecimalToCurrencyString(quantity * salePrice);
        }

        private void LoadSale(int saleId)
        {
            SaleModel sale = _salesData.GetSaleById(saleId);

            // insert data to all the fields
            //InvoiceNo = sale.InvoiceNo;
            //InvoiceDate = sale.SaleDate;
            //InvoiceTime = sale.SaleTime;
            //TotalCost = _currencyHelper.ConvertIntToCurrencyString(sale.TotalCost);
            //TotalProfit = _currencyHelper.ConvertIntToCurrencyString(sale.SaleTotal - sale.TotalCost);
            //SaleTotal = _currencyHelper.ConvertIntToCurrencyString(sale.SaleTotal);
            //Card = _currencyHelper.ConvertIntToCurrencyString(sale.Card);
            //Cash = _currencyHelper.ConvertIntToCurrencyString(sale.Cash);
            //Credit = _currencyHelper.ConvertIntToCurrencyString(sale.Credit);
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
