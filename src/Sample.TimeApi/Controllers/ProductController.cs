using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.TimeApi.Data;
using Sample.TimeApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private readonly IRedisCacheService _redisCacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service for handling product operations.</param>
        /// <param name="logger">The logger for capturing log details.</param>
        /// <param name="redisCacheService">Service for interacting with Redis cache.</param>
        public ProductController(IProductService productService, ILogger<ProductController> logger, IRedisCacheService redisCacheService)
        {
            _productService = productService;
            _logger = logger;
            _redisCacheService = redisCacheService;
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
        /// Fetches a product by its ID. If the product is not found in the Redis cache,
        /// it simulates fetching from a database and stores it in the cache.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the product.</returns>
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var cacheKey = $"product:{id}";
            var product = await _redisCacheService.GetAsync<Product>(cacheKey);

            if (product == null)
            {
                // Simulate fetching from the database
                product = new Product { ProductId = id, ProductName = "Sample Product" };
                await _redisCacheService.SetAsync(cacheKey, product, TimeSpan.FromHours(1));
            }

            return Ok(product);
        }

        /// <summary>
        /// Removes a product's cache entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the product whose cache is to be removed.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the success of the operation.</returns>
        [HttpDelete]
        [Route("DeleteProductCache/{id}")]
        public async Task<IActionResult> DeleteProductCache(int id)
        {
            var cacheKey = $"product:{id}";
            await _redisCacheService.RemoveAsync(cacheKey);
            return Ok($"Cache for product {id} removed.");
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
