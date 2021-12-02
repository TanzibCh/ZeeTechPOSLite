using DesktopUI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.PaymentCompleteCommands
{
    public class CancelCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public CancelCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
