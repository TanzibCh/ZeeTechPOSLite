using DataAccessLibrary.DataAccess.Queries;
using DataAccessLibrary.Models;
using DesktopUI.Commands.ProductCommands;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly LocationStore _locationStore;
        private readonly ProductData _productData = new ProductData(); 
        private ProductSearchModel _selectedSearchedProduct;


        //#region Field Properties

        //private ObservableCollection<ProductDisplayModel> _products;

        //// List to load the products
        //public ObservableCollection<ProductDisplayModel> Products
        //{
        //    get { return _products; }
        //    set
        //    {
        //        _products = value;
        //        OnPropertyChanged(nameof(Products));
        //    }
        //}

        //private string _Productsearch;

        //// Field for searching priducts
        //public string ProductSearch
        //{
        //    get { return _Productsearch; }
        //    set
        //    {
        //        _Productsearch = value;
        //        OnPropertyChanged(nameof(ProductSearch));
        //    }
        //}

        //private string _filterDepartment;

        //// Deparment filter
        //public string FilterDepartment
        //{
        //    get { return _filterDepartment; }
        //    set
        //    {
        //        _filterDepartment = value;
        //        OnPropertyChanged(nameof(FilterDepartment));
        //    }
        //}

        //private bool _sortCost;

        //// sort by cost
        //public bool SortCost
        //{
        //    get { return _sortCost; }
        //    set
        //    {
        //        _sortCost = value;
        //        OnPropertyChanged(nameof(SortCost));
        //    }
        //}

        //private bool _sortPrice;

        //// sort by price
        //public bool SortPrice
        //{
        //    get { return _sortPrice; }
        //    set
        //    {
        //        _sortPrice = value;
        //        OnPropertyChanged(nameof(SortPrice));
        //    }
        //}

        //private bool _sortName;

        //// sort by name
        //public bool SortName
        //{
        //    get { return _sortName; }
        //    set
        //    {
        //        _sortName = value;
        //        OnPropertyChanged(nameof(SortName));
        //    }
        //}

        //#endregion Field Properties

        #region Product Search Properties

        private ObservableCollection<ProductSearchModel> _searchProducts;

        public ObservableCollection<ProductSearchModel> SearchedProducts
        {
            get { return _searchProducts; }
            set
            {
                _searchProducts = value;
                OnPropertyChanged(nameof(SearchedProducts));
            }
        }

        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
            }
        }


        private string _searchBarcode;

        public string SearchBarcode
        {
            get { return _searchBarcode; }
            set
            {
                _searchBarcode = value;
                OnPropertyChanged(nameof(SearchBarcode));
            }
        }


       

        public ProductSearchModel SelectedSearchedProduct
        {
            get { return _selectedSearchedProduct; }
            set
            {
                _selectedSearchedProduct = value;
                OnPropertyChanged(nameof(SelectedSearchedProduct));
            }
        }
        #endregion

        #region Commands

        public ICommand SearchNameCommand { get; }
        public ICommand SearchBarcodeCommand { get; }
        public ICommand CreateProductCommand { get; }

        #endregion

        #region Constructor

        public ProductViewModel(LocationStore locationStore, INavigationService CreateProductNavigationService)
        {
            _locationStore = locationStore;

            GetAllProducts();

            SearchNameCommand = new SearchNameCommand(this, locationStore);
            SearchBarcodeCommand = new SearchBarcodeCommand(this, locationStore);
            CreateProductCommand = new CreateProductCommand(CreateProductNavigationService);
        }
        #endregion

        #region Methods

        public void GetAllProducts()
        {
            List<ProductSearchModel> allProducts = _productData.GetAllProducts(_locationStore.Id);
            ObservableCollection<ProductSearchModel> products = new ObservableCollection<ProductSearchModel>();


            foreach (ProductSearchModel product in allProducts)
            {
                products.Add(product);
            }

            SearchedProducts = products;
        }
        #endregion
    }
}