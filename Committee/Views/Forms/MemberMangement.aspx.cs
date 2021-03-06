﻿using Newtonsoft.Json;
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
                if (Request.QueryString["id"] == "redirectSave")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات العضو بنجاح', 'تم')", true);

                }
                if (Request.QueryString["id"] == "redirectUpdate")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات العضو بنجاح', 'تم')", true);

                }
                if (Session["SystemRole"].ToString() == "5")
                {
                    btnaddMember.Visible = false;
                    LinkButton1.Visible = false;
                }
                else
                {
                    btnaddMember.Visible = true;
                    btnaddMember.Visible = true;
                }
                    Loadmembers();
                int deptId = Convert.ToInt32(Session["DeptId"]);
                if (Session["SystemRole"].ToString()=="1")
                {
                    gvMembers.DataSource = ShowMembersForSystemAdmin();
                    gvMembers.DataBind();
                }
                else
                {
                    gvMembers.DataSource = ShowMembers(deptId);
                    gvMembers.DataBind();
                }
                

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            if (Session["SystemRole"].ToString() == "1")
            {
                gvMembers.DataSource = ShowMembersForSystemAdmin();
                gvMembers.DataBind();
            }
            else
            {
                gvMembers.DataSource = ShowMembers(deptId);
                gvMembers.DataBind();
            }
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
            gvMembers.PageIndex = e.NewPageIndex;
            int deptId = Convert.ToInt32(Session["DeptId"]);
            if (Session["SystemRole"].ToString() == "1")
            {
                gvMembers.DataSource = ShowMembersForSystemAdmin();
                gvMembers.DataBind();
            }
            else
            {
                gvMembers.DataSource = ShowMembers(deptId);
                gvMembers.DataBind();
            }
        }

        protected void gvMembers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvMembers.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = Utilities.BASE_URL+"/api/Users";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = userId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            int member = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/DeleteUser?id=" + userId));
            int deptId = Convert.ToInt32(Session["DeptId"]);
            gvMembers.DataSource = ShowMembers(deptId);
            gvMembers.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات العضو بنجاح', 'تم')", true);
     
        }
        private List<Committee.Models.UserArabicSearch> ShowMembers(int deptId)
        {
            List<Committee.Models.UserArabicSearch> users = new List<Models.UserArabicSearch>();
            string apiUrl3 = Utilities.BASE_URL+"/api/Users";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.UserPoco> members = JsonConvert.DeserializeObject <List<Committee.Models.UserPoco>>(client.DownloadString(apiUrl3 + "/GetUserByPhoneOrName?phone=" + txtSearch.Text.Trim().ToLower() + "&name=" + txtSearch.Text.Trim().ToLower()+"&deptId="+deptId));
            
            foreach (var member in members)
            {
                users.Add(new Models.UserArabicSearch()
                {
                    الرقم = member.ID,
                    الاسم = member.Name,
                    اسم_المستخدم = member.UserName,
                    البريد_اللإلكترونى = member.UserEmailId,
                    رقم_الجوال = member.Phone,
                    العنوان = member.Address,
                  //  النوع = member.Gender,
                    الوظيفه = member.Title,
                    الصلاحيات = member.SystemRoleMap.titleAr,
                    جهة_العمل = member.WorkSide,
                  
                });
            }
            return users;
        }
        private List<Committee.Models.UserArabicSearch> ShowMembersForSystemAdmin()
        {
            List<Committee.Models.UserArabicSearch> users = new List<Models.UserArabicSearch>();
            string apiUrl3 = Utilities.BASE_URL + "/api/Users";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.UserPoco> members = JsonConvert.DeserializeObject<List<Committee.Models.UserPoco>>(client.DownloadString(apiUrl3 + "/GetUserByPhoneOrNameForSystemAdmin?phone=" + txtSearch.Text.Trim().ToLower() + "&name=" + txtSearch.Text.Trim().ToLower()));

            foreach (var member in members)
            {
                users.Add(new Models.UserArabicSearch()
                {
                    الرقم = member.ID,
                    الاسم = member.Name,
                    اسم_المستخدم = member.UserName,
                    البريد_اللإلكترونى = member.UserEmailId,
                    رقم_الجوال = member.Phone,
                    العنوان = member.Address,
                   // النوع = member.Gender,
                    الوظيفه = member.Title,
                    الصلاحيات = member.SystemRoleMap.titleAr,
                    جهة_العمل = member.WorkSide,
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
            int id = Convert.ToInt32(gvMembers.Rows[e.NewSelectedIndex].Cells[1].Text);
            Response.Redirect("Member.aspx?mId=" + id + "&status=selected");
        }

        protected void gvMembers_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Committee.Models.UserArabicSearch> result = (List<Committee.Models.UserArabicSearch>)ViewState["dt"];
            if (result.Count > 0)
            {
                if (ViewState["sort"].ToString() == "Asc")
                {
                    if ("الرقم" == e.SortExpression)
                        result = result.OrderByDescending(r => r.رقم_الجوال).ToList();
                    if ("الاسم" == e.SortExpression)
                        result = result.OrderByDescending(r => r.الاسم).ToList();
                    if ("رقم_الجوال" == e.SortExpression)
                        result = result.OrderByDescending(r => r.رقم_الجوال).ToList();
                    if ("العنوان" == e.SortExpression)
                        result = result.OrderByDescending(r => r.العنوان).ToList();
                    if ("الوظيفه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.الوظيفه).ToList();
                    if ("الصلاحيات" == e.SortExpression)
                        result = result.OrderByDescending(r => r.الصلاحيات).ToList();
                    if ("البريد_اللإلكترونى" == e.SortExpression)
                        result = result.OrderByDescending(r => r.البريد_اللإلكترونى).ToList();
                    if ("اسم_المستخدم" == e.SortExpression)
                        result = result.OrderByDescending(r => r.اسم_المستخدم).ToList();
                    if ("جهة_العمل" == e.SortExpression)
                        result = result.OrderByDescending(r => r.جهة_العمل).ToList();
                   
                    //if ("الادارة" == e.SortExpression)
                    //    result = result.OrderByDescending(r => r.الادارة).ToList();
                   

                    //...do it to all the fields

                    ViewState["sort"] = "Desc";
                }
                else
                {
                    if ("الرقم" == e.SortExpression)
                        result = result.OrderBy(r => r.الرقم).ToList();
                    if ("الاسم" == e.SortExpression)
                        result = result.OrderBy(r => r.الاسم).ToList();
                    if ("رقم_الجوال" == e.SortExpression)
                        result = result.OrderBy(r => r.رقم_الجوال).ToList();
                    if ("العنوان" == e.SortExpression)
                        result = result.OrderBy(r => r.العنوان).ToList();
                    if ("الوظيفه" == e.SortExpression)
                        result = result.OrderBy(r => r.الوظيفه).ToList();
                    if ("الصلاحيات" == e.SortExpression)
                        result = result.OrderBy(r => r.الصلاحيات).ToList();
                    if ("البريد_اللإلكترونى" == e.SortExpression)
                        result = result.OrderBy(r => r.البريد_اللإلكترونى).ToList();
                    if ("اسم_المستخدم" == e.SortExpression)
                        result = result.OrderBy(r => r.اسم_المستخدم).ToList();
                    if ("جهة_العمل" == e.SortExpression)
                        result = result.OrderBy(r => r.جهة_العمل).ToList();
                    ////if ("الادارة" == e.SortExpression)
                    ////    result = result.OrderBy(r => r.الادارة).ToList();


                    ViewState["sort"] = "Asc";
                }

                gvMembers.DataSource = result;
                gvMembers.DataBind();
                ViewState["dt"] = result;
            }
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

                if (Session["SystemRole"].ToString() == "5")
                {
                    selectButton.Visible = true;
                    selectButton.ForeColor = System.Drawing.Color.Blue;
                    selectButton.BorderColor = System.Drawing.Color.White;
                    selectButton.Font.Size = FontUnit.Medium;
                    selectButton.Font.Underline = true;
                    selectButton.Text = "اختيار";
                    deleteButton.Visible = false;
                    editButton.Visible = false;
                    btnaddMember.Visible = false;
                    LinkButton1.Visible = false;

                }
                else
                {
                    selectButton.Visible = true;
                    selectButton.ForeColor = System.Drawing.Color.Blue;
                    selectButton.Font.Underline = true;
                    selectButton.Font.Size = FontUnit.Medium;
                    deleteButton.Visible = true;
                    deleteButton.ForeColor = System.Drawing.Color.DarkRed;
                    deleteButton.BackColor = System.Drawing.Color.White;
                    deleteButton.BorderColor = System.Drawing.Color.DarkRed;
                    deleteButton.Font.Size = FontUnit.Medium;
                    deleteButton.BorderWidth = 2;
                    deleteButton.Text = "مسح";

                    editButton.Visible = true;
                    editButton.ForeColor = System.Drawing.Color.Gray;
                    editButton.BackColor = System.Drawing.Color.White;
                    editButton.BorderColor = System.Drawing.Color.White;
                    editButton.Font.Size = FontUnit.Medium;
                    editButton.BorderWidth = 2;
                    editButton.Text = "تعديل";
                    selectButton.Text = "اختيار";
                }
                   
            }



        }
        private void Loadmembers()
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            if (Session["SystemRole"].ToString() == "1")
            {
                gvMembers.DataSource = ShowMembersForSystemAdmin();
                gvMembers.DataBind();
            }
            else
            {
                gvMembers.DataSource = ShowMembers(deptId);
                gvMembers.DataBind();
            }

            ViewState["dt"] = ShowMembers(deptId);
            ViewState["sort"] = "Asc";
        }
    }
}