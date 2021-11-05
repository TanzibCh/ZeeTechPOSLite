using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class PaymentVewModel : ViewModelBase
    {
        #region Private Properties

        private readonly SaleStore _saleStore;

        #endregion

        #region Sale Info properties


        private decimal _subTotal;

        public decimal SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                OnPropertyChanged(nameof(SubTotal));
            }
        }


        private decimal _tax;

        public decimal Tax
        {
            get { return _tax; }
            set
            {
                _tax = value;
                OnPropertyChanged(nameof(Tax));
            }
        }


        private decimal _total;

        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
        #endregion

        #region Payment Properties


        private decimal _cardPayment;

        public decimal CardPayment
        {
            get { return _cardPayment; }
            set
            {
                _cardPayment = value;
                OnPropertyChanged(nameof(CardPayment));
            }
        }


        private decimal _cashPayment;

        public decimal CashPayment
        {
            get { return _cashPayment; }
            set
            {
                _cashPayment = value;
                OnPropertyChanged(nameof(CashPayment));
            }
        }


        private decimal _creditPayment;

        public decimal CreditPayment
        {
            get { return _creditPayment; }
            set
            {
                _creditPayment = value;
                OnPropertyChanged(nameof(CreditPayment));
            }
        }
        #endregion

        #region Constructor

        public PaymentVewModel(SaleStore saleStore)
        {
            _saleStore = saleStore;

            LoadSaleInformation();
        }
        #endregion

        #region Methods

        private void LoadSaleInformation()
        {
            SubTotal = _saleStore.SubTotal;
            Tax = _saleStore.Tax;
            Total = _saleStore.Total;
        }
        #endregion
    }
}
