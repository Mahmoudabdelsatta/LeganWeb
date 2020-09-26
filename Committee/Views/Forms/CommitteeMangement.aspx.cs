using Committee.Controller;
using Committee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Views.Forms
{
    public partial class CommitteeMangement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                int deptId = Convert.ToInt32(Session["DeptId"]);
                if (Request.QueryString["id"] == "redirectSave")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات اللجنة بنجاح', 'تم')", true);

                }
                if (Request.QueryString["id"] == "redirectUpdate")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات اللجنة بنجاح', 'تم')", true);

                }
                LoadCommittees();
             gvCommittee.DataSource= WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(), deptId);
                gvCommittee.DataBind();
            }
        }
      
        protected void gvCommittee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change the index number as per your gridview design
                e.Row.Cells[1].Enabled = false;



            }

            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
            var editButton = (LinkButton)e.Row.Cells[0].Controls[0];
           var selectButton = (LinkButton)e.Row.Cells[0].Controls[4];


            if (Session["SystemRole"].ToString() == "5")
            {
                selectButton.Visible = true;
                selectButton.ForeColor = System.Drawing.Color.Blue;
                selectButton.BorderColor = System.Drawing.Color.White;
                selectButton.Font.Size = FontUnit.Medium;
                selectButton.Font.Underline = true;
                deleteButton.Visible = false;
                deleteButton.ForeColor = System.Drawing.Color.DarkRed;
                deleteButton.BackColor = System.Drawing.Color.White;
                deleteButton.BorderColor = System.Drawing.Color.DarkRed;
                deleteButton.Font.Size = FontUnit.Medium;
                deleteButton.BorderWidth = 2;
                deleteButton.Text = "مسح";

                editButton.Visible = false;
                editButton.ForeColor = System.Drawing.Color.Gray;
                editButton.BackColor = System.Drawing.Color.White;
                editButton.BorderColor = System.Drawing.Color.White;
                editButton.Font.Size = FontUnit.Medium;
                editButton.BorderWidth = 2;
                editButton.Text = "تعديل";
               selectButton.Text = "اختيار";
                btnaddNewCommittee.Visible = false;
            }
            else
            {
                btnaddNewCommittee.Visible = true;

                deleteButton.Visible = true;
                editButton.Visible = true;
                selectButton.Visible = true;
              selectButton.Text = "اختيار";
                editButton.Text = "تعديل";
                selectButton.ForeColor = System.Drawing.Color.Blue;
                selectButton.BorderColor = System.Drawing.Color.White;
                selectButton.Font.Size = FontUnit.Medium;
                selectButton.Font.Underline = true;

            }
            // var deleteButton2 = (LinkButton)e.Row.Cells[2].Controls[0];
            if (deleteButton.Text == "Delete" || deleteButton.Text == "مسح")
            {
                deleteButton.Text = "مسح";
                deleteButton.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";
                // deleteButton2.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";

            }
        }
        protected void gvCommittee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            int committeeId = Convert.ToInt32(gvCommittee.Rows[e.RowIndex].Cells[1].Text);
           string committee= WebApiConsume.DeleteCommittee(Utilities.BASE_URL+"/api/Committees",committeeId);
            if (committee=="")
            {

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم مسح بيانات اللجنة بنجاح');", true);

                gvCommittee.DataSource = WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(),deptId);
                gvCommittee.DataBind();
            }
           

        }
     
        
        protected void gvCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            gvCommittee.PageIndex = e.NewPageIndex;
            gvCommittee.DataSource = WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(),deptId);
            gvCommittee.DataBind();
        }

   

        protected void gvCommittee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id =Convert.ToInt32(gvCommittee.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Committe.aspx?cId=" +id+"&status=update");
          

        }
        protected void gvCommittee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            gvCommittee.EditIndex = -1;
            gvCommittee.DataSource = WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(),deptId);
            gvCommittee.DataBind();
        }

  

        protected void btnaddNewCommittee_Click(object sender, EventArgs e)
        {

            
            Response.Redirect("Committe.aspx?status=new");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            gvCommittee.DataSource = WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(),deptId);
            gvCommittee.DataBind();
        }

        protected void gvCommittee_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void gvCommittee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int id = Convert.ToInt32(gvCommittee.Rows[e.NewSelectedIndex].Cells[1].Text);
            Response.Redirect("Committe.aspx?cId=" + id + "&status=selected");
        }
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvCommittee_Sorting(object sender, GridViewSortEventArgs e)
        {
            //re-run the query, use linq to sort the objects based on the arg.
            //perform a search using the constraints given 
            //you could have this saved in Session, rather than requerying your datastore


        }
        private void LoadCommittees()
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            this.gvCommittee.DataSource = WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(),deptId);
            this.gvCommittee.DataBind();
            ViewState["dt"] = WebApiConsume.ShowCommittees(txtCommitteeName.Text.Trim().ToLower(),deptId);
            ViewState["sort"] = "Asc";
        }

        protected void gvCommittee_Sorting1(object sender, GridViewSortEventArgs e)
        {
            List<CommitteeSearch> result = (List<CommitteeSearch>)ViewState["dt"];
            if (result.Count > 0)
            {
                if (ViewState["sort"].ToString() == "Asc")
                {
                    if ("اسم_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.اسم_اللجنه).ToList();
                    if ("رئيس_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.رئيس_اللجنه).ToList();
                    if ("رقم_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.رقم_اللجنه).ToList();
                    if ("سكرتير_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.سكرتير_اللجنه).ToList();
                    if ("تاريخ_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.تاريخ_اللجنه).ToList();
                    if ("حال_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.حال_اللجنه).ToList();
                    if ("مستوى_الأهميه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.مستوى_الأهميه).ToList();
                    if ("تصنيف_اللجنه" == e.SortExpression)
                        result = result.OrderByDescending(r => r.تصنيف_اللجنه).ToList();
                    if ("الإداره" == e.SortExpression)
                        result = result.OrderByDescending(r => r.الإداره).ToList();
                    //...do it to all the fields

                    ViewState["sort"] = "Desc";
                }
                else
                {
                    if ("اسم_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.اسم_اللجنه).ToList();
                    if ("رئيس_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.رئيس_اللجنه).ToList();
                    if ("رقم_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.رقم_اللجنه).ToList();
                    if ("سكرتير_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.سكرتير_اللجنه).ToList();
                    if ("تاريخ_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.تاريخ_اللجنه).ToList();
                    if ("حال_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.حال_اللجنه).ToList();
                    if ("مستوى_الأهميه" == e.SortExpression)
                        result = result.OrderBy(r => r.مستوى_الأهميه).ToList();
                    if ("تصنيف_اللجنه" == e.SortExpression)
                        result = result.OrderBy(r => r.تصنيف_اللجنه).ToList();
                    if ("الإداره" == e.SortExpression)
                        result = result.OrderBy(r => r.الإداره).ToList();
                    ViewState["sort"] = "Asc";
                }

                gvCommittee.DataSource = result;
                gvCommittee.DataBind();
                ViewState["dt"] = result;
            }

        }
    }
}