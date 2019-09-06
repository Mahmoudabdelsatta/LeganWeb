using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string Title { get; set; }
        public int SystemRole { get; set; }
        public string UserPassword { get; set; }
        public string UserEmailId { get; set; }
        public string UserImage { get; set; }
        public string Name { get; set; }
        public string WorkSide { get; set; }
        public string Gender { get; set; }
        public string ManagerOfDepartment { get; set; }
        public string FCMToken { get; set; }
        public string DeviceType { get; set; }
        public SystemRole SystemRoleMap { get; set; }
        public   Department Department { get; set; }

    }
}