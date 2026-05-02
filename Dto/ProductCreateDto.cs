namespace Inventory_Management.Dto
{
    public class ProductCreateDto
    {
       

        public string Name { get; set; } = null!;

        public string ProductCode { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int? CurrentStock { get; set; }

        public int? MinimumStock { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }
    }

}
