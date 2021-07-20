using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.NavigationCommands
{
    public class OpenManualSalesCommand : ICommand
    {
        public MainViewModel MainVM { get; set; }

        public OpenManualSalesCommand(MainViewModel mainVM)
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
            //MainVM.OpenManualSalesPage();
        }


    }
}
