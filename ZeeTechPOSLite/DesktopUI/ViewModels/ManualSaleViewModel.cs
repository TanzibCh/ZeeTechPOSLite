using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    class ManualSaleViewModel : INotifyPropertyChanged
    {
        #region Private properties

        private string _currentTime;

        #endregion

        #region Public properties
        public DateTime SaleDate { get; set; }

        public string CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; }
        }

        #endregion

        #region Constructor
        public ManualSaleViewModel()
        {
            SaleDate = DateTime.Now;
            CurrentTime = DateTime.UtcNow.ToString();
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
