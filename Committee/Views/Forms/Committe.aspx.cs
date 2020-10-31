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
using Committee.Controller;
using Committee.Models;

namespace Committee.Views.Forms
{
    public partial class Committe : System.Web.UI.Page
    {
        public static List<int> selectedUsers;
        public static List<int> changedUsers;
        public static List<UserGrid> userGrids = new List<UserGrid>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            txtCommitteeDate.Text = txtCommitteeDateHidden.Value;
            if (!IsPostBack)
            {
              
                txtCommitteeDate.Text = "";
                divAddMember.Visible = false;
                selectedUsers = new List<int>();
                userGrids = new List<UserGrid>();
                List<User> Members = WebApiConsume.GetUsers(Utilities.BASE_URL+"/api/Users/GetUsers");


                ddlMemberSelect.DataSource = Members;
                ddlMemberSelect.DataTextField = "Name";
                ddlMemberSelect.DataValueField = "ID";
                ddlMemberSelect.DataBind();
                ddlMemberSelect.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                ddlCommitteeSecrtary.DataSource = Members;
                ddlCommitteeSecrtary.DataTextField = "Name";
                ddlCommitteeSecrtary.DataValueField = "ID";
                ddlCommitteeSecrtary.DataBind();
                ddlCommitteeSecrtary.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));

                ddlCommitteepresident.DataSource = Members;
                ddlCommitteepresident.DataTextField = "Name";
                ddlCommitteepresident.DataValueField = "ID";
                ddlCommitteepresident.DataBind();
                ddlCommitteepresident.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));


                List<Committee.Models.Department> departments = WebApiConsume.GetDepartments(Utilities.BASE_URL+"/api/Deapartments/GetDepartments");


                ddlCommitteeDept.DataSource = departments;
                ddlCommitteeDept.DataTextField = "DeptName";
                ddlCommitteeDept.DataValueField = "DeptId";
                ddlCommitteeDept.DataBind();
                ddlCommitteeDept.SelectedValue = Session["DeptId"].ToString();
                ddlCommitteeDept.Enabled = false;
                ddlCommitteeDept.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                int committeeId = Convert.ToInt32(Request.QueryString["cId"]);

                List<UserGrid> data = WebApiConsume.ShowCommitteeMembers(committeeId);
                if ( Request.QueryString["status"] == "selected")
                  {
                    gvMeeting.DataSource = ShowMeetings(Convert.ToInt32(Request.QueryString["cId"]));
                    gvMeeting.DataBind();
                    
                 
                   
                    if (data.Count!=0)
                    {
                        lblmembersOfCommittee.Visible = true;
                        lblmembersOfCommittee.Text = "الاعضاء المشتركون فى اللجنة";
                    }
                    else { 
                        lblmembersOfCommittee.Visible = true;
                        lblmembersOfCommittee.Text = "لا يوجد اعضاء مشتركون فى  هذه اللجنة";
                    }
                    gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();

                    gvMembersOfCommittee.DataBind();
                    ddlMemberChange.Visible = true;
                    ddlMemberChange.DataSource = Members;
                    ddlMemberChange.DataTextField = "Name";
                    ddlMemberChange.DataValueField = "ID";
                    ddlMemberChange.DataBind();
                    ddlMemberChange.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                    gvMembersOfCommittee.Visible = true;
                    divAddMember.Visible = true;



                }
                
                    gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();

                    gvMembersOfCommittee.DataBind();
                    LabelddlCommitteepresident.Visible = true;
                    LabeddlCommitteeSecrtary.Visible = true;
                    ddlCommitteeSecrtary.Visible = true;
                    ddlCommitteepresident.Visible = true;
                    divAddMembers.Visible = true;
                    ddlMemberSelect.Visible = true;
                    ddlMemberSelect.DataSource = Members;
                    ddlMemberSelect.DataTextField = "Name";
                    ddlMemberSelect.DataValueField = "ID";
                    ddlMemberSelect.DataBind();
                    ddlMemberSelect.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                
                if (Request.QueryString["status"] == "selected")
                {
                    gvMeeting.DataSource = ShowMeetings(Convert.ToInt32(Request.QueryString["cId"]));
                    gvMeeting.DataBind();


                    committeeId = Convert.ToInt32(Request.QueryString["cId"]);

                    data = WebApiConsume.ShowCommitteeMembers(committeeId);
                    if (data.Count != 0)
                    {
                        lblmembersOfCommittee.Visible = true;
                        lblmembersOfCommittee.Text = "الاعضاء المشتركون فى اللجنة";
                        gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();
                        gvMembersOfCommittee.DataBind();
                    }
                    else
                    {
                        lblmembersOfCommittee.Visible = true;
                        lblmembersOfCommittee.Text = "لا يوجد اعضاء مشتركون فى  هذه اللجنة";
                    }
                }

                    if (Request.QueryString["status"] == "update" || Request.QueryString["status"] == "selected")

                {
                 
                    gvMeeting.Visible = true;
                    List<MeetingSearch>ms= ShowMeetings(Convert.ToInt32(Request.QueryString["cId"]));
                    if (ms.Count!=0)
                    {
                        lblgvMeeting.Visible = true;
                        lblgvMeeting.Text = "اجتماعات اللجنة";
                        gvMeeting.DataSource = ms;
                        gvMeeting.DataBind();
                    }
                    else
                    {
                        lblgvMeeting.Visible = true;
                        lblgvMeeting.Text = " لا يوجد اجتماعات لهذه اللجنة";
                     
                    }
                   
                    data = WebApiConsume.ShowCommitteeMembers(Convert.ToInt32(Request.QueryString["cId"]));
                    if (data.Count != 0)
                    {
                        lblmembersOfCommittee.Visible = true;
                        lblmembersOfCommittee.Text = "الاعضاء المشتركون فى اللجنة";
                        gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();
                        gvMembersOfCommittee.DataBind();
                        lblmembersOfCommittee.Visible = true;
                      
                    }
                    else
                    {
                        lblmembersOfCommittee.Visible = true;
                        lblmembersOfCommittee.Text = "لا يوجد اعضاء مشتركون فى  هذه اللجنة";
                    }
                   

                    lblAddCommitteetitle.Text = "تعديل اللجنة";
                    lblAddCommitteeh2.Text = "تعديل اللجنة";
                    List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
                    string apiUrl3 = Utilities.BASE_URL+"/api/Committees";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    int cId = Convert.ToInt32(Request.QueryString["cId"]);
                    ViewState["CommitteeId"] = cId;
                    Committee.Models.CommitteeRetrieveUpdate Committee = (new JavaScriptSerializer()).Deserialize<Committee.Models.CommitteeRetrieveUpdate>(client.DownloadString(apiUrl3 + "/GetCommitteeByIdForWeb?committeeId=" + cId.ToString()));
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
                    ViewState["CreatedAt"] = Committee.CreatedAt;
                    //gvCommittee.DataSource = ShowCommittees();
                    //gvCommittee.DataBind();
                }
                else
                {
                    lblAddCommitteetitle.Text = "اضافة لجنة جديدة";
                    lblAddCommitteeh2.Text = "اضافة لجنة جديدة";
                }
                if (Request.QueryString["status"] == "selected")
                {
                    lblAddCommitteetitle.Text = "عرض اللجنة";
                    lblAddCommitteeh2.Text = "عرض اللجنة";
                    List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
                    string apiUrl3 = Utilities.BASE_URL+"/api/Committees";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    int cId = Convert.ToInt32(Request.QueryString["cId"]);
                    ViewState["CommitteeId"] = cId;
                    Committee.Models.CommitteeRetrieveUpdate Committee = (new JavaScriptSerializer()).Deserialize<Committee.Models.CommitteeRetrieveUpdate>(client.DownloadString(apiUrl3 + "/GetCommitteeByIdForWeb?committeeId=" + cId.ToString()));
                    txtCommitteeDate.Text = Committee.CommitteeDate;
                    txtCommitteeDate.Enabled = false;
                    ddlCommitteeClassification.SelectedValue = Committee.Type.Id.ToString();
                    ddlCommitteeClassification.Enabled = false;
                    ddlCommitteeImportancy.SelectedValue = Committee.Importance.Id.ToString();
                    ddlCommitteeImportancy.Enabled = false;
                    ddlcommitteeStatus.SelectedValue = Committee.Activity.Id.ToString();
                    ddlcommitteeStatus.Enabled = false;
                    txtCommitteeTopic.Text = Committee.CommitteeTopic;
                    txtCommitteeTopic.Enabled = false;
                    txtCommitteeBasedON.Text = Committee.CommitteeBasedOn;
                    txtCommitteeBasedON.Enabled = false;
                    txtEnrollmentNumber.Text = Committee.CommitteeEnrollmentNumber;
                    txtEnrollmentNumber.Enabled = false;
                    txtEnrollmentDate.Text = Committee.CommitteeEnrollmentDate;
                    txtEnrollmentDate.Enabled = false;
                    txtInboxSide.Text = Committee.CommitteeInbox1;
                    txtInboxSide.Enabled = false;
                    ddlCommitteeDept.SelectedValue = Committee?.Department?.DeptId.ToString();
                    ddlCommitteeDept.Enabled = false;
                    txtCommitteeName.Text = Committee.CommitteeName;
                    txtCommitteeName.Enabled = false;
                    ddlCommitteepresident.SelectedValue = Committee.CommitteeManager;
                    ddlCommitteepresident.Enabled = false;
                    ddlCommitteeSecrtary.SelectedValue = Committee.CommitteeSecretary;
                    ddlCommitteeSecrtary.Enabled = false;
                    btnSave.Visible = false;
                    divAddMember.Visible = false;
                }
                if (Request.QueryString["status"] == "update")
                {
                    divAddMember.Visible = true;
                    divAddMembers.Visible = false;
                    ddlMemberChange.Visible = true;
                    ddlMemberChange.DataSource = Members;
                    ddlMemberChange.DataTextField = "Name";
                    ddlMemberChange.DataValueField = "ID";
                    ddlMemberChange.DataBind();
                    ddlMemberChange.Items.Insert(0, new ListItem("أختر من القائمه", "NULL"));
                }
                if (Session["SystemRole"].ToString() == "1" && Request.QueryString["status"] == "update")
                {
                    divAddMember.Visible = false;
                    divAddMembers.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (Request.QueryString["status"] == "update" || Request.QueryString["status"] == "selected")
            {
                int committeeId = Convert.ToInt32(Request.QueryString["cId"]);

                List<UserGrid> data = WebApiConsume.ShowCommitteeMembers(committeeId);
                gvMembersOfCommittee.DataSource = data.Where(x => x != null).ToList();

                gvMembersOfCommittee.DataBind();
                ViewState["CommitteeId"] = committeeId;
                List<User> Members = WebApiConsume.GetUsers(Utilities.BASE_URL+"/api/Users/GetUsers");
                ddlMemberChange.DataSource = Members;
                ddlMemberChange.DataTextField = "Name";
                ddlMemberChange.DataValueField = "ID";
                ddlMemberChange.DataBind();
                divAddMember.Visible = true;
                List<UserGrid> data2 = WebApiConsume.ShowCommitteeMembers(committeeId);
                gvMembersOfCommittee.DataSource = data2.Where(x => x != null).ToList();

                gvMembersOfCommittee.DataBind();
                if (Session["SystemRole"].ToString() == "2")

                {
                    divAddMember.Visible = true;
                }

                Committee.Models.Committee committeeUpdate = new Models.Committee()
                {
                    CommitteeName = txtCommitteeName.Text,
                    CommitteeDate = txtCommitteeDateHidden.Value,
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
                    CommitteeId = Convert.ToInt32(ViewState["CommitteeId"]),
                    UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedAt = ViewState["CreatedAt"].ToString()


                };

                string inputJson = (new JavaScriptSerializer()).Serialize(committeeUpdate);
                WebApiConsume.UpdateCommittee(Utilities.BASE_URL+"/api/Committees", Convert.ToInt32(ViewState["CommitteeId"]), inputJson);
                btnSave.Text = "حفظ";

                Response.Redirect("CommitteeMangement.aspx?id=redirectUpdate");

                //gvCommittee.EditIndex = -1;
            }
            else if (Request.QueryString["status"] == "new")
            {
                string isCommitteeExist = "";

                bool CommitteeExist = WebApiConsume.IsCommitteeExist(Utilities.BASE_URL+"/api/Committees", txtCommitteeName.Text.Trim().ToLower());
                if (CommitteeExist)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('هذا الاسم موجود بالفعل من فضلك قم بادخال اسم اخر لللجنه');", true);


                }
                else
                {
                    if (ddlCommitteepresident.SelectedItem.Value != "NULL" || ddlCommitteeSecrtary.SelectedItem.Value != "NULL")
                    {
                        if (selectedUsers.Count == 0 && ddlCommitteeSecrtary.Visible == true && ddlCommitteepresident.Visible == true)
                        {
                            selectedUsers.Add(Convert.ToInt32(ddlCommitteeSecrtary.SelectedItem.Value));
                            selectedUsers.Add(Convert.ToInt32(ddlCommitteepresident.SelectedItem.Value));

                        }
                    }

                    Committee.Models.Committee committee = new Models.Committee()
                    {
                        CommitteeName = txtCommitteeName.Text,
                        CommitteeDate = txtCommitteeDateHidden.Value,
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
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")


                    };

                    string inputJson = (new JavaScriptSerializer()).Serialize(committee);
                    WebApiConsume.PostCommittee(Utilities.BASE_URL+"/api/Committees", inputJson);
                    

                    List<CommitteesMember> committeesMembers = new List<CommitteesMember>();
                    int Committee = WebApiConsume.GetCommitteeId(Utilities.BASE_URL+"/api/Committees", txtCommitteeName.Text.Trim().ToLower());
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
                                CommitteeRole = committeeRole,
                                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")


                            });
                        
                    }
                
                    string inputJson2 = (new JavaScriptSerializer()).Serialize(committeesMembers);
                    //WebClient client = new WebClient();

                    WebApiConsume.PostCommitteeMembers(Utilities.BASE_URL+"/api/CommitteesMembers", inputJson2);

                    foreach (var member in committeesMembers)
                    {

                        object memb = new
                        {
                            id = member.MemberId,
                        };

                        try
                        {

                        
                        User userFcm = WebApiConsume.GetUserById(Utilities.BASE_URL+"/api/Users", member.MemberId);
                        WebApiConsume.SendUserNotification(Utilities.BASE_URL+"/api/Fcm", member.CommitteeId,txtCommitteeName.Text, userFcm.FCMToken);
                          SMS.SendSms("تم إنشاء لجنة جديده ", userFcm.Phone);
                            if (userFcm.Phone!="" || userFcm.Phone!=null)
                        {
                           SMS.SendSms(userFcm.Phone, "انت عضو فى لجنة " + "\n" + txtCommitteeName.Text + " المشكلة بتاريخ   " + "\n" +txtCommitteeDate.Text );
                              //  Utilities.SendMailToOnePerson(userFcm.UserEmailId, "انضمام للجنة", "تم اضافتك للجنة بنجاح");

                            }

                        }
                        catch (Exception ex)
                        {
                            string filePath = @Utilities.LogError_Path + "Error.txt";


                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine("-----------------------------------------------------------------------------");
                                writer.WriteLine("Date : " + DateTime.Now.ToString());
                                writer.WriteLine();

                                while (ex != null)
                                {
                                    writer.WriteLine(ex.GetType().FullName);
                                    writer.WriteLine("Message : " + ex.Message);
                                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                                    ex = ex.InnerException;
                                }
                            }


                        }
                        string apiUrlAlert = "";
                        WebApiConsume.PostAlert(Utilities.BASE_URL+"/api/Committees",member.CommitteeId, member.MemberId);
                    }
                    ViewState["update"] = 0;
                    selectedUsers = new List<int>();
                    userGrids = new List<UserGrid>();
                    Response.Redirect("CommitteeMangement.aspx?id=redirectSave");

                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if ((ddlCommitteepresident.SelectedValue!= "NULL" && ddlCommitteeSecrtary.SelectedValue != "NULL"))
            {
                selectedUsers.Add(Convert.ToInt32(ddlCommitteeSecrtary.SelectedItem.Value));
                selectedUsers.Add(Convert.ToInt32(ddlCommitteepresident.SelectedItem.Value));

            }
            else
            {
               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('من فضلك قم باختيار رئيس لجنه وسكرتير لجنه ')", true);

            }

            if (selectedUsers.Count!=0)
            {
                if (ddlMemberSelect?.SelectedItem?.Value!="NULL")
                {
                    if (selectedUsers.Contains(Convert.ToInt32(ddlMemberSelect?.SelectedItem?.Value)))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('هذا العضو موجود بالفعل ')", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('هذا العضو موجود بالفعل ', 'تحذير')", true);
                    }
                    else
                    {

                        List<int> membersList = new List<int>();
                        string apiUrl2 = Utilities.BASE_URL + "/api/CommitteesMembers";
                        int memberId = WebApiConsume.GetUserId(Utilities.BASE_URL + "/api/Users", ddlMemberSelect.SelectedItem.Text);
                        membersList.Add(memberId);
                        if (gvAddMember.Rows.Count != 0)
                        {

                            foreach (GridViewRow row in gvAddMember.Rows)
                            {

                                //membersList.Add(memberId);
                                if (!userGrids.Any(x => x.رقم_العضو == memberId))
                                {


                                    userGrids.Add(WebApiConsume.ShowCommitteeMembersForNew(memberId)[0]);

                                    gvAddMember.Visible = true;

                                    gvAddMember.DataSource = userGrids.Distinct().ToList();

                                    gvAddMember.DataBind();
                                    selectedUsers.Add(Convert.ToInt32(ddlMemberSelect.SelectedItem.Value));
                                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم ادخال العضو للجنة بنجاح')", true);


                                }

                            }
                        }
                        else
                        {
                            userGrids.Add(WebApiConsume.ShowCommitteeMembersForNew(memberId)[0]);

                            gvAddMember.Visible = true;

                            gvAddMember.DataSource = userGrids;

                            gvAddMember.DataBind();
                            selectedUsers.Add(Convert.ToInt32(ddlMemberSelect.SelectedItem.Value));
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('تم ادخال العضو للجنة بنجاح')", true);
                        }
                        if (!membersList.Contains(memberId))
                        {


                            //CommitteesMember input = new CommitteesMember
                            //{
                            //    CommitteeId = Convert.ToInt32(ViewState["CommitteeId"]),
                            //    MemberId = memberId
                            //};
                            //string apiUrlUser = Utilities.BASE_URL + "/api/Users";

                            //string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
                            //WebApiConsume.PostCommitteeMembersUpdate(Utilities.BASE_URL + "/api/CommitteesMembers", inputJson3);

                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم ادخال هذا العضو بنجاح', 'تم')", true);


                        }


                    }
                }
                
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
            btnSave.Text = "Save";
            //gvCommittee.Visible = false;
            gvMembersOfCommittee.Visible = false;



        }

        protected void txtCommitteeName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd3_Click(object sender, EventArgs e)
        {
            divAddMember.Visible = false;


        }




        protected void btnAddChange_Click(object sender, EventArgs e)
        {
            List<int> membersList = new List<int>();
            foreach (GridViewRow row in gvMembersOfCommittee.Rows)
            {

                int member = Convert.ToInt32(row.Cells[1].Text);
                membersList.Add(member);

            }
            string apiUrl2 = Utilities.BASE_URL+"/api/CommitteesMembers";
            int memberId = WebApiConsume.GetUserId(Utilities.BASE_URL+"/api/Users", ddlMemberChange.SelectedItem.Text);
            if (!membersList.Contains(memberId))
            {
                int? meetingId = WebApiConsume.GetMeetingId(Utilities.BASE_URL + "/api/Committees", Convert.ToInt32(ViewState["CommitteeId"]), ViewState["CreatedAt"].ToString());

                CommitteesMember input = new CommitteesMember
                {
                    CommitteeId = Convert.ToInt32(ViewState["CommitteeId"]),
                    MemberId = memberId,
                    CommitteeRole=6,
                    UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedAt = ViewState["CreatedAt"].ToString(),
                    MeetingId=meetingId
                };
                try
                {
                    string apiUrlUser = Utilities.BASE_URL + "/api/Users";

                    string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
                    WebApiConsume.PostCommitteeMembersUpdate(Utilities.BASE_URL + "/api/CommitteesMembers", inputJson3);


                    gvMembersOfCommittee.DataSource = WebApiConsume.ShowCommitteeMembers(Convert.ToInt32(ViewState["CommitteeId"]));

                    gvMembersOfCommittee.DataBind();
                    User userFcm = WebApiConsume.GetUserById(Utilities.BASE_URL + "/api/Users", memberId);
                    string apiUrlFcm = Utilities.BASE_URL + "/api/Fcm";
                    WebApiConsume.SendUserNotification(apiUrlFcm, Convert.ToInt32(ViewState["CommitteeId"]), txtCommitteeName.Text, userFcm.FCMToken);
                    SMS.SendSms("انت عضو فى لجنة " + "\n" + txtCommitteeName.Text + " المشكلة بتاريخ   " + "\n" + txtCommitteeDate.Text,userFcm.Phone);

                    foreach (GridViewRow row in gvMembersOfCommittee.Rows)
                    {
                        string mobile = row.Cells[3].Text;
                        SMS.SendSms("تم اضافة عضو للجنة " + "\n" + txtCommitteeName.Text + " المشكلة بتاريخ   " + "\n" + txtCommitteeDate.Text, mobile);


                    }
                    //  Utilities.SendMailToOnePerson(userFcm.UserEmailId, "انضمام للجنة", "تم اضافتك للجنة بنجاح");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تم", "alert('تم اضافة العضو لللجنة بنجاح');", true);
                }
                catch (Exception ex)
                {
                    string filePath = @Utilities.LogError_Path + "Error.txt";


                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();

                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);

                            ex = ex.InnerException;
                        }
                    }


                }

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم ارسال الاشعارات بنجاح', 'تم')", true);

            }
            else
            {
               Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('هذا العضو موجود بالفعل داخل اللجنه', 'تحذير')", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "تحذير", "alert('هذا العضو موجود بالفعل داخل اللجنه');", true);


            }

        }

        protected void gvMembersOfCommittee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int memberId = Convert.ToInt32(gvMembersOfCommittee.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = Utilities.BASE_URL+"/api/CommitteeMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            int committeeId = Convert.ToInt32(ViewState["CommitteeId"]);
            object input = new
            {
                memberId = memberId,
                committeeId = committeeId
            };

            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            client.UploadString(apiUrl3 + "/DeleteCommitteeMembers?memberId=" + memberId + "&committeeId=" + committeeId, inputJson3);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح العضو من اللجنة بنجاح', 'تم')", true);


            gvMembersOfCommittee.DataSource = WebApiConsume.ShowCommitteeMembers(Convert.ToInt32(ViewState["CommitteeId"]));

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
            if (deleteButton.Text == "Delete" || deleteButton.Text == "مسح")
            {
                deleteButton.Text = "مسح من اللجنة";
                deleteButton.Font.Underline = true;
               

                deleteButton.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";
                // deleteButton2.OnClientClick = "return confirm('هل تريد مسح هذا العنصر؟');";

            }
            if (Request.QueryString["status"] == "selected" || Request.QueryString["status"] == "updated")
            {
                deleteButton.Visible = true;
                
            }
        }

        private List<Committee.Models.MeetingSearch> ShowMeetings(int committeeId)
        {
            List<Committee.Models.MeetingSearch> meetingResults = new List<Models.MeetingSearch>();
            string apiUrl3 = Utilities.BASE_URL+"/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            List<Committee.Models.Meeting> meetings = serializer.Deserialize<List<Committee.Models.Meeting>>(client.DownloadString(apiUrl3 + "/GetCommitteeMeetings?committeeId=" + committeeId));
            if (meetings.Count != 0)
            {
                lblgvMeeting.Visible = true;
                lblgvMeeting.Text = "اجتماعات اللجنة";
                int status = 0;
                foreach (var meeting in meetings)
                {
                    if (meeting != null)
                    {



                        meetingResults.Add(new Models.MeetingSearch()
                        {
                            الرقم = meeting.MeetingId,
                            عنوان_الاجتماع = meeting?.MeetingTitle,
                            تاريخ_الاجتماع = meeting?.MeetingDate,
                            وقت_الاجتماع = meeting?.MeetingTime,
                            موقع_الاجتماع = meeting?.MeetingAddress,
                            حالة_الاجتماع = meeting?.MeetingHistories.Count == 0 ? "" : meeting.MeetingHistories.LastOrDefault(x => x.MeetingId == meeting.MeetingId).TitleAr


                        });
                    }
                }


            }
            else
            {
                lblgvMeeting.Text = " لا يوجد اجتماعات لهذه اللجنة";
            }

            return meetingResults;

        }
        protected void txtCommitteeDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvMembersOfCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMembersOfCommittee.PageIndex = e.NewPageIndex;
            gvMembersOfCommittee.DataSource = WebApiConsume.ShowCommitteeMembers(Convert.ToInt32(ViewState["CommitteeId"]));
            gvMembersOfCommittee.DataBind();
        }

        protected void gvMembersOfCommittee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnAdd1_Command(object sender, CommandEventArgs e)
        {

        }

        protected void gvMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMeeting_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int id = Convert.ToInt32(gvMeeting.Rows[e.NewSelectedIndex].Cells[1].Text);
            Response.Redirect("Meeting.aspx?meetingId=" + id + "&status=selected");
        }

        protected void gvMeeting_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvMeeting.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Meeting.aspx?meetingId=" + id + "&status=update");
        }

        protected void gvMeeting_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int meetingId = Convert.ToInt32(gvMeeting.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = Utilities.BASE_URL+"/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = meetingId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            string meeting = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrl3 + "/DeleteMeeting?id=" + meetingId));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات العضو بنجاح', 'تم')", true);
            gvMeeting.DataSource = ShowMeetings(Convert.ToInt32(ViewState["CommitteeId"]));
            gvMeeting.DataBind();
        }

        protected void gvMeeting_RowDataBound(object sender, GridViewRowEventArgs e)
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
                selectButton.BorderColor = System.Drawing.Color.White;
                selectButton.Font.Size = FontUnit.Medium;
                selectButton.Font.Underline = true;
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

        protected void gvMeeting_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAddMember_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvAddMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAddMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change the index number as per your gridview design
                e.Row.Cells[1].Enabled = false;

                if (e.Row.RowType != DataControlRowType.DataRow) return;

                var deleteButton = (LinkButton)e.Row.Cells[0].Controls[0];
              
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
                deleteButton.ForeColor = System.Drawing.Color.DarkRed;
                deleteButton.BackColor = System.Drawing.Color.White;
                deleteButton.BorderColor = System.Drawing.Color.DarkRed;
                deleteButton.Font.Size = FontUnit.Medium;
                deleteButton.BorderWidth = 2;
                deleteButton.Text = "مسح";

             
            }
        }

        protected void gvAddMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            userGrids.RemoveAt(e.RowIndex);
            gvAddMember.DataSource = userGrids;
            gvAddMember.DataBind();
        }
    }
}