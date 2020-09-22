using Committee.Controller;
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
                string result = webClient.DownloadString(Utilities.BASE_URL+ "/api/Committees/GetCommitteesForWeb?deptId=" + Convert.ToInt32(Session["DeptId"]));
                List<Committee.Models.Committee> committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Committee>>(result);
                ddlCommitteeSpecified.DataSource = committees;
                ddlCommitteeSpecified.DataTextField = "CommitteeName";
                ddlCommitteeSpecified.DataValueField = "CommitteeId";
                ddlCommitteeSpecified.DataBind();

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int committeeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value);
                ViewState["committeeId"] = committeeId;
                string apiUrl2 = Utilities.BASE_URL + "/api/CommitteesMembers";
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
                            Utilities.SendMail(users, textMessage.Text);
                        }
                        if (selectedValue == "2")
                        {
                            foreach (var user in users)
                            {
                                if (!string.IsNullOrEmpty(user.Phone))
                                {
                                    SMS.SendSms("+" + user.Phone, textMessage.Text);

                                }
                            }

                        }
                        if (selectedValue == "3")
                        {
                            int status = Utilities.SendAlert(committeeId, users, textMessage.Text);
                        }

                    }


                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم إرسال الرساله بنجاح');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('هناك خطأ فى إرسال الرساله');", true);


            }




        }
    }
        
}