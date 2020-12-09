using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Interest
    {
        public int InterestId { get; set; }

        [Required]
        public string InterestName { get; set; }

        public ICollection<UserSeviceInterest> UserSeviceInterests { get; set; }
    }
}