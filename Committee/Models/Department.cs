using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    [Serializable]
    public class Department
    {
        public int DeptId { get; set; }
        public  string DeptName { get; set; }
        public string DeptAddress { get; set; }
    }
}