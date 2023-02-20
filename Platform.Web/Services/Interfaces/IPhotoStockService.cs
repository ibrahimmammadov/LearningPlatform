using Platform.Web.Models.PhotoStocks;

namespace Platform.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoVm> UploadPhoto (IFormFile photo);
        Task<bool> DeletePhoto (string photoUrl);
    }
}
