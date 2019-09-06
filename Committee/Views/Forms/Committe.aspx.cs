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
using Committee.Models;

namespace Committee.Views.Forms
{
    public partial class Committe : System.Web.UI.Page
    {
        public static List<int> selectedUsers;
        public static List<int> changedUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"]==null)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                divAddMember.Visible = false;
                selectedUsers = new List<int>();

                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString("http://localhost:1481/api/Users/GetUsers");
                List<Committee.Models.User> Members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(result);
                ddlMemberSelect.DataSource = Members;
                ddlMemberSelect.DataTextField = "UserName";
                ddlMemberSelect.DataValueField = "ID";
                ddlMemberSelect.DataBind();
                ddlMemberSelect.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                ddlCommitteeSecrtary.DataSource = Members;
                ddlCommitteeSecrtary.DataTextField = "UserName";
                ddlCommitteeSecrtary.DataValueField = "ID";
                ddlCommitteeSecrtary.DataBind();
                ddlCommitteeSecrtary.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));

                ddlCommitteepresident.DataSource = Members;
                ddlCommitteepresident.DataTextField = "UserName";
                ddlCommitteepresident.DataValueField = "ID";
                ddlCommitteepresident.DataBind();
                ddlCommitteepresident.Items.Insert(0, new ListItem("أختر من القائمه","NULL"));
                WebClient webClient2 = new WebClient();
                webClient2.Headers["Content-type"] = "application/json";
                webClient2.Encoding = Encoding.UTF8;
                string result2 = webClient.DownloadString("http://localhost:1481/api/Deapartments/GetDepartments");
                List<Committee.Models.Department> departments = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Department>>(result2);

               
                ddlCommitteeDept.DataSource = departments;
                ddlCommitteeDept.DataTextField = "DeptName";
                ddlCommitteeDept.DataValueField = "DeptId";
                ddlCommitteeDept.DataBind();
                ddlCommitteeDept.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                if (Session["SystemRole"].ToString() == "2")

                {
                    //lblsec.Visible = false;
                    //lblmanger.Visible = false;
                    LabelddlCommitteepresident.Visible = false;
                    LabeddlCommitteeSecrtary.Visible = false;
                    ddlCommitteeSecrtary.Visible = false;
                    ddlCommitteepresident.Visible = false;
                    divAddMembers.Visible = true;
                    ddlMemberSelect.Visible = true;
                    ddlMemberSelect.DataSource = Members;
                    ddlMemberSelect.DataBind();
                        

                }
                if (Request.QueryString["status"]== "update")
                    
                {
                    List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
                    string apiUrl3 = "http://localhost:1481/api/Committees";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    int cId = Convert.ToInt32(Request.QueryString["cId"]);
                    ViewState["committeeID"] = cId;
                    Committee.Models.CommitteeRetrieveUpdate Committee = (new JavaScriptSerializer()).Deserialize<Committee.Models.CommitteeRetrieveUpdate>(client.DownloadString(apiUrl3 + "/GetCommitteeByIdForWeb?committeeId=" +cId.ToString()));
                    txtCommitteeDate.Text = Committee.CommitteeDate;
                    ddlCommitteeClassification.SelectedValue = Committee.Type.Id.ToString();
                    ddlCommitteeImportancy.SelectedValue = Committee.Importance.Id.ToString();
                    ddlcommitteeStatus.SelectedValue = Committee.Activity.Id.ToString();
                    txtCommitteeTopic.Text = Committee.CommitteeTopic;
                    txtCommitteeBasedON.Text = Committee.CommitteeBasedOn;
                    txtEnrollmentNumber.Text = Committee.CommitteeEnrollmentNumber;
                    txtEnrollmentDate.Text = Committee.CommitteeEnrollmentDate;
                    txtInboxSide.Text = Committee.CommitteeInbox1;
                    ddlCommitteeDept.SelectedValue = Committee?.Department?.DeptId.ToString();
                    txtCommitteeName.Text = Committee.CommitteeName;
                    ddlCommitteepresident.SelectedValue = Committee.CommitteeManager;
                    ddlCommitteeSecrtary.SelectedValue = Committee.CommitteeSecretary;
                   
                    //gvCommittee.DataSource = ShowCommittees();
                    //gvCommittee.DataBind();
                }
            }
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            string apiUrl = "http://localhost:1481/api/Committees";
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            if (Request.QueryString["status"] == "update")
            {
                int committeeId = Convert.ToInt32(Request.QueryString["cId"]);
              
                List<UserGrid> data = ShowCommitteeMembers(committeeId);
                gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();

                gvMembersOfCommittee.DataBind();
                ViewState["CommitteeId"] = committeeId;
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString("http://localhost:1481/api/Users/GetUsers");
                List<Committee.Models.User> Members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(result);
                ddlMemberChange.DataSource = Members;
                ddlMemberChange.DataTextField = "UserName";
                ddlMemberChange.DataValueField = "ID";
                ddlMemberChange.DataBind();
                divAddMember.Visible = true;
                List<UserGrid> data2 = ShowCommitteeMembers(committeeId);
                gvMembersOfCommittee.DataSource = data2.Where(x => x != null).ToList();

                gvMembersOfCommittee.DataBind();
                if (Session["SystemRole"].ToString() == "2")

                {
                    divAddMember.Visible = true;
                }

                Committee.Models.Committee committeeUpdate = new Models.Committee()
                {
                    CommitteeName = txtCommitteeName.Text,
                    CommitteeDate = txtCommitteeDate.Text,
                    TypeId = Convert.ToInt32(ddlCommitteeClassification.SelectedItem.Value),
                    ActivityId = Convert.ToInt32(ddlcommitteeStatus.SelectedItem.Value),
                    ImportanceId = Convert.ToInt32(ddlCommitteeImportancy.SelectedItem.Value),
                    CommitteeTopic = txtCommitteeTopic.Text,
                    CommitteeBasedOn = txtCommitteeBasedON.Text,
                    CommitteeEnrollmentNumber = txtEnrollmentNumber.Text,
                    CommitteeEnrollmentDate = txtEnrollmentDate.Text,
                    CommitteeManager = ddlCommitteepresident.SelectedItem.Value == "NULL" ? null : ddlCommitteepresident.SelectedItem.Value,
                    CommitteeSecretary = ddlCommitteeSecrtary.SelectedItem.Value == "NULL" ? null : ddlCommitteeSecrtary.SelectedItem.Value,
                    CommitteeInbox1 = txtInboxSide.Text,
                    DeptId = ddlCommitteeDept.SelectedItem.Value == "NULL" ? null : ddlCommitteeDept.SelectedItem.Value,
                    CommitteeId = Convert.ToInt32(ViewState["committeeID"])

                };

                string inputJson = (new JavaScriptSerializer()).Serialize(committeeUpdate);

                client.UploadString(apiUrl + "/PutCommittee?id=" + Convert.ToInt32(ViewState["committeeID"]), inputJson);
                btnAdd1.Text = "حفظ";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم تعديل بيانات اللجنة بنجاح')", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات اللجنة بنجاح', 'تم')", true);
                //gvCommittee.EditIndex = -1;
            }
            else if (Request.QueryString["status"] == "new")
            {
                string isCommitteeExist = "http://localhost:1481/api/Committees";
                object input = new
                {
                    Name = txtCommitteeName.Text.Trim(),
                };
                string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
                bool CommitteeExist = (new JavaScriptSerializer()).Deserialize<bool>(client.DownloadString(apiUrl + "/IsCommitteeExist?committeeName=" + txtCommitteeName.Text));
                if (CommitteeExist)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('هذا الاسم موجود بالفعل من فضلك قم بادخال اسم اخر لللجنه ', 'تحذير')", true);


                }
                else
                {
                    if (ddlCommitteepresident.SelectedItem.Value != "NULL" || ddlCommitteeSecrtary.SelectedItem.Value != "NULL")
                    {
                        if (selectedUsers.Count == 0&&ddlCommitteeSecrtary.Visible==true&&ddlCommitteepresident.Visible==true)
                        {
                            selectedUsers.Add(Convert.ToInt32(ddlCommitteeSecrtary.SelectedItem.Value));
                            selectedUsers.Add(Convert.ToInt32(ddlCommitteepresident.SelectedItem.Value));

                        }
                    }


                    // HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://committeeapi20190806070934.azurewebsites.net/api/CommitteesMembers/ValidateUserNameAndPassword?userName=" + txtuserName.Text + "&password=" + txtPass.Text);

                    string apiUrl2 = "http://localhost:1481/api/CommitteesMembers";
                    string apiUrl3 = "http://localhost:1481/api/Committees";
                    WebClient client3 = new WebClient();
                    client3.Headers["Content-type"] = "application/json";
                    client3.Encoding = Encoding.UTF8;

                    Committee.Models.Committee committee = new Models.Committee()
                    {
                        CommitteeName = txtCommitteeName.Text,
                        CommitteeDate = txtCommitteeDate.Text,
                        TypeId = Convert.ToInt32(ddlCommitteeClassification.SelectedItem.Value),
                        ActivityId = Convert.ToInt32(ddlcommitteeStatus.SelectedItem.Value),
                        ImportanceId = Convert.ToInt32(ddlCommitteeImportancy.SelectedItem.Value),
                        CommitteeTopic = txtCommitteeTopic.Text,
                        CommitteeBasedOn = txtCommitteeBasedON.Text,
                        CommitteeEnrollmentNumber = txtEnrollmentNumber.Text,
                        CommitteeEnrollmentDate = txtEnrollmentDate.Text,
                        CommitteeManager = ddlCommitteepresident.SelectedItem.Value == "NULL" ? null : ddlCommitteepresident.SelectedItem.Value,
                        CommitteeSecretary = ddlCommitteeSecrtary.SelectedItem.Value == "NULL" ? null : ddlCommitteeSecrtary.SelectedItem.Value,
                        CommitteeInbox1 = txtInboxSide.Text,
                        DeptId = ddlCommitteeDept.SelectedItem.Value == "NULL" ? null : ddlCommitteeDept.SelectedItem.Value,
                        CreatedAt= DateTime.Now.ToString("yyyy-MM-dd"),
                        UpdatedAt= DateTime.Now.ToString("yyyy-MM-dd")


                    };



                    string inputJson = (new JavaScriptSerializer()).Serialize(committee);


                    client3.UploadString(apiUrl + "/PostCommitteeForWeb", inputJson);

                    List<CommitteesMember> committeesMembers = new List<CommitteesMember>();
                    int Committee = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/GetCommitteeId?committeeName=" + txtCommitteeName.Text));
                    int committeeRole = 0;
                    for (int i = 0; i < selectedUsers.Count; i++)
                    {
                        // client.DownloadString(apiUrl3 + "/GetCommitteeId?committeeName=" + txtCommitteeName.Text);

                        if (i == 0)
                        {
                            committeeRole = 4;
                        }
                        if (i == 1)
                        {
                            committeeRole = 5;
                        }
                        if (i > 1)
                        {
                            committeeRole = 6;
                        }
                        committeesMembers.Add(new CommitteesMember()
                        {
                            MemberId = selectedUsers[i],
                            CommitteeId = Committee,
                            CommitteeRole = committeeRole


                        });
                    }
                    string inputJson2 = (new JavaScriptSerializer()).Serialize(committeesMembers);
                    //WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    client.UploadString(apiUrl2 + "/PostListOfCommitteesMember", inputJson2);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم حفظ بيانات اللجنة بنجاح')", true);

                  //  Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات اللجنة بنجاح', 'تم')", true);
                    string apiUrlUser = "http://localhost:1481/api/Users";
                    WebClient clientuser = new WebClient();
                    clientuser.Headers["Content-type"] = "application/json";
                    clientuser.Encoding = Encoding.UTF8;
                    //string apiUrlFcm = "http://localhost:1481/api/Fcm";
                    //WebClient clienfcm = new WebClient();
                    //clienfcm.Headers["Content-type"] = "application/json";
                    //clienfcm.Encoding = Encoding.UTF8;
                    foreach (var member in committeesMembers)
                    {
                       
                        object memb = new
                        {
                            id = member.MemberId,
                        };
                       
                        string inputuser = (new JavaScriptSerializer()).Serialize(input);
                        User userFcm = (new JavaScriptSerializer()).Deserialize<User>(client.DownloadString(apiUrlUser + "/GetUser?id=" + member.MemberId));
                        string apiUrlFcm = "http://localhost:1481/api/Fcm";
                        WebClient clienfcm = new WebClient();
                        clienfcm.Headers["Content-type"] = "application/json";
                        clienfcm.Encoding = Encoding.UTF8;
                        object UserFcmo = new
                        {
                          
                           Action_id= member.CommitteeId, Body="اشعار بخصوص لجنة",Click_action= "type_join_committe", Title="اضافة لجنة" 
                        };
                        string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                        clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to="+ userFcm.FCMToken, inputFcm);

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم ارسال الاشعارات بنجاح', 'تم')", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم ارسال الاشعارات بنجاح')", true);

                        string apiUrlAlert = "http://localhost:1481/api/Committees";
                        WebClient client4 = new WebClient();

                        client4.Headers["Content-type"] = "application/json";
                        client4.Encoding = Encoding.UTF8;

                        Alert alert = new Alert()
                        {
                            Title = "لجنة جديده",
                            Message = "اشعار بخصوص اضافة لجنة",
                            UserId = member.MemberId,
                            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
                        };
                        string inputAlert = (new JavaScriptSerializer()).Serialize(alert);


                        client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);
                    }
                    ViewState["update"] = 0;
                    selectedUsers = new List<int>();
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (selectedUsers.Count==0 && ddlCommitteepresident.Visible==true && ddlCommitteeSecrtary.Visible==true)
            {
                selectedUsers.Add(Convert.ToInt32(ddlCommitteeSecrtary.SelectedItem.Value));
                selectedUsers.Add(Convert.ToInt32(ddlCommitteepresident.SelectedItem.Value));
          
            }
            

            if (selectedUsers.Contains(Convert.ToInt32(ddlMemberSelect.SelectedItem.Value)))
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('هذا العضو موجود بالفعل كونه سكرتير او رئيس لجنه')", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('هذا العضو موجود بالفعل ', 'تحذير')", true);
            }
            else
            {
               selectedUsers.Add(Convert.ToInt32(ddlMemberSelect.SelectedItem.Value));
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم ادخال العضو للجنة بنجاح')", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم ادخال هذا العضو بنجاح', 'تم')", true);


            }



        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            ResetControls();
            divAddMember.Visible = false;
        }

        private void ResetControls()
        {
            txtCommitteeName.Text = "";
            txtCommitteeDate.Text = "";
            txtCommitteeTopic.Text = "";
            txtInboxSide.Text = ""; txtCommitteeBasedON.Text = "";
            txtEnrollmentNumber.Text = "";
            txtEnrollmentDate.Text = "";
            ddlCommitteeClassification.SelectedIndex = -1;
            ddlcommitteeStatus.SelectedIndex = -1;
            ddlCommitteeSecrtary.SelectedIndex = -1;
            ddlCommitteepresident.SelectedIndex = -1;
            ddlCommitteeImportancy.SelectedIndex = -1;
            ddlCommitteeDept.SelectedIndex = -1;
            btnAdd1.Text = "Save";
            //gvCommittee.Visible = false;
            gvMembersOfCommittee.Visible = false;



        }

        protected void txtCommitteeName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd3_Click(object sender, EventArgs e)
        {
            divAddMember.Visible = false;
           
            //gvCommittee.Visible = true;
            //if (Committee!=null)
            //{
            //    txtCommitteeDate.Text = Committee.CommitteeDate;
            //    ddlCommitteeClassification.SelectedIndex = Committee.IsMilitarized==true ? 0 : 1;
            //    ddlCommitteeImportancy.SelectedIndex = Committee.IsImportant == true ? 1 : 0;
            //    ddlcommitteeStatus.SelectedIndex = Committee.IsActive == true ? 1 : 0;
            //    txtCommitteeTopic.Text = Committee.CommitteeTopic;
            //    txtCommitteeBasedON.Text = Committee.CommitteeBasedOn;
            //    txtEnrollmentNumber.Text = Committee.CommitteeEnrollmentNumber;
            //    txtEnrollmentDate.Text = Committee.CommitteeEnrollmentDate;
            //    ViewState["committeeID"] = Committee.CommitteeId;
            //}
            //ViewState["update"] = 1;
            //gvCommittee.DataSource = ShowCommittees();
            //gvCommittee.DataBind();
        }

        //protected void gvCommittee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //    int committeeId = Convert.ToInt32(gvCommittee.Rows[e.RowIndex].Cells[1].Text.ToString());
        //    string apiUrl3 = "http://localhost:1481/api/Committees";

        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    object input = new
        //    {
        //        id = committeeId,
        //    };
        //    string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
        //    string CommitteeExist = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrl3 + "/DeleteCommittee?id=" + committeeId.ToString()));
        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات اللجنة بنجاح', 'تم')", true);
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم مسح بيانات اللجنة بنجاح');", true);

        //    gvCommittee.DataSource = ShowCommittees();
        //    gvCommittee.DataBind();

        //}

        private List<CommitteeSearch> ShowCommittees()
        {
            List<CommitteeSearch> committeesUpdate = new List<CommitteeSearch>();
            string apiUrl3 = "http://localhost:1481/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveData> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveData>>(client.DownloadString(apiUrl3 + "/GetCommitteeDetails?committeeName=" + txtCommitteeName.Text.Trim().ToLower()+"&commiteeDate="+txtCommitteeDate.Text));
            foreach (var committee in Committees)
            {
                committeesUpdate.Add(new CommitteeSearch() {رقم_اللجنه=committee.CommitteeId,اسم_اللجنه=committee.CommitteeName,تاريخ_اللجنه=committee.CommitteeDate,موضوع_اللجنه=committee.CommitteeTopic,
                الأمر_المستند_عليه=committee.CommitteeBasedOn,الإداره=committee?.Department?.DeptName,تصنيف_اللجنه=committee.Type.titleAr,
                حال_اللجنه=committee.Activity.titleAr,مستوى_الأهميه=committee.Importance.titleAr,
                رئيس_اللجنه=committee.CommitteeManager,سكرتير_اللجنه=committee.CommitteeSecretary,جهة_الوارد=committee.CommitteeInbox1,
                رقم_القيد=committee.CommitteeEnrollmentNumber,سنة_القيد=committee.CommitteeEnrollmentDate,تاريخ_التعديل=committee.CreatedAt,
                تاريخ_اللإنشاء=committee.UpdatedAt});
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
           

            return Committees.Select(x=>new UserGrid() { رقم_العضو=x.ID,اسم_العضو=x.UserName}).ToList();

        }
 //       protected void gvCommittee_RowDataBound(object sender, GridViewRowEventArgs e)
 //       {
 //           if (e.Row.RowType == DataControlRowType.DataRow)
 //           {
 //               //Change the index number as per your gridview design
 //               e.Row.Cells[1].Enabled = false;



 //           }
           
 //           if (e.Row.RowType != DataControlRowType.DataRow) return;

 //           var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
 //           var editButton = (LinkButton)e.Row.Cells[0].Controls[0];
 //           var selectButton = (LinkButton)e.Row.Cells[0].Controls[4];


 //           if (Session["SystemRole"].ToString() == "1")
 //           {
 //               deleteButton.Visible = true;
 //               deleteButton.Text = "مسح";

 //               editButton.Visible = true;
 //               editButton.Text = "تعديل";
 //               selectButton.Text = "اختيار";
 //           }
 //           else
 //           {
 //               deleteButton.Visible = false;
 //               editButton.Visible = false;
 //               selectButton.Text = "اختيار";
 //           }
 //           // var deleteButton2 = (LinkButton)e.Row.Cells[2].Controls[0];
 //           if (deleteButton.Text == "Delete" || deleteButton.Text == "مسح")
 //           {
 //               deleteButton.Text = "مسح";
 //               deleteButton.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";
 //              // deleteButton2.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";

 //           }
 //       }

 //       protected void gvCommittee_SelectedIndexChanged(object sender, EventArgs e)
 //       {
 //           int committeeId =  Convert.ToInt32(gvCommittee.SelectedRow.Cells[1].Text);
 //           GridViewRow row = gvCommittee.SelectedRow;
 //         List<UserGrid>data = ShowCommitteeMembers(committeeId);
 //           gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();
               
 //           gvMembersOfCommittee.DataBind();
 //           ViewState["CommitteeId"] = committeeId;
 //           WebClient webClient = new WebClient();
 //           webClient.Headers["Content-type"] = "application/json";
 //           webClient.Encoding = Encoding.UTF8;
 //           string result = webClient.DownloadString("http://localhost:1481/api/Users/GetUsers");
 //           List<Committee.Models.User> Members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(result);
 //           ddlMemberChange.DataSource = Members;
 //           ddlMemberChange.DataTextField = "UserName";
 //           ddlMemberChange.DataValueField = "ID";
 //           ddlMemberChange.DataBind();
 //         //  divAddMember.Visible = true;
 //           List<UserGrid> data2 = ShowCommitteeMembers(committeeId);
 //           gvMembersOfCommittee.DataSource = data2.Where(x => x != null).ToList() ;

 //           gvMembersOfCommittee.DataBind();
 //           if (Session["SystemRole"].ToString() == "2")

 //           {
 //               divAddMember.Visible = true;
 //           }
               
 //       }

 //       protected void gvCommittee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
 //       {
 //           //divMembers.Visible = true;
 //           //string committeeId = gvCommittee.SelectedRow.Cells[3].Text;
 //          // gvMembersOfCommittee.DataSource = ShowCommitteeMembers(committeeId);
 //           gvMembersOfCommittee.DataBind();

 //       }

 //       protected void gvCommittee_Sorting(object sender, GridViewSortEventArgs e)
 //       {

 //       }

 //       protected void gvCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
 //       {
 //           gvCommittee.PageIndex = e.NewPageIndex;
 //           gvCommittee.DataSource = ShowCommittees();
 //           gvCommittee.DataBind();
 //       }

 //       protected void gvCommittee_PageIndexChanged(object sender, EventArgs e)
 //       {

 //       }

 //       protected void gvCommittee_RowEditing(object sender, GridViewEditEventArgs e)
 //       {

 //           List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
 //           string apiUrl3 = "http://localhost:1481/api/Committees";

 //           WebClient client = new WebClient();
 //           client.Headers["Content-type"] = "application/json";
 //           client.Encoding = Encoding.UTF8;

 //           Committee.Models.CommitteeRetrieveUpdate Committee = (new JavaScriptSerializer()).Deserialize<Committee.Models.CommitteeRetrieveUpdate>(client.DownloadString(apiUrl3 + "/GetCommitteeByIdForWeb?committeeId=" + gvCommittee.Rows[e.NewEditIndex].Cells[1].Text));
 //           txtCommitteeDate.Text = Committee.CommitteeDate;
 //           ddlCommitteeClassification.SelectedValue = Committee.Type.Id.ToString();
 //           ddlCommitteeImportancy.SelectedValue = Committee.Importance.Id.ToString();
 //           ddlcommitteeStatus.SelectedValue = Committee.Activity.Id.ToString() ;
 //           txtCommitteeTopic.Text = Committee.CommitteeTopic;
 //           txtCommitteeBasedON.Text = Committee.CommitteeBasedOn;
 //           txtEnrollmentNumber.Text = Committee.CommitteeEnrollmentNumber;
 //           txtEnrollmentDate.Text = Committee.CommitteeEnrollmentDate;
 //           txtInboxSide.Text = Committee.CommitteeInbox1;
 //           ddlCommitteeDept.SelectedValue = Committee?.Department?.DeptId.ToString();
 //           txtCommitteeName.Text = Committee.CommitteeName;
 //           ddlCommitteepresident.SelectedValue = Committee.CommitteeManager;
 //           ddlCommitteeSecrtary.SelectedValue = Committee.CommitteeSecretary;

          
 //           btnAdd1.Text = "تعديل";
 //           ViewState["committeeID"] = gvCommittee.Rows[e.NewEditIndex].Cells[1].Text;
 //           gvCommittee.EditIndex = -1;
 //           gvCommittee.DataSource = ShowCommittees();
 //           gvCommittee.DataBind();


 //       }

 //       protected void gvCommittee_RowUpdating(object sender, GridViewUpdateEventArgs e)
 //       {
 //           WebClient client = new WebClient();
 //           client.Headers["Content-type"] = "application/json";
 //           client.Encoding = Encoding.UTF8;

 //           GridViewRow row = (GridViewRow)gvCommittee.Rows[e.RowIndex];
 //           TextBox textid = (TextBox)row.Cells[1].Controls[0];
 //           int id1 = Convert.ToInt32(textid.Text);
 //           TextBox CommitteeName = (TextBox)row.Cells[3].Controls[0];
 //           DropDownList CommitteManagerName = (DropDownList)row.Cells[2].Controls[0];
            
 //           DropDownList CommitteeSecrtary = (DropDownList)row.Cells[4].Controls[0];
 //           DropDownList Active=(DropDownList)row.Cells[11].Controls[0];
 //           DropDownList Important = (DropDownList)row.Cells[12].Controls[0];
 //           DropDownList Militraized = (DropDownList)row.Cells[13].Controls[0];
 //           TextBox committeeDate = (TextBox)row.Cells[5].Controls[0];
 //           TextBox committeeTopic = (TextBox)row.Cells[6].Controls[0];
 //           TextBox committeeBasedOn = (TextBox)row.Cells[7].Controls[0];
 //           TextBox committeeInbox1 = (TextBox)row.Cells[8].Controls[0];
 //           TextBox committeeEnrollmentNumber = (TextBox)row.Cells[9].Controls[0];
 //           TextBox committeeEnrollmentDate = (TextBox)row.Cells[10].Controls[0];
 ////Setting the EditIx property to -1 to cancel the Edit mode in Gridview  
 //           gvCommittee.EditIndex = -1;

 //           //Call ShowData method for displaying updated data  

 //           string apiUrlUpdate = "http://localhost:1481/api/Committees";
 //           Committee.Models.Committee committeeUpdate = new Models.Committee()
 //           {
 //               CommitteeId = Convert.ToInt32(textid.Text),
 //               CommitteeName = CommitteeName.Text,
 //               CommitteeDate = committeeDate.Text,
 //               //IsMilitarized =Militraized.Checked,
 //               //IsActive = Active.Checked,
 //               //IsImportant = Important.Checked,
 //               CommitteeTopic = committeeTopic.Text,
 //               CommitteeBasedOn = committeeBasedOn.Text,
 //               CommitteeInbox1 = committeeInbox1.Text,
 //               CommitteeEnrollmentNumber = committeeEnrollmentNumber.Text,
 //               CommitteeEnrollmentDate = committeeEnrollmentDate.Text,
 //               CommitteeSecretary = CommitteeSecrtary.Text,
 //               CommitteeManager = CommitteManagerName.Text

 //           };
 //           string inputJson = (new JavaScriptSerializer()).Serialize(committeeUpdate);

 //           client.UploadString(apiUrlUpdate + "/PutCommittee?id=" + Convert.ToInt32(textid.Text), inputJson);
 //           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم تعديل بيانات اللجنة بنجاح')", true);

 //           gvCommittee.DataSource = ShowCommittees();
 //           gvCommittee.DataBind();
 //       }

 //       protected void gvCommittee_DataBound(object sender, EventArgs e)
 //       {

 //       }

 //       protected void gvCommittee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
 //       {
 //           gvCommittee.EditIndex = -1;
 //           gvCommittee.DataSource = ShowCommittees();
 //           gvCommittee.DataBind();
 //       }

        protected void btnAddChange_Click(object sender, EventArgs e)
        {
            List<int> membersList = new List<int>();
            foreach (GridViewRow row in gvMembersOfCommittee.Rows)
            {
                    
                    int member =  Convert.ToInt32(row.Cells[1].Text);
                membersList.Add(member);
                
            }
            string apiUrl2 = "http://localhost:1481/api/CommitteesMembers";
            string apiUrl3 = "http://localhost:1481/api/Users";
            WebClient client3 = new WebClient();
            client3.Headers["Content-type"] = "application/json";
            client3.Encoding = Encoding.UTF8;
            int memberId = (new JavaScriptSerializer()).Deserialize<int>(client3.DownloadString(apiUrl3 + "/GetUserId?memberName=" + ddlMemberChange.SelectedItem.Text));
            if (!membersList.Contains(memberId))
            {


                CommitteesMember input = new CommitteesMember
                {
                    CommitteeId = Convert.ToInt32(ViewState["CommitteeId"]),
                    MemberId = memberId
                };
                string apiUrlUser = "http://localhost:1481/api/Users";
                WebClient client2 = new WebClient();
                client2.Headers["Content-type"] = "application/json";
                client2.Encoding = Encoding.UTF8;
                string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
                client2.UploadString(apiUrl2 + "/PostCommitteesMember", inputJson3);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم اضافة العضو لللجنة بنجاح')", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم اضافة العضو لللجنة بنجاح', 'تم')", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم اضافة العضو لللجنة بنجاح');", true);

                User userFcm = (new JavaScriptSerializer()).Deserialize<User>(client2.DownloadString(apiUrlUser + "/GetUser?id=" + memberId));
                string apiUrlFcm = "http://localhost:1481/api/Fcm";
                WebClient clienfcm = new WebClient();
                clienfcm.Headers["Content-type"] = "application/json";
                clienfcm.Encoding = Encoding.UTF8;
                object UserFcmo = new
                {

                    Action_id = Convert.ToInt32(ViewState["CommitteeId"]),
                    Body = "اشعار بخصوص لجنة",
                    Click_action = "type_join_committe", Title = "اضافة لجنة"
                };
                string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + userFcm.FCMToken, inputFcm);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم ارسال الاشعارات بنجاح', 'تم')", true);
                gvMembersOfCommittee.DataSource = ShowCommitteeMembers(Convert.ToInt32(ViewState["CommitteeId"]));

                gvMembersOfCommittee.DataBind();
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('هذا العضو موجود بالفعل داخل اللجنه', 'تحذير')", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تحذير", "alert('هذا العضو موجود بالفعل داخل اللجنه');", true);


            }

        }

        protected void gvMembersOfCommittee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int memberId = Convert.ToInt32(gvMembersOfCommittee.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = "http://localhost:1481/api/CommitteeMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            int committeeId =Convert.ToInt32(ViewState["CommitteeId"]);
            object input = new
            {
                memberId = memberId,
                committeeId= committeeId
            };
            
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            client.UploadString(apiUrl3 + "/DeleteCommitteeMembers?memberId="+memberId+ "&committeeId="+committeeId, inputJson3);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح العضو من اللجنة بنجاح', 'تم')", true);


            gvMembersOfCommittee.DataSource = ShowCommitteeMembers(Convert.ToInt32(ViewState["CommitteeId"]));

            gvMembersOfCommittee.DataBind();
        }

        protected void gvMembersOfCommittee_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvMembersOfCommittee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change the index number as per your gridview design
                e.Row.Cells[1].Enabled = false;



            }

            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var deleteButton = (LinkButton)e.Row.Cells[0].Controls[0];
            if (Session["SystemRole"].ToString() == "1")
            {
                deleteButton.Visible = false;

            }
            else
            {
                deleteButton.Visible = true;
            }
            if (deleteButton.Text == "Delete" || deleteButton.Text=="مسح")
            {
                deleteButton.Text = "مسح";
                deleteButton.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";
                // deleteButton2.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";

            }
        }
      

        protected void txtCommitteeDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvMembersOfCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMembersOfCommittee.PageIndex = e.NewPageIndex;
            gvMembersOfCommittee.DataSource = ShowCommitteeMembers(Convert.ToInt32(ViewState["CommitteeId"]));
            gvMembersOfCommittee.DataBind();
        }

        protected void gvMembersOfCommittee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnAdd1_Command(object sender, CommandEventArgs e)
        {

        }
    }
}