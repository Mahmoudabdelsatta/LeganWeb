using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    [Serializable]
    public class DepartmentArabicSearch
    {
        public int? رقم_الإدارة { get; set; }

        public string اسم_الإدارة { get; set; }
        public string عنوان_الإدارة { get; set; }
    }
}