using DataAccessLibrary.DataAccess.ExpenseQueries;
using DataAccessLibrary.DataAccess.SalesQueries;
using DataAccessLibrary.Models;
using DesktopUI.Commands.BankingCommands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class BankingViewModel : ViewModelBase
    {
        #region private Properties

        //ISalesDataAccess _salesData;

        // Need to use DI in the future
        private SalesData _salesData = new SalesData();

        private SaleProductData _saleProductData = new SaleProductData();
        private ExpenseData _expenseData = new ExpenseData();

        private int _selectedExpenseId;
        private CurrencyHelper _currencyHelper = new CurrencyHelper();
        private DateTimeHelper _dateTimeHelper = new DateTimeHelper();
        private SaleStore _saleStore;

        #endregion private Properties

        #region Totals Properties

        /// <summary>
        /// 
        /// </summary>
        private string _total;

        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
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

        private string _totalTillCash;

        public string TotalTillCash
        {
            get { return _totalTillCash; }
            set
            {
                _totalTillCash = value;
                OnPropertyChanged(nameof(TotalTillCash));
            }
        }

        private string _totalCard;

        public string TotalCard
        {
            get { return _totalCard; }
            set
            {
                _totalCard = value;
                OnPropertyChanged(nameof(TotalCard));
            }
        }

        private string _totalCash;

        public string TotalCash
        {
            get { return _totalCash; }
            set
            {
                _totalCash = value;
                OnPropertyChanged(nameof(TotalCash));
            }
        }

        private string _totalCashOnly;

        public string TotalCashOnly
        {
            get { return _totalCashOnly; }
            set
            {
                _totalCashOnly = value;
                OnPropertyChanged(nameof(TotalCashOnly));
            }
        }

        private string _totalCredit;

        public string TotalCredit
        {
            get { return _totalCredit; }
            set
            {
                _totalCredit = value;
                OnPropertyChanged(nameof(TotalCredit));
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

        private string _totalExpense;

        public string TotalExpense
        {
            get { return _totalExpense; }
            set
            {
                _totalExpense = value;
                OnPropertyChanged(nameof(TotalExpense));
            }
        }

        #endregion Totals Properties

        #region Department Properties

        private int _mobileCount;

        public int MobileCount
        {
            get { return _mobileCount; }
            set
            {
                _mobileCount = value;
                OnPropertyChanged(nameof(MobileCount));
            }
        }

        private string _mobileTotal;

        public string MobileTotal
        {
            get { return _mobileTotal; }
            set
            {
                _mobileTotal = value;
                OnPropertyChanged(nameof(MobileTotal));
            }
        }

        private int _computerCount;

        public int ComputerCount
        {
            get { return _computerCount; }
            set
            {
                _computerCount = value;
                OnPropertyChanged(nameof(ComputerCount));
            }
        }

        private string _computerTotal;

        public string ComputerTotal
        {
            get { return _computerTotal; }
            set
            {
                _computerTotal = value;
                OnPropertyChanged(nameof(ComputerTotal));
            }
        }

        private int _cameraCount;

        public int CameraCount
        {
            get { return _cameraCount; }
            set
            {
                _cameraCount = value;
                OnPropertyChanged(nameof(CameraCount));
            }
        }

        private string _cameraTotal;

        public string CameraTotal
        {
            get { return _cameraTotal; }
            set
            {
                _cameraTotal = value;
                OnPropertyChanged(nameof(CameraTotal));
            }
        }

        private int _homeCount;

        public int HomeCount
        {
            get { return _homeCount; }
            set
            {
                _homeCount = value;
                OnPropertyChanged(nameof(HomeCount));
            }
        }

        private string _homeTotal;

        public string HomeTotal
        {
            get { return _homeTotal; }
            set
            {
                _homeTotal = value;
                OnPropertyChanged(nameof(HomeTotal));
            }
        }

        private int _repairCount;

        public int RepairCount
        {
            get { return _repairCount; }
            set
            {
                _repairCount = value;
                OnPropertyChanged(nameof(RepairCount));
            }
        }

        private string _repairTotal;

        public string RepairTotal
        {
            get { return _repairTotal; }
            set
            {
                _repairTotal = value;
                OnPropertyChanged(nameof(RepairTotal));
            }
        }

        private int _avCount;

        public int AvCount
        {
            get { return _avCount; }
            set
            {
                _avCount = value;
                OnPropertyChanged(nameof(AvCount));
            }
        }

        private string _avTotal;

        public string AvTotal
        {
            get { return _avTotal; }
            set
            {
                _avTotal = value;
                OnPropertyChanged(nameof(AvTotal));
            }
        }

        #endregion Department Properties

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
                decimal total = _currencyHelper.ConvertStringToDecimal(CardExpense) + _currencyHelper.ConvertStringToDecimal(CashExpense);
                return _currencyHelper.ConvertDecimalToString(total);
            }
        }

        private string _expenseLable;

        public string ExpenseLable
        {
            get { return _expenseLable; }
            set
            {
                _expenseLable = value;
                OnPropertyChanged(nameof(ExpenseLable));
            }
        }

        private string _expenseButtonContent;

        public string ExpenseButtonContent
        {
            get { return _expenseButtonContent; }
            set
            {
                _expenseButtonContent = value;
                OnPropertyChanged(nameof(ExpenseButtonContent));
            }
        }

        #endregion Expense properties

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

        private BindingList<ExpenseDisplayModel> _expenses;

        public BindingList<ExpenseDisplayModel> Expenses
        {
            get { return _expenses; }
            set
            {
                _expenses = value;
                OnPropertyChanged(nameof(Expenses));
                GetTotals();
                //TotalExpense = CalculateTotalExpense();
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

        private ExpenseDisplayModel _selectedExpense;

        public ExpenseDisplayModel SelectedExpense
        {
            get { return _selectedExpense; }
            set
            {
                _selectedExpense = value;
                OnPropertyChanged(nameof(SelectedExpense));
            }
        }

        #endregion List Properties

        #region Date and Time properties

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                GetDayOfWeek();
                LoadSales();
                LoadExpense();
                GetDepartmentDetails();
                GetTotals();
            }
        }

        private string _selectedDay;

        public string SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                _selectedDay = value;
                OnPropertyChanged(nameof(SelectedDay));
            }
        }

        #endregion Date and Time properties

        #region Commands Properties

        public AddExpenseCommand AddExpense { get; set; }
        public ICommand EditExpense { get; set; }
        public RemoveExpenseCommand RemoveExpense { get; set; }
        public ClearExpenseCommand ClearExpense { get; set; }
        public ICommand EditSale { get; }
        public ICommand DateBackCommand { get; }
        public ICommand DateNextCommand { get; }
        public ICommand RefundCommand { get; }

        #endregion Commands Properties

        #region Constructor

        public BankingViewModel(INavigationService editSaleNavigationService,
            INavigationService refundNavigationService, SaleStore saleStore)
        {
            _saleStore = saleStore;
            SelectedDate = DateTime.UtcNow.Date;
            ExpenseLable = "New Expense";
            ExpenseButtonContent = "Add";

            Sales = new BindingList<SaleDisplayModel>();
            SaleProducts = new BindingList<SaleProductDisplayModel>();

            // Commands
            AddExpense = new AddExpenseCommand(this);
            EditExpense = new EditExpenseCommand(this);
            RemoveExpense = new RemoveExpenseCommand(this);
            ClearExpense = new ClearExpenseCommand(this);
            EditSale = new EditSaleCommand(editSaleNavigationService, saleStore, this);
            DateBackCommand = new DateBackCommand(this);
            DateNextCommand = new DateNextCommand(this);
            RefundCommand = new RefundCommand(refundNavigationService, this, saleStore);

            // Populate Sale and Expenses ListBoxes
            LoadSales();
            LoadExpense();
            GetTotals();
            GetDayOfWeek();
        }

        #endregion Constructor

        #region Methods

        public void SetupSaleForRefund()
        {
            _saleStore.SelectedSale = SelectedSale;
        }

        private void GetDayOfWeek()
        {
            SelectedDay = SelectedDate.ToString("dddd");
        }

        // Gets all the Department details
        private void GetDepartmentDetails()
        {
            GetMobileDetails();
            GetComputerDetails();
            GetCameraDetails();
            GetHomeDetails();
            GetRepairDetails();
            GetAvDetails();
        }

        // Calculates all the total fields
        private void GetTotals()
        {
            decimal totalCard = Sales.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Card));
            TotalCard = _currencyHelper.ConvertDecimalToString(totalCard);

            decimal totalCash = Sales.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Cash));
            TotalCash = _currencyHelper.ConvertDecimalToString(totalCash);

            List<SaleModel> sales = _salesData.GetCashOnlySalesByDate(SelectedDate.ToString());
            int total = sales.Sum(x => x.SaleTotal);
            TotalCashOnly = _currencyHelper.ConvertIntToCurrencyString(total);

            decimal totalCredit = Sales.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Credit));
            TotalCredit = _currencyHelper.ConvertDecimalToString(totalCredit);

            // Total Expense
            TotalExpense = CalculateTotalExpense();

            // Total
            decimal totalSale = Sales.Sum(x => _currencyHelper.ConvertStringToDecimal(x.SaleTotal));
            Total = _currencyHelper.ConvertDecimalToString(totalSale);

            // TotalProfit
            decimal totalProfit = Sales.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Profit));
            TotalProfit = _currencyHelper.ConvertDecimalToString(totalProfit);

            // Total Cash in Till
            List<SaleModel> cashOnlySales = _salesData.GetCashOnlySalesByDate(SelectedDate.ToString());
            decimal totalCashOnly = cashOnlySales.Sum(x => _currencyHelper.ConvertStringToDecimal(_currencyHelper.ConvertIntToCurrencyString(x.CashOnly)));
            decimal totalCashExpense = Expenses.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Cash));
            decimal tillCash = (totalCash + totalCashOnly) - totalCashExpense;
            TotalTillCash = _currencyHelper.ConvertDecimalToString(tillCash);
        }

        private void GetMobileDetails()
        {
            List<SaleProductModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Mobile");

            decimal total = 0.00m;
            foreach (SaleProductModel item in sales)
            {
                decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
            }

            MobileTotal = string.Format("{0:0.00}", Convert.ToString(total));
            MobileCount = sales.Sum(x => x.QuantitySold);
        }

        private void GetComputerDetails()
        {
            List<SaleProductModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Computer");

            decimal total = 0.00m;
            foreach (SaleProductModel item in sales)
            {
                decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
            }

            ComputerTotal = string.Format("{0:0.00}", Convert.ToString(total));
            ComputerCount = sales.Sum(x => x.QuantitySold);
        }

        private void GetCameraDetails()
        {
            List<SaleProductModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Camera");

            decimal total = 0.00m;
            foreach (SaleProductModel item in sales)
            {
                decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
            }

            CameraTotal = string.Format("{0:0.00}", Convert.ToString(total));
            CameraCount = sales.Sum(x => x.QuantitySold);
        }

        private void GetHomeDetails()
        {
            List<SaleProductModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Home");

            decimal total = 0.00m;
            foreach (SaleProductModel item in sales)
            {
                decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
            }

            HomeTotal = string.Format("{0:0.00}", Convert.ToString(total));
            HomeCount = sales.Sum(x => x.QuantitySold);
        }

        private void GetRepairDetails()
        {
            List<SaleProductModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "Repair");

            decimal total = 0.00m;
            foreach (SaleProductModel item in sales)
            {
                decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
            }

            RepairTotal = string.Format("{0:0.00}", Convert.ToString(total));
            RepairCount = sales.Sum(x => x.QuantitySold);
        }

        private void GetAvDetails()
        {
            List<SaleProductModel> sales = _salesData.GetSalesByDepartmentAndDate(SelectedDate.ToString(), "AV");

            decimal total = 0.00m;
            foreach (SaleProductModel item in sales)
            {
                decimal Saleprice = Convert.ToDecimal(item.SalePrice);
                decimal twoDecimalSalePrice = decimal.Divide(Saleprice, 100m);
                total += Convert.ToDecimal(item.QuantitySold) * twoDecimalSalePrice;
            }

            AvTotal = string.Format("{0:0.00}", Convert.ToString(total));
            AvCount = sales.Sum(x => x.QuantitySold);
        }

        // Gets all the products sold in the selected sale
        private void LoadSaleProducts()
        {
            if (SelectedSale != null)
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
                        ProductCost = _currencyHelper.ConvertIntToCurrencyString(item.ProductCost),
                        SalePrice = _currencyHelper.ConvertIntToCurrencyString(item.SalePrice),
                        QuantitySold = item.QuantitySold,
                        Total = _currencyHelper.ConvertIntToCurrencyString(item.Total),
                        Department = item.Department
                    });
                }

                SaleProducts = new BindingList<SaleProductDisplayModel>(displaySaleProduct);
            }
        }

        //private string ConvertToLocalTime(string timeValue)
        //{
        //    DateTime utcTime = Convert.ToDateTime(timeValue);
        //    DateTime localTime = utcTime.ToLocalTime();

        //    return localTime.ToString("hh:mm tt");
        //}

        public void LoadSales()
        {
            List<SaleModel> saleList = _salesData.GetAllSalesByDate(SelectedDate.ToString());

            BindingList<SaleDisplayModel> displaySales = new BindingList<SaleDisplayModel>();

            foreach (SaleModel item in saleList)
            {
                displaySales.Add(new SaleDisplayModel
                {
                    Id = item.Id,
                    InvoiceNo = item.InvoiceNo,
                    SaleDate = item.SaleDate,
                    SaleTime = _dateTimeHelper.ConvertToLocalTime(item.SaleTime).ToString("hh:mm tt"),
                    Card = _currencyHelper.ConvertIntToCurrencyString(item.Card),
                    Cash = _currencyHelper.ConvertIntToCurrencyString(item.Cash),
                    Credit = _currencyHelper.ConvertIntToCurrencyString(item.Credit),
                    SaleTotal = _currencyHelper.ConvertIntToCurrencyString(item.SaleTotal),
                    Tax = _currencyHelper.ConvertIntToCurrencyString(item.Tax),
                    TotalCost = _currencyHelper.ConvertIntToCurrencyString(item.TotalCost),
                    Profit = _currencyHelper.ConvertIntToCurrencyString(item.Profit),
                    CashOnly = Convert.ToBoolean(item.CashOnly),
                    IsActive = Convert.ToBoolean(item.IsActive)
                });
            }
            Sales = new BindingList<SaleDisplayModel>(displaySales);
        }

        public void ClearExpenseFIelds()
        {
            CardExpense = null;
            CashExpense = null;
            ExpenseDetails = null;
        }

        public void AddNewExpense()
        {
            if ((string.IsNullOrWhiteSpace(CardExpense) && string.IsNullOrWhiteSpace(CashExpense)) || string.IsNullOrWhiteSpace(ExpenseDetails))
            {
                MessageBox.Show("Please enter a value in either Card or Cash, and Details");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(CardExpense))
                {
                    CardExpense = "0.00";
                }
                else if (string.IsNullOrWhiteSpace(CashExpense))
                {
                    CashExpense = "0.00";
                }

                ExpenseModel expense = new ExpenseModel
                {
                    ExpenseDate = SelectedDate.ToString(),
                    ExpenseDetails = ExpenseDetails,
                    Card = _currencyHelper.ConvertStringToInt(CardExpense),
                    Cash = _currencyHelper.ConvertStringToInt(CashExpense),
                    ExpenseTotal = _currencyHelper.ConvertStringToInt(CardExpense) + _currencyHelper.ConvertStringToInt(CashExpense)
                };

                _expenseData.SaveExpense(expense);
                ClearExpenseFIelds();
                LoadExpense();
            }
        }

        private string CalculateTotalExpense()
        {
            decimal totalCard = Expenses.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Card));
            decimal totalCash = Expenses.Sum(x => _currencyHelper.ConvertStringToDecimal(x.Cash));

            decimal totalExpense = totalCard + totalCash;

            return _currencyHelper.ConvertDecimalToString(totalExpense);
        }

        private void LoadExpense()
        {
            List<ExpenseModel> expenses = _expenseData.LoadAllExpenseByDate(SelectedDate.ToString());

            List<ExpenseDisplayModel> displayExpenses = new List<ExpenseDisplayModel>();

            foreach (var expense in expenses)
            {
                displayExpenses.Add(new ExpenseDisplayModel
                {
                    Id = expense.Id,
                    ExpenseDate = expense.ExpenseDate,
                    ExpenseDetails = expense.ExpenseDetails,
                    Card = _currencyHelper.ConvertIntToCurrencyString(expense.Card),
                    Cash = _currencyHelper.ConvertIntToCurrencyString(expense.Cash),
                    ExpenseTotal = _currencyHelper.ConvertIntToCurrencyString(expense.ExpenseTotal)
                });
            }

            Expenses = new BindingList<ExpenseDisplayModel>(displayExpenses);

            TotalExpense = CalculateTotalExpense();
        }

        public void EditSelectedExpense()
        {
            _selectedExpenseId = SelectedExpense.Id;

            // Setup fields for edit
            ExpenseLable = "Edit Expense";
            ExpenseButtonContent = "Update";
            CardExpense = _currencyHelper.RemoveCurrencyFromString(SelectedExpense.Card);
            CashExpense = _currencyHelper.RemoveCurrencyFromString(SelectedExpense.Cash);
            ExpenseDetails = _currencyHelper.RemoveCurrencyFromString(SelectedExpense.ExpenseDetails);
        }

        public void UpdateExpense()
        {
            int card = _currencyHelper.ConvertStringToInt(CardExpense);
            int cash = _currencyHelper.ConvertStringToInt(CashExpense);
            int total = card + cash;

            _expenseData.UpdateExpense(SelectedExpense.Id, card, cash, total, ExpenseDetails);

            ExpenseLable = "New Expense";
            ExpenseButtonContent = "Add";
            LoadExpense();
            ClearExpenseFIelds();
        }

        public void RemoveSelectedExpense()
        {
            _expenseData.VoidExpense(SelectedExpense.Id);
            LoadExpense();
        }

        public void ShowCashOnly()
        {
        }

        #endregion Methods
    }
}