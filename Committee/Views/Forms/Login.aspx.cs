using Committee.Controller;
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool validate = WebApiConsume.login(Utilities.BASE_URL+"/api/Users", txtuserName.Text, txtPass.Text, out string UserName,out string LoginName, out int? SystemRole,out string DepartmentId);
           
            if (validate)
                {
                    Session["SystemRole"] = SystemRole;
                    Session["UserName"] = UserName;
                    Session["Name"] = LoginName;

                    Session["DeptId"] = DepartmentId;
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