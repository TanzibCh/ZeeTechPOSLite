using DataAccessLibrary.Models;
using DesktopUI.Models;
using DesktopUI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class ManualSaleViewModel : INotifyPropertyChanged, IManualSaleViewModel
    {
        #region Private properties
        private int _manualCode;
        private decimal _manualCost;
        private string _manualProductName;
        private string _manualProductDescription;
        private int _manualQuantity;
        private decimal _manualPrice;
        private int _department;

        private BindingList<CartItemDisplayModel> _cart;
        #endregion

        #region Public properties
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

        public int Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
            }
        }

        public string SaleDate { get; set; }
        public string CurrentTime { get; set; }

        public AddManualProductCommand AddManualProduct { get; set; }

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set 
            { 
                _cart = value;
                OnPropertyChanged(nameof(Cart));
            }
        }


        #endregion

        #region Constructor
        public ManualSaleViewModel()
        {
            SaleDate = DateTime.Now.ToString("dd,MM,yyyy");
            CurrentTime = DateTime.UtcNow.ToString("hh:mm:ss");

            AddManualProduct = new AddManualProductCommand(this);
            Cart = new BindingList<CartItemDisplayModel>();
        }
        #endregion

        #region Methods
        private void ConvertCodeToCost()
        {
            int devideBy = 6;
            decimal cost;

            cost = (decimal)ManualCode / (decimal)devideBy;
            ManualCost = Math.Round(cost, 2);
        }

        private ProductModel CreateProduct()
        {
            ProductModel product = new ProductModel
            {
                Id = 0,
                ProductName = ManualProductName,
                ProductDescription = ManualProductDescription,
                AverageCost = ManualCost,
                Price = ManualPrice,
                DepartmentId = Department
            };
            return product;
        }

        public void AddCartItem()
        {
            ProductModel product = CreateProduct();

            decimal total = ManualQuantity * ManualPrice;

            CartItemDisplayModel item = new CartItemDisplayModel
            {
                Product = product,
                Quantity = ManualQuantity,
                Price = ManualPrice,
                Total = total
            };

            Cart.Add(item);
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
