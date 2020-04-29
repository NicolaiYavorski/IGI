using System;

namespace StudentPerfomace.Models
{
    public class StudentPerfomance
    {
        private const int MinMark = 1;
        private const int MaxMark = 10;
        private double _averageMark;

        public double AverageMark
        {
            get { return _averageMark; }
            set
            {
                if (value < MinMark || value > MaxMark)
                    throw new ArgumentOutOfRangeException();
                _averageMark = value;
            }
        }

        public Student Student { get; set; }

        public override string ToString() => $"{Student}, {AverageMark}";
    }
}
