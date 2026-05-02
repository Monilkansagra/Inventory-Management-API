using System;
using System.Collections.Generic;

namespace Inventory_Management.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Supplier? Supplier { get; set; }
}
