using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using CsvHelper.TypeConversion;
using System;
using CsvHelper;
using StudentPerfomace.Interfaces;

namespace StudentPerfomace.Services
{
    internal class ExamCsvReader : Interfaces.IReader
    {
        public IEnumerable<Exam> Read(string path, ICustomLogger logger)
        {
            const string CurrentEncoding = "utf-8";
            const int OffsetOfMarks = 3;
            var exams = new List<Exam>();
            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Encoding = Encoding.GetEncoding(CurrentEncoding);
                var subjectName = csv.Parser.Read().Skip(OffsetOfMarks);

                while (csv.Read())
                {
                    for (int i = 0; i < subjectName.Count(); i++)
                    {
                        exams.Add(new Exam
                        {
                            Student = new Student(csv.GetField(0), csv.GetField(1), csv.GetField(2)),
                            Subject = subjectName.Skip(i).First(),
                            Mark = csv.GetField<int>(i + OffsetOfMarks),
                        });
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                logger.Setup().Fatal(e.Message, e.GetType());
            }
            catch (ArgumentOutOfRangeException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
            catch (TypeConverterException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
            catch (CsvHelper.MissingFieldException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }
            catch (ArgumentNullException e)
            {
                logger.Setup().Error(e.Message, e.GetType());
            }

            return exams;
        }
    }
}
