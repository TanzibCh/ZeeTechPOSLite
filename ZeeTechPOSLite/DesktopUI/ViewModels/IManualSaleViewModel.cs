using DesktopUI.Models;
using System.ComponentModel;

namespace DesktopUI.ViewModels
{
    public interface IManualSaleViewModel
    {
        BindingList<CartItemDisplayModel> Cart { get; set; }
        string CurrentTime { get; set; }
        int Department { get; set; }
        int ManualCode { get; set; }
        decimal ManualCost { get; set; }
        decimal ManualPrice { get; set; }
        string ManualProductDescription { get; set; }
        string ManualProductName { get; set; }
        int ManualQuantity { get; set; }
        string SaleDate { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}