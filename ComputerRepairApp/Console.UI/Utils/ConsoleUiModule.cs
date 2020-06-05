using Autofac;
using BusinessLogic.Utils;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.UI.Utils
{
    public class ConsoleUiModule : Module
    {
        public string ConnectionString { get; }

        public ConsoleUiModule(string connectionString)
            : base()
        {
            ConnectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConnectionString)
                .Options;

            builder.RegisterModule(new BllModule(options));
        }
    }
}
