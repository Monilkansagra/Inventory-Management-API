using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Inventory_Management.Models;

public partial class StockTransaction
{
    public int TransactionId { get; set; }

    public int? ProductId { get; set; }

    public int QuantityChange { get; set; }

    public string? TransactionType { get; set; }

    public int? ReferenceId { get; set; }

    public DateTime? TransactionDate { get; set; }
    [JsonIgnore]
    public virtual Product? Product { get; set; }
    
}
