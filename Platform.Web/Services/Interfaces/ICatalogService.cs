using Platform.Web.Models.Catalog;

namespace Platform.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseVm>> GetAllCourseAsync();
        Task<List<CategoryVm>> GetAllCategoryAsync();
        Task<List<CourseVm>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseVm> GetByCourseIdAsync(string courseId);
        Task<bool> CreateCourseAsync(CourseCreateVm courseCreateVm);
        Task<bool> UpdateCourseAsync(CourseUpdateVm courseUpdateVm);
        Task<bool> DeleteCourseAsync(string courseId,string photophotourl);

       
    }
}
