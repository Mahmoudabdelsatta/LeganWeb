using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    [Serializable]

    public class MeetingSearch
    {
        public Nullable<int> الرقم { get; set; }
        public string عنوان_الاجتماع { get; set; }
        public string تاريخ_الاجتماع { get; set; }
        public string وقت_الاجتماع { get; set; }
        public string موقع_الاجتماع { get; set; }
        //public string وصف_الاجتماع { get; set; }
        public string حالة_الاجتماع { get; set; }
        public string اللجنة { get; set; }
      


}
}