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

namespace DesktopUI.ViewModels
{
    public class ManualSaleViewModel : ViewModelBase
    {
        #region Private properties

        private int _manualCode;
        private decimal _manualCost;
        private string _manualProductName;
        private string _manualProductDescription;
        private int _manualQuantity;
        private decimal _manualPrice;
        private string _department;

        private BindingList<CartItemDisplayModel> _cart;

        private SalesData _salesData = new SalesData();

        #endregion

        #region Date and Time properties

        // Date and Time properties
        public string SaleDate { get; set; }
        public string CurrentTime { get; set; }

        #endregion

        #region Manual Entry Properties

        /// <summary>
        /// All the Manual entry properties for creating New Product
        /// </summary>
        public string ManualProductName
        {
            get { return _manualProductName; }
            set
            {
                _manualProductName = value;
                OnPropertyChanged(nameof(ManualProductName));
            }
        }

        public string ManualProductDescription
        {
            get { return _manualProductDescription; }
            set
            {
                _manualProductDescription = value;
                OnPropertyChanged(nameof(ManualProductDescription));
            }
        }

        public int ManualQuantity
        {
            get { return _manualQuantity; }
            set
            {
                _manualQuantity = value;
                OnPropertyChanged(nameof(ManualQuantity));
            }
        }

        public decimal ManualPrice
        {
            get { return _manualPrice; }
            set
            {
                _manualPrice = value;
                OnPropertyChanged(nameof(ManualPrice));
            }
        }

        public decimal ManualCost
        {
            get { return _manualCost; }
            set
            {
                _manualCost = value;
                OnPropertyChanged(nameof(ManualCost));
            }
        }

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

        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
            }
        }

        private bool _enableManualName;

        public bool EnableManualName
        {
            get { return _enableManualName; }
            set
            {
                _enableManualName = value;
                OnPropertyChanged(nameof(EnableManualName));
            }
        }

        private bool _enableManualCost;

        public bool EnableManualCost
        {
            get { return _enableManualCost; }
            set
            {
                _enableManualCost = value;
                OnPropertyChanged(nameof(EnableManualCost));
            }
        }

        private string _manualEntryLable;

        public string ManualEntryLable
        {
            get { return _manualEntryLable; }
            set
            {
                _manualEntryLable = value;
                OnPropertyChanged(nameof(ManualEntryLable));
            }
        }

        private string _departmentChecked;

        public string DepartmentChecked
        {
            get { return _departmentChecked; }
            set
            {
                _departmentChecked = value;
                OnPropertyChanged(nameof(DepartmentChecked));
            }
        }


        #endregion

        #region Command Properties

        // Command Properties

        public ICommand NavigateBankingCommand { get; }

        public AddManualProductCommand AddManualProduct { get; set; }
        public PayCommand Pay { get; set; }

        private RemoveFromCartCommand _removeFromCart;

        public RemoveFromCartCommand RemoveFromCart
        {
            get { return _removeFromCart; }
            set
            {
                _removeFromCart = value;
                OnPropertyChanged(nameof(RemoveFromCart));
            }
        }

        public EditCartItemCommand EditCartItem { get; set; }

        // Select Department Command properties
        public CameraDepartmentCommand SelectCameraDepartment { get; set; }
        public ComputerDepartmentCommand SelectComputerDepartment { get; set; }
        public HomeDepartmentCommand SelectHomeDepartment { get; set; }
        public MobileDepartmentCommand SelectMobileDepartment { get; set; }
        public RepairDepartmentCommand SelectRepairDepartment { get; set; }

        #endregion

        #region Cart Properties
        // Cart Binding List
        public BindingList<CartItemDisplayModel> Cart
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
        #endregion

        #region Payment Properties

        // Totals Properties
        private decimal _cartTotal;

        public decimal CartTotal
        {
            get
            {
                decimal cartTotal = 0m;

                if (Cart != null)
                {
                    foreach (var item in Cart)
                    {
                        cartTotal += item.Total;
                    }
                }
                _cartTotal = cartTotal;

                return Math.Round(_cartTotal, 2);
            }
        }

        private decimal _subtotal;

        public decimal Subtotal
        {
            get
            {
                decimal divideBy = 1.2m;

                _subtotal = _cartTotal / divideBy;

                return Math.Round(_subtotal, 2);
            }
        }

        private decimal _tax;

        public decimal Tax
        {
            get
            {
                _tax = _cartTotal - _subtotal;

                return Math.Round(_tax, 2);
            }
        }


        // Payment option Properties

        private decimal _cardPayment;

        public decimal CardPayment
        {
            get { return _cardPayment; }
            set
            {
                _cardPayment = value;
                OnPropertyChanged(nameof(CardPayment));
                CheckPaymentSum();
            }
        }

        private decimal _cashPayment;

        public decimal CashPayment
        {
            get { return _cashPayment; }
            set
            {
                _cashPayment = value;
                OnPropertyChanged(nameof(CashPayment));
                CheckPaymentSum();
            }
        }

        private decimal _creditPayment;

        public decimal CreditPayment
        {
            get { return _creditPayment; }
            set
            {
                _creditPayment = value;
                OnPropertyChanged(nameof(CreditPayment));
                CheckPaymentSum();
            }
        }

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
            NavigateBankingCommand = new NavigateCommand(bankingNavigateService);

            SaleDate = DateTime.Now.ToString("dd,MM,yyyy");
            CurrentTime = DateTime.UtcNow.ToString("hh:mm:ss");

            CashOnlySale = false;
            SumPayment = 0m;
            ManualQuantity = 1;
            ManualEntryLable = "Manual Entry";
            EnableManualName = true;
            EnableManualCost = true;

            AddManualProduct = new AddManualProductCommand(this);
            RemoveFromCart = new RemoveFromCartCommand(this);
            EditCartItem = new EditCartItemCommand(this);
            Pay = new PayCommand(this);

            // Department selection commands
            SelectCameraDepartment = new CameraDepartmentCommand(this);
            SelectComputerDepartment = new ComputerDepartmentCommand(this);
            SelectHomeDepartment = new HomeDepartmentCommand(this);
            SelectMobileDepartment = new MobileDepartmentCommand(this);
            SelectRepairDepartment = new RepairDepartmentCommand(this);

            Cart = new BindingList<CartItemDisplayModel>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts Code entered to cost and rounds off at 2 decimal places
        /// </summary>
        private void ConvertCodeToCost()
        {
            int devideBy = 6;
            decimal cost = (decimal)ManualCode / (decimal)devideBy;

            ManualCost = Math.Round(cost, 2);
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <returns>ProductModel</returns>
        private ProductModel CreateManualProduct()
        {
            ProductModel product = new ProductModel
            {
                Id = -1,
                ProductName = ManualProductName,
                ProductDescription = ManualProductDescription,
                AverageCost = ManualCost,
                Price = ManualPrice,
                Department = Department
            };
            return product;
        }

        /// <summary>
        /// Takes the created product and adds it to the cart
        /// also adds the quantity entered and calculates the 
        /// total amount for that product
        /// </summary>
        public void AddCartManualItem()
        {
            if (string.IsNullOrWhiteSpace(ManualProductName))
            {
                MessageBox.Show("You need to enter Product Name");
            }
            else if (string.IsNullOrWhiteSpace(Department))
            {
                MessageBox.Show("You need to select a Department");
            }
            else if (ManualQuantity <= 0)
            {
                MessageBox.Show("You need to enter a Quantity");
            }
            else
            {
                ProductModel product = CreateManualProduct();

                decimal total = ManualQuantity * ManualPrice;

                CartItemDisplayModel item = new CartItemDisplayModel
                {
                    Product = product,
                    Quantity = ManualQuantity,
                    Price = ManualPrice,
                    Total = total
                };

                Cart.Add(item);
                OnPropertyChanged(nameof(CartTotal));
                OnPropertyChanged(nameof(Subtotal));
                OnPropertyChanged(nameof(Tax));
                CheckCashOnlySale();
                CheckPaymentSum();
                ClearManualFields();
            }
        }

        private void ClearManualFields()
        {
            ManualProductName = null;
            ManualProductDescription = null;
            ManualCost = 0m;
            ManualPrice = 0m;
            ManualQuantity = 1;
            Department = null;
            DepartmentChecked = null;
        }

        public void EditSelectedCartItem()
        {
            CartItemEditSetup();
            RemoveItemFromCart();
        }

        private void CartItemEditSetup()
        {
            ManualProductName = SelectedCartItem.Product.ProductName;
            ManualProductDescription = SelectedCartItem.Product.ProductDescription;
            ManualCost = SelectedCartItem.Product.AverageCost;
            ManualPrice = SelectedCartItem.Product.Price;
            ManualQuantity = SelectedCartItem.Quantity;

            ManualEntryLable = "Edit Product";

            if (SelectedCartItem.Product.Id != -1)
            {
                // For products from database - only edit description, price and quantity
                // Disable Name and cost textboxes
                EnableManualName = false;
                EnableManualCost = false;
            }
            else
            {
                EnableManualName = true;
                EnableManualCost = true;
            }
        }

        private decimal CalculateTotalCartCost()
        {
            foreach (var item in Cart)
            {
                item.TotalCost = (item.Product.AverageCost * item.Quantity);
            }

            decimal cartCost = 0m;

            foreach (var item in Cart)
            {
                cartCost += item.TotalCost;
            }

            return cartCost;
        }

        public void CompleteSale()
        {
            // Check if sum of payment methods == to CartTotal
            if (CartTotal == SumPayment)
            {
                _salesData.SaveSale(CreateSaleForDB(), CreateSaleProductsForDB());

                // Clear out items from the cart list
                Cart.Clear();

                // Clear out payment o method fields
                CardPayment = 0m;
                CashPayment = 0m;
                CreditPayment = 0m;
            }
            else
            {
                // Show MessageBox asaking to enter payment method
                MessageBox.Show("Full payment was not taken. Please enter correct Card, Cash or Credit amount");
            }
        }

        private SaleModel CreateSaleForDB()
        {
            decimal cartProfit = CartTotal - CalculateTotalCartCost();

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
                Card = ConvertDecimalToInt(CardPayment),
                Cash = ConvertDecimalToInt(CashPayment),
                Credit = ConvertDecimalToInt(CreditPayment),
                SaleTotal = ConvertDecimalToInt(CartTotal),
                Tax = ConvertDecimalToInt(Tax),
                TotalCost = ConvertDecimalToInt(CalculateTotalCartCost()),
                Profit = ConvertDecimalToInt(cartProfit),
                CashOnly = cashOnly
            };

            return sale;
        }

        private List<SaleProductModel> CreateSaleProductsForDB()
        {
            List<SaleProductModel> saleProducts = new List<SaleProductModel>();

            foreach (var item in Cart)
            {
                saleProducts.Add(new SaleProductModel
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.ProductName,
                    ProductDescription = item.Product.ProductDescription,
                    ProductCost = ConvertDecimalToInt(item.Product.AverageCost),
                    SalePrice = ConvertDecimalToInt(item.Price),
                    QuantitySold = item.Quantity,
                    Department = item.Product.Department
                });
            }

            return saleProducts;
        }

        private int ConvertDecimalToInt(decimal decimalValue)
        {
            int intValue = Convert.ToInt32(Math.Truncate(decimalValue * 100m));

            return intValue;
        }

        private void CheckPaymentSum()
        {
            decimal total = _cardPayment + _cashPayment + _creditPayment;

            SumPayment = Math.Round(total, 2);
        }

        /// <summary>
        /// Remove Item from Cart
        /// </summary>
        public void RemoveItemFromCart()
        {
            Cart.Remove(SelectedCartItem);

            OnPropertyChanged(nameof(CartTotal));
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(Tax));
        }

        private void CheckCashOnlySale()
        {
            if (CashOnlySale == true)
            {
                CashPayment = CartTotal;
                CardPayment = 0m;

                EnableCash = true;
                EnableCard = false;
            }
            else
            {
                CashPayment = 0m;
                CardPayment = 0m;

                EnableCard = true;
                EnableCash = true;
            }
        }

        public void SelectMobile()
        {
            Department = "Mobile";
        }

        public void SelectCamera()
        {
            Department = "Camera";
        }

        public void SelectComputer()
        {
            Department = "Computer";
        }

        public void SelectHome()
        {
            Department = "Home";
        }

        public void SelectRepair()
        {
            Department = "Repair";
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
