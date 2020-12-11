using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace api.Models
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public ICollection<UserServiceInterest> UserServiceInterests { get; set; }
    }
}