using System;
using System.Linq;
using Lab1.Tests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab1.Tests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        private static readonly Student[] Students = {
            new Student("Alexander", "Pushkin", "Literature", DateTime.Parse("02/08/1822"), 10),
            new Student("Josef", "Stalin", "Philosophy", DateTime.Parse("03/06/1942"), 3),
            new Student("Albert", "Einstein", "Mathematics", DateTime.Parse("04/02/1899"), 6)
        };
        private static readonly Point[] Points = {
            new Point { X = 1, Y = 2},
            new Point { X = 2, Y = 3},
            new Point { X = 3, Y = 8},
            new Point { X = 4, Y = 6},
            new Point { X = 5, Y = 4},
            new Point { X = 6, Y = 3},
            new Point { X = 7, Y = 9},
        };

        private readonly BinarySearchTree<int> _intTree =
            new BinarySearchTree<int>(new[] { 2, 9, 8, 7, 6, 1, 0, 3, 5, 4 });
        private readonly BinarySearchTree<int> _inversedIntComparerTree =
            new BinarySearchTree<int>(new[] { 2, 9, 8, 7, 6, 1, 0, 3, 5, 4 }, new InversedIntComparer());

        private readonly BinarySearchTree<Student> _studentTree =
            new BinarySearchTree<Student>(Students);
        private readonly BinarySearchTree<Student> _ratingStudentComparerTree =
            new BinarySearchTree<Student>(Students, new StudentRatingComparer());

        private readonly BinarySearchTree<Point> _xPointComparerTree =
            new BinarySearchTree<Point>(Points, new XPointComparer());


        [TestMethod]
        public void IntPreOrderComparerTest()
        {
            int[] expected = { 2, 1, 0, 9, 8, 7, 6, 3, 5, 4 };
            var result = _intTree.BypassTree(BypassType.PreOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IntInOrderComparerTest()
        {
            int[] expected = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = _intTree.BypassTree(BypassType.InOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IntPostOrderComparerTest()
        {
            int[] expected = { 0, 1, 4, 5, 3, 6, 7, 8, 9, 2 };
            var result = _intTree.BypassTree(BypassType.PostOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void InversedIntPreOrderComparerTest()
        {
            int[] expected = { 2, 9, 8, 7, 6, 3, 5, 4, 1, 0 };
            var result = _inversedIntComparerTree.BypassTree(BypassType.PreOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void InversedIntInOrderComparerTest()
        {
            int[] expected = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            var result = _inversedIntComparerTree.BypassTree(BypassType.InOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void InversedIntPostOrderComparerTest()
        {
            int[] expected = { 4, 5, 3, 6, 7, 8, 9, 0, 1, 2 };
            var result = _inversedIntComparerTree.BypassTree(BypassType.PostOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void StudentPreOrderTest()
        {
            Student[] expected = { Students[0], Students[1], Students[2] };
            var result = _studentTree.BypassTree(BypassType.PreOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void StudentInOrderTest()
        {
            Student[] expected = { Students[1], Students[2], Students[0] };
            var result = _studentTree.BypassTree(BypassType.InOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void StudentPostOrderTest()
        {
            Student[] expected = { Students[2], Students[1], Students[0] };
            var result = _studentTree.BypassTree(BypassType.PostOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void RatingStudentInOrderComparerTest()
        {
            Student[] expected = { Students[1], Students[2], Students[0] };
            var result = _ratingStudentComparerTree.BypassTree(BypassType.InOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void XPointPreOrderTest()
        {
            Point[] expected =
                { Points[0], Points[1], Points[2], Points[3], Points[4], Points[5], Points[6] };
            var result = _xPointComparerTree.BypassTree(BypassType.PreOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void XPointInOrderTest()
        {
            Point[] expected =
                { Points[0], Points[1], Points[2], Points[3], Points[4], Points[5], Points[6] };
            var result = _xPointComparerTree.BypassTree(BypassType.InOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void XPointPostOrderTest()
        {
            Point[] expected =
                { Points[6], Points[5], Points[4], Points[3], Points[2], Points[1], Points[0] };
            var result = _xPointComparerTree.BypassTree(BypassType.PostOrder).ToArray();

            CollectionAssert.AreEqual(result, expected);
        }
    }
}