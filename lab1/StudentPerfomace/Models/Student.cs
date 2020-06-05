namespace StudentPerfomace
{
    public class Student
    {
        public Student(string lastName, string firstName, string middleName)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
        }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public override string ToString() => $"{LastName}, {FirstName}, {MiddleName}";
    }
}
