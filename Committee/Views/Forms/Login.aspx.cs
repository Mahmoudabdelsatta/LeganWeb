using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Forms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
              


                string apiUrl = "https://committeeapi20190806070934.azurewebsites.net/api/CommitteesMembers";
            string apiUrl2 = "https://committeeapi20190806070934.azurewebsites.net/api/Users";

            object input = new
            {
                Name = txtuserName.Text.Trim(),
                Pass = txtPass.Text.Trim()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            WebClient client2 = new WebClient();
            client2.Headers["Content-type"] = "application/json";
            client2.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrl + "/ValidateUserNameAndPassword?userName=" + txtuserName.Text + "&password=" + txtPass.Text, inputJson);
                bool validate = (new JavaScriptSerializer()).Deserialize<bool>(json);
                
            if (validate)
                {
                    Committee.Models.User user = (new JavaScriptSerializer()).Deserialize<Committee.Models.User > (client2.DownloadString(apiUrl2 + "/GetUser?userName=" + txtuserName.Text.Trim().ToLower()+"&password="+txtPass.Text.Trim().ToLower()));
            Session["SystemRole"] = user.SystemRole;
                    Session["UserName"] = user.UserName;
                    Response.Redirect("Dashboard.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('دخول خطأ. تأكد من اسم المستخدم والرقم السرى')", true);

                }
            }
            catch (Exception ex )
            {

                Response.Write(ex.Message);

            }

        }
    }
}