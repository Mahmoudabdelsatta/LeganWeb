using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Views.Forms
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["status"] == "update")
                {
                    ImgUser.Visible = true;
                    string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Users";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;

                    Committee.Models.User user = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client.DownloadString(apiUrl3 + "/GetUserById?id=" + Request.QueryString["mId"]));


                    txtMemberName.Text = user.Name;
                    txtMemberEmail.Text = user.UserEmailId;
                    txtPhoneNumber.Text = user.Phone;
                    txtMemberJob.Text = user.Title;
                    ddlMemberType.SelectedValue = user.Gender;
                    txtWorkSide.Text = user.WorkSide;
                    ddlMemberRole.SelectedValue = user?.SystemRole.ToString();
                    txtUserName.Text = user.UserName;
                    txtPass.Text = user.UserPassword;
                    txtAddress.Text = user.Address;
                    ddlCommitteeDept.SelectedValue = user?.Department?.DeptId.ToString();
                  
                    ImgUser.ImageUrl = "~/MasterPage/Uploads" + user?.UserImage;



                   
                    ViewState["UserID"] = Request.QueryString["mId"];
                 
                }
            }
        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            ResetControld();
        }

        private void ResetControld()
        {
            txtMemberName.Text = "";
            txtPhoneNumber.Text = "";
            txtWorkSide.Text = "";
            ddlMemberType.SelectedIndex = -1;
            txtMemberJob.Text = "";
            txtMemberEmail.Text = "";
            txtAddress.Text = "";
            txtPass.Text = "";
            txtUserName.Text = "";
            txtWorkSide.Text = "";
            ddlMemberType.SelectedIndex = 0;
            btnAdd1.Text = "حفظ";
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            if (Request.QueryString["status"] == "update")
            {
             
                ViewState["UserID"] = Request.QueryString["mId"];
              
                string apiUrlUpdate = "https://committeeapi20190806070934.azurewebsites.net/api/Users";

                Committee.Models.User memberUpdate = new Models.User()
                {
                    ID = Convert.ToInt32(Request.QueryString["mId"]),
                    Name = txtMemberName.Text,
                    UserEmailId = txtMemberEmail.Text,
                    Phone = txtPhoneNumber.Text,
                    Title = txtMemberJob.Text,
                    Gender = ddlMemberType.SelectedItem.Text,
                    WorkSide = txtWorkSide.Text,
                    SystemRole = Convert.ToInt32(ddlMemberRole.SelectedItem.Value),
                    UserName = txtUserName.Text,
                    UserPassword = txtPass.Text,
                    Address = txtAddress.Text,
                    UserImage = "/"+ImgUpload?.PostedFile?.FileName
                };
                

                string inputJson = (new JavaScriptSerializer()).Serialize(memberUpdate);

                client.UploadString(apiUrlUpdate + "/PutUser?id=" + Convert.ToInt32(ViewState["UserID"]), inputJson);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات العضو بنجاح', 'تم')", true);
                
             
            }

            else if (Request.QueryString["status"] == "new")
            {

                if (ImgUpload.PostedFile != null && ImgUpload.PostedFile.ContentLength > 0)
                {
                    string fname = Path.GetFileName(ImgUpload.PostedFile.FileName);
                    ImgUpload.SaveAs(Server.MapPath(Path.Combine("~/Uploads/", fname)));

                }
               

                string apiUrlMember = "https://committeeapi20190806070934.azurewebsites.net/api/Users";
                Committee.Models.User member = new Models.User()
                {
                    ID = Convert.ToInt32(ViewState["memberID"]),
                    Name = txtMemberName.Text,
                    UserEmailId = txtMemberEmail.Text,
                    Phone = txtPhoneNumber.Text,
                    Title = txtMemberJob.Text,
                    Gender = ddlMemberType.SelectedItem.Text,
                    WorkSide = txtWorkSide.Text,
                    SystemRole = Convert.ToInt32(ddlMemberRole.SelectedItem.Value),
                    UserName = txtUserName.Text,
                    UserPassword = txtPass.Text,
                    Address = txtAddress.Text,
                    UserImage = "/" + ImgUpload.PostedFile.FileName,
                    ManagerOfDepartment = ddlCommitteeDept?.SelectedItem?.Value




            };

            

                string inputJson = (new JavaScriptSerializer()).Serialize(member);


                client.UploadString(apiUrlMember + "/PostUser", inputJson);



                ResetControld();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات العضو بنجاح', 'تم')", true);

            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            

        }


        private List<Committee.Models.UserArabicSearch> ShowMembers()
        {
            List<Committee.Models.UserArabicSearch> users = new List<Models.UserArabicSearch>();
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Users";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.User> members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(client.DownloadString(apiUrl3 + "/GetUserByPhoneOrName?phone=" + txtPhoneNumber.Text.Trim().ToLower() + "&name=" + txtMemberName.Text.Trim().ToLower()));
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

            protected void btnAdd3_Click(object sender, EventArgs e)
        {
           
            //gvMembers.DataSource = ShowMembers();
            //gvMembers.DataBind();
            //if (member != null)
            //{
            //    txtMemberName.Text = member.MemberName;
            //    ddlMemberType.SelectedItem.Text = member.MemberType;
            //    txtPhoneNumber.Text = member.MemberPhone;
            //    txtMemberJob.Text = member.MemberTitle;
            //    txtMemberEmail.Text = member.MemberMail;
            //    txtWorkSide.Text = member.MemberWorkSide1;
              
            //    ViewState["memberID"] = member.MemberId;
            //}
            ViewState["update"] = 1;
            btnAdd1.Text = "تعديل";
        }

        //protected void gvMembers_PageIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void gvMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //}

        //protected void gvMembers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{

        //}

        //protected void gvMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int userId = Convert.ToInt32(gvMembers.Rows[e.RowIndex].Cells[1].Text.ToString());
      

        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    object input = new
        //    {
        //        id = userId,
        //    };
        //    string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
        //   int member = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/DeleteUser?id=" + userId));
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات العضو بنجاح', 'تم')", true);
        //    gvMembers.DataSource = ShowMembers();
        //    gvMembers.DataBind();
        //}

        //protected void gvMembers_RowEditing(object sender, GridViewEditEventArgs e)
        //{
          
    

        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;

        //    Committee.Models.User user = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client.DownloadString(apiUrl3 + "/GetUserById?id=" + gvMembers.Rows[e.NewEditIndex].Cells[1].Text));
            
                    
        //    txtMemberName.Text = user.Name;
        //    txtMemberEmail.Text = user.UserEmailId;
        //    txtPhoneNumber.Text = user.Phone;
        //    txtMemberJob.Text = user.Title;
        //    ddlMemberType.SelectedItem.Text = user.Gender;
        //    txtWorkSide.Text = user.WorkSide;
        //    ddlMemberRole.SelectedValue = user?.SystemRole.ToString();
        //    txtUserName.Text = user.UserName;
        //    txtPass.Text = user.UserPassword;
        //    txtAddress.Text = user.Address;
        //    ddlCommitteeDept.SelectedValue = user?.Department?.DeptId.ToString();
            


        //    btnAdd1.Text = "تعديل";
        //    ViewState["UserID"] = gvMembers.Rows[e.NewEditIndex].Cells[1].Text;
        //    gvMembers.SelectedIndex = gvMembers.Rows[e.NewEditIndex].RowIndex;
        //    ViewState["UserID"] = gvMembers.Rows[e.NewEditIndex].Cells[1].Text;
        //}

        //protected void gvMembers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //}

        //protected void gvMembers_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void gvMembers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        //{

        //}

        //protected void gvMembers_Sorting(object sender, GridViewSortEventArgs e)
        //{

        //}

        //protected void gvMembers_DataBound(object sender, EventArgs e)
        //{

        //}

        //protected void gvMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //Change the index number as per your gridview design
        //        e.Row.Cells[1].Enabled = false;

        //        if (e.Row.RowType != DataControlRowType.DataRow) return;

        //        var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
        //        var editButton = (LinkButton)e.Row.Cells[0].Controls[0];
        //        var selectButton = (LinkButton)e.Row.Cells[0].Controls[4];
        //        if (deleteButton.Text == "Delete")
        //        {
        //            deleteButton.Text = "مسح";
        //            deleteButton.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";
        //            // deleteButton2.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";

        //        }
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            //Change the index number as per your gridview design
        //            e.Row.Cells[1].Enabled = false;



        //        }

             
        //            deleteButton.Visible = true;
        //            deleteButton.Text = "مسح";

        //            editButton.Visible = true;
        //            editButton.Text = "تعديل";
        //            selectButton.Text = "اختيار";
        //        }
               
          

        //}

        protected void ddlMemberRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberRole.SelectedIndex==1||ddlMemberRole.SelectedIndex==2)
            {
                divUserNameandpass.Visible = true;
            }
            else
            {
                divUserNameandpass.Visible = false;
                txtUserName.Text = "";
                txtPass.Text = "";
            }
            if (ddlMemberRole.SelectedIndex == 4)
            {
                divDept.Visible = true;
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result2 = webClient.DownloadString("https://committeeapi20190806070934.azurewebsites.net/api/Deapartments/GetDepartments");
                List<Committee.Models.Department> departments = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Department>>(result2);


                ddlCommitteeDept.DataSource = departments;
                ddlCommitteeDept.DataTextField = "DeptName";
                ddlCommitteeDept.DataValueField = "DeptId";
                ddlCommitteeDept.DataBind();
                ddlCommitteeDept.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
            }
            else
            {
                divDept.Visible = false;
               // divUserNameandpass.Visible = false;
            }

        }

      
    }
    }
