using Autofac;
using StudentPerfomace.Interfaces;
using StudentPerfomace.Services;

namespace StudentPerfomace.Utils
{
    public class DiModule : Module
    {
        public DiModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ExamCsvReader>().As<IReader>();
            builder.RegisterType<ExcelWriter>().Keyed<IWriter>(WriterType.Excel);
            builder.RegisterType<JsonWriter>().Keyed<IWriter>(WriterType.Json);
            builder.RegisterType<Logger>().As<ICustomLogger>();
        }
    }

}
