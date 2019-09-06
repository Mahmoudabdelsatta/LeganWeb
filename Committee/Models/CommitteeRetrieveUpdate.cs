using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class CommitteeRetrieveUpdate
    {
        public int CommitteeId { get; set; }
        public string CommitteeManager { get; set; }
        public string CommitteeName { get; set; }
        public string CommitteeSecretary { get; set; }
        public string CommitteeDate { get; set; }
        public string CommitteeTopic { get; set; }
        public string CommitteeBasedOn { get; set; }
        public string CommitteeInbox1 { get; set; }
        public string CommitteeEnrollmentNumber { get; set; }
        public string CommitteeEnrollmentDate { get; set; }
        public User User { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual Importance Importance { get; set; }
        public virtual Type Type { get; set; }
        public virtual Department Department { get; set; }
    }
    }
