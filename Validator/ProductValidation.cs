using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Product name is required.")
           .Length(3, 50).WithMessage("Product name must be between 3 and 50 characters.")
           .Matches(@"^[A-Za-z0-9\s\-]+$").WithMessage("Product name can contain letters, numbers, spaces, and hyphens only.");

            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Product code is required.")
                .Length(3, 20).WithMessage("Product code must be between 3 and 20 characters.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("Product code must be uppercase letters, numbers, or hyphens.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

            RuleFor(x => x.CurrentStock)
                .GreaterThanOrEqualTo(0).WithMessage("Current stock cannot be negative.")
                .When(x => x.CurrentStock.HasValue);

            RuleFor(x => x.MinimumStock)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum stock cannot be negative.")
                .When(x => x.MinimumStock.HasValue);

            RuleFor(x => x.CategoryId)
            .NotNull().WithMessage("Category must be selected.");
        }
    }
}
