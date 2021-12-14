using DesktopUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Commands.ProductCommands
{
    internal class CreateProductCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public CreateProductCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
