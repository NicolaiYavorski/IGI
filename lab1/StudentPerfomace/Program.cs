using Autofac;
using McMaster.Extensions.CommandLineUtils;
using StudentPerfomace.Extensions;
using StudentPerfomace.Interfaces;
using StudentPerfomace.Models;
using StudentPerfomace.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerfomace
{
    public class Program
    {
        private const string ExcelFormatName = "xlsx";
        private const string JsonFormatName = "json";

        [Option("-i", Description = "The path of the input file")]
        public string InputFilePath { get; }

        [Option("-o", Description = "The path of the output file")]
        public string OutputFilePath { get; set; }

        [Option("-f", Description = "The file format")]
        public string FileFormat { get; set; }

        public static void Main(string[] args) =>
            CommandLineApplication.Execute<Program>(args);

        public void OnExecute()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DiModule());
            var container = builder.Build();

            var csvReader = container.Resolve<IReader>();
            var excelWriter = container.ResolveKeyed<IWriter>(WriterType.Excel);
            var jsonWriter = container.ResolveKeyed<IWriter>(WriterType.Json);
            var logger = container.Resolve<ICustomLogger>();

            try
            {
                IEnumerable<Exam> exams = csvReader.Read(InputFilePath, logger);
                IReadOnlyCollection<StudentPerfomance> studentsPerfomance = exams.GetAverageStudentMark();
                IReadOnlyCollection<MarkForSubject> averageSubjectMark = exams.GetAverageSubjectMark();

                OutputFilePath = FormatEditing(OutputFilePath, FileFormat);
                switch (FileFormat.ToLower())
                {
                    case ExcelFormatName:
                        excelWriter.Write(OutputFilePath, studentsPerfomance, averageSubjectMark, logger);
                        break;
                    case JsonFormatName:
                        jsonWriter.Write(OutputFilePath, studentsPerfomance, averageSubjectMark, logger);
                        break;
                    default:
                        logger.Setup().Error($"{FileFormat} is not supported");
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
            catch (ArgumentOutOfRangeException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
        }

        private string FormatEditing(string outputPath, string format)
        {
            var separator = '.';
            var extension = outputPath.Split(separator).Last().ToLower();
            return extension switch
            {
                ExcelFormatName => outputPath,
                JsonFormatName => outputPath,
                _ => $"{outputPath}.{format.ToLower()}",
            };
        }
    }
}
