using DataAccessLibrary.Models;
using DesktopUI.Models;
using DesktopUI.Commands.ManualSaleCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace DesktopUI.ViewModels
{
    public class ManualSaleViewModel : INotifyPropertyChanged
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
        #endregion

        // Date and Time properties
        public string SaleDate { get; set; }
        public string CurrentTime { get; set; }

        #region Command Properties
        // Command Properties
        public AddManualProductCommand AddManualProduct { get; set; }
        public RemoveFromCartCommand RemoveFromCart { get; set; }
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
                CalculateCartTotal();
                CalculateSubtotal(_cartTotal, 1.2m);
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
        // Payment Properties
        private decimal _cartTotal;

        public decimal CartTotal
        {
            get { return _cartTotal; }
            set
            {
                _cartTotal = value;
                OnPropertyChanged(nameof(CartTotal));
            }
        }

        private decimal _subtotal;

        public decimal Subtotal
        {
            get { return _subtotal; }
            set
            {
                _subtotal = value;
                OnPropertyChanged(nameof(Subtotal));
            }
        }

        private decimal _tax;

        public decimal Tax
        {
            get { return _tax; }
            set
            {
                _tax = value;
                OnPropertyChanged(nameof(Tax));
            }
        }
        #endregion

        #region Constructor
        public ManualSaleViewModel()
        {
            SaleDate = DateTime.Now.ToString("dd,MM,yyyy");
            CurrentTime = DateTime.UtcNow.ToString("hh:mm:ss");

            AddManualProduct = new AddManualProductCommand(this);
            RemoveFromCart = new RemoveFromCartCommand(this);

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
        private void CalculateSubtotal(decimal total, decimal divideBy)
        {
            _subtotal = total / divideBy;

            Subtotal = Math.Round(_subtotal, 2);
        }
        
        private void CalculateTax()
        {
            _tax = _cartTotal - _subtotal;

            Tax = Math.Round(_tax, 2);
        }

        private void CalculateCartTotal()
        {
            decimal cartTotal = 0m;

            foreach (var item in Cart)
            {
                cartTotal += item.Total;
            }

            _cartTotal = cartTotal;

            CartTotal = Math.Round(_cartTotal, 2);
        }

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
                DepartmentId = Department
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

                CalculateCartTotal();
                CalculateSubtotal(_cartTotal, 1.2m);
                CalculateTax();
            } 
        }

        /// <summary>
        /// Remove Item from Cart
        /// </summary>
        public void RemoveItemFromCart()
        {
            Cart.Remove(SelectedCartItem);
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
