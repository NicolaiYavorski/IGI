using StudentPerfomace.Models;
using StudentPerfomace.Utils;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerfomace.Extensions
{
    public static class ExamExtensions
    {
        public static IReadOnlyCollection<MarkForSubject> GetAverageSubjectMark(this IEnumerable<Exam> exams)
          => exams
              .GroupBy(item => item.Subject)
              .Select(subject => new MarkForSubject
              {
                  Subject = subject.Key,
                  AverageMark = subject.Average(x => x.Mark),
              }).ToList().AsReadOnly();

        public static IReadOnlyCollection<StudentPerfomance> GetAverageStudentMark(this IEnumerable<Exam> exams)
            =>
            exams
                .GroupBy(item => item.Student, new StudentComparer())
                .Select(students => new StudentPerfomance
                {
                    Student = students.Key,
                    AverageMark = students.Average(exam => exam.Mark),
                }).ToList().AsReadOnly();
    }
}
