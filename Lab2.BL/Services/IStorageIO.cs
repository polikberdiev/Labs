using System.Collections.Generic;

namespace Lab2.BL.Services
{
    // ReSharper disable once InconsistentNaming
    public interface IStorageIO<T>
    {
        IEnumerable<T> ReadAll();

        void WriteAll(IEnumerable<T> source);
    }
}