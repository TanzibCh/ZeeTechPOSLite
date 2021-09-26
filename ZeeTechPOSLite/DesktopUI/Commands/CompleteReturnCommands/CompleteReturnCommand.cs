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
        private readonly ReturnStore _returnStore;

        public CompleteReturnCommand(ReturnCompleteViewModel returnCompleteViewModel,
            ReturnStore returnStore)
        {
            _returnCompleteViewModel = returnCompleteViewModel;
            _returnStore = returnStore;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}