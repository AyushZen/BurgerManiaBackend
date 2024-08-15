using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_BurgerManiaBackend.Data;
using API_BurgerManiaBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_BurgerManiaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDatasApi : ControllerBase
    {
        private readonly BurgerManiaDbContext _context;

        public OrdersDatasApi(BurgerManiaDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdersDatasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersData>>> GetOrdersDatas()
        {
            return await _context.OrdersDatas.ToListAsync();
        }

        // GET: api/OrdersDatasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersData>> GetOrdersData(Guid id)
        {
            var ordersData = await _context.OrdersDatas.FindAsync(id);

            if (ordersData == null)
            {
                return NotFound();
            }

            return ordersData;
        }
        // GET : "user/{userId}"  - Get used orders using order id
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrdersData>>> GetOrdersDatas(Guid userId)
        {
            var orderData = await _context.OrdersDatas
                .Where(o => o.UserId == userId)
                .ToListAsync();

            if (orderData == null || !orderData.Any())
            {
                return NotFound();
            }

            return orderData;
        }

        // PUT: api/OrdersDatasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdersData(Guid id, OrdersData ordersData)
        {
            if (id != ordersData.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(ordersData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrdersDatasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrdersData>> PostOrdersData(OrdersData ordersData)
        {
            _context.OrdersDatas.Add(ordersData);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrdersData", new { id = ordersData.OrderId }, ordersData);
        }

        // DELETE: api/OrdersDatasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersData(Guid id)
        {
            var ordersData = await _context.OrdersDatas.FindAsync(id);
            if (ordersData == null)
            {
                return NotFound();
            }

            _context.OrdersDatas.Remove(ordersData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersDataExists(Guid id)
        {
            return _context.OrdersDatas.Any(e => e.OrderId == id);
        }
    }
}
