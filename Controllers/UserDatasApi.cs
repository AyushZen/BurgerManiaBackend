using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_BurgerManiaBackend.Data;
using API_BurgerManiaBackend.Models;
using API_BurgerManiaBackend.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_BurgerManiaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDatasApi : ControllerBase
    {
        private readonly BurgerManiaDbContext _context;
        private readonly ITokenService _tokenService;

        public UserDatasApi(BurgerManiaDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/UserDatasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUserDatas()
        {
            return await _context.UserDatas.ToListAsync();
        }

        // GET: api/UserDatasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserData(Guid id)
        {
            var userData = await _context.UserDatas.FindAsync(id);

            if (userData == null)
            {
                return NotFound();
            }

            return userData;
        }

        // GET: api/GetUserByMobileNo/{mobileNo} to get user by mobile number
        [HttpGet("GetUserByMobileNo/{mobileNo}")]
        public IActionResult GetUserByMobileNo(string mobileNo)
        {
            Console.WriteLine($"mobileNo: {mobileNo}");
            // Assuming you have a DbSet<User> in your DbContext
            var user = _context.UserDatas.FirstOrDefault(u => u.Number == mobileNo);

            if (user == null)
            {
                return NotFound($"User with mobile number {mobileNo} not found.");
            }
            var token = _tokenService.GenerateToken(mobileNo);
            return Ok(new { user, Token = token }); // Return the user as JSON
        }

        // PUT: api/UserDatasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserData(Guid id, UserData userData)
        {
            if (id != userData.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/UserDatasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserData>> PostUserData(UserData userData)
        {
            _context.UserDatas.Add(userData);
            await _context.SaveChangesAsync();
            var token = _tokenService.GenerateToken(userData.Number);
            return CreatedAtAction("GetUserData", new { id = userData.UserId }, new { userData , Token = token});
        }

        // DELETE: api/UserDatasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(Guid id)
        {
            var userData = await _context.UserDatas.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }

            _context.UserDatas.Remove(userData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDataExists(Guid id)
        {
            return _context.UserDatas.Any(e => e.UserId == id);
        }
    }
}
