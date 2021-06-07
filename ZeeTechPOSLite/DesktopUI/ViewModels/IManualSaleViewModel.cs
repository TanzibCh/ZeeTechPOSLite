using DesktopUI.Commands.ManualSaleCommands;
using DesktopUI.Models;
using System.ComponentModel;

namespace DesktopUI.ViewModels
{
    public interface IManualSaleViewModel
    {
        AddManualProductCommand AddManualProduct { get; set; }
        decimal CardPayment { get; set; }
        BindingList<CartItemDisplayModel> Cart { get; set; }
        decimal CartTotal { get; }
        bool CashOnlySale { get; set; }
        decimal CashPayment { get; set; }
        decimal CreditPayment { get; set; }
        string CurrentTime { get; set; }
        string Department { get; set; }
        string DepartmentChecked { get; set; }
        EditCartItemCommand EditCartItem { get; set; }
        bool EnableCard { get; set; }
        bool EnableCash { get; set; }
        bool EnableManualCost { get; set; }
        bool EnableManualName { get; set; }
        int ManualCode { get; set; }
        decimal ManualCost { get; set; }
        string ManualEntryLable { get; set; }
        decimal ManualPrice { get; set; }
        string ManualProductDescription { get; set; }
        string ManualProductName { get; set; }
        int ManualQuantity { get; set; }
        PayCommand Pay { get; set; }
        RemoveFromCartCommand RemoveFromCart { get; set; }
        string SaleDate { get; set; }
        CameraDepartmentCommand SelectCameraDepartment { get; set; }
        ComputerDepartmentCommand SelectComputerDepartment { get; set; }
        CartItemDisplayModel SelectedCartItem { get; set; }
        HomeDepartmentCommand SelectHomeDepartment { get; set; }
        MobileDepartmentCommand SelectMobileDepartment { get; set; }
        RepairDepartmentCommand SelectRepairDepartment { get; set; }
        decimal Subtotal { get; }
        decimal SumPayment { get; set; }
        decimal Tax { get; }

        event PropertyChangedEventHandler PropertyChanged;

        void AddCartManualItem();
        void CompleteSale();
        void EditSelectedCartItem();
        void RemoveItemFromCart();
        void SelectCamera();
        void SelectComputer();
        void SelectHome();
        void SelectMobile();
        void SelectRepair();
    }
}