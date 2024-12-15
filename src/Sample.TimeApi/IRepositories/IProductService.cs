using Sample.TimeApi.Data;
using System.Collections.Generic;

namespace Sample.TimeApi.IRepositories
{
    /// <summary>
    /// Defines the operations for managing products in the application.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Adds a new product to the collection.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>The added product with any generated values, such as the ID.</returns>
        Product AddProduct(Product product);

        /// <summary>
        /// Retrieves all products from the collection.
        /// </summary>
        /// <returns>A list of all products.</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The product that matches the specified ID, or null if not found.</returns>
        Product GetProductById(int productId);

        /// <summary>
        /// Updates the details of an existing product.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to be deleted.</param>
        void DeleteProduct(int productId);
    }
}
