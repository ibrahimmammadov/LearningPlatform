using FluentValidation;
using Platform.Web.Models.Discounts;

namespace Platform.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("This area must be full");
        }
    }
}
