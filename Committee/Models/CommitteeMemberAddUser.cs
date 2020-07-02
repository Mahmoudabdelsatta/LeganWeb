using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class CommitteeMemberAddUser
    {
        public int CommitteeId { get; set; }
        public int MemberId { get; set; }
    }
}