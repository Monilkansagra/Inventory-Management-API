using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class SalesOrderValidation : AbstractValidator<SalesOrder>
    {
        public SalesOrderValidation()
        {
            RuleFor(x => x.CustomerId)
            .NotNull().WithMessage("Customer is required.");

            RuleFor(x => x.OrderDate)
                .NotNull().WithMessage("Order date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Order date cannot be in the future.");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount is required.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");
        }
    }
}
