using API.Helper;
using Core.Contracts.Repository;
using Core.Entities;
using Core.Specifications.Models.ProductModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBarandSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecipication(productParams);

            var totalItems = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.LsitAsync(spec);
            return Ok(new Pagination<Product>(
                productParams.PageNumber,
                productParams.PageSize,
                totalItems,
                products
           ));
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> Get(long id)
        {
            var spec = new ProductWithTypeAndBarandSpecification(id);
            var products = await _productRepository.GetEntityWithSpec(spec);
            return Ok(products);
        }
    }
}
