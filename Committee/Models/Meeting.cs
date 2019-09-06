
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public string MeetingTitle { get; set; }
        public string MeetingDate { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string longitude { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public string proceedings { get; set; }
        public string MeetingAddress { get; set; }
        public string MeetingStatus { get; set; }
        public string AgendaTime { get; set; }
        public string AgendaDescription { get; set; }
        public string MinutesOfMeeting { get; set; }
        public Nullable<int> CommitteeId { get; set; }
        public string MeetingDesc { get; set; }
        public string MeetingTime { get; set; }
        public virtual ICollection<MeetingHistory> MeetingHistories { get; set; }

    }
}