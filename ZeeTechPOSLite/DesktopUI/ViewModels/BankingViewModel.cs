using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class BankingViewModel : INotifyPropertyChanged
    {
        #region private Properties

        //ISalesDataAccess _salesData;

        // Need to use DI in the future
        private SalesDataAccess _salesData = new SalesDataAccess();
        private SaleProductDataAccess _saleProductData = new SaleProductDataAccess();

        #endregion

        // Properties for Totals
        #region Totals Properties

        public string Total
        {
            get
            {
                decimal totalSale = Sales.Sum(x => ConvertCurrencyStringToDecimal(x.SaleTotal));
                string output = string.Format("{0:0.00}", Convert.ToString(totalSale));
                return $"£{output}";
            }
        }
        public string TotalProfit
        {
            get
            {
                decimal totalProfit = Sales.Sum(x => ConvertCurrencyStringToDecimal(x.Profit));
                string output = string.Format("{0:0.00}", Convert.ToString(totalProfit));
                return $"£{output}";
            }
        }
        public string TotalTillCash
        {
            get
            {
                List<SaleModel> sales = _salesData.GetCashOnlySalesByDate(SelectedDate.ToString());


                return "";
            }
        }
        public string TotalCard
        {
            get
            {
                decimal totalCard = Sales.Sum(x => ConvertCurrencyStringToDecimal(x.Card));
                string output = string.Format("{0:0.00}", Convert.ToString(totalCard));
                return $"£{output}";
            }
        }
        public string TotalCash
        {
            get
            {
                decimal totalCash = Sales.Sum(x => ConvertCurrencyStringToDecimal(x.Cash));
                string output = string.Format("{0:0.00}", Convert.ToString(totalCash));
                return $"£{output}";
            }
        }
        public string TotalCashOnly
        {
            get
            {
                List<SaleModel> sales = _salesData.GetCashOnlySalesByDate(SelectedDate.ToString());

                int total = sales.Sum(x => x.SaleTotal);
                string output = ConvertIntToCurrencyString(total);
                return output;
            }
        }
        public string TotalCredit
        {
            get
            {
                decimal totalCredit = Sales.Sum(x => ConvertCurrencyStringToDecimal(x.Credit));
                string output = string.Format("{0:0.00}", Convert.ToString(totalCredit));
                return $"£{output}";
            }
        }
        public string TotalRefund
        {
            get
            {
                // Query for total refund
                return "";
            }
        }
        public string TotalExpense
        {
            get
            {
                // query for total expenses
                return "";
            }
        }

        #endregion

        // Properties for Department overview
        #region Department Properties

        public int MobileCount
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Mobile");
                return sales.Sum(x => x.QuantitySold);
            }
        }
        public string MobileTotal
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Mobile");

                decimal total = 0.00m;
                foreach (SaleProductDBModel item in sales)
                {
                    decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                    decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                    total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
                }
                string output = string.Format("{0:0.00}", Convert.ToString(total));

                return $"£{output}";
            }
        }
        public int ComputerCount
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Computer");
                return sales.Sum(x => x.QuantitySold);
            }
        }
        public string ComputerTotal
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Computer");

                decimal total = 0.00m;
                foreach (SaleProductDBModel item in sales)
                {
                    decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                    decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                    total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
                }
                string output = string.Format("{0:0.00}", Convert.ToString(total));

                return $"£{output}";
            }
        }

        public int CameraCount
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Camera");
                return sales.Sum(x => x.QuantitySold);
            }
        }
        public string CameraTotal
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Camera");

                decimal total = 0.00m;
                foreach (SaleProductDBModel item in sales)
                {
                    decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                    decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                    total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
                }
                string output = string.Format("{0:0.00}", Convert.ToString(total));

                return $"£{output}";
            }
        }
        public int HomeCount
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Home");
                return sales.Sum(x => x.QuantitySold);
            }
        }
        public string HomeTotal
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Home");

                decimal total = 0.00m;
                foreach (SaleProductDBModel item in sales)
                {
                    decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                    decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                    total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
                }
                string output = string.Format("{0:0.00}", Convert.ToString(total));

                return $"£{output}";
            }
        }
        public int RepairCount
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Repair");
                return sales.Sum(x => x.QuantitySold);
            }
        }
        public string RepairTotal
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Repair");

                decimal total = 0.00m;
                foreach (SaleProductDBModel item in sales)
                {
                    decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                    decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                    total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
                }
                string output = string.Format("{0:0.00}", Convert.ToString(total));

                return $"£{output}";
            }
        }
        public int AVCount
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "AV");
                return sales.Sum(x => x.QuantitySold);
            }
        }
        public string AVTotal
        {
            get
            {
                List<SaleProductDBModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "AV");

                decimal total = 0.00m;
                foreach (SaleProductDBModel item in sales)
                {
                    decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                    decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                    total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
                }
                string output = string.Format("{0:0.00}", Convert.ToString(total));

                return $"£{output}";
            }
        }

        #endregion

        // Properties for Sales and SaleProducts List
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

        private SaleDisplayModel _selectedSale;

        public SaleDisplayModel SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                OnPropertyChanged(nameof(SelectedSale));
                LoadSaleProducts();
            }
        }


        #endregion

        #region Date and Time properties
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
        #endregion

        // Default Constructor
        #region Constructor

        public BankingViewModel()
        {
            SelectedDate = DateTime.UtcNow.Date;

            Sales = new BindingList<SaleDisplayModel>();
            SaleProducts = new BindingList<SaleProductDisplayModel>();

            LoadSales();
        }

        #endregion

        #region Methods

        private void LoadSaleProducts()
        {
            int saleId = SelectedSale.Id;
            List<SaleProductModel> saleProducts = _saleProductData.GetSaleProductBySaleId(saleId);

            BindingList<SaleProductDisplayModel> displaySaleProduct = new BindingList<SaleProductDisplayModel>();

            foreach (var item in saleProducts)
            {
                displaySaleProduct.Add(new SaleProductDisplayModel
                {
                    Id = item.Id,
                    SaleId = item.SaleId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    ProductCost = ConvertIntToCurrencyString(item.ProductCost),
                    SalePrice = ConvertIntToCurrencyString(item.SalePrice),
                    QuantitySold = item.QuantitySold,
                    Total = ConvertIntToCurrencyString(item.Total),
                    Department = item.Department
                });
            }

            SaleProducts = new BindingList<SaleProductDisplayModel>(displaySaleProduct);
        }

        private void LoadSales()
        {
            var saleList = _salesData.GetAllSalesByDate(SelectedDate.ToString());

            BindingList<SaleDisplayModel> displaySales = new BindingList<SaleDisplayModel>();


            foreach (SaleModel item in saleList)
            {
                displaySales.Add(new SaleDisplayModel
                {
                    Id = item.Id,
                    InvoiceNo = item.InvoiceNo,
                    SaleDate = item.SaleDate,
                    SaleTime = item.SaleTime,
                    Card = ConvertIntToCurrencyString(item.Card),
                    Cash = ConvertIntToCurrencyString(item.Cash),
                    Credit = ConvertIntToCurrencyString(item.Credit),
                    SaleTotal = ConvertIntToCurrencyString(item.SaleTotal),
                    Tax =  ConvertIntToCurrencyString(item.Tax),
                    TotalCost = ConvertIntToCurrencyString(item.TotalCost),
                    Profit = ConvertIntToCurrencyString(item.Profit),
                    CashOnly = Convert.ToBoolean(item.CashOnly)
                });
            }
            Sales = new BindingList<SaleDisplayModel>(displaySales);
        }

        // Converts an int value to a decimal value and returns a sting formtted as 0.00
        private string ConvertIntToCurrencyString(int intValue)
        {
            decimal decimalValue = Convert.ToDecimal(intValue);
            decimal twoDecimalplaceValue = decimal.Divide(decimalValue, 100m);
            decimal currencyValue = Math.Round(twoDecimalplaceValue, 2);

            return $"£{string.Format("{0:0.00}", currencyValue)}";
        }

        private decimal ConvertCurrencyStringToDecimal(string stringValue)
        {
            if (stringValue.StartsWith("£"))
            {
                stringValue = stringValue.Remove(0, 1);
            }

            decimal decimalValue = Convert.ToDecimal(stringValue);

            return decimalValue;
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
