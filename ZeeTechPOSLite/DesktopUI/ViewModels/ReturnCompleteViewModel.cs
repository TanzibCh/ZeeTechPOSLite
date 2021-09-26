using DesktopUI.Commands;
using DesktopUI.Commands.CompleteReturnCommands;
using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class ReturnCompleteViewModel : ViewModelBase
    {
        #region Private properties

        private readonly ReturnStore _returnStore;

        #endregion Private properties

        #region Field Properties

        public int SaleId { get; }

        private string _creditNote;

        public string CreditNote
        {
            get { return _creditNote; }
            set
            {
                _creditNote = value;
                OnPropertyChanged(nameof(CreditNote));
            }
        }

        private DateTime _ValidTill;

        public DateTime ValidTill
        {
            get { return _ValidTill; }
            set
            {
                _ValidTill = value;
                OnPropertyChanged(nameof(ValidTill));
            }
        }

        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private string _refundNote;

        public string RefundNote
        {
            get { return _refundNote; }
            set
            {
                _refundNote = value;
                OnPropertyChanged(nameof(RefundNote));
            }
        }

        private bool _card;

        public bool Card
        {
            get { return _card; }
            set
            {
                _card = value;
                OnPropertyChanged(nameof(Card));
            }
        }

        private bool _cash;

        public bool Cash
        {
            get { return _cash; }
            set
            {
                _cash = value;
                OnPropertyChanged(nameof(Cash));
            }
        }

        private bool _creditIsExpanded;

        public bool CreditIsExpanded
        {
            get { return _creditIsExpanded; }
            set
            {
                _creditIsExpanded = value;
                OnPropertyChanged(nameof(CreditIsExpanded));
                ToggleCredit();
                CheckCreditOrRfund();
            }
        }

        private bool _refundIsExpanded;

        public bool RefundIsExpanded
        {
            get { return _refundIsExpanded; }
            set
            {
                _refundIsExpanded = value;
                OnPropertyChanged(nameof(RefundIsExpanded));
                ToggleRefund();
                CheckCreditOrRfund();
            }
        }

        private string _completeLable;

        public string CompleteLable
        {
            get { return _completeLable; }
            set
            {
                _completeLable = value;
                OnPropertyChanged(nameof(CompleteLable));
            }
        }

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        #endregion Field Properties

        #region Commands

        public ICommand CompleteCommand { get; }
        public ICommand CloseCommand { get; }

        #endregion Commands

        #region Constructor

        public ReturnCompleteViewModel(ReturnStore returnStore,
            INavigationService closeModalNavigationService)
        {
            _returnStore = returnStore;

            // Set Credit as the default option
            CreditIsExpanded = true;
            RefundIsExpanded = false;

            // Fill in the information from the return store
            Amount = _returnStore.Amount;
            SaleId = _returnStore.SaleId;
            ValidTill = DateTime.Now.AddDays(30);

            // Commads
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
            CompleteCommand = new CompleteReturnCommand(this, returnStore);
        }

        #endregion Constructor

        #region Methods

        // Method to opan close Credit section and close
        private void ToggleCredit()
        {
            if (CreditIsExpanded == true)
            {
                if (RefundIsExpanded == true)
                {
                    RefundIsExpanded = false;
                }
            }
            else
            {
                if (RefundIsExpanded == false)
                {
                    RefundIsExpanded = true;
                }
            }
        }

        // Method to open and close the Refund section
        private void ToggleRefund()
        {
            if (RefundIsExpanded == true)
            {
                if (CreditIsExpanded == true)
                {
                    CreditIsExpanded = false;
                }
            }
            else
            {
                if (CreditIsExpanded == false)
                {
                    CreditIsExpanded = true;
                }
            }
        }

        // Changes the text in the Complete button depending on
        // Credit or Refund selection
        private void CheckCreditOrRfund()
        {
            if (CreditIsExpanded == true && RefundIsExpanded == false)
            {
                CompleteLable = "Create Credit";
            }
            else if (RefundIsExpanded == true && CreditIsExpanded == false)
            {
                CompleteLable = "Create Refund";
            }
        }

        private void CompleteReturn()
        {
        }

        #endregion Methods
    }
}