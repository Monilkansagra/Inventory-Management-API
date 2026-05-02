using System;
using System.Collections.Generic;

namespace Inventory_Management.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public int? CurrentStock { get; set; }

    public int? MinimumStock { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductImage { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
