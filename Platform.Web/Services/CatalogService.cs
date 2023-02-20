using Platform.Web.Helpers;
using Platform.Web.Models.Catalog;
using Platform.Web.Services.Interfaces;
using Shared.DTOs;

namespace Platform.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateVm courseCreateVm)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateVm.PhotoFormFile);
            if (resultPhotoService is not null)
            {
                courseCreateVm.Picture = resultPhotoService.Url;
            }
           var response = await _httpClient.PostAsJsonAsync<CourseCreateVm>("course",courseCreateVm);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId,string photourl)
        {

            var response = await _httpClient.DeleteAsync($"course/{ courseId}");
            if (response.IsSuccessStatusCode)
            {
               await _photoStockService.DeletePhoto(photourl);
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryVm>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("category");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryVm>>>();
            return responseSuccess.Data;
        }

        public async Task<List<CourseVm>> GetAllCourseAsync()
        {
            var response =await _httpClient.GetAsync("course");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVm>>>();
            responseSuccess.Data.ForEach(x => x.StockPictureUrl = _photoHelper.GetPhotoUrl(x.Picture));
            return responseSuccess.Data;
        }

        public async Task<List<CourseVm>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVm>>>();
            responseSuccess.Data.ForEach(x => x.StockPictureUrl = _photoHelper.GetPhotoUrl(x.Picture));
            return responseSuccess.Data;
        }

        public async Task<CourseVm> GetByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"course/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseVm>>();
            responseSuccess.Data.StockPictureUrl = _photoHelper.GetPhotoUrl(responseSuccess.Data.Picture);

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateVm courseUpdateVm)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateVm.PhotoFormFile);
            if (resultPhotoService is not null)
            {
                await _photoStockService.DeletePhoto(courseUpdateVm.Picture);
                courseUpdateVm.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateVm>("course", courseUpdateVm);
            return response.IsSuccessStatusCode;
        }
    }
}
