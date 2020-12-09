using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : Controller
    {
        private DBContext _dBContext;

        public InterestController(DBContext context)
        {
            _dBContext = context;
        }

        [HttpGet]          
        public ActionResult<IEnumerable<Interest>> GetInterests()
        {
            return _dBContext.Interests.ToList();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Interest interest)
        {
            if (interest == null)
            {
                return NotFound("interest data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dBContext.Interests.AddAsync(interest);
            await _dBContext.SaveChangesAsync();
            return Ok(interest);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]Interest interest)
        {
            if (interest == null)
            {
                return NotFound("Interest data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Interest existingInterest = _dBContext.Interests.FirstOrDefault(s => s.InterestId == interest.InterestId);
            if (existingInterest == null)
            {
                return NotFound("Interest does not exist in the database");
            }
            existingInterest.InterestName = interest.InterestName;
            _dBContext.Attach(existingInterest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return Ok(existingInterest);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            Interest interest = _dBContext.Interests.FirstOrDefault(s => s.InterestId == id);
            if (interest == null)
            {
                return NotFound("No Interest found with particular id supplied");
            }
            _dBContext.Interests.Remove(interest);
            await _dBContext.SaveChangesAsync();
            return Ok("Interest is deleted sucessfully.");
        }

        ~InterestController()
        {
            _dBContext.Dispose();
        }
    }
}
