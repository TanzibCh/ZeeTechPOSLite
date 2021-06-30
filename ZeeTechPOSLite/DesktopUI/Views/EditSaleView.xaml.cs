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
using System.Windows.Shapes;

namespace DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for EditSaleView.xaml
    /// </summary>
    public partial class EditSaleView : Window
    {
        public EditSaleView()
        {
            InitializeComponent();
            this.DataContext = new EditSaleViewModel();
        }
    }
}
