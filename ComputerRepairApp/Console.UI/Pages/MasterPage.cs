using Autofac;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using EasyConsole;
using System;

namespace ConsoleApp.UI.Pages
{
    public class MasterPage : Page
    {
        private readonly IMasterService _masterService;
        public MasterPage(Program program, IMasterService masterService)
            : base("Master page", program)
        {
            _masterService = masterService;
        }

        public override void Display()
        {
            base.Display();

            var menu = new Menu()
                .Add("All masters", () => ShowAll())
                .Add("Get master by id", () => GetById())
                .Add("Create master", () => Create())
                .Add("Update master", () => Update())
                .Add("Delete master", () => Delete())
                .Add("Back to menu", () => Program.NavigateBack());

            menu.Display();
        }

        public void ShowAll()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "All masters");
            foreach (var client in _masterService.GetAll())
            {
                Output.WriteLine(ConsoleColor.Gray, "-------------");
                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, client.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, client.Surname);
                Console.Write("Phone: ");
                Output.WriteLine(ConsoleColor.Green, client.Phone);
                Console.Write("Passport id: ");
                Output.WriteLine(ConsoleColor.Green, client.PassportId);
            }

            Console.ReadKey();
            Program.NavigateTo<MasterPage>();
        }

        public void Create()
        {
            Console.Clear();
            var master = new MasterDto();
            Output.WriteLine(ConsoleColor.Cyan, "Create master");

            try
            {
                Console.Write("First name: ");
                master.FirstName = Console.ReadLine();
                Console.Write("Surname: ");
                master.Surname = Console.ReadLine();
                Console.Write($"Phone: ");
                master.Phone = Console.ReadLine();
                Console.Write($"Passport id: ");
                master.PassportId = Console.ReadLine();

                _masterService.Add(master);
                Output.WriteLine(ConsoleColor.Green, "Creation was successful");
                Console.ReadKey();
                Program.NavigateTo<MasterPage>();
            }
            catch (ArgumentNullException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void Update()
        {
            Console.Clear();
            var master = new MasterDto();
            Output.WriteLine(ConsoleColor.Cyan, "Update master");

            try
            {
                Console.Write("Master id: ");
                master.Id = int.Parse(Console.ReadLine());
                Console.Write("First name: ");
                master.FirstName = Console.ReadLine();
                Console.Write("Surname: ");
                master.Surname = Console.ReadLine();
                Console.Write("Middle name: ");
                master.Phone = Console.ReadLine();
                Console.Write("Passport id: ");
                master.PassportId = Console.ReadLine();

                _masterService.Update(master);
                Output.WriteLine(ConsoleColor.Green, "Update was successful");
                Console.ReadKey();
                Program.NavigateTo<MasterPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void GetById()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "Get master by id");

            try
            {
                Console.Write("Master id: ");
                var masterId = int.Parse(Console.ReadLine());
                var findClient = _masterService.GetById(masterId);

                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, findClient.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, findClient.Surname);
                Console.Write("Phone: ");
                Output.WriteLine(ConsoleColor.Green, findClient.Phone);
                Console.Write("Passport id: ");
                Output.WriteLine(ConsoleColor.Green, findClient.PassportId);
                Console.ReadKey();
                Program.NavigateTo<MasterPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void Delete()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "Delete master");

            try
            {
                Console.WriteLine("Master id: ");
                var clientId = int.Parse(Console.ReadLine());
                _masterService.Delete(clientId);

                Output.WriteLine(ConsoleColor.Green, "Removal was successful");
                Console.ReadKey();
                Program.NavigateTo<MasterPage>();

            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }
    }
}
