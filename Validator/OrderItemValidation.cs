using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class OrderItemValidation : AbstractValidator<OrderItem>
    {
        public OrderItemValidation()
        {
            RuleFor(x => x.ProductId)
          .NotNull()
          .WithMessage("Product is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0.");

            // Optional: You can also check if either PurchaseOrderId or SalesOrderId is provided
            RuleFor(x => x)
                .Must(x => x.PurchaseOrderId.HasValue || x.SalesOrderId.HasValue)
                .WithMessage("Either PurchaseOrderId or SalesOrderId must be provided.");
        }
    }
}
