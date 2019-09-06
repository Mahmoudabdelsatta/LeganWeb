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
             gvCommittee.DataSource= ShowCommittees();
                gvCommittee.DataBind();
            }
        }
        private List<CommitteeSearch> ShowCommittees()
        {
            List<CommitteeSearch> committeesUpdate = new List<CommitteeSearch>();
            string apiUrl3 = "http://localhost:1481/api/Committees";
            string apiUrl2 = "http://localhost:1481/api/Users";

            WebClient client2 = new WebClient();
            client2.Headers["Content-type"] = "application/json";
            client2.Encoding = Encoding.UTF8;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveData> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveData>>(client.DownloadString(apiUrl3 + "/GetCommitteeDetails?committeeName=" + txtCommitteeName.Text.Trim().ToLower() + "&commiteeDate=" + txtCommitteeName.Text));
            foreach (var committee in Committees)
            {
                Committee.Models.User userman = new User();
                Committee.Models.User userssec = new User();

                if (committee.CommitteeManager!=null&& committee.CommitteeSecretary!=null)
                {

                
                 userman = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client2.DownloadString(apiUrl2 + "/GetUserById?id=" + committee.CommitteeManager));
                 userssec = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client2.DownloadString(apiUrl2 + "/GetUserById?id=" + committee.CommitteeSecretary));
                }

                committeesUpdate.Add(new CommitteeSearch()
                {
                    رقم_اللجنه = committee.CommitteeId,
                    اسم_اللجنه = committee.CommitteeName,
                    تاريخ_اللجنه = committee.CommitteeDate,
                    موضوع_اللجنه = committee.CommitteeTopic,
                    الأمر_المستند_عليه = committee.CommitteeBasedOn,
                    الإداره = committee?.Department?.DeptName,
                    تصنيف_اللجنه = committee.Type.titleAr,
                    حال_اللجنه = committee.Activity.titleAr,
                    مستوى_الأهميه = committee.Importance.titleAr,
                    رئيس_اللجنه = userman?.UserName,
                    سكرتير_اللجنه =userssec?.UserName,
                    جهة_الوارد = committee.CommitteeInbox1,
                    رقم_القيد = committee.CommitteeEnrollmentNumber,
                    سنة_القيد = committee.CommitteeEnrollmentDate,
                    تاريخ_التعديل = committee.CreatedAt,
                    تاريخ_اللإنشاء = committee.UpdatedAt
                });
            }

            return committeesUpdate;
        }
        private List<CommitteeSearchUpdate> ShowsCommitteesForUpdate(int committeeId)
        {
            List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
            string apiUrl3 = "http://localhost:1481/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveUpdate> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveUpdate>>(client.DownloadString(apiUrl3 + "/GetCommitteeDetails?committeeName=" + txtCommitteeName.Text.Trim().ToLower()));
            foreach (var committee in Committees)
            {
                committeesUpdate.Add(new CommitteeSearchUpdate()
                {
                    رقم_اللجنه = committee.CommitteeId,
                    اسم_اللجنه = committee.CommitteeName,
                    رئيس_اللجنه = committee.CommitteeManager,
                    سكرتير_اللجنه = committee.CommitteeSecretary,
                    تاريخ_اللجنه = committee.CommitteeDate,
                    موضوع_اللجنه = committee.CommitteeTopic,
                    الأمر_المستند_عليه = committee.CommitteeBasedOn,
                    الإداره = committee.Department,
                    تصنيف_اللجنه = committee.Type,
                    حال_اللجنه = committee.Activity,
                    مستوى_الأهميه = committee.Importance,
                    جهة_الوارد = committee.CommitteeInbox1,
                    رقم_القيد = committee.CommitteeEnrollmentNumber,
                    سنة_القيد = committee.CommitteeEnrollmentDate,
                });
            }

            return committeesUpdate;
        }
        private List<UserGrid> ShowCommitteeMembers(int committeeId)
        {
            string apiUrl3 = "http://localhost:1481/api/CommitteesMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.User> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(client.DownloadString(apiUrl3 + "/GetCommitteesMember?id=" + committeeId));


            return Committees.Select(x => new UserGrid() { رقم_العضو = x.ID, اسم_العضو = x.UserName }).ToList();

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
            //var selectButton = (LinkButton)e.Row.Cells[0].Controls[4];


            if (Session["SystemRole"].ToString() == "1")
            {

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
                //selectButton.Text = "اختيار";
            }
            else
            {
                deleteButton.Visible = false;
                editButton.Visible = false;
              //  selectButton.Text = "اختيار";
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

            int committeeId = Convert.ToInt32(gvCommittee.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = "http://localhost:1481/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = committeeId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            string CommitteeExist = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrl3 + "/DeleteCommittee?id=" + committeeId.ToString()));
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات اللجنة بنجاح', 'تم')", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم مسح بيانات اللجنة بنجاح');", true);

            gvCommittee.DataSource = ShowCommittees();
            gvCommittee.DataBind();

        }
        protected void gvCommittee_SelectedIndexChanged(object sender, EventArgs e) { 
        //{
        //    int committeeId = Convert.ToInt32(gvCommittee.SelectedRow.Cells[1].Text);
        //    GridViewRow row = gvCommittee.SelectedRow;
        //    List<UserGrid> data = ShowCommitteeMembers(committeeId);
        //    gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();

        //    gvMembersOfCommittee.DataBind();
        //    ViewState["CommitteeId"] = committeeId;
        //    WebClient webClient = new WebClient();
        //    webClient.Headers["Content-type"] = "application/json";
        //    webClient.Encoding = Encoding.UTF8;
        //    string result = webClient.DownloadString("http://localhost:1481/api/Users/GetUsers");
        //    List<Committee.Models.User> Members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(result);
        //    ddlMemberChange.DataSource = Members;
        //    ddlMemberChange.DataTextField = "UserName";
        //    ddlMemberChange.DataValueField = "ID";
        //    ddlMemberChange.DataBind();
        //    divAddMember.Visible = true;
        //    List<UserGrid> data2 = ShowCommitteeMembers(committeeId);
        //    gvMembersOfCommittee.DataSource = data2.Where(x => x != null).ToList();

        //    gvMembersOfCommittee.DataBind();
        //    if (Session["SystemRole"].ToString() == "2")

        //    {
        //        divAddMember.Visible = true;
        //    }

        }

        protected void gvCommittee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //divMembers.Visible = true;
            //string committeeId = gvCommittee.SelectedRow.Cells[3].Text;
            // gvMembersOfCommittee.DataSource = ShowCommitteeMembers(committeeId);
            //gvMembersOfCommittee.DataBind();

        }

        protected void gvCommittee_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommittee.PageIndex = e.NewPageIndex;
            gvCommittee.DataSource = ShowCommittees();
            gvCommittee.DataBind();
        }

        protected void gvCommittee_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvCommittee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id =Convert.ToInt32(gvCommittee.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Committe.aspx?cId=" +id+"&status=update");
            //List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
            //string apiUrl3 = "http://localhost:1481/api/Committees";

            //WebClient client = new WebClient();
            //client.Headers["Content-type"] = "application/json";
            //client.Encoding = Encoding.UTF8;

            //Committee.Models.CommitteeRetrieveUpdate Committee = (new JavaScriptSerializer()).Deserialize<Committee.Models.CommitteeRetrieveUpdate>(client.DownloadString(apiUrl3 + "/GetCommitteeByIdForWeb?committeeId=" + gvCommittee.Rows[e.NewEditIndex].Cells[1].Text));
            //txtCommitteeDate.Text = Committee.CommitteeDate;
            //ddlCommitteeClassification.SelectedValue = Committee.Type.Id.ToString();
            //ddlCommitteeImportancy.SelectedValue = Committee.Importance.Id.ToString();
            //ddlcommitteeStatus.SelectedValue = Committee.Activity.Id.ToString();
            //txtCommitteeTopic.Text = Committee.CommitteeTopic;
            //txtCommitteeBasedON.Text = Committee.CommitteeBasedOn;
            //txtEnrollmentNumber.Text = Committee.CommitteeEnrollmentNumber;
            //txtEnrollmentDate.Text = Committee.CommitteeEnrollmentDate;
            //txtInboxSide.Text = Committee.CommitteeInbox1;
            //ddlCommitteeDept.SelectedValue = Committee?.Department?.DeptId.ToString();
            //txtCommitteeName.Text = Committee.CommitteeName;
            //ddlCommitteepresident.SelectedValue = Committee.CommitteeManager;
            //ddlCommitteeSecrtary.SelectedValue = Committee.CommitteeSecretary;


            //btnAdd1.Text = "تعديل";
            //ViewState["committeeID"] = gvCommittee.Rows[e.NewEditIndex].Cells[1].Text;
            //gvCommittee.EditIndex = -1;
            //gvCommittee.DataSource = ShowCommittees();
            //gvCommittee.DataBind();


        }

        protected void gvCommittee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            GridViewRow row = (GridViewRow)gvCommittee.Rows[e.RowIndex];
            TextBox textid = (TextBox)row.Cells[1].Controls[0];
            int id1 = Convert.ToInt32(textid.Text);
            TextBox CommitteeName = (TextBox)row.Cells[3].Controls[0];
            DropDownList CommitteManagerName = (DropDownList)row.Cells[2].Controls[0];

            DropDownList CommitteeSecrtary = (DropDownList)row.Cells[4].Controls[0];
            DropDownList Active = (DropDownList)row.Cells[11].Controls[0];
            DropDownList Important = (DropDownList)row.Cells[12].Controls[0];
            DropDownList Militraized = (DropDownList)row.Cells[13].Controls[0];
            TextBox committeeDate = (TextBox)row.Cells[5].Controls[0];
            TextBox committeeTopic = (TextBox)row.Cells[6].Controls[0];
            TextBox committeeBasedOn = (TextBox)row.Cells[7].Controls[0];
            TextBox committeeInbox1 = (TextBox)row.Cells[8].Controls[0];
            TextBox committeeEnrollmentNumber = (TextBox)row.Cells[9].Controls[0];
            TextBox committeeEnrollmentDate = (TextBox)row.Cells[10].Controls[0];
            //Setting the EditIx property to -1 to cancel the Edit mode in Gridview  
            gvCommittee.EditIndex = -1;

            //Call ShowData method for displaying updated data  

            string apiUrlUpdate = "http://localhost:1481/api/Committees";
            Committee.Models.Committee committeeUpdate = new Models.Committee()
            {
                CommitteeId = Convert.ToInt32(textid.Text),
                CommitteeName = CommitteeName.Text,
                CommitteeDate = committeeDate.Text,
                //IsMilitarized =Militraized.Checked,
                //IsActive = Active.Checked,
                //IsImportant = Important.Checked,
                CommitteeTopic = committeeTopic.Text,
                CommitteeBasedOn = committeeBasedOn.Text,
                CommitteeInbox1 = committeeInbox1.Text,
                CommitteeEnrollmentNumber = committeeEnrollmentNumber.Text,
                CommitteeEnrollmentDate = committeeEnrollmentDate.Text,
                CommitteeSecretary = CommitteeSecrtary.Text,
                CommitteeManager = CommitteManagerName.Text

            };
            string inputJson = (new JavaScriptSerializer()).Serialize(committeeUpdate);

            client.UploadString(apiUrlUpdate + "/PutCommittee?id=" + Convert.ToInt32(textid.Text), inputJson);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم تعديل بيانات اللجنة بنجاح')", true);

            gvCommittee.DataSource = ShowCommittees();
            gvCommittee.DataBind();
        }

        protected void gvCommittee_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvCommittee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCommittee.EditIndex = -1;
            gvCommittee.DataSource = ShowCommittees();
            gvCommittee.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnaddNewCommittee_Click(object sender, EventArgs e)
        {

            
            Response.Redirect("Committe.aspx?status=new");
        }
    }
}