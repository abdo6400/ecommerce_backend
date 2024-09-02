using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> CreateProductAsync(Product product);
        Task<Product?> DeleteProductAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> UpdateProductAsync(Product product);
        Task<List<Product>> GetTopProductsAsync(int limit);
        Task<List<Product>> GetRecommendedProductsAsync(string userId, int limit);
        Task<List<Product>> GetRelatedProductsAsync(int id,int subCategoryId);
    }
}