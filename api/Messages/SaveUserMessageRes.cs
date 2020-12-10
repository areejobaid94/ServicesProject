using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models;
namespace api.Messages
{
    public class SaveUserMessageRes
    {
        public User User { get; set; }

        public IList<Service> Services { get; set; }
    }
}