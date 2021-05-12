using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class RepairCategoryCommand : ICommand
    {
        public ManualSaleViewModel ManualSaleVM { get; set; }

        public RepairCategoryCommand(ManualSaleViewModel manualSaleVM)
        {
            ManualSaleVM = manualSaleVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ManualSaleVM.SelectRepair();
        }
    }
}
