using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IBrandRepository
    {

        Task<List<Brand>> GetAllAsync();

        Task<Brand?> GetBrandByIdAsync(int id);

        Task<Brand?> CreateBrandAsync(Brand brand);

        Task<Brand?> UpdateBrandAsync(Brand brand);

        Task<Brand?> DeleteBrandAsync(int id);

    }
}