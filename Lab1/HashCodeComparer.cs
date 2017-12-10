using System.Collections.Generic;
// ReSharper disable PossibleNullReferenceException

namespace Lab1
{
    public class HashCodeComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return x.GetHashCode() - y.GetHashCode();
        }
    }
}