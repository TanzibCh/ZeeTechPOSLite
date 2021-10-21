using System;
using System.Collections.Generic;
using System.Text;
using DesktopUI.Stores;

namespace DesktopUI.Services
{
    public class CloseAllModalNavigationService : INavigationService
    {
        private readonly ModalNavigationStore _navigationStore;

        public CloseAllModalNavigationService(ModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.Clear();
        }
    }
}
