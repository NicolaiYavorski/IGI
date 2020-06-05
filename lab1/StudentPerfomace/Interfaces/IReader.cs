using System.Collections.Generic;

namespace StudentPerfomace.Interfaces
{
    public interface IReader
    {
        IEnumerable<Exam> Read(string path, ICustomLogger logger);
    }
}
