using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Private Properties

        private readonly LocationStore _locationStore;
        #endregion

        #region Constructor

        public DashboardViewModel(LocationStore locationStore)
        {
            _locationStore = locationStore;


            // set location to 1
            _locationStore.Id = 1;
        }
        #endregion
    }
}
