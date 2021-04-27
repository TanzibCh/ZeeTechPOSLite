using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
