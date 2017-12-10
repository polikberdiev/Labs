using System;
using System.Collections;
using System.Collections.Generic;
// ReSharper disable TailRecursiveCall

namespace Lab1
{
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        private const BypassType DefaultBypassType = BypassType.InOrder;

        private readonly IComparer<T> _comparer;
        private Node _root;


        public BinarySearchTree(IEnumerable<T> items)
            : this(items, new HashCodeComparer<T>())
        {

        }

        public BinarySearchTree(IEnumerable<T> items, IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            foreach (var item in items)
            {
                Add(item);
            }
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumeratorCore();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumeratorCore();
        }


        public void Add(T item)
        {
            if (_root == null)
            {
                _root = new Node(item);
                return;
            }

            AddToNode(_root, item);
        }

        public IEnumerable<T> BypassTree(BypassType bypassType)
        {
            switch (bypassType)
            {
                case BypassType.PreOrder:
                    return PreOrderBypassTree(_root);
                case BypassType.InOrder:
                    return InOrderBypassTree(_root);
                case BypassType.PostOrder:
                    return PostOrderBypassTree(_root);
                default:
                    throw new ArgumentOutOfRangeException(nameof(bypassType), bypassType, null);
            }
        }


        private IEnumerator<T> GetEnumeratorCore()
        {
            return BypassTree(DefaultBypassType).GetEnumerator();
        }

        private void AddToNode(Node parent, T item)
        {
            var parentCompation = _comparer.Compare(item, parent.Item);
            if (parentCompation == 0)
            {
                return;
            }

            if (parentCompation < 0)
            {
                if (parent.Left == null)
                {
                    parent.Left = new Node(item);
                    return;
                }

                AddToNode(parent.Left, item);
            }
            else if (parentCompation > 0)
            {
                if (parent.Right == null)
                {
                    parent.Right = new Node(item);
                    return;
                }

                AddToNode(parent.Right, item);
            }
        }

        private static IEnumerable<T> PreOrderBypassTree(Node node)
        {
            yield return node.Item;

            if (node.Left != null)
            {
                foreach (var item in PreOrderBypassTree(node.Left))
                {
                    yield return item;
                }
            }

            if (node.Right != null)
            {
                foreach (var item in PreOrderBypassTree(node.Right))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<T> InOrderBypassTree(Node node)
        {
            if (node.Left != null)
            {
                foreach (var item in InOrderBypassTree(node.Left))
                {
                    yield return item;
                }
            }

            yield return node.Item;

            if (node.Right != null)
            {
                foreach (var item in InOrderBypassTree(node.Right))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<T> PostOrderBypassTree(Node node)
        {
            if (node.Left != null)
            {
                foreach (var item in PostOrderBypassTree(node.Left))
                {
                    yield return item;
                }
            }

            if (node.Right != null)
            {
                foreach (var item in PostOrderBypassTree(node.Right))
                {
                    yield return item;
                }
            }

            yield return node.Item;
        }


        private class Node
        {
            public T Item { get; }

            public Node Left { get; set; }

            public Node Right { get; set; }


            public Node(T item)
            {
                Item = item;
            }
        }
    }
}