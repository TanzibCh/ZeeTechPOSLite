using DesktopUI.Commands;
using DesktopUI.Commands.EditProductCommands;
using DesktopUI.Helpers;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class EditSaleProductViewModel : ViewModelBase
    {
        #region Private Properties

        private readonly ProductStore _productStore;
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();
        private readonly ManualSaleViewModel _manualSaleVM;

        #endregion

        #region Field Properties

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string _productDescription;

        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string _cost;

        public string Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(_price));
            }
        }


        // Combobox source
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

        #endregion

        #region Command Properties

        public ICommand UpdateChangesCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand UndoChanges { get; }

        #endregion

        #region Constructors

        public EditSaleProductViewModel(INavigationService closeModalNavigationService,
            ProductStore productStore, ManualSaleViewModel manualSaleVM)
        {
            _productStore = productStore;

            Departments = new ObservableCollection<DepartmantModel>()
            {
                new DepartmantModel(){ DepartmentName = "Mobile"},
                new DepartmantModel(){ DepartmentName = "Computer"},
                new DepartmantModel(){ DepartmentName = "Camera"},
                new DepartmantModel(){ DepartmentName = "Home"},
                new DepartmantModel(){ DepartmentName = "AV"},
                new DepartmantModel(){ DepartmentName = "Repair"}
            };

            // Commands
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
            UpdateChangesCommand = new UpdateEditedProductCommand(this, closeModalNavigationService);

            LoadProduct();
            _manualSaleVM = manualSaleVM;
        }

        #endregion

        #region Methods

        private void LoadProduct()
        {
            ProductName = _productStore.SelectedSaleProduct.Product.ProductName;
            ProductDescription = _productStore.SelectedSaleProduct.Product.ProductDescription;
            Quantity = _productStore.SelectedSaleProduct.Quantity;
            Cost = _productStore.SelectedSaleProduct.Product.ProductCost;
            Price = _productStore.SelectedSaleProduct.Product.SalePrice;

            SelectedDepartment = Departments.FirstOrDefault(
                s => s.DepartmentName == _productStore.SelectedSaleProduct.Product.Department);
        }

        public void UpdateChanges()
        {
            SaleProductDisplayModel product = CreateManualProduct();

            decimal total = Quantity * _cHelper.ConvertStringToDecimal(Price);

            CartItemDisplayModel item = new CartItemDisplayModel
            {
                Product = product,
                Quantity = Quantity,
                Price = Price,
                Total = _cHelper.ConvertDecimalToString(total)
            };

            _productStore.EditedCartItem = item;
        }

        private SaleProductDisplayModel CreateManualProduct()
        {
            SaleProductDisplayModel product = new SaleProductDisplayModel
            {
                Id = -1,
                ProductName = ProductName,
                ProductDescription = ProductDescription,
                ProductCost = Cost,
                SalePrice = Price,
                Department = SelectedDepartment.DepartmentName
            };
            return product;
        }

        public void UndoChangesMade()
        {

        }
        #endregion
    }
}
