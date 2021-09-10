using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.RefundCommands
{
    public class UndoChangesCommand : CommandBase
    {
        private readonly RefundViewModel _refundViewModel;

        public UndoChangesCommand(RefundViewModel refundViewModel)
        {
            _refundViewModel = refundViewModel;
        }

        public override void Execute(object parameter)
        {
            _refundViewModel.UndoChanges();
        }
    }
}
