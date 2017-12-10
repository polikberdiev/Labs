using System;

namespace Lab1.Tests.Core
{
    public class Student : IComparable<Student>
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string TestTitle { get; }

        public DateTime Timestamp { get; }

        public int Rating { get; }


        public Student(string firstName, string lastName, string testTitle, DateTime timestamp, int rating)
        {
            FirstName = firstName;
            LastName = lastName;
            TestTitle = testTitle;
            Timestamp = timestamp;
            Rating = rating;
        }


        public int CompareTo(Student other)
        {
            return LastName.Length * TestTitle.Length * Rating
                - other.LastName.Length * other.TestTitle.Length * other.Rating;
        }
    }
}