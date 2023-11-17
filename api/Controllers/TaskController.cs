using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public TaskController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _context.Task.ToListAsync();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasksByUser(int Id)
        {
            //IEnumerable<Models.Task
            if (Id == null)
            {
                return BadRequest(string.Empty);
            }
            else
            {
                var user = await _context.User.Include(t => t.Task).FirstOrDefaultAsync(x=>x.Id==Id);
                if (user == null)
                {
                    return NotFound();
                }
                var e = user.Task.ToList();
                return Ok(e);
            }
        }
        [HttpPost("{name},{desc},{createDate},{setDate},{userID}")]
        public async Task<ActionResult> CreateQuest(string name, string desc,string createDate, string setDate ,int userID)
        {
            var user = await _context.User.FirstOrDefaultAsync(x=>x.Id==userID);
            if (user == null || name == null || desc == null || createDate == null|| setDate==null)
            {
                return BadRequest(string.Empty); ;
            }
            var task = new Models.Task { Name = name, Description = desc, User = user, CreateDate = createDate, SetDate = setDate, IsCompleted = false };
            _context.Task.Add(task);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id},{status}")]
        public async Task<ActionResult> UpdateTaskStatus(int id, bool status)
        {
            if (id == null || status == null)
            {
                return BadRequest(string.Empty);
            }
            else
            { 
                var task = _context.Task.Find(id);
                if (task == null)
                {
                    return NotFound();
                }
                task.IsCompleted = status;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            if (id == null)
            {
                return BadRequest(string.Empty);
            }
            else
            {
                var task = await _context.Task.FindAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                _context.Task.Remove(task);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
