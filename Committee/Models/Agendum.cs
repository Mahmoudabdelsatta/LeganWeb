using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class Agendum
    {
        public int id { get; set; }
      //  public string AgendaTime { get; set; }
        public string AgendaDesc { get; set; }
        public int MeetingId { get; set; }
        public string CreatedAt { get; set; }
    }
}