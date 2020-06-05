using OfficeOpenXml;
using StudentPerfomace.Interfaces;
using StudentPerfomace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentPerfomace.Services
{
    internal class ExcelWriter : IWriter
    {
        public void Write(
            string path,
            IReadOnlyCollection<StudentPerfomance> students,
            IReadOnlyCollection<MarkForSubject> subjects,
            ICustomLogger logger)
        {
            try
            {
                int subjectCount = 0,
                    studentCount = 0,
                    leftEdgeOfTable = 1,
                    topEdgeOfTable = 1,
                    headerOffset = 1,
                    tableSplitting = 2,
                    subjectsOffset = students.Count() + headerOffset,
                    marksOffset = subjectsOffset + 1,
                    totalMarkOffset = marksOffset + 1;

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add("output");
                ws.Cells.Style.Font.Size = 11;
                ws.Cells.Style.Font.Name = "Calibri";
                ws.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                ws.Cells[1, 1].Value = "Фамилия";
                ws.Cells[1, 2].Value = "Имя";
                ws.Cells[1, 3].Value = "Отчество";
                ws.Cells[1, 4].Value = "Средняя оценка";
                foreach (var item in students)
                {
                    ws.Cells[leftEdgeOfTable, topEdgeOfTable].Style.Font.Bold = true;
                    ws.Cells[leftEdgeOfTable + headerOffset + studentCount, topEdgeOfTable].Value = item.Student.FirstName;
                    ws.Cells[leftEdgeOfTable + headerOffset + studentCount, topEdgeOfTable + 1].Value = item.Student.LastName;
                    ws.Cells[leftEdgeOfTable + headerOffset + studentCount, topEdgeOfTable + 2].Value = item.Student.MiddleName;
                    ws.Cells[leftEdgeOfTable + headerOffset + studentCount++, topEdgeOfTable + 3].Value = item.AverageMark;
                }
                foreach (var item in subjects)
                {
                    ws.Cells[subjectsOffset + tableSplitting, leftEdgeOfTable + subjectCount].Style.Font.Bold = true;
                    ws.Cells[subjectsOffset + tableSplitting, leftEdgeOfTable + subjectCount].Value = item.Subject;
                    ws.Cells[marksOffset + tableSplitting, leftEdgeOfTable + subjectCount++].Value = item.AverageMark;
                }

                ws.Cells[totalMarkOffset + tableSplitting + 1, leftEdgeOfTable].Value = "Средняя оценка в группе";
                ws.Cells[totalMarkOffset + tableSplitting + 1, leftEdgeOfTable].Style.Font.Bold = true;
                ws.Cells[totalMarkOffset + tableSplitting + 2, leftEdgeOfTable].Value = subjects.Average(x => x.AverageMark);
                var file = new FileInfo(path);
                excel.SaveAs(file);
            }
            catch (ArgumentNullException e)
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
