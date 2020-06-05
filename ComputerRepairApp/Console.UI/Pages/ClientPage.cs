using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using EasyConsole;
using System;

namespace ConsoleApp.UI.Pages
{
    public class ClientPage : Page
    {
        private readonly IClientService _clientService;

        public ClientPage(Program program, IClientService clientService)
          : base("Client page", program)
        {
            _clientService = clientService;
        }

        public override void Display()
        {
            base.Display();

            var menu = new Menu()
                .Add("All clients", () => ShowAll())
                .Add("Get client by id", () => GetById())
                .Add("Create client", () => Create())
                .Add("Update client", () => Update())
                .Add("Delete client", () => Delete())
                .Add("Back to menu", () => Program.NavigateBack());

            menu.Display();
        }

        public void ShowAll()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "All clients");
            foreach (var client in _clientService.GetAll())
            {
                Output.WriteLine(ConsoleColor.Gray, "-------------");
                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, client.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, client.Surname);
                Console.Write("Middle name: ");
                Output.WriteLine(ConsoleColor.Green, client.MiddleName);
                Console.Write("Address: ");
                Output.WriteLine(ConsoleColor.Green, client.Address);
                Console.Write("Phone: ");
                Output.WriteLine(ConsoleColor.Green, client.Phone);
            }

            Console.ReadKey();
            Program.NavigateTo<ClientPage>();
        }

        public void Create()
        {
            Console.Clear();
            var client = new ClientDto();
            Output.WriteLine(ConsoleColor.Cyan, "Create client");

            try
            {
                Console.Write("First name: ");
                client.FirstName = Console.ReadLine();
                Console.Write("Surname: ");
                client.Surname = Console.ReadLine();
                Console.Write("Middle name: ");
                client.MiddleName = Console.ReadLine();
                Console.Write("Address: ");
                client.Address = Console.ReadLine();
                Console.Write("Phone: ");
                client.Phone = Console.ReadLine();

                _clientService.Add(client);
                Output.WriteLine(ConsoleColor.Green, "Creation was successful");
                Console.ReadKey();
                Program.NavigateTo<ClientPage>();
            }
            catch (ArgumentNullException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void Update()
        {
            Console.Clear();
            var client = new ClientDto();
            Output.WriteLine(ConsoleColor.Cyan, "Update client");

            try
            {
                Console.Write("Client id: ");
                client.Id = int.Parse(Console.ReadLine());
                Console.Write("First name: ");
                client.FirstName = Console.ReadLine();
                Console.Write("Surname: ");
                client.Surname = Console.ReadLine();
                Console.Write("Middle name: ");
                client.MiddleName = Console.ReadLine();
                Console.Write("Address: ");
                client.Address = Console.ReadLine();
                Console.Write("Phone: ");
                client.Phone = Console.ReadLine();

                _clientService.Update(client);
                Output.WriteLine(ConsoleColor.Green, "Update was successful");
                Console.ReadKey();
                Program.NavigateTo<ClientPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void GetById()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "Get client by id");

            try
            {
                Console.Write("Client id: ");
                var clientId = int.Parse(Console.ReadLine());
                var findClient = _clientService.GetById(clientId);

                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, findClient.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, findClient.Surname);
                Console.Write("Middle name: ");
                Output.WriteLine(ConsoleColor.Green, findClient.MiddleName);
                Console.Write("Address: ");
                Output.WriteLine(ConsoleColor.Green, findClient.Address);
                Console.Write("Phone: ");
                Output.WriteLine(ConsoleColor.Green, findClient.Phone);

                Console.ReadKey();
                Program.NavigateTo<ClientPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void Delete()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "Delete client");

            try
            {
                Console.WriteLine("Client id: ");
                var clientId = int.Parse(Console.ReadLine());
                _clientService.Delete(clientId);

                Output.WriteLine(ConsoleColor.Green, "Removal was successful");
                Console.ReadKey();
                Program.NavigateTo<ClientPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }
    }
}
