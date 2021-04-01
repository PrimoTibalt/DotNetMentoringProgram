// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ModuleTwoDebugTask
{
    /// <summary>
    /// Some product with it's properties.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// Sets name and price of the product.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="price">Price.</param>
        public Product(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        /// <summary>
        /// Gets or sets name of product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets price of product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Sets equality rules for the Product type.
        /// </summary>
        /// <param name="obj">Product to compare with this instance.</param>
        /// <returns>Are name and price from input equal to name and price of this instance.</returns>
        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product?.Name == this.Name && product.Price == this.Price;
        }

        /// <summary>
        /// Overrides standard function to get rid of warning.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
