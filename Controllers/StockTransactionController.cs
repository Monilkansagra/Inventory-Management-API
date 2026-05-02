using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockTransactionController : ControllerBase
    {
        private readonly InventoryManagementContext _context;

        public StockTransactionController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: api/StockTransaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockTransaction>>> GetAllTransactions()
        {
            return await _context.StockTransactions
                                 .Include(t => t.Product) // load related Product info
                                 .ToListAsync();
        }

        // GET: api/StockTransaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockTransaction>> GetTransactionById(int id)
        {
            var transaction = await _context.StockTransactions
                                            .Include(t => t.Product)
                                            .FirstOrDefaultAsync(t => t.TransactionId == id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // POST: api/StockTransaction
        [HttpPost]
        public async Task<ActionResult<StockTransaction>> CreateTransaction([FromBody] StockTransaction transaction)
        {
            if (transaction.ProductId == null ||
                transaction.QuantityChange == 0 ||
                string.IsNullOrEmpty(transaction.TransactionType))
            {
                return BadRequest("ProductId, QuantityChange and TransactionType are required.");
            }

            // validate product
            var product = await _context.Products.FindAsync(transaction.ProductId);
            if (product == null)
                return BadRequest("Invalid ProductId. Product not found.");

            transaction.TransactionDate ??= DateTime.Now;

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactionById),
                                   new { id = transaction.TransactionId },
                                   transaction);
        }

        // PUT: api/StockTransaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] StockTransaction transaction)
        {
            if (id != transaction.TransactionId)
                return BadRequest("Transaction ID mismatch.");

            var existing = await _context.StockTransactions.FindAsync(id);
            if (existing == null)
                return NotFound();

            // update fields
            existing.ProductId = transaction.ProductId;
            existing.QuantityChange = transaction.QuantityChange;
            existing.TransactionType = transaction.TransactionType;
            existing.TransactionDate = transaction.TransactionDate ?? DateTime.Now;

            _context.StockTransactions.Update(existing);
            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/StockTransaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.StockTransactions.FindAsync(id);
            if (transaction == null)
                return NotFound();

            _context.StockTransactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("dropdown")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductDropdown()
        {
            return await _context.Products
                .Select(p => new
                {
                    p.ProductId,
                    p.Name
                })
                .ToListAsync();
        }
    }
}
