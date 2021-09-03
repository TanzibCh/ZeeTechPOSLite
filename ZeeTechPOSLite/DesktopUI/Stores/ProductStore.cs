using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesktopUI.Stores
{
    public class ProductStore
    {
        // Product Model used in the ManualSale View Model
        private CartItemDisplayModel _selectedSaleProduct;

        public CartItemDisplayModel SelectedSaleProduct
        {
            get { return _selectedSaleProduct; }
            set { _selectedSaleProduct = value; }
        }

        // Edited Cart Item in EditSale View Model
        private CartItemDisplayModel _editedCartItem;

        public CartItemDisplayModel EditedCartItem
        {
            get { return _editedCartItem; }
            set
            {
                _editedCartItem = value;
                EditedCartItemChanged?.Invoke();
            }
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


        public event Action EditedCartItemChanged;
        public event Action EditedSaleProductChanged;
        public event Action CartChanged;
    }
}
