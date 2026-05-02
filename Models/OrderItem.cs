using System;
using System.Collections.Generic;

namespace Inventory_Management.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int? PurchaseOrderId { get; set; }

    public int? SalesOrderId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual PurchaseOrder? PurchaseOrder { get; set; }

    public virtual SalesOrder? SalesOrder { get; set; }
}
