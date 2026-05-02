using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private readonly InventoryManagementContext _context;

        public SalesOrderController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: api/SalesOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrder>>> GetAllSalesOrders()
        {
            return await _context.SalesOrders.ToListAsync();
        }

        // GET: api/SalesOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrder>> GetSalesOrderById(int id)
        {
            var order = await _context.SalesOrders.FindAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/SalesOrder
        [HttpPost]
        public async Task<ActionResult<SalesOrder>> CreateSalesOrder(SalesOrder order)
        {
            try
            {
                _context.SalesOrders.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSalesOrderById), new { id = order.SalesOrderId }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SalesOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesOrder(int id, SalesOrder order)
        {
            if (id != order.SalesOrderId)
                return BadRequest("SalesOrder ID mismatch");

            var existing = await _context.SalesOrders.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.CustomerId = order.CustomerId;
            existing.OrderDate = order.OrderDate;
            existing.TotalAmount = order.TotalAmount;

            _context.SalesOrders.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/SalesOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            var order = await _context.SalesOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.SalesOrders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("dropdown")]
        public async Task<ActionResult<IEnumerable<object>>> GetCustomerDropdown()
        {
            return await _context.Customers
                .Select(c => new
                {
                    c.CustomerId,
                    c.Name
                })
                .ToListAsync();
        }
    }
}
    