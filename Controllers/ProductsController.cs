using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;
using Inventory_Management.Dto;

namespace Inventory_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly InventoryManagementContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(InventoryManagementContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromForm] ProductCreateDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid Product Dto");
            }

            if (productDto.ProductImage == null)
            {
                return NotFound("Product image is required.");
            }

            var ext = Path.GetExtension(productDto.ProductImage.FileName).ToLower();
            var size = productDto.ProductImage.Length;

            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png"))
            {
                return BadRequest("Only .jpg, .png and .jpeg are allowed");
            }

            if (size > 5000000)
            {
                return BadRequest("Maximum size allowed is 5 MB");
            }

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "images");
            Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + "_" + productDto.ProductImage.FileName;
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.ProductImage.CopyToAsync(stream);
            }

            // Create product object
            var product = new Product()
            {
                Name = productDto.Name,
                ProductCode = productDto.ProductCode,
                UnitPrice = productDto.UnitPrice,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                CurrentStock = productDto.CurrentStock,
                MinimumStock = productDto.MinimumStock,
                ProductImage = fileName
            };

            // Save to DB so ProductId gets generated
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return created product with generated ID
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }


        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest("Product ID mismatch");

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.ProductCode = product.ProductCode;
            existingProduct.Description = product.Description;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.CurrentStock = product.CurrentStock;
            existingProduct.MinimumStock = product.MinimumStock;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ProductImage = product.ProductImage;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpGet("dropdown/product")]
        public async Task<ActionResult<IEnumerable<object>>> GetCategory()
        {
            return await _context.Products
                .Select(c => new { c.CategoryId, c.Name })
                .ToListAsync();
        }
    }
}
