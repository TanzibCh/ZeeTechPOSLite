using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    class ManualSaleViewModel : INotifyPropertyChanged
    {
        #region Private properties

        

        #endregion

        #region Public properties

        public string SaleDate { get; set; }

        public string CurrentTime { get; set; }

        #endregion

        #region Constructor
        public ManualSaleViewModel()
        {
            SaleDate = DateTime.Now.ToString("dd,MM,yyyy");
            CurrentTime = DateTime.UtcNow.ToString("hh:mm:ss");
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
