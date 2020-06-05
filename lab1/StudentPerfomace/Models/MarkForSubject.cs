using System;

namespace StudentPerfomace.Models
{
    public class MarkForSubject
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

        public string Subject { get; set; }
    }
}
