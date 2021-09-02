using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesktopUI.Stores
{
    public class ProductStore
    {
        private ObservableCollection<CartItemDisplayModel> _cart;

        public ObservableCollection<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set { _cart = value; }
        }


        private SaleProductDisplayModel _selectedSaleProduct;

        public SaleProductDisplayModel SelectedSaleProduct
        {
            get { return _selectedSaleProduct; }
            set { _selectedSaleProduct = value; }
        }

        private SaleProductDisplayModel _editedSaleProduct;

        public SaleProductDisplayModel EditedSaleProduct
        {
            get { return _editedSaleProduct; }
            set
            {
                _editedSaleProduct = value;
                EditedSaleProductChanged?.Invoke();
            }
        }

        public Action EditedSaleProductChanged;
    }
}
