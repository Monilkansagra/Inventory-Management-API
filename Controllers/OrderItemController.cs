using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly InventoryManagementContext _context;

        public OrderItemController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        // GET: api/OrderItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
                return NotFound();

            return Ok(orderItem);
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.OrderItemId }, orderItem);
        }

        // PUT: api/OrderItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.OrderItemId)
                return BadRequest("OrderItem ID mismatch");

            var existing = await _context.OrderItems.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.ProductId = orderItem.ProductId;
            existing.Quantity = orderItem.Quantity;
            existing.UnitPrice = orderItem.UnitPrice;
            existing.PurchaseOrderId = orderItem.PurchaseOrderId;
            existing.SalesOrderId = orderItem.SalesOrderId;

            _context.OrderItems.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OrderItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
                return NotFound();

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 🔹 Product dropdown
        [HttpGet("dropdown/product")]
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

        [HttpGet("dropdown/purchaseorder")]
        public async Task<ActionResult<IEnumerable<object>>> GetPurchaseOrderDropdown()
        {
            return await _context.PurchaseOrders
                .Select(po => new
                {
                    po.PurchaseOrderId
                })
                .ToListAsync();
        }
        [HttpGet("dropdown/salesorder")]
        public async Task<ActionResult<IEnumerable<object>>> GetSalesOrderDropdown()
        {
            return await _context.SalesOrders
                .Select(so => new
                {
                    so.SalesOrderId
                })
                .ToListAsync();
        }
    }
}
