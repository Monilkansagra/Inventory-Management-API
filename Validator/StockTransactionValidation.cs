using FluentValidation;
using Inventory_Management.Models;

namespace Inventory_Management.Validator
{
    public class StockTransactionValidation : AbstractValidator<StockTransaction>
    {
        public StockTransactionValidation()
        {
            RuleFor(x => x.ProductId)
            .NotNull().WithMessage("Product is required.");

            RuleFor(x => x.QuantityChange)
                .NotEqual(0).WithMessage("Quantity change must not be zero.");

            RuleFor(x => x.TransactionType)
                .NotEmpty().WithMessage("Transaction type is required.")
                .Must(type => type == "IN" || type == "OUT")
                .WithMessage("Transaction type must be either 'IN' or 'OUT'.");

            RuleFor(x => x.TransactionDate)
                .NotNull().WithMessage("Transaction date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Transaction date cannot be in the future.");
        }
    }
}
