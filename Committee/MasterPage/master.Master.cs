using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee
{
    public partial class master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Session["UserName"].ToString();
            lblUserName2.Text = Session["UserName"].ToString();
           lblDate.Text= DateTime.Now.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));

        }

        protected void logOut_Click(object sender, EventArgs e)
        {
            Session.Contents.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}