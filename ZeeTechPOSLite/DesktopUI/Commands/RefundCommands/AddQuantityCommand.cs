using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class AddQuantityCommand : CommandBase
    {
        private readonly RefundViewModel _refundViewModel;

        public AddQuantityCommand(RefundViewModel refundViewModel)
        {
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _refundViewModel.AddQuantity();
        }
    }
}
