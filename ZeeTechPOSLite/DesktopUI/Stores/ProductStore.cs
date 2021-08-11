using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Stores
{
    public class ProductStore
    {
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
