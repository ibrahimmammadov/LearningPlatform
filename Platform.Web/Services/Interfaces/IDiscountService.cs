using Platform.Web.Models.Discounts;

namespace Platform.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountVm> GetDiscount(string discountCode);
    }
}
