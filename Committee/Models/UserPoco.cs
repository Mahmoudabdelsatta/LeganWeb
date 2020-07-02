using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class UserPoco
    {

        public UserPoco()
        {
            this.Alerts = new HashSet<Alert>();
        }

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string Title { get; set; }
        public Nullable<int> UserRole { get; set; }
        public string UserPassword { get; set; }
        public string UserEmailId { get; set; }
        public string UserImage { get; set; }
        public Nullable<int> SystemRole { get; set; }
        public string WorkSide { get; set; }
        public Nullable<int> ManagerOfDepartment { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string FCMToken { get; set; }
        public string DeviceType { get; set; }

        public virtual Role Role { get; set; }
        public virtual SystemRole SystemRoleMap { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}