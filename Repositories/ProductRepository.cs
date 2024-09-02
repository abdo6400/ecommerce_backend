using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProductRepository(ApplicationDbContext _context) : IProductRepository
    {
        public async Task<Product?> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Reviews).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.Informations).Include(p => p.Reviews).Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetRecommendedProductsAsync(string userId, int limit)
        {
            // Attempt to get recommended products based on sales
            var recommendedProducts = await _context.OrderItems
                .Where(oi => oi.Order.UserId == userId)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    SalesCount = g.Count()
                })
                .OrderByDescending(p => p.SalesCount)
                .Take(limit)
                .Join(_context.Products,
                    p => p.ProductId,
                    prod => prod.Id,
                    (p, prod) => prod)
                .ToListAsync();

            // If no products were found from OrderItems, try to get products from Wishlists
            if (recommendedProducts.Count == 0)
            {
                recommendedProducts = await _context.Wishlists
                    .Where(w => w.UserId == userId)
                    .GroupBy(w => w.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        WishlistCount = g.Count()
                    })
                    .OrderByDescending(p => p.WishlistCount)
                    .Take(limit)
                    .Join(_context.Products,
                        p => p.ProductId,
                        prod => prod.Id,
                        (p, prod) => prod)
                    .ToListAsync();
            }

            return recommendedProducts;
        }

        public async Task<List<Product>> GetRelatedProductsAsync(int id, int subCategoryId)
        {
            var relatedProducts = await _context.Products
                .Where(p => p.Brand.SubCategoryId == subCategoryId && p.Id != id)
                .Include(p => p.Reviews)
                .ToListAsync();
            return relatedProducts;
        }

        public async Task<List<Product>> GetTopProductsAsync(int limit)
        {
            // Attempt to get the top-rated products
            var topRatedProducts = await _context.Reviews
                .GroupBy(r => r.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    AverageRating = g.Average(r => r.Rating)
                })
                .OrderByDescending(p => p.AverageRating)
                .Take(limit)
                .Join(_context.Products,
                    p => p.ProductId,
                    prod => prod.Id,
                    (p, prod) => prod)
                .ToListAsync();

            if (topRatedProducts.Any())
            {
                return topRatedProducts;
            }

            // If no top-rated products are found, attempt to get top-selling products
            var topSellingProducts = await _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(p => p.TotalSold)
                .Take(limit)
                .Join(_context.Products,
                    p => p.ProductId,
                    prod => prod.Id,
                    (p, prod) => prod)
                .ToListAsync();

            if (topSellingProducts.Any())
            {
                return topSellingProducts;
            }

            // If no top-selling products are found, get random products
            var randomProducts = await _context.Products
                .OrderBy(p => Guid.NewGuid()) // Random order
                .Take(limit)
                .ToListAsync();

            return randomProducts;
        }


        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var productExist = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (productExist == null)
            {
                return null;
            }
            productExist.Title = product.Title;
            productExist.Description = product.Description;
            productExist.Price = product.Price;
            productExist.Stock = product.Stock;
            productExist.BrandId = product.BrandId;
            productExist.Images = product.Images;
            productExist.MinimumOrderQuantity = product.MinimumOrderQuantity;
            productExist.DiscountPercentage = product.DiscountPercentage;
            productExist.Product_Unit = product.Product_Unit;
            await _context.SaveChangesAsync();
            return productExist;
        }
    }
}