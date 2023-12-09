using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToSqlTest.Utils
{
    public sealed class ReverseComparer<T> : IComparer<T> where T : IComparable
    {
        public int Compare(T x, T y)
        {
            return x.CompareTo(y) * -1;
        }
    }
}
