using Committee.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Views.Forms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static List<Committee.Models.Agendum> Agendas = new List<Committee.Models.Agendum>();

        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        [WebMethod]
        public List<int> GetActiveAndNonActive()
        {
            string apiUrl3 = "http://localhost:1481/api/Chart";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<int> Charts = (new JavaScriptSerializer()).Deserialize<List<int>>(client.DownloadString(apiUrl3 + "/GetActiveAndNonActiveCommittee"));
            return Charts;
        }
    }
}