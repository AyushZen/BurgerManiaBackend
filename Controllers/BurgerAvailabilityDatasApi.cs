using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_BurgerManiaBackend.Data;
using API_BurgerManiaBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_BurgerManiaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerAvailabilityDatasApi : ControllerBase
    {
        private readonly BurgerManiaDbContext _context;

        public BurgerAvailabilityDatasApi(BurgerManiaDbContext context)
        {
            _context = context;
        }

        // GET: api/BurgerAvailabilityDatasApi
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<BurgerAvailabilityData>>> GetBurgerDatas()
        {
            return await _context.BurgerDatas.ToListAsync();
        }

        // GET: api/BurgerAvailabilityDatasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BurgerAvailabilityData>> GetBurgerAvailabilityData(string id)
        {
            var burgerAvailabilityData = await _context.BurgerDatas.FindAsync(id);

            if (burgerAvailabilityData == null)
            {
                return NotFound();
            }

            return burgerAvailabilityData;
        }

        // PUT: api/BurgerAvailabilityDatasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBurgerAvailabilityData(string id, BurgerAvailabilityData burgerAvailabilityData)
        {
            if (id != burgerAvailabilityData.BurgerId)
            {
                return BadRequest();
            }

            _context.Entry(burgerAvailabilityData).State = EntityState.Modified;

            try
            {   
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerAvailabilityDataExists(id))
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

        // POST: api/BurgerAvailabilityDatasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BurgerAvailabilityData>> PostBurgerAvailabilityData(BurgerAvailabilityData burgerAvailabilityData)
        {
            _context.BurgerDatas.Add(burgerAvailabilityData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BurgerAvailabilityDataExists(burgerAvailabilityData.BurgerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBurgerAvailabilityData", new { id = burgerAvailabilityData.BurgerId }, burgerAvailabilityData);
        }

        // DELETE: api/BurgerAvailabilityDatasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBurgerAvailabilityData(string id)
        {
            var burgerAvailabilityData = await _context.BurgerDatas.FindAsync(id);
            if (burgerAvailabilityData == null)
            {
                return NotFound();
            }

            _context.BurgerDatas.Remove(burgerAvailabilityData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BurgerAvailabilityDataExists(string id)
        {
            return _context.BurgerDatas.Any(e => e.BurgerId == id);
        }
    }
}
