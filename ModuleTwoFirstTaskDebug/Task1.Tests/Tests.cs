// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ModuleTwoDebugTask.Tests
{
    using System;
    using ModuleTwoDebugTask;
    using NUnit.Framework;

    /// <summary>
    /// Test class.
    /// </summary>
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// Is sorting in right way.
        /// </summary>
        [Test]
        public void Sort_Numbers_ReturnsAscendingSortedNumbers()
        {
            int[] numbers = new[] { 4, 2, 1, 3, -5 };

            Utilities.Sort(numbers);

            CollectionAssert.AreEqual(new[] { -5, 1, 2, 3, 4 }, numbers);
        }

        /// <summary>
        /// Is sorting raises ArgumentNullException on null input.
        /// </summary>
        [Test]
        public void Sort_Null_ThrowsArgumentNullException()
        {
            Assert.That(() => Utilities.Sort(null), Throws.InstanceOf<ArgumentNullException>());
        }

        /// <summary>
        /// Is sorting return empty array with empty array in input value.
        /// </summary>
        [Test]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            int[] numbers = new int[0];

            Utilities.Sort(numbers);

            CollectionAssert.AreEqual(new int[0], numbers);
        }

        /// <summary>
        /// Is IndexOf determines index of instance with same properties value.
        /// </summary>
        [Test]
        public void IndexOf_Products_ReturnsTwo()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = new Product("Product 3", 30.0d);

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(2));
        }

        /// <summary>
        /// Is IndexOf returns -1 when no equal instancies presented.
        /// </summary>
        [Test]
        public void IndexOf_NoMatch_ReturnsMinusOne()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = new Product("Product 4", 30.0d);

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }

        /// <summary>
        /// Is -1 returned when input is null.
        /// </summary>
        [Test]
        public void IndexOf_EqualsWithNull_ReturnsMinusOne()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            Product productToFind = null;

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }

        /// <summary>
        /// Is -1 returned when types are different.
        /// </summary>
        [Test]
        public void IndexOf_SearchForNonProductTypeObject_ReturnsMinusOne()
        {
            var products = new Product[]
            {
                new Product("Product 1", 10.0d),
                new Product("Product 2", 20.0d),
                new Product("Product 3", 30.0d),
            };
            var productToFind = 42;

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }

        /// <summary>
        /// Is ArgumentNullException raised when trying to find an index of element in null.
        /// </summary>
        [Test]
        public void IndexOf_NullProducts_ThrowsArgumentNullException()
        {
            Assert.That(
                () =>
            {
                var productToFind = new Product("Product 3", 30.0d);
                int index = Utilities.IndexOf(null, product => product.Equals(productToFind));
            }, Throws.InstanceOf<ArgumentNullException>());
        }

        /// <summary>
        /// Is ArgumentNullException raised on null predicate of IndexOf.
        /// </summary>
        [Test]
        public void IndexOf_NullPredicate_ThrowsArgumentNullException()
        {
            Assert.That(
                () =>
            {
                var products = new Product[] { new Product("Product 1", 10.0d) };
                int index = Utilities.IndexOf(products, null);
            }, Throws.InstanceOf<ArgumentNullException>());
        }

        /// <summary>
        /// Is -1 returned when trying to find index in empty array.
        /// </summary>
        [Test]
        public void IndexOf_EmptyArray_ReturnsMinusOne()
        {
            var products = new Product[0];
            var productToFind = new Product("Product 3", 30.0d);

            int index = Utilities.IndexOf(products, product => product.Equals(productToFind));

            Assert.That(index, Is.EqualTo(-1));
        }
    }
}