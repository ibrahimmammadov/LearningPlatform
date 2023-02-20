using Platform.Web.Models.Baskets;

namespace Platform.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketVm basketViewModel);

        Task<BasketVm> Get();

        Task<bool> Delete();

        Task AddBasketItem(BasketItemVm basketItemViewModel);

        Task<bool> RemoveBasketItem(string courseId);

        Task<bool> ApplyDiscount(string discountCode);

        Task<bool> CancelApplyDiscount();
    }
}
