﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class CommitteeRetrieveData
    {
        public int CommitteeId { get; set; }
        public string CommitteeManager { get; set; }
        public string CommitteeName { get; set; }
        public string CommitteeSecretary { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsImportant { get; set; }
        public Nullable<bool> IsMilitarized { get; set; }
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
        public Nullable<int> DeptId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual List<User> User { get; set; }
        public virtual Importance Importance { get; set; }
        public virtual Type Type { get; set; }
        public virtual Department Department { get; set; }
    }
    }
