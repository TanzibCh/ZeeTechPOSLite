using AutoMapper;
using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class BankingViewModel : INotifyPropertyChanged, IBankingViewModel
    {
        #region private Properties

        //ISalesDataAccess _salesData;

        // Need to use DI in the future
        private SalesDataAccess _salesData = new SalesDataAccess();

        

        #endregion

        // Properties for Totals
        #region Totals Properties

        public decimal Total { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalTillCash { get; set; }
        public decimal TotalCard { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalCashOnly { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalRefund { get; set; }
        public decimal TotalExpense { get; set; }

        #endregion

        // Properties for Department overview
        #region Department Properties

        public int MobileCount { get; set; }
        public decimal MobileTotal { get; set; }
        public int ComputerCount { get; set; }
        public decimal ComputerTotal { get; set; }
        public int CameraCount { get; set; }
        public decimal CameraTotal { get; set; }
        public int HomeCount { get; set; }
        public decimal HomeTotal { get; set; }
        public int RepairCount { get; set; }
        public decimal RepairTotal { get; set; }
        public int AVCount { get; set; }
        public decimal AVTotal { get; set; }

        #endregion

        // Properties for Sales and SaleProducts List
        #region List Properties

        private BindingList<SaleProductModel> _saleProducts;

        public BindingList<SaleProductModel> SaleProducts
        {
            get { return _saleProducts; }
            set
            {
                _saleProducts = value;
                OnPropertyChanged(nameof(SaleProducts));
            }
        }

        private BindingList<SaleDisplayModel> _sales;

        public BindingList<SaleDisplayModel> Sales
        {
            get { return _sales; }
            set
            {
                _sales = value;
                OnPropertyChanged(nameof(Sales));
            }
        }

        private BindingList<ExpenseModel> _expensesar;

        public BindingList<ExpenseModel> Expenses
        {
            get { return _expensesar; }
            set
            {
                _expensesar = value;
                OnPropertyChanged(nameof(Expenses));
            }
        }

        #endregion

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        #region Constructor

        public BankingViewModel()
        {
            SelectedDate = DateTime.UtcNow.Date;
<<<<<<< HEAD
<<<<<<< HEAD
            
=======
            //_mapper = mapper;
>>>>>>> b6ea7cdd0281ab171603194bf5a36a2b8338070d
=======
            //_mapper = mapper;
>>>>>>> b6ea7cdd0281ab171603194bf5a36a2b8338070d
            LoadSales();

        }

        #endregion

        #region Methods

        private void LoadSales()
        {
            var saleList = _salesData.GetAllSalesByDate(SelectedDate.ToString());

            BindingList<SaleDisplayModel> displaySales = new BindingList<SaleDisplayModel>();

<<<<<<< HEAD
<<<<<<< HEAD
            foreach (var item in saleList)
            {
                displaySales.Add(new SaleDisplayModel
                {
                    Id = item.Id,
                    InvoiceNo = item.InvoiceNo,
                    SaleDate = item.SaleDate,
                    SaleTime = item.SaleTime,
                    Card = item.Card / 100,
                    Cash = item.Cash / 100,
                    Credit = item.Credit / 100,
                    SaleTotal = item.SaleTotal / 100,
                    Tax = item.Tax / 100,
                    TotalCost = item.TotalCost / 100,
                    Profit = item.Profit /100,
                    CashOnly = Convert.ToBoolean(item.CashOnly)
                });
            }
=======
            //foreach (var item in sales)
            //{
            //    item.Card = Convert.ToDecimal(item.Card, 2);
            //}
>>>>>>> b6ea7cdd0281ab171603194bf5a36a2b8338070d

            Sales = new BindingList<SaleDisplayModel>(displaySales);
=======
            //foreach (var item in sales)
            //{
            //    item.Card = Convert.ToDecimal(item.Card, 2);
            //}

            //var sales = _mapper.Map<List<SaleDisplayModel>>(saleList);


            Sales = new BindingList<SaleDisplayModel>();
<<<<<<< HEAD
>>>>>>> b6ea7cdd0281ab171603194bf5a36a2b8338070d
=======
>>>>>>> b6ea7cdd0281ab171603194bf5a36a2b8338070d
        }

        public void EditSale()
        {

        }

        public void AddExpense()
        {

        }

        public void EditExpense()
        {

        }

        public void RemoveExpense()
        {

        }

        public void ShowCashOnly()
        {

        }

        public void PreviousDay()
        {

        }

        public void NextDay()
        {

        }

        //public void GetAllSaleByDate()
        //{

        //}

        public void GetAllSaleProducts()
        {

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
