using DesktopUI.Services;
using DesktopUI.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.EditSaleCommands
{
    public class RefundCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public RefundCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
