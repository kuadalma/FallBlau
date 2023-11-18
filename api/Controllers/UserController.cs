using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet("{email},{pass}")]
        public async Task<ActionResult<User>> LoginUser(string email, string pass)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                return BadRequest(string.Empty);
            }
            else
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    return BadRequest("hasło lub email nie poprawne");
                }
                if (user.Password != pass)
                {
                    return BadRequest("hasło lub email nie poprawne");
                }
                return user;
            }
        }

        [HttpPost("{nazwa},{email},{pass}")]
        public async Task<ActionResult<User>> CreateUser(string nazwa, string email, string pass)
        {
            if (string.IsNullOrEmpty(nazwa) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                return BadRequest(string.Empty);
            }
            else
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    _context.User.Add(new User { Name = nazwa, Email = email, Password = pass });
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
