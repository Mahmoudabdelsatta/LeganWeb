using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class ChatMessage
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CreatedAt { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public int CommitteeId { get; set; }

        public string Type { get; set; }
        public Nullable<long> Extra { get; set; }

    }
}