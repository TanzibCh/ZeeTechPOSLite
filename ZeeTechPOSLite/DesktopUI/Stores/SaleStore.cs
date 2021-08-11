using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Stores
{
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

        public event Action SelectedSaleChanged;
    }
}
