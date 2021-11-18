using DataAccessLibrary.Models;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesktopUI.Stores
{
    // Used for editing and updating SaleProducts
    public class SaleStore
    {
        private SaleDisplayModel _selectedSale;

        public SaleDisplayModel SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                SelectedSaleChanged?.Invoke();
            }
        }

        // Total without the tax Value from NewSaleVM
        public decimal SubTotal { get; set; }

        // Tax Value from NewSaleVM
        public decimal Tax { get; set; }

        // Total Value from NewSaleVM
        public decimal Total { get; set; }

        // TotalCost value from NewSaleVM
        public decimal TotalCost { get; set; }

        // TotalProfit value from NewSaleVM
        public decimal TotalPtofit { get; set; }

        // Cart Value from the NewSaleVM
        public ObservableCollection<CartItemDisplayModel> Cart { get; set; }

        // List of SaleProductModel to save in the database when sale is completed
        public List<SaleProductModel> SaleProductsForDB { get; set; }

        // Event used to trigger when editing sale
        public event Action SelectedSaleChanged;
    }
}
