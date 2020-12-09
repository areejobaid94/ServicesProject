using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private DBContext _dBContext;

        public ServiceController(DBContext context)
        {
            _dBContext = context;
        }

        [HttpGet]          
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return _dBContext.Services.ToList();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]Service service)
        {
            if (service == null)
            {
                return NotFound("Service data is not supplied");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Service existingService = _dBContext.Services.FirstOrDefault(s => s.ServiceId == service.ServiceId);
            if (existingService == null)
            {
                return NotFound("Service does not exist in the database");
            }
            existingService.ServiceName = service.ServiceName;
            _dBContext.Attach(existingService).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return Ok(existingService);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            Service service = _dBContext.Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null)
            {
                return NotFound("No service found with particular id supplied");
            }
            _dBContext.Services.Remove(service);
            await _dBContext.SaveChangesAsync();
            return Ok("service is deleted sucessfully.");
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Service service)
        {
            if (service == null)
            {
                return NotFound("service data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dBContext.Services.AddAsync(service);
            await _dBContext.SaveChangesAsync();
            return Ok(service);
        }
        ~ServiceController()
        {
            _dBContext.Dispose();
        }
    }
}
