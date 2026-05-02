using System.Diagnostics.Metrics;
using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class CategoriesValidation : AbstractValidator<Category>
    {
        public CategoriesValidation()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Category name must not be empty.")
                .Length(3, 20).WithMessage("Category name must be between 3 and 20 characters.")
                .Matches("^[A-Za-z ]*$").WithMessage("Category name must contain only letters and spaces.");

            //RuleFor(c => c.CountryCode)
            //    .NotNull().WithMessage("Country code must not be empty.")
            //    .MaximumLength(4).WithMessage("Country code must not exceed 4 characters.")
            //    .Matches("^[A-Za-z]{2,4}$").WithMessage("Country code must contain only letters and be between 2 to 4 characters.");
        }
    }
}
