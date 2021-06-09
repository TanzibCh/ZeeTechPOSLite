using AutoMapper;
using DesktopUI.Commands.NavigationCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IMainViewModel
    {
        #region Private Properties

        private object _selectedViewModel;
        IBankingViewModel _bankingVM;
        IManualSaleViewModel _manualSaleVM;
        #endregion

        #region Construtors
        public MainViewModel(IBankingViewModel bankingVM, IManualSaleViewModel manualSaleVM)
        {
            _bankingVM = bankingVM;
            _manualSaleVM = manualSaleVM;

            SelectedViewModel = _manualSaleVM;

            OpenManualSales = new OpenManualSalesCommand(this);

            OpenBanking = new OpenBankingCommand(this);
        }
        #endregion

        #region Public Properties
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public OpenManualSalesCommand OpenManualSales { get; set; }

        public OpenBankingCommand OpenBanking { get; set; }

        #endregion

        #region Methods
        public void OpenManualSalesPage()
        {
            SelectedViewModel = _manualSaleVM;
        }

        public void OpenBankingPage()
        {
            SelectedViewModel = _bankingVM;
        }

        #endregion

        #region INotifyPropertyChanged implimentation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
