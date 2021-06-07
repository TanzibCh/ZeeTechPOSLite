using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ManualSaleView.xaml
    /// </summary>
    public partial class ManualSaleView : UserControl
    {
        IManualSaleViewModel _manualSaleVM;
        public ManualSaleView(IManualSaleViewModel manualSaleVM)
        {
            _manualSaleVM = manualSaleVM;

            InitializeComponent();
            this.DataContext =_manualSaleVM;
        }
    }
}
