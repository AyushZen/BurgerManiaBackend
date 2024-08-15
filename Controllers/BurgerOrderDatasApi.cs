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
    public class BurgerOrderDatasApi : ControllerBase
    {
        private readonly BurgerManiaDbContext _context;

        public BurgerOrderDatasApi(BurgerManiaDbContext context)
        {
            _context = context;
        }

        // GET: api/BurgerOrderDatasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BurgerOrderData>>> GetBurgerOrderDatas()
        {
            return await _context.BurgerOrderDatas.ToListAsync();
        }

        // GET: api/BurgerOrderDatasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BurgerOrderData>> GetBurgerOrderData(Guid id)
        {
            var burgerOrderData = await _context.BurgerOrderDatas.FindAsync(id);

            if (burgerOrderData == null)
            {
                return NotFound();
            }

            return burgerOrderData;
        }

        // PUT: api/BurgerOrderDatasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBurgerOrderData(Guid id, BurgerOrderData burgerOrderData)
        {
            if (id != burgerOrderData.BurgerId)
            {
                return BadRequest();
            }

            _context.Entry(burgerOrderData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerOrderDataExists(id))
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

        // POST: api/BurgerOrderDatasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BurgerOrderData>> PostBurgerOrderData(BurgerOrderData burgerOrderData)
        {
            _context.BurgerOrderDatas.Add(burgerOrderData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBurgerOrderData", new { id = burgerOrderData.BurgerId }, burgerOrderData);
        }

        // DELETE: api/BurgerOrderDatasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBurgerOrderData(Guid id)
        {
            var burgerOrderData = await _context.BurgerOrderDatas.FindAsync(id);
            if (burgerOrderData == null)
            {
                return NotFound();
            }

            _context.BurgerOrderDatas.Remove(burgerOrderData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BurgerOrderDataExists(Guid id)
        {
            return _context.BurgerOrderDatas.Any(e => e.BurgerId == id);
        }
    }
}
