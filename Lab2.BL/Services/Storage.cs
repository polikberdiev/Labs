using System;
using System.Collections.Generic;
using System.Linq;
using Lab1;
using Lab2.BL.Core;

namespace Lab2.BL.Services
{
    public class Storage<T> : IStorage<T>
    {
        private readonly IStorageIO<T> _storageIo;
        private readonly BinarySearchTree<T> _binaryTree;


        public Storage(IStorageIO<T> storageIo)
        {
            _storageIo = storageIo;
            _binaryTree = new BinarySearchTree<T>(_storageIo.ReadAll());
        }


        public virtual void Add(T data)
        {
            _binaryTree.Add(data);
        }

        public IEnumerable<T> GetSortedBy(string propertyName, bool asc, int maxCount)
        {
            return GetSortedBy(
                // ReSharper disable once PossibleNullReferenceException
                s => typeof(T).GetProperty(propertyName).GetValue(s),
                asc,
                maxCount);
        }

        public IEnumerable<T> GetSortedBy<TKey>(
            Func<T, TKey> propertySelector,
            bool asc,
            int maxCount)
        {
            var result = _binaryTree
                .BypassTree(BypassType.InOrder)
                .OrderBy(propertySelector, asc)
                .Take(maxCount);

            return result;
        }


        public void Save()
        {
            _storageIo.WriteAll(_binaryTree.BypassTree(BypassType.InOrder));
        }
    }
}