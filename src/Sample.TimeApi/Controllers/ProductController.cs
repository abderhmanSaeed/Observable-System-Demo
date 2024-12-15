using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.TimeApi.Data;
using Sample.TimeApi.IRepositories;
using System.Collections.Generic;

namespace Sample.TimeApi.Controllers
{
    /// <summary>
    /// Provides endpoints for managing product data.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service for handling product operations.</param>
        /// <param name="logger">The logger for capturing log details.</param>
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products from the system.
        /// </summary>
        /// <returns>An enumerable collection of all products.</returns>
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetAllProducts();
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        [HttpPost]
        [Route("[action]")]
        public void AddProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);
        }

        /// <summary>
        /// Updates an existing product in the system.
        /// </summary>
        /// <param name="product">The product with updated information.</param>
        [HttpPost]
        [Route("[action]")]
        public void UpdateProduct([FromBody] Product product)
        {
            _productService.UpdateProduct(product);
        }

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }

}
