using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Mail;
using System.Text; 

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userSeviceInterestController : Controller
    {
        private DBContext _dBContext;

        public userSeviceInterestController(DBContext context)
        {
            _dBContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserSeviceInterest>> Get()
        {
            return _dBContext.UserSeviceInterests.Include(i=>i.User).Include(i=>i.Service).Include(i=>i.Interest).ToList();
        }

        [HttpPost]
        public ActionResult Save([FromBody]UserSeviceInterest userSeviceInterest)
        {
            if (userSeviceInterest == null)
            {
                return NotFound("data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dBContext.UserSeviceInterests.Add(userSeviceInterest);
            _dBContext.SaveChanges();
            var userData = _dBContext.UserSeviceInterests.Include(i=>i.User).Include(i=>i.Service).Include(i=>i.Interest).Where(i => i.User.UserId == userSeviceInterest.UserId).ToList();
            var val = SendEmailToUser(userData);
            return Ok(val);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            UserSeviceInterest userSeviceInterest = _dBContext.UserSeviceInterests.FirstOrDefault(s => s.UserSeviceInterestID == id);
            if (userSeviceInterest == null)
            {
                return NotFound("No User Sevice Interest found with particular id supplied");
            }
            _dBContext.UserSeviceInterests.Remove(userSeviceInterest);
            await _dBContext.SaveChangesAsync();
            return Ok("User Sevice Interest is deleted sucessfully.");
        }
        public JsonResult SendEmailToUser(List<UserSeviceInterest> models )
        {
            string htmlString = "";
            if (models.Count > 0 && models != null)
            {
                htmlString = "<p> Hi " + models[0].User.Name + " Thank you for your Submission,</p></br><p> Your Email: "
                    + models[0].User.Email + ",</p></br><p> Your Phone Namber: " + models[0].User.Mobile + "</br></p>";
                var count = 0;
                foreach (var model in models)
                {
                    count++;
                    htmlString += "<p>You Have Selected: " + count.ToString() + "- " + model.Service.ServiceName + " and you are " + model.Interest.InterestName  + " on it</pr><br/>";
                }
            }
            var result = true;
            result = SendEmail( models[0].User.Email, "You Have Submitted Successfully", htmlString);
            return Json(result);
        }
          public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string SenderEmail = "areej.obaid17894@gmail.com";
                string SenderPassword = "QweAsdZxc123-";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 100000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(SenderEmail, SenderPassword);

                MailMessage mailMessage = new MailMessage(SenderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }
        ~userSeviceInterestController()
        {
            _dBContext.Dispose();
        }
    }
}
