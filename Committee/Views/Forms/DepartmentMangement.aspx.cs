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
    public partial class DepartmentMangement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"]=="redirectSave")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات الإدارة بنجاح', 'تم')", true);

                }
                if (Request.QueryString["id"] == "redirectUpdate")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات الإدارة بنجاح', 'تم')", true);

                }
                Loadmembers();
                gvDepts.DataSource = ShowDepartments();
                gvDepts.DataBind();

            }

        }
        private void Loadmembers()
        {
            this.gvDepts.DataSource = ShowDepartments();
            this.gvDepts.DataBind();
            ViewState["dt"] = ShowDepartments();
            ViewState["sort"] = "Asc";
        }
        private List<Committee.Models.DepartmentArabicSearch> ShowDepartments()
        {
            List<Committee.Models.DepartmentArabicSearch> depts = new List<Models.DepartmentArabicSearch>();
            string apiUrl3 = Utilities.BASE_URL + "/api/Departments";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Department> departments = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Department>>(client.DownloadString(apiUrl3 + "/GetDepartments?name=" + txtSearch.Text.Trim()));
            foreach (var department in departments)
            {
                depts.Add(new Models.DepartmentArabicSearch()
                {
                    رقم_الإدارة = department.DeptId,
                    اسم_الإدارة =department.DeptName,

                عنوان_الإدارة=department.DeptAddress,
                
              
                    
                });
            }
            return depts;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvDepts.DataSource = ShowDepartments();
            gvDepts.DataBind();

        }

        protected void gvDepts_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvDepts_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvDepts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepts.PageIndex = e.NewPageIndex;
            gvDepts.DataSource = ShowDepartments();
            gvDepts.DataBind();
        }

        protected void gvDepts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvDepts_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvDepts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            int deptId = Convert.ToInt32(gvDepts.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = Utilities.BASE_URL + "/api/Departments";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = deptId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            try
            {
                int dept = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/DeleteDepartment?id=" + deptId));

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات الادارة بنجاح', 'تم')", true);
            gvDepts.DataSource = ShowDepartments();
            gvDepts.DataBind();
        }

        protected void gvDepts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvDepts.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Department.aspx?dId=" + id + "&status=update");

        }

        protected void gvDepts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvDepts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvDepts_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int id = Convert.ToInt32(gvDepts.Rows[e.NewSelectedIndex].Cells[1].Text);
            Response.Redirect("Department.aspx?dId=" + id + "&status=selected");
        }

        protected void gvDepts_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Committee.Models.DepartmentArabicSearch> result = (List<Committee.Models.DepartmentArabicSearch>)ViewState["dt"];
            if (result.Count > 0)
            {
                if (ViewState["sort"].ToString() == "Asc")
                {
                    if ("رقم_الإدارة" == e.SortExpression)
                        result = result.OrderByDescending(r => r.رقم_الإدارة).ToList();
                    if ("الاسم" == e.SortExpression)
                        result = result.OrderByDescending(r => r.اسم_الإدارة).ToList();
                    if ("العنوان" == e.SortExpression)
                        result = result.OrderByDescending(r => r.عنوان_الإدارة).ToList();
                 


                    //...do it to all the fields

                    ViewState["sort"] = "Desc";
                }
                else
                {
                    
                         if ("رقم_الإدارة" == e.SortExpression)
                        result = result.OrderBy(r => r.رقم_الإدارة).ToList();
                    if ("الاسم" == e.SortExpression)
                        result = result.OrderBy(r => r.اسم_الإدارة).ToList();
                    if ("العنوان" == e.SortExpression)
                        result = result.OrderBy(r => r.عنوان_الإدارة).ToList();
                   


                    ViewState["sort"] = "Asc";
                }

                gvDepts.DataSource = result;
                gvDepts.DataBind();
                ViewState["dt"] = result;
            }

        }

        protected void btnaddDept_Click(object sender, EventArgs e)
        {
            Response.Redirect("Department.aspx?status=new");
        }
    }
}