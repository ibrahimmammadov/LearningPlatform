using FluentValidation;
using Platform.Web.Models.Catalog;

namespace Platform.Web.Validators
{
    public class CourseUpdateValidator : AbstractValidator<CourseUpdateVm>
    {
        public CourseUpdateValidator()
        {
            RuleFor(x => x.Price).NotEmpty().ScalePrecision(2, 6).WithMessage("Invalid price format");
        }
    }
}
