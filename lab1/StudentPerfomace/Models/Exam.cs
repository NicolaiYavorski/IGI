using System;

namespace StudentPerfomace
{
    public class Exam
    {
        private const int MinMark = 1;
        private const int MaxMark = 10;
        private int _mark;

        public Student Student { get; set; }

        public string Subject { get; set; }
        
        public int Mark
        {
            get { return _mark; }
            set
            {
                if (value < MinMark || value > MaxMark)
                    throw new ArgumentOutOfRangeException();
                _mark = value;
            }
        }
    }
}
