using DataAccessLibrary.Models;
using DesktopUI.Models;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.Commands.ManualSaleCommands
{
    public class AddManualProductCommand : ICommand
    {
        public ManualSaleViewModel ManualSaleVM { get; set; }

        public AddManualProductCommand(ManualSaleViewModel manualSaleVM)
        {
            ManualSaleVM = manualSaleVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (parameter != null)
            //{
            //    bool canAdd = (bool)parameter;

            //    if (canAdd == true)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }

        public void Execute(object parameter)
        {
            ManualSaleVM.AddCartManualItem();
        }
    }
}
