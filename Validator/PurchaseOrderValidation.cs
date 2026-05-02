using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class PurchaseOrderValidation : AbstractValidator<PurchaseOrder>
    {
        public PurchaseOrderValidation()
        {
            RuleFor(x => x.SupplierId)
           .NotNull().WithMessage("Supplier is required.");

            RuleFor(x => x.OrderDate)
                .NotNull().WithMessage("Order date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Order date cannot be in the future.");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount is required.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");
        }
    }
}
