using Autofac;
using DataAccess.EfContext;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Utils
{
    public class DalModule : Module
    {
        private readonly DbContextOptions _options;

        public DalModule(DbContextOptions options)
            : base()
        {
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ComputerContext>().AsSelf().WithParameter("options", _options).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<,>)).As(typeof(IRepository<,>));
        }
    }
}
