using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class MeetingHistory
    {
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public int MeetingId { get; set; }
        public string MeetingDate { get; set; }
        public string CreatedAt { get; set; }
    }
}