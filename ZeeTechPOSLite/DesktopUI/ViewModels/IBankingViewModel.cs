using DataAccessLibrary.Models;
using DesktopUI.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DesktopUI.ViewModels
{
    public interface IBankingViewModel
    {
        int AVCount { get; set; }
        decimal AVTotal { get; set; }
        int CameraCount { get; set; }
        decimal CameraTotal { get; set; }
        int ComputerCount { get; set; }
        decimal ComputerTotal { get; set; }
        ObservableCollection<ExpenseModel> Expenses { get; set; }
        int HomeCount { get; set; }
        decimal HomeTotal { get; set; }
        int MobileCount { get; set; }
        decimal MobileTotal { get; set; }
        int RepairCount { get; set; }
        decimal RepairTotal { get; set; }
        BindingList<SaleProductModel> SaleProducts { get; set; }
        BindingList<SaleDisplayModel> Sales { get; set; }
        DateTime SelectedDate { get; set; }
        decimal Total { get; set; }
        decimal TotalCard { get; set; }
        decimal TotalCash { get; set; }
        decimal TotalCashOnly { get; set; }
        decimal TotalCredit { get; set; }
        decimal TotalExpense { get; set; }
        decimal TotalProfit { get; set; }
        decimal TotalRefund { get; set; }
        decimal TotalTillCash { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void AddExpense();
        void EditExpense();
        void EditSale();
        void GetAllSaleProducts();
        void NextDay();
        void PreviousDay();
        void RemoveExpense();
        void ShowCashOnly();
    }
}