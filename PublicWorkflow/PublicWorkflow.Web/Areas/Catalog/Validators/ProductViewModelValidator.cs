using PublicWorkflow.Web.Areas.Catalog.Models;
using FluentValidation;

namespace PublicWorkflow.Web.Areas.Catalog.Validators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Rate).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than 1");
        }
    }
}