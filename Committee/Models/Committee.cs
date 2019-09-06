using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class Committee
    {
        public int  CommitteeId { get; set; }
        public string CommitteeManager { get; set; }
            public string CommitteeName { get; set; }
            public string CommitteeSecretary { get; set; }
           
            public string MinutesOfTheCommittee { get; set; }
            public string CommitteePDFView { get; set; }
            public string CommitteeDate { get; set; }
            public string CommitteeTopic { get; set; }
            public string CommitteeBasedOn { get; set; }
            public string CommitteeInbox1 { get; set; }
            public string CommitteeInbox2 { get; set; }
            public string CommitteeInbox3 { get; set; }
            public string CommitteeEnrollmentNumber { get; set; }
            public string CommitteeEnrollmentDate { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public int ActivityId { get; set; }
        public int ImportanceId { get; set; }
        public int TypeId { get; set; }
        public string DeptId { get; set; }
    }
    }
