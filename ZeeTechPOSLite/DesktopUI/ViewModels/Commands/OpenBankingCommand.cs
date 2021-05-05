using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Commands
{
    public class OpenBankingCommand : ICommand
    {
        public MainViewModel MainVM { get; set; }

        public OpenBankingCommand(MainViewModel mainVM)
        {
            MainVM = mainVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainVM.OpenBankingPage();
        }
    }
}
