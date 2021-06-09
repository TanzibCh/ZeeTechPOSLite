using Autofac;
using DataAccessLibrary.DataAccess.SalesQueries;
using DesktopUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DesktopUI
{
    public class ProgramModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SalesDataAccess>().As<ISalesDataAccess>();

            builder.RegisterType<MainView>().AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(DesktopUI)))
                .Where(t => t.Namespace.Contains("ViewModels"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
        }
    }
}
