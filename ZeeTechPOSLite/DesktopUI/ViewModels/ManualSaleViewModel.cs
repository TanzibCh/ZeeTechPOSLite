using DataAccessLibrary.Models;
using DesktopUI.Models;
using DesktopUI.Commands.ManualSaleCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using DataAccessLibrary.DataAccess.SalesQueries;
using System.Windows.Input;
using DesktopUI.Commands;
using DesktopUI.Stores;
using DesktopUI.Services;
using DesktopUI.Helpers;
using System.Linq;

namespace DesktopUI.ViewModels
{
    public class ManualSaleViewModel : ViewModelBase
    {
        #region Private properties

        private readonly CurrencyHelper _cHelper = new CurrencyHelper();
        private SalesData _salesData = new SalesData();

        #endregion

        #region Date and Time properties

        // Date and Time properties
        public string SaleDate { get; set; }
        public string CurrentTime { get; set; }

        #endregion

        #region Manual Entry Properties

        private ObservableCollection<DepartmantModel> _departments;

        public ObservableCollection<DepartmantModel> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private DepartmantModel _selectedDepartment;

        public DepartmantModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }

        private string _manualProductName;

        public string ManualProductName
        {
            get { return _manualProductName; }
            set
            {
                _manualProductName = value;
                OnPropertyChanged(nameof(ManualProductName));
            }
        }

        private string _manualProductDescription;

        public string ManualProductDescription
        {
            get { return _manualProductDescription; }
            set
            {
                _manualProductDescription = value;
                OnPropertyChanged(nameof(ManualProductDescription));
            }
        }

        private int _manualQuantity;

        public int ManualQuantity
        {
            get { return _manualQuantity; }
            set
            {
                _manualQuantity = value;
                OnPropertyChanged(nameof(ManualQuantity));
            }
        }

        private string _manualPrice;

        public string ManualPrice
        {
            get { return _manualPrice; }
            set
            {
                _manualPrice = value;
                OnPropertyChanged(nameof(ManualPrice));
            }
        }


        private string _manualCost;

        public string ManualCost
        {
            get { return _manualCost; }
            set
            {
                _manualCost = value;
                OnPropertyChanged(nameof(ManualCost));
            }
        }

        private int _manualCode;

        public int ManualCode
        {
            get { return _manualCode; }
            set
            {
                _manualCode = value;
                OnPropertyChanged(nameof(ManualCode));
                ConvertCodeToCost();
            }
        }

        #endregion

        #region Command Properties

        // Command Properties

        public ICommand NavigateBankingCommand { get; }

        public ICommand AddManualProduct { get; }
        public PayCommand Pay { get; set; }

        public ICommand RemoveItemFromCartCommand { get; }

        public EditCartItemCommand EditCartItem { get; set; }

        #endregion

        #region Cart Properties
        private ObservableCollection<CartItemDisplayModel> _cart;

        public ObservableCollection<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                OnPropertyChanged(nameof(Cart));
            }
        }

        private CartItemDisplayModel _selectedCartItem;

        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set
            {
                _selectedCartItem = value;
                OnPropertyChanged(nameof(SelectedCartItem));
            }
        }

        private string _cartProductName;

        public string CartProductName
        {
            get { return _cartProductName; }
            set
            {
                _cartProductName = value;
                OnPropertyChanged(nameof(CartProductName));
            }
        }

        #endregion

        #region Currency Properties

        // Totals Properties
        private string _cartTotal;

        public string CartTotal
        {
            get { return _cartTotal; }
            set
            {
                _cartTotal = value;
                OnPropertyChanged(nameof(CartTotal));
            }
        }

        private string _subtotal;

        public string Subtotal
        {
            get { return _subtotal; }
            set
            {
                _subtotal = value;
                OnPropertyChanged(nameof(Subtotal));
            }
        }

        private string _tax;

        public string Tax
        {
            get { return _tax; }
            set
            {
                _tax = value;
                OnPropertyChanged(nameof(Tax));
            }
        }

        // Payment Type Properties
        private string _cardPayment;

        public string CardPayment
        {
            get { return _cardPayment; }
            set
            {
                _cardPayment = value;
                OnPropertyChanged(nameof(CardPayment));
                CheckPaymentSum();
            }
        }

        private string _cashPayment;

        public string CashPayment
        {
            get { return _cashPayment; }
            set
            {
                _cashPayment = value;
                OnPropertyChanged(nameof(CashPayment));
                CheckPaymentSum();
            }
        }

        private string _creditPayment;

        public string CreditPayment
        {
            get { return _creditPayment; }
            set
            {
                _creditPayment = value;
                OnPropertyChanged(nameof(CreditPayment));
                CheckPaymentSum();
            }
        }

        // Payment Option properties
        private bool _cashOnlySale;

        public bool CashOnlySale
        {
            get { return _cashOnlySale; }
            set
            {
                _cashOnlySale = value;
                OnPropertyChanged(nameof(CashOnlySale));
                CheckCashOnlySale();
                CheckPaymentSum();
            }
        }

        private bool _enableCash;

        public bool EnableCash
        {
            get { return _enableCash; }
            set
            {
                _enableCash = value;
                OnPropertyChanged(nameof(EnableCash));
            }
        }

        private bool _enableCard;

        public bool EnableCard
        {
            get { return _enableCard; }
            set
            {
                _enableCard = value;
                OnPropertyChanged(nameof(EnableCard));
            }
        }

        // Property to check that sum of cart and sum of
        // payment types entered matches.
        private decimal _sumPayment;

        public decimal SumPayment
        {
            get { return _sumPayment; }
            set
            {
                _sumPayment = value;
                OnPropertyChanged(nameof(SumPayment));
            }
        }

        #endregion

        #region Constructor

        public ManualSaleViewModel(INavigationService bankingNavigateService)
        {
            Departments = new ObservableCollection<DepartmantModel>()
            {
                new DepartmantModel(){ DepartmentName = "Mobile"},
                new DepartmantModel(){ DepartmentName = "Computer"},
                new DepartmantModel(){ DepartmentName = "Camera"},
                new DepartmantModel(){ DepartmentName = "Home"},
                new DepartmantModel(){ DepartmentName = "AV"},
                new DepartmantModel(){ DepartmentName = "Repair"}
            };

            NavigateBankingCommand = new NavigateCommand(bankingNavigateService);

            SaleDate = DateTime.UtcNow.ToString("dd,MM,yyyy");
            CurrentTime = DateTime.UtcNow.ToString("hh:mm:ss");

            CashOnlySale = false;
            SumPayment = 0m;
            ManualQuantity = 1;
            CardPayment = "0.00";
            CashPayment = "0.00";
            CreditPayment = "0.00";

            // Commands
            AddManualProduct = new AddManualProductCommand(this);
            RemoveItemFromCartCommand = new RemoveFromCartCommand(this);
            EditCartItem = new EditCartItemCommand(this);
            Pay = new PayCommand(this);

            Cart = new ObservableCollection<CartItemDisplayModel>();
        }

        #endregion

        #region Methods

        // Modify Selected Product in Cart
        private void ModifyCartItem()
        {
            if (SelectedCartItem != null)
            {
                CartItemDisplayModel item = Cart.FirstOrDefault(i => i == SelectedCartItem);

                if (item != null)
                {
                    item.Product.ProductName = CartProductName;
                }
            }
            else
            {
                MessageBox.Show("Select a product to modify first.");
            }
        }

        /// <summary>
        /// Converts Code entered to cost and rounds off at 2 decimal places
        /// </summary>
        private void ConvertCodeToCost()
        {
            // TODO : make the devideBy value changable
            decimal devideBy = 6;
            decimal cost = (decimal)ManualCode / devideBy;

            ManualCost = _cHelper.ConvertDecimalToString(cost);
        }

        // Create Manual Product 
        private SaleProductDisplayModel CreateManualProduct()
        {
            SaleProductDisplayModel product = new SaleProductDisplayModel
            {
                Id = -1,
                ProductName = ManualProductName,
                ProductDescription = ManualProductDescription,
                ProductCost = ManualCost,
                SalePrice = ManualPrice,
                Department = SelectedDepartment.DepartmentName
            };
            return product;
        }

        /// <summary>
        /// Takes the created product and adds it to the cart
        /// also adds the quantity entered and calculates the 
        /// total amount for that product
        /// </summary>
        public void AddToCartManualItem()
        {
            if (string.IsNullOrWhiteSpace(ManualProductName))
            {
                MessageBox.Show("You need to enter Product Name");
            }
            else if (SelectedDepartment == null)
            {
                MessageBox.Show("You need to select a Department");
            }
            else if (ManualQuantity < 1)
            {
                MessageBox.Show("You need to enter a Quantity");
            }
            else
            {
                SaleProductDisplayModel product = CreateManualProduct();

                decimal total = ManualQuantity * _cHelper.ConvertStringToDecimal(ManualPrice);

                CartItemDisplayModel item = new CartItemDisplayModel
                {
                    Product = product,
                    Quantity = ManualQuantity,
                    Price = ManualPrice,
                    Total = _cHelper.ConvertDecimalToString(total)
                };

                Cart.Add(item);

                CalculateCartTotal();
                CalculateSubtotal();
                CalculateTax();
                CheckCashOnlySale();
                CheckPaymentSum();

                ClearManualFields();
            }
        }

        private void CalculateCartTotal()
        {
            decimal cartTotal = 0m;

            if (Cart != null)
            {
                foreach (CartItemDisplayModel item in Cart)
                {
                    cartTotal += _cHelper.ConvertStringToDecimal(item.Total);
                }
            }

            CartTotal = _cHelper.ConvertDecimalToString(cartTotal);
        }

        private void CalculateSubtotal()
        {
            decimal divideBy = 1.2m;

            decimal subtotal = _cHelper.ConvertStringToDecimal(_cartTotal) / divideBy;
            Subtotal = _cHelper.ConvertDecimalToString(subtotal);
        }

        private void CalculateTax()
        {
            decimal tax = _cHelper.ConvertStringToDecimal(_cartTotal) - _cHelper.ConvertStringToDecimal(_subtotal);

            Tax = _cHelper.ConvertDecimalToString(tax);
        }

        private void ClearManualFields()
        {
            ManualProductName = null;
            ManualProductDescription = null;
            ManualCost = null;
            ManualPrice = null;
            ManualQuantity = 1;
            SelectedDepartment = null;
        }

        public void EditSelectedCartItem()
        {
           // todo :  Create new Modal to edit selected cart item
        }

        private decimal CalculateCartProfit()
        {
            decimal cartTotal = 0;
            decimal totalCost = CalculateTotalCartCost();

            foreach (CartItemDisplayModel item in Cart)
            {
                cartTotal += _cHelper.ConvertStringToDecimal(item.Total);
            }

            return cartTotal - totalCost;
        }

        private decimal CalculateTotalCartCost()
        {
            foreach (CartItemDisplayModel item in Cart)
            {
                decimal cost = _cHelper.ConvertStringToDecimal(item.Product.ProductCost);

                item.TotalCost = _cHelper.ConvertDecimalToString((cost * item.Quantity));
            }

            decimal cartCost = 0m;

            foreach (CartItemDisplayModel item in Cart)
            {
                cartCost += _cHelper.ConvertStringToDecimal(item.TotalCost);
            }

            return cartCost;
        }

        public void CompleteSale()
        {
            // Check if sum of payment methods == to CartTotal
            if (_cHelper.ConvertStringToDecimal(CartTotal) == SumPayment)
            {
                _salesData.SaveSale(CreateSaleForDB(), CreateSaleProductsForDB());

                // Clear out items from the cart list
                Cart.Clear();

                // Clear out payment o method fields
                CardPayment = "0.00";
                CashPayment = "0.00";
                CreditPayment = "0.00";
            }
            else
            {
                // Show MessageBox asaking to enter payment method
                MessageBox.Show("Full payment was not taken. Please enter correct Card, Cash or Credit amount");
            }
        }

        private SaleModel CreateSaleForDB()
        {
            //decimal cartProfit = CartTotal - CalculateTotalCartCost();

            // Set CashIn as int to save in database
            int cashOnly = 0;
            if (CashOnlySale == true)
            {
                cashOnly = 1;
            }
            else
            {
                cashOnly = 0;
            }

            SaleModel sale = new SaleModel
            {
                Card = _cHelper.ConvertStringToInt(CardPayment),
                Cash = _cHelper.ConvertStringToInt(CashPayment),
                Credit = _cHelper.ConvertStringToInt(CreditPayment),
                SaleTotal = _cHelper.ConvertStringToInt(CartTotal),
                Tax = _cHelper.ConvertStringToInt(Tax),
                TotalCost = _cHelper.ConvertDecimalToInt(CalculateTotalCartCost()),
                Profit = _cHelper.ConvertDecimalToInt(CalculateCartProfit()),
                CashOnly = cashOnly
            };

            return sale;
        }

        private List<SaleProductModel> CreateSaleProductsForDB()
        {
            List<SaleProductModel> saleProducts = new List<SaleProductModel>();

            foreach (CartItemDisplayModel item in Cart)
            {
                saleProducts.Add(new SaleProductModel
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.ProductName,
                    ProductDescription = item.Product.ProductDescription,
                    ProductCost = _cHelper.ConvertStringToInt(item.Product.ProductCost),
                    SalePrice = _cHelper.ConvertStringToInt(item.Price),
                    QuantitySold = item.Quantity,
                    Department = item.Product.Department
                });
            }

            return saleProducts;
        }

        private void CheckPaymentSum()
        {
            decimal card = _cHelper.ConvertStringToDecimal(_cardPayment);
            decimal cash = _cHelper.ConvertStringToDecimal(_cashPayment);
            decimal credit = _cHelper.ConvertStringToDecimal(_creditPayment);

            decimal total = card + cash + credit;

            SumPayment = Math.Round(total, 2);
        }

        /// <summary>
        /// Remove Item from Cart
        /// </summary>
        public void RemoveItemFromCart()
        {
            Cart.Remove(SelectedCartItem);

            CalculateCartTotal();
            CalculateSubtotal();
            CalculateTax();
        }

        private void CheckCashOnlySale()
        {
            if (CashOnlySale == true)
            {
                decimal balance = _cHelper.ConvertStringToDecimal(CartTotal) - _cHelper.ConvertStringToDecimal(CreditPayment);

                CashPayment = _cHelper.ConvertDecimalToString(balance);
                CardPayment = "0.00";

                EnableCash = true;
                EnableCard = false;
            }
            else
            {
                CashPayment = "0.00";
                CardPayment = "0.00";

                EnableCard = true;
                EnableCash = true;
            }
        }
        #endregion
    }
}
