using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Services
{
    public class ModalParameterNavigationService<TParameter, TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _navigationStore;
        private readonly Func<TParameter, TViewModel> _createViewModel;

        public ModalParameterNavigationService(ModalNavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            //_navigationStore.CurrentViewModel = _createViewModel(parameter);
        }
    }
}