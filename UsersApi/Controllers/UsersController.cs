using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Data;
using UsersApi.Models;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersDbContext _users;

        public UsersController(UsersDbContext users)
        {
            _users = users;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_users.Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var user = await _users.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("No user found with that ID");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostAsync([FromBody] User user)
        {
            await _users.Users.AddAsync(user);
            await _users.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetAsync),
                new
                {
                    id = user.Id, email = user.Email, firstName = user.FirstName, lastName = user.LastName,
                    phone = user.Phone
                }, user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _users.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound("No user found with that ID");
            }

            _users.Users.Remove(user);
            await _users.SaveChangesAsync();

            return NoContent();
        }
    }
}