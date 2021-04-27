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

        #endregion

        #region Methods

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
