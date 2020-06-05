using Newtonsoft.Json;
using StudentPerfomace.Interfaces;
using StudentPerfomace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StudentPerfomace.Services
{
    internal class JsonWriter : IWriter
    {
        public void Write(string path, IReadOnlyCollection<StudentPerfomance> students, IReadOnlyCollection<MarkForSubject> subjects, ICustomLogger logger)
        {
            try
            {
                var totalMark = subjects.Average(x => x.AverageMark);
                using var writer = new StreamWriter(path, false, Encoding.Default);
                writer.Write(JsonConvert.SerializeObject(students));
                writer.Write(JsonConvert.SerializeObject(subjects));
                writer.Write($"Total mark: {totalMark}");
            }
            catch (ArgumentException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
            catch (InvalidOperationException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
        }
    }
}
