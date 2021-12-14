using DesktopUI.Components;
using DesktopUI.Models;
using DesktopUI.Services;
using DesktopUI.Stores;
using DesktopUI.ViewModels;
using DesktopUI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        // Constructor
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            // Stores
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<SaleStore>();
            services.AddSingleton<ProductStore>();
            services.AddSingleton<ReturnStore>();
            services.AddSingleton<LocationStore>();
            services.AddSingleton<ModalNavigationStore>();

            // Main window
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<INavigationService>(s => CreateDashboardNavigationService(_serviceProvider));

            // register ViewModels
            services.AddTransient<DashboardViewModel>(s => new DashboardViewModel(
                s.GetRequiredService<LocationStore>()));

            services.AddTransient<ProductViewModel>(s => new ProductViewModel(
                s.GetRequiredService<LocationStore>()));

            services.AddTransient<ManualSaleViewModel>(s => new ManualSaleViewModel(
                CreateEditProductNavigationService(s),
                CreatePaymentNavigatonService(s),
                s.GetRequiredService<ProductStore>(),
                s.GetRequiredService<LocationStore>(),
                s.GetRequiredService<SaleStore>()));

            services.AddTransient<PaymentVewModel>(s => new PaymentVewModel(
                s.GetRequiredService<SaleStore>(),
                CreateCloseModalNavigationService(s),
                CreatePaymentNavigatonService(s)));

            services.AddTransient<BankingViewModel>(s => new BankingViewModel(
                CreateEditSaleNavigationService(s),
                CreateRefundNavigationService(s),
                s.GetRequiredService<SaleStore>()));

            services.AddTransient<RefundViewModel>(s => new RefundViewModel(
                CreateCloseModalNavigationService(s),
                CreateReturnCompleteNavigationService(s),
                s.GetRequiredService<SaleStore>(),
                s.GetRequiredService<ReturnStore>()));

            services.AddTransient<ReturnCompleteViewModel>(s => new ReturnCompleteViewModel(
                s.GetRequiredService<ReturnStore>(),
                CreateCloseModalNavigationService(s),
                s.GetRequiredService<LocationStore>(),
                CreateCloseAllModalNavigationService(s)));

            services.AddTransient<EditSaleViewModel>(s => new EditSaleViewModel(
                CreateCloseModalNavigationService(s),
                CreateRefundNavigationService(s),
                s.GetRequiredService<SaleStore>()));

            services.AddSingleton<MainView>(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            // Build service provider
            _serviceProvider = services.BuildServiceProvider();

            // Culture setup
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        // Startup Override
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            // Launch Main Window at the start of the app
            MainWindow = _serviceProvider.GetRequiredService<MainView>();
            MainWindow.Show();

            SelectAllTextInTextBox();

            SelectItemInListBox();

            base.OnStartup(e);
        }

        // Navigation Services

        // Dashboard Navigation Service
        private INavigationService CreateDashboardNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<DashboardViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => CreateNavigationBarViewModel(serviceProvider),
                () => serviceProvider.GetRequiredService<DashboardViewModel>());
        }

        // Product Navigation Service
        private INavigationService CreateProductNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ProductViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => CreateNavigationBarViewModel(serviceProvider),
                () => serviceProvider.GetRequiredService<ProductViewModel>());
        }



        // Navigation Bar View Model
        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateManualSaleNavigationService(serviceProvider),
                CreateBankingNavigationService(serviceProvider),
                CreateProductNavigationService(serviceProvider),
                CreateDashboardNavigationService(serviceProvider));
        }

        // New Sale Navigation Service
        private INavigationService CreateManualSaleNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ManualSaleViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => CreateNavigationBarViewModel(serviceProvider),
                () => serviceProvider.GetRequiredService<ManualSaleViewModel>());
        }

        // Banking Navigation Service
        private INavigationService CreateBankingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<BankingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => CreateNavigationBarViewModel(serviceProvider),
                () => new BankingViewModel(CreateEditSaleNavigationService(serviceProvider),
                CreateRefundNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<SaleStore>()));
        }

        // Edit Sale Navigation Service
        private INavigationService CreateEditSaleNavigationService(IServiceProvider serviceProvider)
        {
            CompositeNavigationService closeNavigationService = new CompositeNavigationService(
                CreateCloseModalNavigationService(serviceProvider),
                CreateBankingNavigationService(serviceProvider));

            CompositeNavigationService refundNavigationService = new CompositeNavigationService(
                CreateCloseModalNavigationService(serviceProvider),
                CreateRefundNavigationService(serviceProvider));

            return new ModalNavigationService<EditSaleViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => new EditSaleViewModel(closeNavigationService,
                refundNavigationService,
                serviceProvider.GetRequiredService<SaleStore>()));
        }

        // Edit Product Navigation Service
        private INavigationService CreateEditProductNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<EditSaleProductViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => new EditSaleProductViewModel(CreateCloseModalNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<ProductStore>(),
                serviceProvider.GetRequiredService<ManualSaleViewModel>()));
        }

        // Payment Navigation Service
        private INavigationService CreatePaymentNavigatonService(IServiceProvider serviceProvider)
        {
            CompositeNavigationService completeNavigationService = new CompositeNavigationService(
                CreateCloseModalNavigationService(serviceProvider),
                CreateManualSaleNavigationService(serviceProvider));

            return new ModalNavigationService<PaymentVewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => new PaymentVewModel(serviceProvider.GetRequiredService<SaleStore>(),
                CreateCloseModalNavigationService(serviceProvider),
                completeNavigationService));
        }

        // Refund Navigation Service
        private INavigationService CreateRefundNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<RefundViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => new RefundViewModel(CreateCloseModalNavigationService(serviceProvider),
                CreateReturnCompleteNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<SaleStore>(),
                serviceProvider.GetRequiredService<ReturnStore>()));
        }

        private INavigationService CreateReturnCompleteNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<ReturnCompleteViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => new ReturnCompleteViewModel(
                    serviceProvider.GetRequiredService<ReturnStore>(),
                    CreateCloseModalNavigationService(serviceProvider),
                    serviceProvider.GetRequiredService<LocationStore>(),
                    CreateCloseAllModalNavigationService(serviceProvider)));
        }

        // Close Modal Navigation Service
        private INavigationService CreateCloseModalNavigationService(IServiceProvider serviceProvider)
        {
            ModalNavigationStore modalNavigationStore = serviceProvider.GetRequiredService<ModalNavigationStore>();

            return new CloseModalNavigationService(modalNavigationStore);
        }

        // Close All Modal Navigation Service
        private INavigationService CreateCloseAllModalNavigationService(IServiceProvider serviceProvider)
        {
            ModalNavigationStore modalNavigationStore = serviceProvider.GetRequiredService<ModalNavigationStore>();

            return new CloseAllModalNavigationService(modalNavigationStore);
        }

        // Select the item in a ListBox when clicked on any controles within that item.
        private static void SelectItemInListBox()
        {
            EventManager.RegisterClassHandler(typeof(ListBoxItem), ListBoxItem.PreviewGotKeyboardFocusEvent,
                            new RoutedEventHandler((x, _) => (x as ListBoxItem).IsSelected = true));
        }

        // Select all the text in a TextBox when it receives focus.
        private void SelectAllTextInTextBox()
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent,
                            new MouseButtonEventHandler(SelectivelyIgnoreMouseButton));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent,
                new RoutedEventHandler(SelectAllText));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.MouseDoubleClickEvent,
                new RoutedEventHandler(SelectAllText));
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            // Find the TextBox
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);

            if (parent != null)
            {
                var textBox = (TextBox)parent;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    // If the text box is not yet focused, give it the focus and
                    // stop further processing of this click event.
                    textBox.Focus();
                    e.Handled = true;
                }
            }
        }

        private void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }
    }
}