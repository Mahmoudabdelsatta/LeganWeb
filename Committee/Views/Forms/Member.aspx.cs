using Committee.Controller;
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["SystemRole"].ToString() == "1")
                {
                    mangerForDept.Visible = true;
                    divuserNameOfManager.Visible = true;
                    divPasswordOfManager.Visible = true;
                    divDept.Visible = false;
                    divRole.Visible = true;
                    WebClient webClient = new WebClient();
                    webClient.Headers["Content-type"] = "application/json";
                    webClient.Encoding = Encoding.UTF8;
                    string result2 = webClient.DownloadString(Utilities.BASE_URL + "/api/Deapartments/GetDepartments");
                    List<Committee.Models.Department> departments = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Department>>(result2);


                    ddlmangerForDept.DataSource = departments;
                    ddlmangerForDept.DataTextField = "DeptName";
                    ddlmangerForDept.DataValueField = "DeptId";
                    ddlmangerForDept.DataBind();
                   ddlmangerForDept.Items.Insert(0, new ListItem("أختر من القائمه", "0"));
                    ddlMemberType.DataBind();
                    divRoles.Visible = false;
                }
                else
                {
                    ddlMemberRole.SelectedValue = "6";
                    ddlMemberRole.Enabled = false;
                }
                Committee.Models.User user = new Models.User();
                string apiUrl3 = Utilities.BASE_URL+"/api/Users";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                if (Request.QueryString["status"] == "new")
                {
                    divImgPreview.Visible = false;
                
                        lblMemberNew.Text = "اضافة عضو";
                        lblMemberH1.Text = "اضافة عضو جديد";
                   
                }
                else
                {
                   user = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client.DownloadString(apiUrl3 + "/GetUserById?id=" +Convert.ToInt32(Request.QueryString["mId"])));

                }
                if (Request.QueryString["status"] == "update")
                {
                    ViewState["UserID"] = Request.QueryString["mId"];
                    lblMemberNew.Text = "تعديل عضو";
                    lblMemberH1.Text= "تعديل عضو";
                    ImgUser.Visible = true;
                 

                    
                    txtPass.Text = Encryptor.MD5Hash(user.UserPassword);

                    txtMemberName.Text = user.Name;
                    txtMemberEmail.Text = user.UserEmailId;
                    txtPhoneNumber.Text = user.Phone;
                    ViewState["Phone"] = txtPhoneNumber.Text;
                    txtMemberJob.Text = user.Title;
                    ddlMemberType.SelectedValue = user.SecrtaryOfDept;
                    txtWorkSide.Text = user.WorkSide;
              
                    txtUserName.Text = user.UserName;
                   // txtPass.Text = user.UserPassword;
                    txtAddress.Text = user.Address;
                    ddlCommitteeDept.SelectedValue = user?.Department?.DeptId.ToString();
                    if (Session["SystemRole"].ToString() == "1")
                    {
                        divDept.Visible = false;
                        divRoles.Visible = false;
                        mangerForDept.Visible = true;
                        divUserName.Visible = false;
                        divuserNameOfManager.Visible = true;
                        divpass.Visible = false;
                        divPasswordOfManager.Visible = true;
                        txtuserNameOfManager.Text = user.UserName;
                        txtPasswordOfManager.Text = Encryptor.MD5Hash(user.UserPassword);
                        ddlmangerForDept.SelectedValue = user?.Department?.DeptId.ToString();
                        ddlMemberType.SelectedValue = user?.SystemRole.ToString();

                    }
                    else
                    {
                        // ddlMemberRole.SelectedValue = "6";
                        ddlMemberRole.SelectedValue = user?.SystemRole.ToString();
                        ddlMemberRole.Enabled = false;
                    }
                }

               
                    if (!String.IsNullOrWhiteSpace(user.UserImage))
                    {
                        ImgUser.ImageUrl = "~/MasterPage/Uploads/"+user?.UserImage;
                        divImgPreview.Visible = true;

                    }
                    else
                    {
                        ImgUser.Visible = false;
                        divImgPreview.Visible = false;

                    }





               
                 
                
                if (Request.QueryString["status"] == "selected")
                {
                    lblMemberNew.Text = "عرض العضو";
                    lblMemberH1.Text = "عرض العضو";
                    txtPass.Text = Encryptor.MD5Hash(user.UserPassword);
                    txtPass.Enabled = false;
                    txtMemberName.Text = user.Name;
                    txtMemberName.Enabled = false;
                    txtMemberEmail.Text = user.UserEmailId;
                    txtMemberEmail.Enabled = false;
                    txtPhoneNumber.Text = user.Phone;
                    txtPhoneNumber.Enabled = false;
                    txtMemberJob.Text = user.Title;
                    txtMemberJob.Enabled = false;
                    ddlMemberType.SelectedValue = user.SecrtaryOfDept;
                    ddlMemberType.Enabled = false;
                    txtWorkSide.Text = user.WorkSide;
                    txtWorkSide.Enabled = false;
                    ddlMemberRole.SelectedValue = user?.SystemRole.ToString();
                    ddlMemberRole.Enabled = false;
                    txtUserName.Text = user.UserName;
                    txtUserName.Enabled = false;
                    // txtPass.Text = user.UserPassword;
                    txtAddress.Text = user.Address;
                    txtAddress.Enabled = false;
                    ddlCommitteeDept.SelectedValue = user?.Department?.DeptId.ToString();
                    ddlCommitteeDept.Enabled = false;
                    divImage.Visible=false;
                    ImgUser.Visible = true;
                    btnSave.Visible = false;
                    if (!String.IsNullOrWhiteSpace(user.UserImage))
                    {
                        ImgUser.ImageUrl = "~/MasterPage/Uploads" + user?.UserImage;
                        divImgPreview.Visible = true;

                    }
                    else
                    {
                        divImgPreview.Visible = false;
                    }





                    ViewState["UserID"] = Request.QueryString["mId"];
                }
                
            }
            else
            {

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
            btnSave.Text = "حفظ";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            if (Request.QueryString["status"] == "update")
            {
           
               
               
                    ViewState["UserID"] = Request.QueryString["mId"];

                    string apiUrlUser = Utilities.BASE_URL+"/api/Users";
                if (Session["SystemRole"].ToString() == "1")
                {
                    Committee.Models.User memberManger = new Models.User()
                    {
                        ID = Convert.ToInt32(Request.QueryString["mId"]),
                        Name = txtMemberName.Text,
                        UserEmailId = txtMemberEmail.Text,
                        Phone = txtPhoneNumber.Text,
                        Title = txtMemberJob.Text,
                        SecrtaryOfDept = ddlmangerForDept?.SelectedItem?.Value,
                        WorkSide = txtWorkSide.Text,
                        SystemRole =Convert.ToInt32(ddlMemberType?.SelectedItem?.Value),
                        UserName = txtuserNameOfManager.Text,
                        UserPassword = Encryptor.MD5Hash(txtPasswordOfManager.Text),
                        Address = txtAddress.Text,
                        UserImage = "/" + ImgUpload?.PostedFile?.FileName,
                        ManagerOfDepartment = ddlmangerForDept?.SelectedItem?.Value

                    };

                    if (string.IsNullOrEmpty(txtuserNameOfManager.Text))
                    {
                        memberManger.UserName = memberManger.Name;
                    }
                    else
                    {
                        memberManger.UserName = txtuserNameOfManager.Text;
                    }
                    if (ViewState["Phone"].ToString() == txtPhoneNumber.Text.Trim().ToLower())
                    {
                        memberManger.Phone = txtPhoneNumber.Text.Trim().ToLower();
                        if (ImgUpload.PostedFile != null && ImgUpload.PostedFile.ContentLength > 0)
                        {
                            string fname = Path.GetFileName(ImgUpload.PostedFile.FileName);
                            ImgUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads/", fname)));
                            ImgUser.ImageUrl = "~/MasterPage/Uploads/" + fname;

                            memberManger.UserImage = "/" + fname;

                        }
                        else
                        {

                            memberManger.UserImage = ImgUser.ImageUrl.Replace("~/MasterPage/Uploads/", "");
                        }


                        string inputJson = (new JavaScriptSerializer()).Serialize(memberManger);
                        client.UploadString(apiUrlUser + "/PutUser?id=" + Convert.ToInt32(ViewState["UserID"]), inputJson);
                        Response.Redirect("MemberMangement.aspx?id=redirectUpdate");
                    }
                    else
                    {
                        bool phoneExist = WebApiConsume.ValidateUserPhone(txtPhoneNumber.Text.Trim().ToLower());
                        if (!phoneExist)
                        {
                            memberManger.Phone = txtPhoneNumber.Text.Trim().ToLower();
                            if (ImgUpload.PostedFile != null && ImgUpload.PostedFile.ContentLength > 0)
                            {
                                string fname = Path.GetFileName(ImgUpload.PostedFile.FileName);
                                ImgUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads/", fname)));
                                ImgUser.ImageUrl = "~/MasterPage/Uploads/" + fname;

                                memberManger.UserImage = "/" + fname;

                            }
                            else
                            {
                                memberManger.UserImage = ImgUser.ImageUrl;
                            }
                            string inputJson = (new JavaScriptSerializer()).Serialize(memberManger);

                            client.UploadString(apiUrlUser + "/PutUser?id=" + Convert.ToInt32(ViewState["UserID"]), inputJson);
                            Response.Redirect("MemberMangement.aspx?id=redirectUpdate");

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('هذا الرقم موجود من قبل .من فضلك قك بتسجيل رقم اخر')", true);

                        }
                    }

                }
                else
                {
                    Committee.Models.User memberUpdate = new Models.User()
                    {
                        ID = Convert.ToInt32(Request.QueryString["mId"]),
                        Name = txtMemberName.Text,
                        UserEmailId = txtMemberEmail.Text,

                        Title = txtMemberJob.Text,
                        SecrtaryOfDept = Session["DeptId"].ToString(),
                        WorkSide = txtWorkSide.Text,
                        SystemRole = Convert.ToInt32(ddlMemberRole.SelectedItem.Value),
                        UserName = txtUserName.Text,
                        UserPassword = Encryptor.MD5Hash(txtPass.Text),
                        Address = txtAddress.Text,
                        ManagerOfDepartment = Session["DeptId"].ToString(),

                        // UserImage = "/" + ImgUpload?.PostedFile?.FileName
                    };
                    if (string.IsNullOrEmpty(txtUserName.Text))
                    {
                        memberUpdate.UserName = memberUpdate.Name;
                    }
                    if (ViewState["Phone"].ToString() == txtPhoneNumber.Text.Trim().ToLower())
                    {
                        memberUpdate.Phone = txtPhoneNumber.Text.Trim().ToLower();
                        if (ImgUpload.PostedFile != null && ImgUpload.PostedFile.ContentLength > 0)
                        {
                            string fname = Path.GetFileName(ImgUpload.PostedFile.FileName);
                            ImgUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads/", fname)));
                            ImgUser.ImageUrl = "~/MasterPage/Uploads/" + fname;

                            memberUpdate.UserImage = "/" + fname;

                        }
                        else
                        {

                            memberUpdate.UserImage = ImgUser.ImageUrl.Replace("~/MasterPage/Uploads/", "");
                        }


                        string inputJson = (new JavaScriptSerializer()).Serialize(memberUpdate);
                        client.UploadString(apiUrlUser + "/PutUser?id=" + Convert.ToInt32(ViewState["UserID"]), inputJson);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات العضو بنجاح', 'تم')", true);
                    }
                    else
                    {
                        bool phoneExist = WebApiConsume.ValidateUserPhone(txtPhoneNumber.Text.Trim().ToLower());
                        if (!phoneExist)
                        {
                            memberUpdate.Phone = txtPhoneNumber.Text.Trim().ToLower();
                            if (ImgUpload.PostedFile != null && ImgUpload.PostedFile.ContentLength > 0)
                            {
                                string fname = Path.GetFileName(ImgUpload.PostedFile.FileName);
                                ImgUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads/", fname)));
                                ImgUser.ImageUrl = "~/MasterPage/Uploads/" + fname;

                                memberUpdate.UserImage = "/" + fname;

                            }
                            else
                            {
                                memberUpdate.UserImage = ImgUser.ImageUrl;
                            }
                            string inputJson = (new JavaScriptSerializer()).Serialize(memberUpdate);

                            client.UploadString(apiUrlUser + "/PutUser?id=" + Convert.ToInt32(ViewState["UserID"]), inputJson);
                            Response.Redirect("MemberMangement.aspx?id=redirectUpdate");

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('هذا الرقم موجود من قبل .من فضلك قك بتسجيل رقم اخر')", true);

                        }
                    }
                }

            }

            else if (Request.QueryString["status"] == "new")
            {
                bool phoneExist = WebApiConsume.ValidateUserPhone(txtPhoneNumber.Text.Trim().ToLower());
                if (!phoneExist)
                {


                    if (ImgUpload.PostedFile != null && ImgUpload.PostedFile.ContentLength > 0)
                    {
                        string fname = Path.GetFileName(ImgUpload.PostedFile.FileName);
                        ImgUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads/", fname)));

                    }


                    string apiUrlMember = Utilities.BASE_URL+"/api/Users";
                
                    if (Session["SystemRole"].ToString() == "1")
                    {
                        int systemRole = 0;
                        string secRole=null;
                        if (ddlMemberType.SelectedItem.Value=="4")
                        {
                            systemRole = 4;
                            secRole = ddlmangerForDept?.SelectedItem?.Value;
                        }
                        if (ddlMemberType.SelectedItem.Value == "5")
                        {
                            systemRole = 5;
                            secRole = null;

                        }
                        Committee.Models.User memberManger = new Models.User()
                        {

                            Name = txtMemberName.Text,
                            UserEmailId = txtMemberEmail.Text,
                            Phone = txtPhoneNumber.Text,
                            Title = txtMemberJob.Text,
                            SecrtaryOfDept = secRole,
                            WorkSide = txtWorkSide.Text,
                            SystemRole = systemRole,
                            UserName = txtUserName.Text,
                            UserPassword = Encryptor.MD5Hash(txtPasswordOfManager.Text),
                            Address = txtAddress.Text,
                            UserImage = "/" + ImgUpload?.PostedFile?.FileName,
                            ManagerOfDepartment = ddlmangerForDept?.SelectedItem?.Value,


                        };

                        if (string.IsNullOrEmpty(txtuserNameOfManager.Text))
                        {
                            memberManger.UserName = memberManger.Name;
                        }
                        else
                        {
                            memberManger.UserName = txtuserNameOfManager.Text;
                        }
                        string inputJsonMember = (new JavaScriptSerializer()).Serialize(memberManger);


                        client.UploadString(apiUrlMember + "/PostUser", inputJsonMember);
                    }
                    else
                    {
                        Committee.Models.User member = new Models.User()
                        {

                            Name = txtMemberName.Text,
                            UserEmailId = txtMemberEmail.Text,
                            Phone = txtPhoneNumber.Text,
                            Title = txtMemberJob.Text,
                            SecrtaryOfDept = ddlmangerForDept?.SelectedItem?.Value,
                            WorkSide = txtWorkSide.Text,
                            SystemRole = 6,
                            UserName = txtUserName.Text,
                            UserPassword = Encryptor.MD5Hash(txtPass.Text),
                            Address = txtAddress.Text,
                            UserImage = "/" + ImgUpload?.PostedFile?.FileName,
                            ManagerOfDepartment = Session["DeptId"].ToString(),


                        };
                        if (string.IsNullOrEmpty(txtUserName.Text))
                        {
                            member.UserName = member.Name;
                        }
                        string inputJson = (new JavaScriptSerializer()).Serialize(member);


                        client.UploadString(apiUrlMember + "/PostUser", inputJson);
                    }

                   



                    ResetControld();
                    Response.Redirect("MemberMangement.aspx?id=redirectSave");


                }
                else
                {
                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('هذا الرقم موجود من قبل .من فضلك قك بتسجيل رقم اخر', 'تم')", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('هذا الرقم موجود من قبل .من فضلك قك بتسجيل رقم اخر');", true);

                }
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            

        }


        private List<Committee.Models.UserArabicSearch> ShowMembers()
        {
            List<Committee.Models.UserArabicSearch> users = new List<Models.UserArabicSearch>();
            string apiUrl3 = Utilities.BASE_URL+"/api/Users";

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
                    رقم_الجوال = member.Phone,
                    العنوان = member.Address,
                    //النوع = member.Gender,
                    الوظيفه = member.Title,
                    الصلاحيات = member.SystemRoleMap.titleAr,
                    جهة_العمل = member.WorkSide,
                    //الادارة = member?.Department?.DeptName
                });
            }
            return users;
        }

      

        protected void ddlMemberRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberRole.SelectedIndex==1||ddlMemberRole.SelectedIndex==2)
            {
                divUserName.Visible = true;
                divpass.Visible = true;
                divDept.Visible = false;

            }
            else if (ddlMemberRole.SelectedIndex == 3 )
            {
                divDept.Visible = false;
                divUserName.Visible = true;
                divpass.Visible = true;
                txtUserName.Text = "";
                txtPass.Text = "";
            }
            if (ddlMemberRole.SelectedIndex == 4)
            {
                divDept.Visible = true;
                divpass.Visible = false;
                divUserName.Visible = false;
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result2 = webClient.DownloadString(Utilities.BASE_URL+"/api/Deapartments/GetDepartments");
                List<Committee.Models.Department> departments = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Department>>(result2);


                ddlCommitteeDept.DataSource = departments;
                ddlCommitteeDept.DataTextField = "DeptName";
                ddlCommitteeDept.DataValueField = "DeptId";
                ddlCommitteeDept.DataBind();
                ddlCommitteeDept.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
            }
            else if(ddlMemberRole.SelectedIndex != 4 && ddlMemberRole.SelectedIndex != 1 && ddlMemberRole.SelectedIndex != 2 && ddlMemberRole.SelectedIndex != 3)
            {
                divDept.Visible = false;
                divUserName.Visible = false;
                divpass.Visible = false;
            }

        }

      
    }
    }
