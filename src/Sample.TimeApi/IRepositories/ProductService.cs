using Elasticsearch.Net;
using Nest;
using Sample.TimeApi.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sample.TimeApi.IRepositories
{
    /// <summary>
    /// Provides CRUD operations for managing products using Elasticsearch.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IElasticClient _elasticClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="elasticClient">The Elasticsearch client used for data operations.</param>
        public ProductService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        /// <summary>
        /// Adds a new product to Elasticsearch.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>The added product if successful, otherwise an empty product object.</returns>
        public Product AddProduct(Product product)
        {
            var response = _elasticClient.IndexDocument(product);

            if (response.IsValid)
            {
                return product;
            }
            else
            {
                return new Product();
            }
        }

        /// <summary>
        /// Deletes a product from Elasticsearch by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to be deleted.</param>
        public void DeleteProduct(int productId)
        {
            _elasticClient.DeleteByQuery<Product>(p => p.Query(q1 => q1
                .Match(m => m
                    .Field(f => f.ProductId)
                    .Query(productId.ToString())
                )));
        }

        /// <summary>
        /// Retrieves all products from Elasticsearch.
        /// </summary>
        /// <returns>A list of all products in Elasticsearch.</returns>
        public List<Product> GetAllProducts()
        {
            var esResponse = _elasticClient.Search<Product>().Documents;

            return esResponse.ToList();
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The product with the specified ID, or null if not found.</returns>
        public Product GetProductById(int productId)
        {
            var esResponse = _elasticClient.Search<Product>(x => x
                .Query(q1 => q1.Bool(b => b.Must(m =>
                    m.Terms(t => t
                        .Field(f => f.ProductId)
                        .Terms<int>(productId))))));

            return esResponse.Documents.FirstOrDefault();
        }

        /// <summary>
        /// Updates an existing product in Elasticsearch.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        public void UpdateProduct(Product product)
        {
            if (product != null)
            {
                var updateResponse = _elasticClient.UpdateByQueryAsync<Product>(q =>
                    q.Query(q1 => q1.Bool(b => b.Must(m =>
                        m.Match(x => x.Field(f => f.ProductId == product.ProductId)))))
                    .Script(s => s.Source(
                        "ctx._source.price = params.price;" +
                        "ctx._source.productDescription = params.productDescription;" +
                        "ctx._source.category = params.category;")
                    .Lang("painless")
                    .Params(p => p.Add("price", product.Price)
                        .Add("productDescription", product.ProductDescription)
                        .Add("category", product.Category)))
                    .Conflicts(Conflicts.Proceed));
            }
        }
    }

}
