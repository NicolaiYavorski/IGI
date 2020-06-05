using Microsoft.Extensions.Configuration;
using System;
using Autofac;
using ConsoleApp.UI.Utils;
using BusinessLogic.Interfaces;
using ConsoleApp.UI.Pages;

namespace ConsoleApp.UI
{
    public class Program : EasyConsole.Program
    {
        public Program()
            : base("Computer Repair", breadcrumbHeader: true)
        {
            var container = ServiceRegistration();

            AddPage(new MainPage(this));
            AddPage(new ClientPage(this, container.Resolve<IClientService>()));
            AddPage(new MasterPage(this, container.Resolve<IMasterService>()));
            AddPage(new OrderPage(this, container.Resolve<IOrderService>()));

            SetPage<MainPage>();
        }

        public static IContainer ServiceRegistration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("ComputerConnection");
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConsoleUiModule(connectionString));
            return builder.Build();
        }
    }
}
