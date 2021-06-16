using DataAccessLibrary.DataAccess.ExpenseQueries;
using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands.BankingCommands;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DesktopUI.ViewModels
{
    public class BankingViewModel : INotifyPropertyChanged
    {
        #region private Properties

        //ISalesDataAccess _salesData;

        // Need to use DI in the future
        private SalesData _salesData = new SalesData();
        private SaleProductData _saleProductData = new SaleProductData();
        private ExpenseData _expenseData = new ExpenseData();

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

        // Properties for Expenses
        #region Expense properties

        private string _cardExpense;

        public string CardExpense
        {
            get { return _cardExpense; }
            set
            {
                _cardExpense = value;
                OnPropertyChanged(nameof(CardExpense));
            }
        }

        private string _cashExpense;

        public string CashExpense
        {
            get { return _cashExpense; }
            set
            {
                _cashExpense = value;
                OnPropertyChanged(nameof(CashExpense));
            }
        }

        private string _expenseDetails;

        public string ExpenseDetails
        {
            get { return _expenseDetails; }
            set
            {
                _expenseDetails = value;
                OnPropertyChanged(nameof(ExpenseDetails));
            }
        }

        public string ExpenseTotal
        {
            get
            {
                decimal total = ConvertCurrencyStringToDecimal(CardExpense) + ConvertCurrencyStringToDecimal(CashExpense);
                return ConvertDecimalToCurrencyString(total);
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

        #region Commands Properties

        public AddExpenseCommand AddExpenseCmd { get; set; }

        #endregion

        // Default Constructor
        #region Constructor

        public BankingViewModel()
        {
            SelectedDate = DateTime.UtcNow.Date;

            // Expense default value
            CardExpense = "£0.00";
            CashExpense = "£0.00";

            Sales = new BindingList<SaleDisplayModel>();
            SaleProducts = new BindingList<SaleProductDisplayModel>();

            // Commands
            AddExpenseCmd = new AddExpenseCommand(this);

            // Populate Sale ListBox
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

        private string ConvertDecimalToCurrencyString(decimal decimalValue)
        {
            return $"£{string.Format("{0:0.00}", decimalValue)}";
        }

        // Converts an int value to a decimal value and returns a sting formtted as 0.00
        private string ConvertIntToCurrencyString(int intValue)
        {
            decimal decimalValue = Convert.ToDecimal(intValue);
            decimal twoDecimalplaceValue = decimal.Divide(decimalValue, 100m);
            decimal currencyValue = Math.Round(twoDecimalplaceValue, 2);

            return $"£{string.Format("{0:0.00}", currencyValue)}";
        }

        // Checks if string Value precedes with £, removes it and converts the string value to decimal value.
        private decimal ConvertCurrencyStringToDecimal(string stringValue)
        {
            if (stringValue.StartsWith("£"))
            {
                stringValue = stringValue.Remove(0, 1);
            }

            decimal decimalValue = Convert.ToDecimal(stringValue);

            return decimalValue;
        }

        private int ConvertCurrencyStringToInt(string stringValue)
        {
            if (stringValue != null && stringValue.StartsWith("£"))
            {
                stringValue = stringValue.Remove(0, 1);
            }

            decimal decimalValue = Convert.ToDecimal(stringValue);
            int intValue = Convert.ToInt32(Math.Truncate(decimalValue * 100m));

            return intValue;
        }

        public void EditSale()
        {

        }

        public void AddExpense()
        {
            if (CardExpense == "0.00" && CashExpense == "0.00")
            {
                MessageBox.Show("Please enter a value in either Card or Cash");
            }
            else
            {
                ExpenseModel expense = new ExpenseModel
                {
                    ExpenseDate = DateTime.UtcNow.Date,
                    ExpenseDetails = ExpenseDetails,
                    Card = ConvertCurrencyStringToInt(CardExpense),
                    Cash = ConvertCurrencyStringToInt(CashExpense),
                    Total = ConvertCurrencyStringToInt(CardExpense) + ConvertCurrencyStringToInt(CashExpense)
                };

                _expenseData.SaveExpense(expense);
            }
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
