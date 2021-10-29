using DesktopUI.Commands;
using DesktopUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateManualSaleCommand { get; }
        public ICommand NavigateBankingCommand { get; }
        public ICommand NavigateDashboardCommand { get; }

        public NavigationBarViewModel(INavigationService manualSaleNavigationService,
                                      INavigationService bankingNavigationService,
                                      INavigationService dashboardNavigationService)
        {
            NavigateManualSaleCommand = new NavigateCommand(manualSaleNavigationService);
            NavigateBankingCommand = new NavigateCommand(bankingNavigationService);
            NavigateDashboardCommand = new NavigateCommand(dashboardNavigationService);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}