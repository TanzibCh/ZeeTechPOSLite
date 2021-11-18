using DesktopUI.Commands.PaymentCompleteCommands;
using DesktopUI.Helpers;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class PaymentVewModel : ViewModelBase
    {
        #region Private Properties

        private readonly SaleStore _saleStore;
        private readonly CurrencyHelper _cHelper = new CurrencyHelper();

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
                CalculateChange();
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
                CalculateChange();
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
                CalculateChange();
                InsertBalanceWithCredit();
            }
        }


        private bool _cardPaymentIsEnabled;

        public bool CardPaymentIsEnabled
        {
            get { return _cardPaymentIsEnabled; }
            set
            {
                _cardPaymentIsEnabled = value;
                OnPropertyChanged(nameof(CardPaymentIsEnabled));
            }
        }


        private bool _cashPaymentIsEnabled;

        public bool CashPaymentIsEnabled
        {
            get { return _cashPaymentIsEnabled; }
            set
            {
                _cashPaymentIsEnabled = value;
                OnPropertyChanged(nameof(CashPaymentIsEnabled));
            }
        }


        private bool _creditPaymentIsEnabled;

        public bool CreditPaymentIsEnabled
        {
            get { return _creditPaymentIsEnabled; }
            set
            {
                _creditPaymentIsEnabled = value;
                OnPropertyChanged(nameof(CreditPaymentIsEnabled));
            }
        }


        private decimal _cashChange;

        public decimal CashChange
        {
            get { return _cashChange; }
            set
            {
                _cashChange = value;
                OnPropertyChanged(nameof(CashChange));
            }
        }

        // Used for storing the total of all the entered payment methods
        // Like total of Card, Cash and Credit
        public decimal PaymentSum { get; set; }


        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        #endregion

        #region Command Properties

        // Used for checking if the Card Radio button is selected
        private bool _cardIsChecked;

        public bool CardIsChecked
        {
            get { return _cardIsChecked; }
            set
            {
                _cardIsChecked = value;
                OnPropertyChanged(nameof(CardIsChecked));

                if (CardIsChecked == true)
                {
                    CardPaymentIsEnabled = true;
                    CashPaymentIsEnabled = false;
                    CreditPaymentIsEnabled = true;
                }

                SetDefaultPayments();
            }
        }

        // Used for checking if the Card Cash radio button is selected
        private bool _cardCashIsChecked;

        public bool CardCashIsChecked
        {
            get { return _cardCashIsChecked; }
            set
            {
                _cardCashIsChecked = value;
                OnPropertyChanged(nameof(CardCashIsChecked));

                if (CardCashIsChecked == true)
                {
                    CardPaymentIsEnabled = true;
                    CashPaymentIsEnabled = true;
                    CreditPaymentIsEnabled = true;
                }

                SetDefaultPayments();
            }
        }

        // Used for checking if Cash only radio button is selected
        private bool _cashOnlyChecked;

        public bool CashOnlyIsChecked
        {
            get { return _cashOnlyChecked; }
            set
            {
                _cashOnlyChecked = value;
                OnPropertyChanged(nameof(CashOnlyIsChecked));

                if (CashOnlyIsChecked == true)
                {
                    CardPaymentIsEnabled = false;
                    CashPaymentIsEnabled = true;
                    CreditPaymentIsEnabled = false;
                }

                SetDefaultPayments();
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        #endregion

        #region Constructor

        public PaymentVewModel(SaleStore saleStore,
            INavigationService CloseModalNavigationService,
            INavigationService completeNavigationService)
        {
            _saleStore = saleStore;

            // Comamnds
            CancelCommand = new CancelCommand(CloseModalNavigationService);
            CompleteCommand = new CompleteCommand(this, _saleStore, completeNavigationService);

            // by default activate Card as payment method
            CardIsChecked = true;
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

        private void SetDefaultPayments()
        {
            if (CardIsChecked == true)
            {
                CardPayment = decimal.Round(_saleStore.Total, 2);
                CashPayment = 0m;
                CreditPayment = 0m;
                ErrorMessage = null;
            }
            else if (CardCashIsChecked == true)
            {
                CardPayment = 0m;
                CashPayment = 0m;
                CreditPayment = 0m;
                ErrorMessage = null;
            }
            else if (CashOnlyIsChecked == true)
            {
                CardPayment = 0m;
                CashPayment = _saleStore.Total;
                CreditPayment = 0m;
                ErrorMessage = null;
            }
        }

        private void CalculateChange()
        {
            if (CardIsChecked == true)
            {
                CashChange = decimal.Round(0m, 2);

                ErrorMessage = null;
            }
            else if (CardCashIsChecked == true)
            {
                // Total amount the customer paid including card, cash and credit
                PaymentSum = CardPayment + CashPayment + CreditPayment;

                if (CashPayment != 0m && PaymentSum >= Total)
                {
                    CashChange = PaymentSum - Total;

                    ErrorMessage = null;
                }
            }
            else if (CashOnlyIsChecked == true)
            {
                // check if cash payment is more than or equal to total
                if (CashPayment >= Total)
                {
                    CashChange = CashPayment - Total;
                    ErrorMessage = null;
                }
            }
        }

        /// <summary>
        /// When Card is selected and 
        /// a  value more than 0 is entered in the Credit field
        /// the diffence of the Total and the Credit is entered in Card
        /// </summary>
        private void InsertBalanceWithCredit()
        {
            if (CardIsChecked && CreditPayment != 0m)
            {
                CardPayment = Total - CreditPayment;
            }
        }
        #endregion
    }
}
