using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class CommitteeSearchUpdate
    {
        public int رقم_اللجنه { get; set; }

        public string رئيس_اللجنه { get; set; }
            public string اسم_اللجنه { get; set; }
            public string سكرتير_اللجنه { get; set; }  
            public string تاريخ_اللجنه { get; set; }
            public string موضوع_اللجنه { get; set; }
            public string الأمر_المستند_عليه { get; set; }
            public string جهة_الوارد { get; set; } 
            public string رقم_القيد { get; set; }
            public string سنة_القيد { get; set; }
        public Activity حال_اللجنه { get; set; }
        public Importance مستوى_الأهميه{ get; set; }
        public Type تصنيف_اللجنه { get; set; }
        public Department الإداره { get; set; }
    }
    }
