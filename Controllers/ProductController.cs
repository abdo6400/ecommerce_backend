using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IProductRepository _productRepository, IFileService _fileService) : ControllerBase
    {
        [HttpGet]
        [Route("top-popular")]
        public async Task<ActionResult<List<ProductDto>>> GetTopRatedProducts([FromQuery] int limit = 10)
        {
            var products = await _productRepository.GetTopProductsAsync(limit);
            return Ok(products.Select(x => x.ToProductDto()).ToList());
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("recommended")]
        public async Task<ActionResult<List<ProductDto>>> GetRecommendedProducts([FromQuery] int limit = 10)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var products = await _productRepository.GetRecommendedProductsAsync(userId, limit);
            return Ok(products.Select(x => x.ToProductDto()).ToList());
        }


        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products.Select(x => x.ToProductDto()).ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductByIdDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var relatedProducts = await _productRepository.GetRelatedProductsAsync(product.Id, product.Brand.SubCategoryId);
            return Ok(product.ToProductByIdDto(relatedProducts));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] ProductCreateRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<string> images = await _fileService.SaveFilesAsync(productDto.Images, "images", "products", productDto.Title);

            var product = productDto.ToProductFromCreateRequestDto(images);
            await _productRepository.CreateProductAsync(product);
            return Ok(product.ToProductDto());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromForm] ProductUpdateRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<string> images = await _fileService.SaveFilesAsync(productDto.Images, "images", "products", productDto.Title);
            var product = productDto.ToProductFromUpdateRequestDto(id, images);
            if (id != product.Id)
            {
                return BadRequest("Id not matched");
            }
            await _productRepository.UpdateProductAsync(product);
            return Ok(product.ToProductDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            var product = await _productRepository.DeleteProductAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product.ToProductDto());
        }
    }
}