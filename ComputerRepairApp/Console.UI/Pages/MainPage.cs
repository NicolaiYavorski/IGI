using Autofac;
using EasyConsole;

namespace ConsoleApp.UI.Pages
{
    public class MainPage : MenuPage
    {
        public MainPage(Program program)
            : base("Main page", program,
                  new Option("Work with clients", () => program.NavigateTo<ClientPage>()),
                  new Option("Work with masters", () => program.NavigateTo<MasterPage>()),
                  new Option("Work with orders", () => program.NavigateTo<OrderPage>()))
        {
        }
    }
}
