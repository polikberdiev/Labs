using System.Collections.Generic;
// ReSharper disable PossibleNullReferenceException

namespace Lab1.Tests.Core
{
    public class StudentRatingComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return x.Rating - y.Rating;
        }
    }
}