<<<<<<< HEAD
﻿using DesktopUI.Models;
=======
﻿
using Autofac;
>>>>>>> 3afcf790f0db64ae09d8432f50771d2732bf9625
using DesktopUI.ViewModels;
using DesktopUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;

        // Constructor
        public App()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

<<<<<<< HEAD
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection servicees)
        {
            servicees.AddSingleton<MainView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainView = _host.Services.GetRequiredService<MainView>();
=======
        protected override void OnStartup(StartupEventArgs e)
        {
<<<<<<< HEAD
            //services.AddAutoMapper(typeof(App));


            //var host = Host.CreateDefaultBuilder()
            //    .ConfigureServices((context, services) =>
            //    {
            //        services.AddSingleton<MainView>();

=======
            // Dependency Injection setup
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ProgramModule>();
            var container = containerBuilder.Build();

            var mainView = container.Resolve<MainView>();
>>>>>>> 3afcf790f0db64ae09d8432f50771d2732bf9625
            mainView.Show();


            //var host = Host.CreateDefaultBuilder()
            //    .ConfigureServices((context, services) =>
            //    {
            //        services.AddSingleton<MainView>();

>>>>>>> b6ea7cdd0281ab171603194bf5a36a2b8338070d
            //        // VewModels
            //        services.AddTransient<IMainViewModel, MainViewModel>();
            //        services.AddTransient<IBankingViewModel, BankingViewModel>();
            //    })
            //    .Build();

            // Launch Main Window at the start of the app
            //var mainView = host.Services.GetRequiredService<MainView>();
            //mainView.Show();

            // Select all the text in a TextBox when it receives focus.
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent,
                new MouseButtonEventHandler(SelectivelyIgnoreMouseButton));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent,
                new RoutedEventHandler(SelectAllText));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.MouseDoubleClickEvent,
                new RoutedEventHandler(SelectAllText));

            // Select the item in a ListBox when clicked on any controles within that item.
            EventManager.RegisterClassHandler(typeof(ListBoxItem), ListBoxItem.PreviewGotKeyboardFocusEvent,
                new RoutedEventHandler((x, _) => (x as ListBoxItem).IsSelected = true));

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }

        void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
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

        void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }
    }
}
