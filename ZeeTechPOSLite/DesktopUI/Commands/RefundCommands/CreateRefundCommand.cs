using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class CreateRefundCommand : CommandBase
    {
        private readonly RefundViewModel _refundViewModel;
public CreateRefundCommand(RefundViewModel refundViewModel)
        {
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _refundViewModel.CreateRefund();
        }
    }
}
