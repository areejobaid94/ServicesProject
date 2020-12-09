using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        
        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Name { get; set; }
         
        [Required]
        public string Email { get; set; }

        public ICollection<UserSeviceInterest> UserSeviceInterests { get; set; }
    }
    
}