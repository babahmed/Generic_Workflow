using PublicWorkflow.Web.Areas.Catalog.Models;
using FluentValidation;

namespace PublicWorkflow.Web.Areas.Catalog.Validators
{
    public class ProcessConfigViewModelValidator : AbstractValidator<ProcessConfigViewModel>
    {
        public ProcessConfigViewModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 500 characters.");

           // RuleFor(p => p.Rate).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than 1");
        }
    }
}