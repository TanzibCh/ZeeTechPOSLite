using DesktopUI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands
{
    class CloseModalCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public CloseModalCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
