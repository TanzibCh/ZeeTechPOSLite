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

        public NavigationBarViewModel(INavigationService manualSaleNavigationService,
                                      INavigationService bankingNavigationService)
        {
            NavigateManualSaleCommand = new NavigateCommand(manualSaleNavigationService);
            NavigateBankingCommand = new NavigateCommand(bankingNavigationService);
        }
    }
}
