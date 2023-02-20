using AutoMapper;
using MongoDB.Driver;
using Service.Catalog.Dtos;
using Service.Catalog.Models;
using Service.Catalog.Settings;
using Shared.DTOs;

namespace Service.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _coursecollect;
        private readonly IMongoCollection<Category> _categorycollect;
        private readonly IMapper _mapper;

        public CourseService(IDataBaseSettings dataBaseSettings, IMapper mapper)
        {
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(dataBaseSettings.DatabaseName);
            _coursecollect = database.GetCollection<Course>(dataBaseSettings.CourseCollectionName);
            _categorycollect = database.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var course = await _coursecollect.Find(course => true).ToListAsync();
            if (course.Any())
            {
                foreach (var item in course)
                {
                    item.Category = await _categorycollect.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                course = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(course), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _coursecollect.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course is null)
            {
                return Response<CourseDto>.Fail("Course not found!", 404);
            }
            course.Category = await _categorycollect.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userid)
        {
            var course = await _coursecollect.Find(course => course.UserId == userid).ToListAsync();
            if (course.Any())
            {
                foreach (var item in course)
                {
                    item.Category = await _categorycollect.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else course = new List<Course>();
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(course), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newcourse = _mapper.Map<Course>(courseCreateDto);
            newcourse.CreatedTime = DateTime.Now;
            await _coursecollect.InsertOneAsync(newcourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newcourse), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var update = _mapper.Map<Course>(courseUpdateDto);
            var result = await _coursecollect.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, update);
            if (result is null)
            {
                return Response<NoContent>.Fail("Course not found.", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _coursecollect.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else return Response<NoContent>.Fail("Course not found", 404);
        }
    }
}
