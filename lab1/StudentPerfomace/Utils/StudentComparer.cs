using System.Collections.Generic;

namespace StudentPerfomace.Utils
{
    internal class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            if (x.FirstName == y.FirstName
                && x.LastName == x.LastName
                && x.MiddleName == x.MiddleName)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Student student)
        {
            return student.FirstName.GetHashCode() + student.LastName.GetHashCode() + student.MiddleName.GetHashCode();
        }
    }
}
