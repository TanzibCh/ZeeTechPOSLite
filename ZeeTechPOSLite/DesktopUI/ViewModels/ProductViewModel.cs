using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        #region Field Properties

        private ObservableCollection<ProductDisplayModel> _products;

        // List to load the products
        public ObservableCollection<ProductDisplayModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private string _Productsearch;

        // Field for searching priducts
        public string ProductSearch
        {
            get { return _Productsearch; }
            set
            {
                _Productsearch = value;
                OnPropertyChanged(nameof(ProductSearch));
            }
        }

        private string _filterDepartment;

        // Deparment filter
        public string FilterDepartment
        {
            get { return _filterDepartment; }
            set
            {
                _filterDepartment = value;
                OnPropertyChanged(nameof(FilterDepartment));
            }
        }

        private bool _sortCost;

        // sort by cost
        public bool SortCost
        {
            get { return _sortCost; }
            set
            {
                _sortCost = value;
                OnPropertyChanged(nameof(SortCost));
            }
        }

        private bool _sortPrice;

        // sort by price
        public bool SortPrice
        {
            get { return _sortPrice; }
            set
            {
                _sortPrice = value;
                OnPropertyChanged(nameof(SortPrice));
            }
        }

        private bool _sortName;

        // sort by name
        public bool SortName
        {
            get { return _sortName; }
            set
            {
                _sortName = value;
                OnPropertyChanged(nameof(SortName));
            }
        }

        #endregion Field Properties
    }
}