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


        private decimal _subTotal;

        public decimal SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
            }
        }

        private decimal _tax;

        public decimal Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        private decimal _total;

        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }


        public event Action SelectedSaleChanged;
    }
}
