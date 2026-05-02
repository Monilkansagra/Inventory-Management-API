using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Supplier name is required.")
            .Length(3, 50).WithMessage("Supplier name must be between 3 and 50 characters.")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Supplier name must contain only letters and spaces.");

            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address cannot be longer than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));
        }
    }
}
