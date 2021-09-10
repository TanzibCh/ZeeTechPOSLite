using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class GetProductInfoCommand : CommandBase
    {
        private readonly RefundViewModel _refundViewModel;

        public GetProductInfoCommand(RefundViewModel refundViewModel)
        {
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _refundViewModel.SelectSaleProduct();
        }
    }
}
