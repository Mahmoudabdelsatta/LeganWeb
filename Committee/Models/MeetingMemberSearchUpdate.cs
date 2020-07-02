using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class MeetingMemberSearchUpdate
    {
     
        public string MemberWillAttend { get; set; }
       
        public string IsMemberAcceptedMiutesOfCommittee { get; set; }
        public string MemberSignature { get; set; }
        public string RejectionReason { get; set; }

        public virtual User User { get; set; }

      
    }
}