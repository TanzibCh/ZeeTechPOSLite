using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Services
{
    public class ModalNavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public ModalNavigationService(ModalNavigationStore modalNavigationStore, Func<TViewModel> createViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _modalNavigationStore.Push(_createViewModel());
        }
    }
}