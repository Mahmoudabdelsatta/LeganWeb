
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class Meeting
    {
        public Meeting()
        {
            this.Agenda = new HashSet<Agendum>();
            this.MeetingHistories = new HashSet<MeetingHistory>();
            this.CommitteesMembers = new HashSet<CommitteesMember>();
        }

        public int MeetingId { get; set; }
        public string MeetingTitle { get; set; }
        public Nullable<int> CommitteeId { get; set; }
        public string MeetingDate { get; set; }
        public string Latitude { get; set; }
        public string longitude { get; set; }
        public string MeetingDesc { get; set; }
        public Nullable<int> Status { get; set; }
        public string proceedings { get; set; }
        public string MeetingAddress { get; set; }
        public string AgendaTime { get; set; }
        public string AgendaDescription { get; set; }
        public string MinutesOfMeeting { get; set; }
        public string MeetingTime { get; set; }
        public Nullable<int> MeetingHistory { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public virtual ICollection<Agendum> Agenda { get; set; }
        public virtual ICollection<MeetingHistory> MeetingHistories { get; set; }
        public virtual ICollection<CommitteesMember> CommitteesMembers { get; set; }

    }
}