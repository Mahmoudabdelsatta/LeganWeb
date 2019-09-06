using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberTitle { get; set; }
        public string MemberAddress { get; set; }
        public string MemberPhone { get; set; }
        public string MemberMail { get; set; }
        public string MemberType { get; set; }
        public string MemberWorkSide1 { get; set; }
        public string MemberWorkSide2 { get; set; }
        public string MemberWorkSide3 { get; set; }
    }
}