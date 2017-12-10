using System.Collections.Generic;

namespace Lab1.Tests.Core
{
    public class InversedIntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y - x;
        }
    }
}