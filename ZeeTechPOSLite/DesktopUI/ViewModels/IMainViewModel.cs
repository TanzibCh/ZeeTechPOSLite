using DesktopUI.Commands.NavigationCommands;
using System.ComponentModel;

namespace DesktopUI.ViewModels
{
    public interface IMainViewModel
    {
        OpenBankingCommand OpenBanking { get; set; }
        OpenManualSalesCommand OpenManualSales { get; set; }
        object SelectedViewModel { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void OpenBankingPage();
        void OpenManualSalesPage();
    }
}