using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class RemoveCommand : CommandBase
    {
        private readonly RefundViewModel _refundViewModel;

        public RemoveCommand(RefundViewModel refundViewModel)
        {
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _refundViewModel.RemoveFromRefundList();
        }
    }
}