using System;
using System.Collections.Generic;

namespace Lab2.BL.Services
{
    public interface IStorage<T>
    {
        void Add(T data);

        IEnumerable<T> GetSortedBy(string propertyName, bool asc, int maxCount);

        IEnumerable<T> GetSortedBy<TKey>(Func<T, TKey> propertySelector, bool asc = true, int maxCount = 10);

        void Save();
    }
}