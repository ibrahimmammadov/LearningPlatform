using Service.Catalog.Dtos;
using Service.Catalog.Models;
using Shared.DTOs;

namespace Service.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}