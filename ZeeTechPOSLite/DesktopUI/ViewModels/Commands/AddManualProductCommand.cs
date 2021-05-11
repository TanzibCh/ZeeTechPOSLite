﻿using DataAccessLibrary.Models;
using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Commands
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
            return true;
        }

        public void Execute(object parameter)
        {
            ManualSaleVM.AddCartItem();
        }
    }
}