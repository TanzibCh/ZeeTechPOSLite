using AutoMapper;
using DesktopUI.Commands.NavigationCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Private Properties

        private object _selectedViewModel;

        #endregion

        #region Construtors
        public MainViewModel()
        {
            SelectedViewModel = new ManualSaleViewModel();

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
            SelectedViewModel = new ManualSaleViewModel();
        }

        public void OpenBankingPage()
        {
            SelectedViewModel = new BankingViewModel();
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
