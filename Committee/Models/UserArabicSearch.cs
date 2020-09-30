using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    [Serializable]
    public class UserArabicSearch
    {
        public int الرقم { get; set; }
        public string الاسم { get; set; }
        public string  رقم_الجوال { get; set; }
        public string العنوان { get; set; }
      //  public Nullable<bool> IsConfirmed { get; set; }
        public string الوظيفه { get; set; }
        public string الصلاحيات { get; set; }
      //  public string UserPassword { get; set; }
        public string البريد_اللإلكترونى { get; set; }
        //public string  { get; set; }
        public string اسم_المستخدم { get; set; }
        public string جهة_العمل { get; set; }
      //  public string النوع { get; set; }
     //   public string الادارة { get; set; }
        public  SystemRole SystemRoleMap { get; set; }
        public  Department Department { get; set; }

    }
}