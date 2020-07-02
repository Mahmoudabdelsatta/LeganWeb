using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class CommitteesMember
    {
        public int CommitteeMemberId { get; set; }
        public int CommitteeId { get; set; }
        public int MemberId { get; set; }
        public string CommitteeDate { get; set; }
        public Nullable<bool> MemberWillAttend { get; set; }
        public Nullable<bool> AcceptProceedings { get; set; }
        public string ReasonOfRejection { get; set; }
        public string AgendamodifingSuggession { get; set; }
        public string AlertTime { get; set; }
        public string MeetingAddress { get; set; }
        public string MeetingTitle { get; set; }
        public string MeetingStatus { get; set; }
        public string AgendaTime { get; set; }
        public string AgendaDescription { get; set; }
        public string MeetingDate { get; set; }
        public Nullable<int> MeetingId { get; set; }
        public Nullable<bool> IsMemberAcceptedMiutesOfCommittee { get; set; }
        public string MemberSignature { get; set; }
        public Nullable<int> AlertId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public Nullable<int> CommitteeRole { get; set; }
        public Nullable<bool>IsMinuteAccepted { get; set; }
        public virtual User User { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual Committee Committee { get; set; }
        public virtual SystemRole SystemRole { get; set; }
    }
}