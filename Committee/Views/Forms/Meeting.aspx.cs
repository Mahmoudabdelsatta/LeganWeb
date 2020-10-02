using Committee.Controller;
using Committee.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Views.Forms
{
    public partial class Meeting : System.Web.UI.Page
    {
        public String filenamepdf { set; get; }
        static List<Committee.Models.Agendum> Agendas = new List<Committee.Models.Agendum>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            txtMeetingDate.Text = txtMeetingDateHidden.Value;
            txtMeetingTime.Text = txtmeetingTimeHiddenfField.Value;
            
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                //   BindGridview();
                txtMeetingDate.Text = "";

                if (Request.QueryString["status"] == "update")
                {
                    lblmeetingmembers.Visible = true;
                    lblmeetingmembers.Text = "حالات الاعضاء";
                    lblMeetingImg.Visible = true;
                    lblMeetingImg.Text = " صور الاجتماع";
                    lblgvAgenda.Visible = true;
                    lblgvAgenda.Text = "اجندة الاجتماع";
                    lblgvStatus.Visible = true;
                    lblgvStatus.Text = "حالات الاجتماع";
                    gvStatus.Visible = true;
                    gvAgenda.Visible = true;
                    addAgenda.Visible = false;
                    lblAddMeeting.Text = "تعديل اجتماع";
                    lblAddMeetingh2.Text = "تعديل اجتماع";
                    List<Committee.Models.StatusSearch> histories = new List<Models.StatusSearch>();
                    string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;

                    Committee.Models.Meeting meeting = (new JavaScriptSerializer()).Deserialize<Committee.Models.Meeting>(client.DownloadString(apiUrl3 + "/GetMeetingForWeb?meetingId=" + Request.QueryString["MeetingId"]));
                    txtMeetingName.Text = meeting.MeetingTitle;
                    txtMeetingDate.Text = meeting?.MeetingDate;
                    txtMeetingLocation.Text = meeting.MeetingAddress;
                    txtMeetingTopic.Text = meeting.MeetingDesc;
                    lat.Value = meeting.Latitude;
                    lng.Value = meeting.longitude;
                    ddlCommitteeSpecified.SelectedValue = meeting.CommitteeId.ToString();
                    txtmeetingTimeHiddenfField.Value = meeting.MeetingTime;
                    string dbstatus = meeting?.MeetingHistories.Count == 0 ? "جديد" : meeting?.MeetingHistories?.LastOrDefault(x => x.MeetingId == meeting?.MeetingId).TitleEn;
                    ddlMeetingStatus.SelectedValue = meeting?.MeetingHistories?.Count == 0 ? "جديد" : dbstatus;
                    btnDownloadMinutesOfMeetingUpload.Text = "محضر الاجتماع";
                    if (meeting.MinutesOfMeeting == "/")
                    {
                        btnDownloadMinutesOfMeetingUpload.Visible = false;
                    }
                    Session["meetingMinute"] = meeting.MinutesOfMeeting;
                    ViewState["createdAt"] = meeting.CreatedAt;
                    ViewState["MeetingId"] = Request.QueryString["meetingId"];

                    List<Committee.Models.AgendaUpdate> data = ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
                    gvAgenda.DataSource = data.Where(x => x != null).ToList();

                    gvAgenda.DataBind();

                    string result2 = client.DownloadString(Utilities.BASE_URL + "/api/Meetings/GetMeetingHistory?meetingId=" + Convert.ToInt32(ViewState["MeetingId"]));
                    List<Committee.Models.MeetingHistory> Statuss = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingHistory>>(result2);
                    foreach (var status in Statuss)
                    {
                        DateTime createdAt = DateTime.Parse(status.CreatedAt);
                        string t = Utilities.ConvertDateCalendar(createdAt, "Gregorian", "en-US");
                        histories.Add(new Models.StatusSearch()
                        {
                            //الرقم = status.Id,
                            الحاله = status.TitleAr,
                            التاريخ = t

                        });
                    }

                    gvStatus.DataSource = histories.Where(x => x != null).ToList();

                    gvStatus.DataBind();
                    ShowMeetingMembers(Convert.ToInt32(Request.QueryString["meetingId"]));
                    ShowMeetingImages(Convert.ToInt32(Request.QueryString["meetingId"]));
                }
                else if (Request.QueryString["status"] == "new")
                {
                    lblAddMeeting.Text = "اضافة اجتماع";
                    lblAddMeetingh2.Text = "اضافة اجتماع";

                }
                if (Request.QueryString["status"] == "selected")
                {

                    lblmeetingmembers.Visible = true;
                    lblmeetingmembers.Text = "حالات الاعضاء";
                    lblMeetingImg.Visible = true;
                    lblMeetingImg.Text = " صور الاجتماع";
                    lblgvAgenda.Visible = true;
                    lblgvAgenda.Text = "اجندة الاجتماع";
                    lblgvStatus.Visible = true;
                    lblgvStatus.Text = "حالات الاجتماع";
                    addAgenda.Visible = false;
                    dvMap.Visible = false;
                    MinutesOfMeetingUpload.Visible = false;

                    List<Committee.Models.StatusSearch> histories = new List<Models.StatusSearch>();
                    string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";
                    ddlCommitteeSpecified.Enabled = false;
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;

                    Committee.Models.Meeting meeting = (new JavaScriptSerializer()).Deserialize<Committee.Models.Meeting>(client.DownloadString(apiUrl3 + "/GetMeetingForWeb?meetingId=" + Request.QueryString["MeetingId"]));
                    txtMeetingName.Text = meeting.MeetingTitle;
                    txtMeetingName.Enabled = false;
                    if (meeting.MinutesOfMeeting == "/")
                    {
                        btnDownloadMinutesOfMeetingUpload.Visible = false;
                    }
                    Session["meetingMinute"] = meeting.MinutesOfMeeting;
                    btnDownloadMinutesOfMeetingUpload.Text = "محضر الاجتماع";
                    ddlMeetingStatus.Enabled = false;
                    txtMeetingDate.Text = meeting?.MeetingDate;
                    txtMeetingDate.Enabled = false;
                    txtMeetingLocation.Text = meeting.MeetingAddress;
                    txtMeetingLocation.Enabled = false;

                    txtMeetingTopic.Text = meeting.MeetingDesc;
                    txtMeetingTopic.Enabled = false;

                    lat.Value = meeting.Latitude;
                    lng.Value = meeting.longitude;
                    ddlCommitteeSpecified.SelectedValue = meeting.CommitteeId.ToString();
                    txtmeetingTimeHiddenfField.Value = meeting.MeetingTime;
                    string dbstatus = meeting?.MeetingHistories?.Last(x => x.MeetingId == meeting.MeetingId).TitleEn;
                    ddlMeetingStatus.SelectedValue = meeting?.MeetingHistories?.Count == 0 ? "جديد" : dbstatus;

                    ViewState["MeetingId"] = Request.QueryString["meetingId"];
                    ViewState["createdAt"] = meeting.CreatedAt;

                    List<Committee.Models.AgendaUpdate> data = ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
                    gvAgenda.DataSource = data.Where(x => x != null).ToList();

                    gvAgenda.DataBind();

                    string result2 = client.DownloadString(Utilities.BASE_URL + "/api/Meetings/GetMeetingHistory?meetingId=" + Convert.ToInt32(ViewState["MeetingId"]));
                    List<Committee.Models.MeetingHistory> Statuss = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingHistory>>(result2);
                    foreach (var status in Statuss)
                    {

                        DateTime createdAt = DateTime.Parse(status.CreatedAt);
                        string t = Utilities.ConvertDateCalendar(createdAt, "Hijri", "en-US");
                        histories.Add(new Models.StatusSearch()
                        {
                            //الرقم = status.Id,
                            الحاله = status.TitleAr,
                            التاريخ = t

                        });
                    }

                    gvStatus.DataSource = histories.Where(x => x != null).ToList();

                    gvStatus.DataBind();

                    lblAddMeeting.Text = "عرض بيانات الاجتماع";
                    lblAddMeetingh2.Text = "عرض بيانات  الاجتماع";
                    gvCommitteeMembers.Visible = true;
                    gvUploadImages.Visible = true;
                    btnAdd1.Visible = false;
                    lnkCancel.Text = "رجوع";
                    ShowMeetingMembers(Convert.ToInt32(Request.QueryString["meetingId"]));
                    ShowMeetingImages(Convert.ToInt32(Request.QueryString["meetingId"]));

                }



                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString(Utilities.BASE_URL + "/api/Committees/GetCommitteesForWeb?deptId=" + Convert.ToInt32(Session["DeptId"]));
                List<Committee.Models.Committee> committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Committee>>(result);
                ddlCommitteeSpecified.DataSource = committees;
                ddlCommitteeSpecified.DataTextField = "CommitteeName";
                ddlCommitteeSpecified.DataValueField = "CommitteeId";
                ddlCommitteeSpecified?.DataBind();

                //gvStatus.DataSource = ShowmeetingHistories(4);
                //gvStatus.DataBind();
                //gvAgenda.DataSource = ShowmeetingAgenda(4);
                //gvAgenda.DataBind();

            }
        }

        private void ShowMeetingMembers(int meetingId)
        {
            List<MeetingMemberSearchUpdate> meetingMembers = new List<MeetingMemberSearchUpdate>();
            string apiUrl3 = Utilities.BASE_URL + "/api/CommitteesMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.MeetingMemberSearch> members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingMemberSearch>>(client.DownloadString(apiUrl3 + "/GetCommitteesMembersMeeting?meetingId=" + meetingId));
            if (members.Count != 0)
            {
                //  btnExportPdf.Visible = true;
                lblmeetingmembers.Visible = true;
                lblmeetingmembers.Text = "حالات الاعضاء";
                foreach (var member in members)
                {
                    if (member.User.ID != 0)
                    {
                        meetingMembers.Add(new MeetingMemberSearchUpdate()
                        {
                            User = member.User,
                            IsMemberAcceptedMiutesOfCommittee = member.IsMemberAcceptedMiutesOfCommittee == null ? "لم يتم ارسال الرد" : member.IsMemberAcceptedMiutesOfCommittee == true ? "وافق" : "لم يوافق",
                            MemberWillAttend = member.MemberWillAttend == null ? "لم يتم ارسال الرد" : member.MemberWillAttend == true ? "حضر" : "لم يحضر",
                            MemberSignature = member.MemberSignature == null || member.MemberSignature == "" ? member.MemberSignature = "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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgMX1/wHcgumF7bQG9wAAAABJRU5ErkJggg==" :
                                            member.MemberSignature,
                            RejectionReason = member.RejectionReason == null ? "لم يتم ارسال سبب الرفض" : member.RejectionReason,

                        });
                    }


                }

                gvCommitteeMembers.DataSource = meetingMembers;
                gvCommitteeMembers.DataBind();
            }
            else
            {
                lblmeetingmembers.Visible = true;
                lblmeetingmembers.Text = "لا يوجد اعضاء";
            }




        }
        protected string GetUrl(string page)

        {

            string[] splits = Request.Url.AbsoluteUri.Split('/');

            if (splits.Length >= 2)

            {

                string url = splits[0] + "//";

                for (int i = 2; i < splits.Length - 1; i++)

                {

                    url += splits[i];

                    url += "/";

                }

                return url + page;

            }

            return page;

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void PDF_Export()

        {



        }
        private void ShowMeetingImages(int meetingId)
        {
            List<MeetingImageForGrid> meetingMembers = new List<MeetingImageForGrid>();
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.MeetingUploadImage> meetings = (new JavaScriptSerializer()).Deserialize<List<MeetingUploadImage>>(client.DownloadString(apiUrl3 + "/GetMeetingImageForWeb?meetingId=" + meetingId));
            if (meetings.Count != 0)
            {
                lblMeetingImg.Visible = true;
                lblMeetingImg.Text = " صور الاجتماع";
                foreach (var meeting in meetings)
                {

                    meetingMembers.Add(new MeetingImageForGrid()
                    {
                        MeetingImage = meeting.MeetingImage1,

                    });
                }



                gvUploadImages.DataSource = meetingMembers;
                gvUploadImages.DataBind();
            }
            else
            {
                lblMeetingImg.Visible = true;
                lblMeetingImg.Text = "لا يوجد صور للإجتماع";
            }




        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            if (Request.QueryString["status"] == "update")
            {

                ViewState["MeetingId"] = Request.QueryString["MeetingId"];
                string fname = null;
                string meetingId = null;
                string base64String = null;
                if (MinutesOfMeetingUpload.PostedFile != null && MinutesOfMeetingUpload.PostedFile.ContentLength > 0)
                {
                    fname = Path.GetFileName(MinutesOfMeetingUpload.PostedFile.FileName);
                    MinutesOfMeetingUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads", fname)));
                    System.IO.Stream fs = MinutesOfMeetingUpload.PostedFile.InputStream;
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                }
                string apiUrlUpdate = Utilities.BASE_URL + "/api/Meetings";
                int state = 1;
                if (ddlMeetingStatus.SelectedItem.Text == "جديد")
                {
                    state = 1;
                }
                if (ddlMeetingStatus.SelectedItem.Text == "تم رفع المحضر")
                {
                    state = 2;
                }
                if (ddlMeetingStatus.SelectedItem.Text == "مغلق")
                {
                    state = 3;

                }
                if (base64String == null)
                {
                    base64String = Session["meetingMinute"]?.ToString();
                }
                else
                {
                    Session["meetingMinute"] = base64String;
                }
                Committee.Models.Meeting meetingUpdate = new Models.Meeting()
                {

                    MeetingTitle = txtMeetingName.Text,
                    //  Address = txtMeetingLocation.Text,
                    CommitteeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value),
                    MeetingDate = txtMeetingDateHidden.Value,
                    MeetingDesc = txtMeetingTopic.Text,
                    MeetingTime = txtmeetingTimeHiddenfField.Value,
                    longitude = lng.Value,
                    Latitude = lat.Value,
                    MeetingAddress = txtMeetingLocation.Text,
                    MinutesOfMeeting = base64String,
                    Status = state,
                    CreatedAt = ViewState["createdAt"]?.ToString(),
                    UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    //  AgendaDescription = txtMeetingAgenda.Text,
                    MeetingId = Convert.ToInt32(ViewState["MeetingId"])

                };
                Committee.Models.MeetingHistory status = new Models.MeetingHistory()
                {

                    TitleAr = ddlMeetingStatus.SelectedItem.Text,
                    TitleEn = ddlMeetingStatus.SelectedItem.Value,
                    MeetingDate = txtMeetingDate.Text,

                    MeetingId = Convert.ToInt32(ViewState["MeetingId"]),
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),



                    // AgendaDescription = txtMeetingAgenda.Text,
                    //MinutesOfMeeting = file.ToString()

                };
                WebClient client3 = new WebClient();
                client3.Headers["Content-type"] = "application/json";
                client3.Encoding = Encoding.UTF8;
                string inputJson3 = (new JavaScriptSerializer()).Serialize(status);
                client3.UploadString(apiUrlUpdate + "/PostStatusOfMeeting", inputJson3);
                string inputJson = (new JavaScriptSerializer()).Serialize(meetingUpdate);

                client.UploadString(apiUrlUpdate + "/putMeeting?id=" + Convert.ToInt32(ViewState["MeetingId"]), inputJson);
                Session["meetingMinute"] = base64String;
                string apiUrl2 = Utilities.BASE_URL + "/api/CommitteesMembers";
                WebClient members = new WebClient();
                members.Headers["Content-type"] = "application/json";
                members.Encoding = Encoding.UTF8;
                List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(members.DownloadString(apiUrl2 + "/GetCommitteesMember?id=" + Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value)));
                foreach (var user in users)
                {

                    string apiUrlFcm = Utilities.BASE_URL + "/api/Fcm";
                    WebClient clienfcm = new WebClient();
                    clienfcm.Headers["Content-type"] = "application/json";
                    clienfcm.Encoding = Encoding.UTF8;
                    object UserFcmo = new
                    {

                        Action_Id1 = ddlCommitteeSpecified.SelectedItem.Value,
                        Body = "تم رفع محضر",
                        Title = "رفع محضر",
                        Action_Id2 = Convert.ToInt32(ViewState["MeetingId"]),
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Action_Type = "type_upload_minute"


                    };

                    if (ddlMeetingStatus.SelectedItem.Text == "تم رفع المحضر")
                    {
                        string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                        clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                        SMS.SendSms("تم رفع محضر", user.Phone);
                        Utilities.SendMailToOnePerson(user.UserEmailId, "رفع المحضر", "تم رفع المحضر الخاص باللجنة");
                        string apiUrlAlert = Utilities.BASE_URL + "/api/Committees";
                        WebClient client4 = new WebClient();

                        client4.Headers["Content-type"] = "application/json";
                        client4.Encoding = Encoding.UTF8;

                        Alert alert = new Alert()
                        {
                            Action_Id1 = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value),
                            Message = "تم رفع محضر",
                            Title = "رفع محضر",
                            Action_Id2 = Convert.ToInt32(ViewState["MeetingId"]),
                            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Action_Type = 5,
                            UserId = user.ID



                        };
                        string inputAlert = (new JavaScriptSerializer()).Serialize(alert);
                        client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);
                    }
                       


                }

                filenamepdf = Session["meetingMinute"]?.ToString();
                shpwPdf.Visible = true;
                Response.Redirect("MeetingMangement.aspx?id=redirectUpdate");
            }
            else
            {
                string fname = null;
                string meetingId = null;
                string base64String = null;
                if (MinutesOfMeetingUpload.PostedFile != null && MinutesOfMeetingUpload.PostedFile.ContentLength > 0)
                {
                    fname = Path.GetFileName(MinutesOfMeetingUpload.PostedFile.FileName);
                    MinutesOfMeetingUpload.SaveAs(Server.MapPath(Path.Combine("~/MasterPage/Uploads", fname)));
                    System.IO.Stream fs = MinutesOfMeetingUpload.PostedFile.InputStream;
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                }
                //object file = ReadStream(MinutesOfMeetingUpload.PostedFile.InputStream);
                string apiUrlUpdate = Utilities.BASE_URL + "/api/Meetings";
                if (Session["fileNem"] == "" || Session["fileNem"] == null)
                {
                    // string fileName = "/" + fname;
                }
                else
                {
                    fname = Session["fileNem"].ToString();
                }
                int status = 1;
                if (ddlMeetingStatus.SelectedItem.Text == "جديد")
                {
                    status = 1;
                }
                if (ddlMeetingStatus.SelectedItem.Text == "تم رفع المحضر")
                {
                    status = 2;
                }
                if (ddlMeetingStatus.SelectedItem.Text == "مغلق")
                {
                    status = 3;

                }
                Committee.Models.Meeting meeting = new Models.Meeting()
                {

                    MeetingTitle = txtMeetingName.Text,
                    //Address = txtMeetingLocation.Text,
                    CommitteeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value),
                    MeetingDate = txtMeetingDateHidden.Value,
                    MeetingDesc = txtMeetingTopic.Text,
                    MeetingTime = txtmeetingTimeHiddenfField.Value,
                    longitude = lng.Value,
                    Latitude = lat.Value,
                    MeetingAddress = txtMeetingLocation.Text,
                    MinutesOfMeeting = base64String,
                    Status = status,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),



                    // AgendaDescription = txtMeetingAgenda.Text,
                    //MinutesOfMeeting = file.ToString()

                };
                string inputJson = (new JavaScriptSerializer()).Serialize(meeting);




                int committeeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value);
                ViewState["committeeId"] = committeeId;
                WebClient client5 = new WebClient();
                client5.Headers["Content-type"] = "application/json";
                client5.Encoding = Encoding.UTF8;

                meetingId = client5.UploadString(apiUrlUpdate + "/PostMeeting", inputJson);
                string apiUrl2 = Utilities.BASE_URL + "/api/CommitteesMembers";
                WebClient members = new WebClient();
                members.Headers["Content-type"] = "application/json";
                members.Encoding = Encoding.UTF8;
                List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(members.DownloadString(apiUrl2 + "/GetCommitteesMember?id=" + committeeId));
                try
                {
                    foreach (var user in users)
                    {
                        if (user.ID != 0)
                        {


                            string apiUrlFcm = Utilities.BASE_URL + "/api/Fcm";
                            WebClient clienfcm = new WebClient();
                            clienfcm.Headers["Content-type"] = "application/json";
                            clienfcm.Encoding = Encoding.UTF8;
                            object UserFcmo = new
                            {
                                Action_Id1 = committeeId,
                                Body = "اشعار بخصوص اجتماع",
                                Title = "اجتماع جديد",
                                Action_Id2 = Convert.ToInt32(meetingId),
                                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                Action_Type = "type_new_meeting"


                            };
                            string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                            clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                          SMS.SendSms("تم اضافتك للاجتماع",user.Phone);
                            Utilities.SendMailToOnePerson(user.UserEmailId, "انضمام للاجتماع", "تم اضافتك للاجتماع");

                            string apiUrlAlert = Utilities.BASE_URL + "/api/Committees";
                            WebClient client4 = new WebClient();

                            client4.Headers["Content-type"] = "application/json";
                            client4.Encoding = Encoding.UTF8;

                            Alert alert = new Alert()
                            {
                                Title = "اجتماع جديد",
                                Message = "اشعار بخصوص اجتماع جديد",
                                UserId = user.ID,
                                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                Action_Id1 = committeeId,
                                Action_Id2 = Convert.ToInt32(meetingId),
                                Action_Type = 2,




                            };
                            string inputAlert = (new JavaScriptSerializer()).Serialize(alert);
                            client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);

                            string apiUrlFcm2 = Utilities.BASE_URL + "/api/Fcm";
                            if (ddlMeetingStatus.SelectedItem.Text == "تم رفع المحضر")
                            {
                                WebClient clienfcm2 = new WebClient();
                                clienfcm.Headers["Content-type"] = "application/json";
                                clienfcm.Encoding = Encoding.UTF8;
                                object UserFcmo2 = new
                                {
                                    Action_Id1 = ddlCommitteeSpecified.SelectedItem.Value,
                                    Body = "تم رفع محضر",
                                    Title = "رفع محضر",
                                    Action_Id2 = Convert.ToInt32(ViewState["MeetingId"]),
                                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Action_Type = "type_upload_minute"

                                };
                                string inputFcm2 = (new JavaScriptSerializer()).Serialize(UserFcmo);
                                clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                                SMS.SendSms("تم رفع محضرالاجتماع", user.Phone);
                                Utilities.SendMailToOnePerson(user.UserEmailId, "محضر للاجتماع", "تم رفع محضرالاجتماع");
                            }
                           

                        }
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogError(ex);
                }
                Committee.Models.MeetingHistory History = new Models.MeetingHistory()
                {

                    TitleAr = ddlMeetingStatus.SelectedItem.Text,
                    TitleEn = ddlMeetingStatus.SelectedItem.Value,
                    MeetingDate = txtMeetingDate.Text,

                    MeetingId = Convert.ToInt32(meetingId),
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),



                    // AgendaDescription = txtMeetingAgenda.Text,
                    //MinutesOfMeeting = file.ToString()

                };
                WebClient client3 = new WebClient();
                client3.Headers["Content-type"] = "application/json";
                client3.Encoding = Encoding.UTF8;
                string inputJson3 = (new JavaScriptSerializer()).Serialize(History);
                client3.UploadString(apiUrlUpdate + "/PostStatusOfMeeting", inputJson3);

                for (int i = 0; i < Agendas.Count; i++)
                {
                    string apiUrlAgenda = Utilities.BASE_URL + "/api/Meetings";
                    Committee.Models.Agendum agenda = new Models.Agendum()
                    {

                        AgendaDesc = ViewState["agendaDesc"].ToString(),
                        // AgendaTime = Agendas[0].AgendaTime,
                        MeetingId = Convert.ToInt32(meetingId),
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")


                        // AgendaDescription = txtMeetingAgenda.Text,
                        //MinutesOfMeeting = file.ToString()

                    };
                    WebClient client2 = new WebClient();
                    client2.Headers["Content-type"] = "application/json";
                    client2.Encoding = Encoding.UTF8;
                    string inputJson2 = (new JavaScriptSerializer()).Serialize(agenda);

                    client2.UploadString(apiUrlUpdate + "/PostAgendaOfMeeting", inputJson2);
                }
                Agendas = new List<Models.Agendum>();
                Response.Redirect("MeetingMangement.aspx?id=redirectSave");

            }
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();

        }
        protected void Upload(object sender, EventArgs e)
        {

        }

        private void ResetControls()
        {
            Agendas = new List<Agendum>();
            txtMeetingName.Text = "";
            txtMeetingLocation.Text = "";
            txtMeetingDate.Text = "";
            txtMeetingTopic.Text = "";
            txtmeetingTimeHiddenfField.Value = "";
            //  txtMeetingAgenda.Text = "";
            ddlCommitteeSpecified.SelectedIndex = -1;
            //txtAgendaTime.Text = "";
            txtMeetingAgenda.Text = "";
            lat.Value = "";
            lng.Value = "";


            gvAgenda.Visible = false;
            gvStatus.Visible = false;


        }
        private List<Committee.Models.MeetingSearch> ShowMeetings()
        {
            List<Committee.Models.MeetingSearch> meetingResults = new List<Models.MeetingSearch>();
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Meeting> meetings = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Meeting>>(client.DownloadString(apiUrl3 + "/GetMeeting?meetingName=" + txtMeetingName.Text));
            if (meetings.Count != 0)
            {
                int status = 0;
                foreach (var meeting in meetings)
                {
                    status = Convert.ToInt32(meeting.Status);
                    meetingResults.Add(new Models.MeetingSearch()
                    {
                        الرقم = meeting.MeetingId,
                        عنوان_الاجتماع = meeting.MeetingTitle,
                        تاريخ_الاجتماع = meeting.MeetingDate,
                        وقت_الاجتماع = meeting.MeetingTime,
                        موقع_الاجتماع = meeting.MeetingAddress,

                        حالة_الاجتماع = meeting.MeetingHistories.Count == 0 ? "" : meeting.MeetingHistories.LastOrDefault(x => x.MeetingId == meeting.MeetingId).TitleAr,


                    });
                }



            }

            return meetingResults;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {


            ViewState["update"] = 1;

        }

        protected void btnOpenPopUp_Click(object sender, EventArgs e)
        {
            if (MinutesOfMeetingUpload.PostedFile.FileName != "")
            {
                Session["fileNem"] = MinutesOfMeetingUpload.PostedFile.FileName;
            }
            mpePopUp.Show();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Agendas.Add(new Models.Agendum()
            {
                AgendaDesc = txtMeetingAgenda.Text,
                MeetingId = 4
            });
            mpePopUp.Hide();
            mpePopUp.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpePopUp.Hide();
        }

        protected void btnOpenPopUp_Click1(object sender, EventArgs e)
        {
            if (MinutesOfMeetingUpload.PostedFile.FileName != "")
            {
                Session["fileNem"] = MinutesOfMeetingUpload.PostedFile.FileName;
            }

            mpePopUp.Show();
            // MinutesOfMeetingUpload.v = Session["fileNem"].ToString();
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            Agendas = new List<Agendum>();
            ViewState["agendaDesc"] = txtMeetingAgenda.Text;
            Agendas.Add(new Models.Agendum()
            {
                AgendaDesc = txtMeetingAgenda.Text,
                MeetingId = 4
            });
            mpePopUp.Hide();
            txtMeetingAgendaLoader.Visible = true;
            txtMeetingAgendaLoader.Text = txtMeetingAgenda.Text;

        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            mpePopUp.Hide();

        }

        protected void linkAddAgenda_Click(object sender, EventArgs e)
        {

        }

        protected void linkRemoveAgenda_Click(object sender, EventArgs e)
        {

        }


        private List<Committee.Models.AgendaUpdate> ShowmeetingAgenda(int meetingId)
        {
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Agendum> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Agendum>>(client.DownloadString(apiUrl3 + "/GetMeetingAgendas?meetingId=" + meetingId));



            return Committees.Select(x => new Models.AgendaUpdate() { الرقم = x.id, الاجندة = x.AgendaDesc }).ToList();
        }
        private void ShowmeetingAgendaUpdate(int meetingId)
        {
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Agendum> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Agendum>>(client.DownloadString(apiUrl3 + "/GetMeetingAgendas?meetingId=" + meetingId));
            gvAgenda.DataSource = Committees;
            gvAgenda.DataBind();


        }

        protected void gvAgenda_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int agendaId = Convert.ToInt32(gvAgenda.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = agendaId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            string meeting = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrl3 + "/DeleteAgenda?id=" + agendaId));
            gvAgenda.DataSource = ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
            gvAgenda.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات العضو بنجاح', 'تم')", true);

        }

        protected void gvAgenda_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (Request.QueryString["status"] == "selected")
                {
                    selectButton.Visible = false;
                    deleteButton.Visible = false;
                    editButton.Visible = false;
                }
                else
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

                }
            }
        }

        protected void gvAgenda_RowEditing(object sender, GridViewEditEventArgs e)
        {
            divagendaUpdate.Visible = true;
            GridViewRow row = (GridViewRow)gvAgenda.Rows[e.NewEditIndex];
            txtAgendaUpdate.Text = row.Cells[2].Text;
            //txtTimeUpdate.Text = row.Cells[2].Text;
            AgendaId.Value = row.Cells[1].Text;
            ViewState["agendaId"] = AgendaId.Value;
            gvAgenda.SelectedIndex = -1;
            gvStatus.SelectedIndex = -1;
        }

        protected void gvAgenda_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvAgenda.Rows[e.RowIndex];
            TextBox textAgendaTime = (TextBox)row.Cells[2].Controls[0];
            int id1 = Convert.ToInt32(row.Cells[1].Controls[0]);

            TextBox textAgendaDesc = (TextBox)row.Cells[3].Controls[0];
            //   TextBox textitemName = (TextBox)row.Cells[7].Controls[0];



            //WebClient client = new WebClient();
            //client.Headers["Content-type"] = "application/json";
            //client.Encoding = Encoding.UTF8;

            //Committee.Models.Agendum agenda = new Models.Agendum()
            //{
            //     id=id1,
            //     AgendaTime=textAgendaTime.Text,
            //     AgendaDesc= textAgendaDesc.Text


            //    // AgendaDescription = txtMeetingAgenda.Text,
            //    //MinutesOfMeeting = file.ToString()

            //};
            //string inputJson = (new JavaScriptSerializer()).Serialize(agenda);


            //client.UploadString(apiUrl3 + "/putMeetingAgenda?id=" + id1, inputJson);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات الاجندة بنجاح', 'تم')", true);
        }

        protected void gvAgenda_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAgenda.EditIndex = -1;

            ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
        }

        protected void gvAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvAgenda_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAgenda_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAgenda_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if (Request.QueryString["status"] == "selected")
                {
                    selectButton.Visible = false;

                    deleteButton.Visible = false;
                    editButton.Visible = false;
                }

                else
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
            }
        }

        protected void gvStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int statusId = Convert.ToInt32(gvStatus.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = statusId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            string status = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrl3 + "/DeleteStatus?id=" + statusId));
            gvStatus.DataSource = ShowmeetingHistories(Convert.ToInt32(ViewState["MeetingId"]));
            gvStatus.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم مسح بيانات العضو بنجاح', 'تم')", true);
        }

        private List<Committee.Models.StatusSearch> ShowmeetingHistories(int v)
        {
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            int meetingId = v;
            List<Committee.Models.MeetingHistory> histories = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingHistory>>(client.DownloadString(apiUrl3 + "/GetMeetingHistory?meetingId=" + meetingId));

            //DateTime createdAt = DateTime.Parse(committeeMeeting.CreatedAt);
            //string t = Utilities.ConvertDateCalendar(Convert.ToDateTime(committeeMeeting.Meeting.MeetingDate), "Gregorian", "en-US");
            return histories.Select(x => new Models.StatusSearch() { الحاله = x.TitleAr, التاريخ = Utilities.ConvertDateCalendar(Convert.ToDateTime(x.CreatedAt), "Gregorian", "en-US") }).ToList();
        }

        protected void gvStatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            divStatus.Visible = true;
            GridViewRow row = (GridViewRow)gvStatus.Rows[e.NewEditIndex];
            txtHistoryUpdate.Text = row.Cells[2].Text;
            txtHistoryTime.Text = row.Cells[3].Text;
            HistoryId.Value = row.Cells[1].Text;
            gvStatus.SelectedIndex = -1;
            gvAgenda.SelectedIndex = -1;
            //gvStatus.EditIndex = e.NewEditIndex;

            ////gvAgenda.EditIndex = e.NewEditIndex;
            //ShowmeetingHistories(Convert.ToInt32(ViewState["MeetingId"]));

        }

        protected void gvStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvStatus.Rows[e.RowIndex];
            // TextBox textAgendaTime = (TextBox)row.Cells[2].Controls[0];
            int id1 = Convert.ToInt32(row.Cells[1].Controls[0]);

            TextBox textAgendaDesc = (TextBox)row.Cells[3].Controls[0];
            //   TextBox textitemName = (TextBox)row.Cells[7].Controls[0];


        }

        protected void gvStatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStatus.EditIndex = -1;

            ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
        }

        protected void btnAgendaUpdate_Click(object sender, EventArgs e)
        {
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            Committee.Models.Agendum agenda = new Models.Agendum()
            {
                id = Convert.ToInt32(ViewState["agendaId"]),
                // AgendaTime = txtTimeUpdate.Text,
                AgendaDesc = txtAgendaUpdate.Text,
                MeetingId = Convert.ToInt32(ViewState["MeetingId"]),

                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")


                // AgendaDescription = txtMeetingAgenda.Text,
                //MinutesOfMeeting = file.ToString()

            };
            string inputJson = (new JavaScriptSerializer()).Serialize(agenda);


            client.UploadString(apiUrl3 + "/putMeetingAgenda?id=" + Convert.ToInt32(AgendaId.Value), inputJson);
            string apiUrl2 = Utilities.BASE_URL + "/api/CommitteesMembers";
            WebClient members = new WebClient();
            members.Headers["Content-type"] = "application/json";
            members.Encoding = Encoding.UTF8;
            List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(members.DownloadString(apiUrl2 + "/GetCommitteesMember?id=" + ddlCommitteeSpecified.SelectedItem.Value));
            foreach (var user in users)
            {

                string apiUrlFcm = Utilities.BASE_URL + "/api/Fcm";
                WebClient clienfcm = new WebClient();
                clienfcm.Headers["Content-type"] = "application/json";
                clienfcm.Encoding = Encoding.UTF8;
                object UserFcmo = new
                {
                    Action_Id1 = Convert.ToInt32(ViewState["committeeId"]),
                    Body = "تم تعديل اجندة",
                    Title = "تعديل اجندة",
                    Action_Id2 = Convert.ToInt32(ViewState["MeetingId"]),
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Click_action = "type_update_agenda"


                };
                string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                SMS.SendSms("تم تعديل بيانات  اجندة الاجتماع", user.Phone);
                Utilities.SendMailToOnePerson(user.UserEmailId, "اجندة الاجتماع", "تم تعديل بيانات اجندة الاجتماع");

                string apiUrlAlert = Utilities.BASE_URL + "/api/Committees";
                WebClient client4 = new WebClient();

                client4.Headers["Content-type"] = "application/json";
                client4.Encoding = Encoding.UTF8;

                Alert alert = new Alert()
                {
                    Title = " اجندة",
                    Message = "اشعار بخصوص تعديل اجندة",
                    UserId = user.ID,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Action_Id1 = Convert.ToInt32(ViewState["committeeId"]),
                    Action_Id2 = Convert.ToInt32(ViewState["MeetingId"]),
                    Action_Type = 3
                };
                string inputAlert = (new JavaScriptSerializer()).Serialize(alert);


                client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);


            }

            gvAgenda.EditIndex = -1;
            gvAgenda.DataSource = ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
            gvAgenda.DataBind();
            divagendaUpdate.Visible = false;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات الاجندة بنجاح', 'تم')", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            divagendaUpdate.Visible = false;
            gvAgenda.SelectedIndex = -1;
            gvAgenda.DataSource = ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
            gvAgenda.DataBind();

        }

        protected void btnHistoryUpdate_Click(object sender, EventArgs e)
        {
            string apiUrl3 = Utilities.BASE_URL + "/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            Committee.Models.MeetingHistory status = new Models.MeetingHistory()
            {
                Id = Convert.ToInt32(HistoryId.Value),
                CreatedAt = txtHistoryTime.Text,
                TitleAr = txtHistoryUpdate.Text,
                MeetingId = Convert.ToInt32(ViewState["MeetingId"])


                // AgendaDescription = txtMeetingAgenda.Text,
                //MinutesOfMeeting = file.ToString()

            };
            string inputJson = (new JavaScriptSerializer()).Serialize(status);


            client.UploadString(apiUrl3 + "/putMeetingHistory?id=" + Convert.ToInt32(HistoryId.Value), inputJson);
            gvStatus.EditIndex = -1;
            gvStatus.DataSource = ShowmeetingHistories(Convert.ToInt32(ViewState["MeetingId"]));
            gvStatus.DataBind();
            divStatus.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات الاجندة بنجاح', 'تم')", true);
        }

        protected void btnHistoryCancel_Click(object sender, EventArgs e)
        {
            divStatus.Visible = false;
            gvStatus.SelectedIndex = -1;
            gvStatus.DataSource = ShowmeetingHistories(Convert.ToInt32(ViewState["MeetingId"]));
            gvStatus.DataBind();

        }

        protected void MinutesOfMeetingUpload_Load(object sender, EventArgs e)
        {

        }

        protected void gvCommitteeMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCommitteeMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommitteeMembers.PageIndex = e.NewPageIndex;
            ShowMeetingMembers(Convert.ToInt32(Request.QueryString["meetingId"]));
        }
        protected void btnDownload_OnClick(object sender, EventArgs e)
        {

            //string filename = btnDownloadMinutesOfMeetingUpload.Text.TrimStart('/');




            if (filenamepdf != "/")

            {
                if (Session["meetingMinute"] != null)
                {
                    filenamepdf = Session["meetingMinute"].ToString();
                    shpwPdf.Visible = true;

                }
                else
                {
                    btnDownloadMinutesOfMeetingUpload.Visible = false;
                }

                //  string path = Server.MapPath("~/MasterPage/Uploads/"+filename);
                //btnDownloadMinutesOfMeetingUpload.Attributes.Add("href", filenamepdf);
                //btnDownloadMinutesOfMeetingUpload.Attributes.Add("target", "_blank");
            }

        }

        protected void txtMeetingAgendaLoader_TextChanged(object sender, EventArgs e)
        {
            txtMeetingAgenda.Text = txtMeetingAgendaLoader.Text;
        }

        //protected void btnExportPdf_Click(object sender, EventArgs e)
        //{
        //    PDF_Export();

        //}
    }

}
