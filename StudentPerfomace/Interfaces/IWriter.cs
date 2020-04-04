using StudentPerfomace.Models;
using System.Collections.Generic;

namespace StudentPerfomace.Interfaces
{
    public interface IWriter
    {
        void Write(string path, IReadOnlyCollection<StudentPerfomance> students, IReadOnlyCollection<MarkForSubject> subjects, ICustomLogger logger);
    }
}
