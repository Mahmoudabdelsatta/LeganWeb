using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class MeetingMembers
    {
        public string اسم_العضو { get; set; }

        public string حالة_الحضور { get; set; }
        public string قبول_المحضر { get; set; }
        public string صورة_التوقيع { get; set; }

    }
}