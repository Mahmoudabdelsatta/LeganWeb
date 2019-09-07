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
    public partial class MemberMangement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
              gvMembers.DataSource= ShowMembers();
                gvMembers.DataBind();

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvMembers.DataSource = ShowMembers();
            gvMembers.DataBind();
        }

        protected void btnaddMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("Member.aspx?status=new");
        }
        protected void gvMembers_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvMembers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvMembers.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Users";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = userId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            int member = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/DeleteUser?id=" + userId));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات العضو بنجاح', 'تم')", true);
            gvMembers.DataSource = ShowMembers();
            gvMembers.DataBind();
        }
        private List<Committee.Models.UserArabicSearch> ShowMembers()
        {
            List<Committee.Models.UserArabicSearch> users = new List<Models.UserArabicSearch>();
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Users";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.User> members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(client.DownloadString(apiUrl3 + "/GetUserByPhoneOrName?phone=" + txtSearch.Text.Trim().ToLower() + "&name=" + txtSearch.Text.Trim().ToLower()));
            foreach (var member in members)
            {
                users.Add(new Models.UserArabicSearch()
                {
                    الرقم = member.ID,
                    الاسم = member.Name,
                    اسم_المستخدم = member.UserName,
                    البريد_اللإلكترونى = member.UserEmailId,
                    التليفون = member.Phone,
                    العنوان = member.Address,
                    النوع = member.Gender,
                    الوظيفه = member.Title,
                    الدور = member.SystemRoleMap.titleAr,
                    جهة_العمل = member.WorkSide,
                    الادارة = member?.Department?.DeptName
                });
            }
            return users;
        }

        protected void gvMembers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvMembers.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Member.aspx?mId=" + id + "&status=update");
        }

        protected void gvMembers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvMembers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMembers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvMembers_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvMembers_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change the index number as per your gridview design
                e.Row.Cells[1].Enabled = false;

                if (e.Row.RowType != DataControlRowType.DataRow) return;

                var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
                var editButton = (LinkButton)e.Row.Cells[0].Controls[0];
                var selectButton = (LinkButton)e.Row.Cells[0].Controls[4];
                if (deleteButton.Text == "Delete")
                {
                    deleteButton.Text = "مسح";
                    deleteButton.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";
                    // deleteButton2.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";

                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Change the index number as per your gridview design
                    e.Row.Cells[1].Enabled = false;



                }


                deleteButton.Visible = true;
                deleteButton.Text = "مسح";

                editButton.Visible = true;
                editButton.Text = "تعديل";
                selectButton.Text = "اختيار";
            }



        }
    }
}