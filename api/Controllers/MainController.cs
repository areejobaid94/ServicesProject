using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using System.Threading.Tasks;
using api.Messages;
namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : Controller
    {
        private DBContext _dBContext;

        public MainController(DBContext context)
        {
            _dBContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _dBContext.Users.ToList();
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public ActionResult<User> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("User id must be higher than zero");
            }
            User user = _dBContext.Users.FirstOrDefault(s => s.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]User user)
        {
            if (user == null)
            {
                return NotFound("User data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var saveUserMessage = new SaveUserMessageRes();
            await _dBContext.Users.AddAsync(user);
            await _dBContext.SaveChangesAsync();
            saveUserMessage.Services = _dBContext.Services.ToList();
            saveUserMessage.User = user;
            return Ok(saveUserMessage);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]User user)
        {
            if (user == null)
            {
                return NotFound("User data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User existingUser = _dBContext.Users.FirstOrDefault(s => s.UserId == user.UserId);
            if (existingUser == null)
            {
                return NotFound("User does not exist in the database");
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Mobile = user.Mobile;
            _dBContext.Attach(existingUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            User user = _dBContext.Users.FirstOrDefault(s => s.UserId == id);
            if (user == null)
            {
                return NotFound("No user found with particular id supplied");
            }
            _dBContext.Users.Remove(user);
            await _dBContext.SaveChangesAsync();
            return Ok("User is deleted sucessfully.");
        }

        ~MainController()
        {
            _dBContext.Dispose();
        }
    }
}
