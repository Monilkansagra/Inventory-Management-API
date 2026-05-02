using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.ContactEmail).EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^[0-9]\d{9}$").WithMessage("Invalid phone number. Must be a 10-digit Indian number.");
            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MinimumLength(5).WithMessage("Address must be at least 5 characters long.");
        }
    }

}
