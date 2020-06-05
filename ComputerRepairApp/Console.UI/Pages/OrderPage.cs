using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using EasyConsole;
using System;
using System.Linq;

namespace ConsoleApp.UI.Pages
{
    public class OrderPage : Page
    {
        private readonly IOrderService _orderService;

        public OrderPage(Program program, IOrderService orderService)
            : base("Order page", program)
        {
            _orderService = orderService;
        }

        public override void Display()
        {
            base.Display();

            var menu = new Menu()
                .Add("All orders", () => ShowAll())
                .Add("Get order by id", () => GetById())
                .Add("Create order", () => Create())
                .Add("Update order", () => Update())
                .Add("Delete order", () => Delete())
                .Add("Back to menu", () => Program.NavigateBack());

            menu.Display();
        }

        public void ShowAll()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "All orders");

            foreach (var order in _orderService.GetAll())
            {
                Output.WriteLine(ConsoleColor.Gray, "-------------");
                Console.Write("Computer: ");
                Output.WriteLine(ConsoleColor.Green, order.Computer.ToString());
                Console.Write("Malfunction: ");
                Output.WriteLine(ConsoleColor.Green, order.Malfunction);
                Console.Write("Price: ");
                Output.WriteLine(ConsoleColor.Green, order.Price.ToString());
                Console.Write("Date time: ");
                Output.WriteLine(ConsoleColor.Green, order.DateTime.ToString());
            }

            Console.ReadKey();
            Program.NavigateTo<OrderPage>();
        }

        public void Create()
        {
            Console.Clear();
            var order = new OrderDto();
            Output.WriteLine(ConsoleColor.Cyan, "Create order");

            try
            {
                order.Computer = ChooseComputers();
                Console.Write("Malfunction: ");
                order.Malfunction = Console.ReadLine();
                Console.Write("Price: ");
                order.Price = decimal.Parse(Console.ReadLine());
                order.DateTime = DateTime.Now;

                _orderService.Add(order);
                Output.WriteLine(ConsoleColor.Green, "Creation was successful");
                Console.ReadKey();
                Program.NavigateTo<OrderPage>();
            }
            catch (ArgumentNullException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void Update()
        {
            Console.Clear();
            var order = new OrderDto();
            Output.WriteLine(ConsoleColor.Cyan, "Update order");

            try
            {
                Console.Write("Order id: ");
                order.Id = Input.ReadInt(0, _orderService.GetAll().Count());
                Console.Write("Malfunction: ");
                order.Malfunction = Console.ReadLine();
                Console.Write("Price: ");
                order.Price = decimal.Parse(Console.ReadLine());

                _orderService.Update(order);
                Output.WriteLine(ConsoleColor.Green, "Update was successful");
                Console.ReadKey();
                Program.NavigateTo<OrderPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void GetById()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "Get order by id");

            try
            {
                Console.Write("Order id: ");
                var orderId = int.Parse(Console.ReadLine());
                var findOrder = _orderService.GetById(orderId);

                Console.Write("Computer: ");
                Output.WriteLine(ConsoleColor.Green, findOrder.Computer.ToString());
                Console.Write("Malfunction: ");
                Output.WriteLine(ConsoleColor.Green, findOrder.Malfunction);
                Console.Write("Price: ");
                Output.WriteLine(ConsoleColor.Green, findOrder.Price.ToString());
                Console.Write("Date time: ");
                Output.WriteLine(ConsoleColor.Green, findOrder.DateTime.ToString());
                Console.ReadKey();
                Program.NavigateTo<OrderPage>();
            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        public void Delete()
        {
            Console.Clear();
            Output.WriteLine(ConsoleColor.Cyan, "Delete order");

            try
            {
                Console.WriteLine("Order id: ");
                var clientId = Input.ReadInt(0, _orderService.GetAll().Count());
                _orderService.Delete(clientId);

                Output.WriteLine(ConsoleColor.Green, "Removal was successful");
                Console.ReadKey();
                Program.NavigateTo<OrderPage>();

            }
            catch (ArgumentException e)
            {
                Output.WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        private Computer ChooseComputers()
        {
            Console.WriteLine("Computers. Choose one of the options: ");
            var computers = Enum.GetValues(typeof(Computer));
            var i = 1;
            foreach (var computer in computers)
            {
                Console.WriteLine($"\t{i++}. {computer}");
            }

            var selectedIndex = Input.ReadInt(1, computers.Length) - 1;
            return (Computer)Enum.GetValues(typeof(Computer)).GetValue(selectedIndex);
        }
    }
}
