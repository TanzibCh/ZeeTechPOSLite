using DesktopUI.Commands;
using DesktopUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class EditSaleProductViewModel : ViewModelBase
    {
        #region Input Properties

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string _productDescription;

        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }

        #endregion

        #region Command Properties

        public ICommand UpdateProductCommand { get; }

        public ICommand CloseCommand { get; }

        #endregion

        public EditSaleProductViewModel(INavigationService closeModalNavigationService)
        {
            CloseCommand = new CloseModalCommand(closeModalNavigationService);
        }
    }
}
