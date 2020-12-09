using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class UserSeviceInterest
    {
        public int UserSeviceInterestID{ get; set; }
        public int  UserId { get; set; }
        public User User { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }

    }
    
}