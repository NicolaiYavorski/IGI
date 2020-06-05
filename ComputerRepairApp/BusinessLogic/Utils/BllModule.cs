using Autofac;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Utils
{
    public class BllModule : Module
    {
        private readonly DbContextOptions _options;

        public BllModule(DbContextOptions options)
            : base()
        {
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule(new DalModule(_options));
            builder.RegisterType<ClientService>().As<IClientService>();
            builder.RegisterType<MasterService>().As<IMasterService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
        }
    }
}
