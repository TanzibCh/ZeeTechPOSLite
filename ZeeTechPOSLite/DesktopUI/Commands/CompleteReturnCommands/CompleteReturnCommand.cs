using DesktopUI.Stores;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Commands.CompleteReturnCommands
{
    public class CompleteReturnCommand : CommandBase
    {
        private readonly ReturnCompleteViewModel _returnCompleteViewModel;
        private readonly CreditStore _creditStore;

        public CompleteReturnCommand(ReturnCompleteViewModel returnCompleteViewModel,
            CreditStore creditStore)
        {
            _returnCompleteViewModel = returnCompleteViewModel;
            _creditStore = creditStore;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}