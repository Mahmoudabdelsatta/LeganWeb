using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Models
{
    public class ClickAction
    {
       
        public ClickAction()
        {
            this.Alerts = new HashSet<Alert>();
        }

        public int Id { get; set; }
        public string Click_Action { get; set; }

      
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}