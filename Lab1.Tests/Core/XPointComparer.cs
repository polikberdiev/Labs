using System.Collections.Generic;

namespace Lab1.Tests.Core
{
    public class XPointComparer : IComparer<Point>
    {
        public int Compare(Point x, Point y)
        {
            return x.X - y.X;
        }
    }
}