using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Views.Forms
{
    public partial class Notification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString("https://committeeapi20190806070934.azurewebsites.net/api/Committees/GetCommitteesForWeb");
                List<Committee.Models.Committee> committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Committee>>(result);
                ddlCommitteeSpecified.DataSource = committees;
                ddlCommitteeSpecified.DataTextField = "CommitteeName";
                ddlCommitteeSpecified.DataValueField = "CommitteeId";
                ddlCommitteeSpecified.DataBind();

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int committeeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value);
            ViewState["committeeId"] = committeeId;
            string apiUrl2 = "https://committeeapi20190806070934.azurewebsites.net/api/CommitteesMembers";
            WebClient members = new WebClient();
            members.Headers["Content-type"] = "application/json";
            members.Encoding = Encoding.UTF8;
            List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(members.DownloadString(apiUrl2 + "/GetCommitteesMember?id=" + committeeId));
                 foreach (ListItem item in ChkSentWay.Items)
                {
                    if (item.Selected)
                    {
                        string selectedValue = item.Value;
                        if (selectedValue == "1")
                        {
                        Utilities.SendMail(users);
                        }
                        if (selectedValue == "2")
                        {
                            // sendsms(users)
                        }
                        if (selectedValue == "3")
                        {
                          int status=Utilities.SendAlert(committeeId, users);
                        }
                        // sendemail(users)
                    }
               

            }
        }
    }
        
}