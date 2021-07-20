using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Stores
{
    public class SaleStore
    {
        //public event Action<int> SaleSelected;

        //public void SelectedSaleId(int id)
        //{
        //    SaleSelected?.Invoke(id);
        //}

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
