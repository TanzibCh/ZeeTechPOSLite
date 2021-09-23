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

        private readonly CreditStore _creditStore;

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
        }

        private bool _refundIsExpanded;

        public bool RefundIsExpanded
        {
            get { return _refundIsExpanded; }
            set
            {
                _refundIsExpanded = value;
                OnPropertyChanged(nameof(RefundIsExpanded));
            }
        }

        #endregion Field Properties

        #region Commands

        public ICommand CompleteCommand { get; }
        public ICommand CloseCommand { get; }

        #endregion Commands

        #region Constructor

        public ReturnCompleteViewModel(CreditStore creditStore,
            INavigationService closeModalNavigationService)
        {
            _creditStore = creditStore;

            CreditIsExpanded = true;
            RefundIsExpanded = false;
            Amount = _creditStore.Amount;
            SaleId = _creditStore.SaleId;

            // Commads
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
            CompleteCommand = new CompleteReturnCommand(this, creditStore);
        }

        #endregion Constructor

        #region Methods

        private void CompleteReturn()
        {
        }

        #endregion Methods
    }
}