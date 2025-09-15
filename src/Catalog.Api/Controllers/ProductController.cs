using System.Net;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region constructor
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        #endregion

        #region get products
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }
        #endregion

        #region get product by id
        [HttpGet("GetProduct/{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"product with id:{id} is not found");
                return NotFound();
            }
            return Ok(product);
        }
        #endregion

        #region get product by category
        [HttpGet("GetProductByCategory/{category}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);
            if (products == null)
            {
                _logger.LogError($"products with category:{category} is not found");
                return NotFound();
            }
            return Ok(products);
        }
        #endregion

        #region create product
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id },product);
        }
        #endregion

        #region update product
        [HttpPut("EditProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> EditProduct([FromBody] Product product)
        {
            await _productRepository.UpdateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        #endregion

        #region delete product
        [HttpDelete("DeleteProduct/{id:length(24)}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }
        #endregion
    }
}
