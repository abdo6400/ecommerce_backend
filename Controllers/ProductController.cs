using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IProductRepository productRepository, IFileService fileService, IStringLocalizer<ProductController> localizer) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IFileService _fileService = fileService;
        private readonly IStringLocalizer<ProductController> _localizer = localizer;

        // GET api/products/top-popular
        [HttpGet("top-popular")]
        public async Task<ActionResult<List<ProductDto>>> GetTopRatedProducts([FromQuery] int limit = 10)
        {
            var products = await _productRepository.GetTopProductsAsync(limit);
            return Ok(products.Select(x => x.ToProductDto()).ToList());
        }

        // GET api/products/recommended
        [Authorize(Roles = "User")]
        [HttpGet("recommended")]
        public async Task<ActionResult<List<ProductDto>>> GetRecommendedProducts([FromQuery] int limit = 10)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var products = await _productRepository.GetRecommendedProductsAsync(userId, limit);
            return Ok(products.Select(x => x.ToProductDto()).ToList());
        }

        // GET api/products/all
        [HttpGet("all")]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products.Select(x => x.ToProductDto()).ToList());
        }

        // GET api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductByIdDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(_localizer.GetString(AppStrings.productNotFound));
            }
            var relatedProducts = await _productRepository.GetRelatedProductsAsync(product.Id, product.Brand.SubCategoryId);
            return Ok(product.ToProductByIdDto(relatedProducts));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] ProductCreateRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> images = await _fileService.SaveFilesAsync(productDto.Images, "images", "products", productDto.TitleEn);
            var product = productDto.ToProductFromCreateRequestDto(images);
            await _productRepository.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT api/products/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromForm] ProductUpdateRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> images = await _fileService.SaveFilesAsync(productDto.Images, "images", "products", productDto.TitleEn);
            var product = productDto.ToProductFromUpdateRequestDto(id, images);

            if (id != product.Id)
            {
                return BadRequest(_localizer.GetString(AppStrings.idNotMatch));
            }

            await _productRepository.UpdateProductAsync(product);
            return Ok(product.ToProductDto());
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.DeleteProductAsync(id);
            if (product == null)
            {
                return NotFound(_localizer.GetString(AppStrings.productNotFound));
            }

            return NoContent();
        }

        // POST api/products/bulk-create-products
        [HttpPost("bulk-create-products")]
        public async Task<ActionResult<List<ProductDto>>> BulkCreateProducts([FromBody] List<ProductJsonCreateRequestDto> productDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productDtosList = new List<ProductDto>();

            foreach (var productDto in productDtos)
            {
                var product = productDto.ToProductFromCreateRequestJsonDto();
                var createdProduct = await _productRepository.CreateProductAsync(product);

                if (createdProduct == null)
                {
                    return BadRequest(_localizer.GetString(AppStrings.productNotCreated));
                }

                productDtosList.Add(createdProduct.ToProductDto());
            }

            return Ok(productDtosList);
        }
    }
}
