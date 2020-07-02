using Committee.Controller;
using Committee.Models;
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
    public partial class Department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                LoadCommittees();
                string apiUrl3 = Utilities.BASE_URL + "/api/Departments";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                Committee.Models.Department department = (new JavaScriptSerializer()).Deserialize<Committee.Models.Department>(client.DownloadString(apiUrl3 + "/GetDepartmentById?id=" +Convert.ToInt32(Request.QueryString["dId"])));
                if (Request.QueryString["status"] == "update")
                {
                    lblDeptNew.Text = "تعديل إدارة ";
                    lblDeptH1.Text = "تعديل إدارة";
                    
                    txtDeptName.Text = department.DeptName;
                    txtDeptAddress.Text = department.DeptAddress;
                    ViewState["DeptID"] = Request.QueryString["dId"];

                }
                else
                {
                    lblDeptNew.Text = "اضافة إدارة";
                    lblDeptH1.Text = "اضافة إدارة جديدة";
                }
                if (Request.QueryString["status"] == "selected")
                {
                    gvCommittee.Visible = true;
                    gvCommittee.DataSource = ShowDeptCommittees(Convert.ToInt32(Request.QueryString["dId"]));
                    gvCommittee.DataBind();
                    lblDeptNew.Text = "عرض الإدارة";
                    lblDeptH1.Text = "عرض الإدارة";
                 
                    txtDeptName.Text = department.DeptName;
                    txtDeptName.Enabled = false;
                    txtDeptAddress.Text = department.DeptAddress;
                    txtDeptAddress.Enabled = false;
                    ViewState["DeptID"] = Request.QueryString["dId"];
                    btnSave.Visible = false;
                    LinkButton1.Text = "رجوع";
                }
                
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            if (Request.QueryString["status"] == "update")
            {



                ViewState["DeptID"] = Request.QueryString["dId"];

                string apiUrlUser = Utilities.BASE_URL + "/api/Departments";

                Committee.Models.Department department = new Models.Department()
                {
                    DeptId = Convert.ToInt32(Request.QueryString["dId"]),
                    DeptName = txtDeptName.Text,
                    DeptAddress = txtDeptAddress.Text,

                  
                    // UserImage = "/" + ImgUpload?.PostedFile?.FileName
                };
                


                    string inputJson = (new JavaScriptSerializer()).Serialize(department);
                try
                {
                    client.UploadString(apiUrlUser + "/PutDepartment?id=" + Convert.ToInt32(ViewState["DeptID"]), inputJson);

                }
                catch (Exception ex)
                {

                    Response.Write(ex.Message);
                }
                Response.Redirect("DepartmentMangement.aspx?id=redirectUpdate");

            }


            else if (Request.QueryString["status"] == "new")
            {
             


                    string apiUrlMember = Utilities.BASE_URL + "/api/Departments";
                    Committee.Models.Department dept = new Models.Department()
                    {

                        DeptName = txtDeptName.Text,
                        DeptAddress = txtDeptAddress.Text,
                    };



                    string inputJson = (new JavaScriptSerializer()).Serialize(dept);


                    client.UploadString(apiUrlMember + "/PostDepartment", inputJson);
                Response.Redirect("DepartmentMangement.aspx?id=redirectSave");


            }
        }

        protected void gvCommittee_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvCommittee_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommittee.PageIndex = e.NewPageIndex;
            gvCommittee.DataSource = ShowDeptCommittees(Convert.ToInt32(Request.QueryString["dId"]));
            this.gvCommittee.DataBind();
            gvCommittee.DataBind();
        }

        protected void gvCommittee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

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


            if (Session["SystemRole"].ToString() == "1")
            {
                selectButton.Visible = false;
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
            else
            {
                deleteButton.Visible = false;
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
            int committeeId = Convert.ToInt32(gvCommittee.Rows[e.RowIndex].Cells[1].Text);
            string committee = WebApiConsume.DeleteCommittee(Utilities.BASE_URL+"/api/Committees", committeeId);
            if (committee == "")
            {

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم مسح بيانات اللجنة بنجاح');", true);

                gvCommittee.DataSource = ShowDeptCommittees(Convert.ToInt32(Request.QueryString["dId"]));
                gvCommittee.DataBind();
            }

        }

        protected void gvCommittee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvCommittee.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Committe.aspx?cId=" + id + "&status=update");
        }

        protected void gvCommittee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvCommittee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvCommittee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int id = Convert.ToInt32(gvCommittee.Rows[e.NewSelectedIndex].Cells[1].Text);
            Response.Redirect("Committe.aspx?cId=" + id + "&status=selected");
        }
        private void LoadCommittees()
        {
            this.gvCommittee.DataSource = ShowDeptCommittees(Convert.ToInt32(Request.QueryString["dId"]));
            this.gvCommittee.DataBind();
            ViewState["dt"] = ShowDeptCommittees(Convert.ToInt32(Request.QueryString["dId"]));
            ViewState["sort"] = "Asc";
        }
        protected void gvCommittee_Sorting(object sender, GridViewSortEventArgs e)
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
        public  List<CommitteeSearch> ShowDeptCommittees(int deptId)
        {

            List<CommitteeSearch> committeesUpdate = new List<CommitteeSearch>();
            string apiUrl3 = Utilities.BASE_URL + "/api/Departments";
            string apiUrl2 = Utilities.BASE_URL + "/api/Users";
            WebClient client2 = new WebClient();
            client2.Headers["Content-type"] = "application/json";
            client2.Encoding = Encoding.UTF8;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveData> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveData>>(client.DownloadString(apiUrl3 + "/GetDepartmentCommittees?deptId=" + Convert.ToInt32(Request.QueryString["dId"])));
            foreach (var committee in Committees)
            {
                Committee.Models.User userman = new User();
                Committee.Models.User userssec = new User();

                if (committee.CommitteeManager != null && committee.CommitteeSecretary != null)
                {


                    userman = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client2.DownloadString(apiUrl2 + "/GetUserById?id=" + committee.CommitteeManager));
                    userssec = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client2.DownloadString(apiUrl2 + "/GetUserById?id=" + committee.CommitteeSecretary));
                }

                committeesUpdate.Add(new CommitteeSearch()
                {
                    رقم_اللجنه = committee.CommitteeId,
                    اسم_اللجنه = committee.CommitteeName,
                    تاريخ_اللجنه = committee.CommitteeDate,
                    //موضوع_اللجنه = committee.CommitteeTopic,
                    //الأمر_المستند_عليه = committee.CommitteeBasedOn,
                    الإداره = committee?.Department?.DeptName,
                    تصنيف_اللجنه = committee.Type.titleAr,
                    حال_اللجنه = committee.Activity.titleAr,
                    مستوى_الأهميه = committee.Importance.titleAr,
                    رئيس_اللجنه = userman?.UserName,
                    سكرتير_اللجنه = userssec?.UserName,
                    //جهة_الوارد = committee.CommitteeInbox1,
                    //رقم_القيد = committee.CommitteeEnrollmentNumber,
                    //سنة_القيد = committee.CommitteeEnrollmentDate,
                    //تاريخ_التعديل = committee.CreatedAt,
                    //تاريخ_اللإنشاء = committee.UpdatedAt
                });
            }

            return committeesUpdate;

        }
    }
}