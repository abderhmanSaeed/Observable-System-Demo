using System;

namespace Sample.TimeApi.Data
{
    /// <summary>
    /// Represents a product entity with details such as ID, name, description, price, launch date, and category.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the launch date of the product.
        /// </summary>
        public DateTime LaunchDate { get; set; }

        /// <summary>
        /// Gets or sets the category to which the product belongs.
        /// </summary>
        public string Category { get; set; }
    }

}
