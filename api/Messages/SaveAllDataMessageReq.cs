using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models;
namespace api.Messages
{
    public class SaveAllDataMessageReq
    {
        public int UserId { get; set; }
        public int InterestId { get; set; }
        public List<string> ServiceIds { get; set; }
    }
}