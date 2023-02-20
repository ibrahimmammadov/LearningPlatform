using AutoMapper;
using MongoDB.Driver;
using Service.Catalog.Dtos;
using Service.Catalog.Models;
using Service.Catalog.Settings;
using Shared.DTOs;

namespace Service.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categorycollect;
        private readonly IMapper _mapper;

        public CategoryService(IDataBaseSettings dataBaseSettings, IMapper mapper)
        {
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(dataBaseSettings.DatabaseName);
            _categorycollect = database.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categorycollect.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categorydto)
        {
           var category=_mapper.Map<Category>(categorydto);
            await _categorycollect.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categorycollect.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (category is null)
            {
                return Response<CategoryDto>.Fail("Category not found!", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
