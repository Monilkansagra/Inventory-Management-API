using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly InventoryManagementContext _context;

        public PurchaseOrderController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetAllPurchaseOrders()
        {
            return await _context.PurchaseOrders.ToListAsync();
        }

        // GET: api/PurchaseOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderById(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/PurchaseOrder
        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> CreatePurchaseOrder(PurchaseOrder order)
        {
            try
            {
                _context.PurchaseOrders.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = order.PurchaseOrderId }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PurchaseOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchaseOrder(int id, PurchaseOrder order)
        {
            if (id != order.PurchaseOrderId)
                return BadRequest("PurchaseOrder ID mismatch");

            var existing = await _context.PurchaseOrders.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.SupplierId = order.SupplierId;
            existing.OrderDate = order.OrderDate;
            existing.TotalAmount = order.TotalAmount;

            _context.PurchaseOrders.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PurchaseOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrder(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.PurchaseOrders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("dropdown")]
        public async Task<ActionResult<IEnumerable<object>>> GetSupplierDropdown()
        {
            return await _context.Suppliers
                .Select(s => new
                {
                    s.SupplierId,
                    s.Name
                })
                .ToListAsync();
        }
    }
}
