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
        static List<Committee.Models.Agendum> Agendas = new List<Committee.Models.Agendum>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            MinutesOfMeetingUpload.Visible = true;
            if (!IsPostBack)
            {
                if (Request.QueryString["status"] == "update")
                {
                    List<Committee.Models.StatusSearch> histories = new List<Models.StatusSearch>();
                    string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;

                    Committee.Models.Meeting meeting = (new JavaScriptSerializer()).Deserialize<Committee.Models.Meeting>(client.DownloadString(apiUrl3 + "/GetMeetingForWeb?meetingId=" + Request.QueryString["MeetingId"]));
                    txtMeetingName.Text = meeting.MeetingTitle;
                    txtMeetingDate.Text = meeting.MeetingDate;
                    txtMeetingLocation.Text = meeting.MeetingAddress;
                    txtMeetingTopic.Text = meeting.MeetingDesc;
                    lat.Value = meeting.Latitude;
                    lng.Value = meeting.longitude;
                    ddlCommitteeSpecified.SelectedValue = meeting.CommitteeId.ToString();
                    txtMeetingTime.Text = meeting.MeetingTime;
                    txtStatus.Text = meeting.MeetingHistories.Count == 0 ? "" : meeting.MeetingHistories.Last(x => x.MeetingId == meeting.MeetingId).TitleAr;
                    
                    ViewState["MeetingId"] = Request.QueryString["meetingId"];
                    List<Committee.Models.AgendaUpdate> data = ShowmeetingAgenda(Convert.ToInt32(ViewState["MeetingId"]));
                    gvAgenda.DataSource = data.Where(x => x != null).ToList();

                    gvAgenda.DataBind();
                  
                    string result2 = client.DownloadString("https://committeeapi20190806070934.azurewebsites.net/api/Meetings/GetMeetingHistory?meetingId=" + Convert.ToInt32(ViewState["MeetingId"]));
                    List<Committee.Models.MeetingHistory> Statuss = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingHistory>>(result2);
                    foreach (var status in Statuss)
                    {
                        histories.Add(new Models.StatusSearch()
                        {
                            الرقم = status.Id,
                            الحاله = status.TitleAr,
                            التاريخ = status.CreatedAt

                        });
                    }

                    gvStatus.DataSource = histories.Where(x => x != null).ToList();

                    gvStatus.DataBind();

                }
                MinutesOfMeetingUpload.Visible = true;


                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString("https://committeeapi20190806070934.azurewebsites.net/api/Committees/GetCommitteesForWeb");
                List<Committee.Models.Committee> committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Committee>>(result);
                ddlCommitteeSpecified.DataSource = committees;
                ddlCommitteeSpecified.DataTextField = "CommitteeName";
                ddlCommitteeSpecified.DataValueField = "CommitteeId";
                ddlCommitteeSpecified.DataBind();

                //gvStatus.DataSource = ShowmeetingHistories(4);
                //gvStatus.DataBind();
                //gvAgenda.DataSource = ShowmeetingAgenda(4);
                //gvAgenda.DataBind();

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
                if (MinutesOfMeetingUpload.PostedFile != null && MinutesOfMeetingUpload.PostedFile.ContentLength > 0)
                {
                    fname = Path.GetFileName(MinutesOfMeetingUpload.PostedFile.FileName);
                    MinutesOfMeetingUpload.SaveAs(Server.MapPath(Path.Combine("~/Uploads/", fname)));

                }
                string apiUrlUpdate = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";
                Committee.Models.Meeting meetingUpdate = new Models.Meeting()
                {

                    MeetingTitle = txtMeetingName.Text,
                    Address = txtMeetingLocation.Text,
                    CommitteeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value),
                    MeetingDate = txtMeetingDate.Text,
                    MeetingDesc = txtMeetingTopic.Text,
                    MeetingTime = txtMeetingTime.Text,
                    longitude = lng.Value,
                    Latitude = lat.Value,
                    MeetingAddress = txtMeetingLocation.Text,
                    MinutesOfMeeting = "/" + fname,
                    Status = txtStatus.Text,

                  //  AgendaDescription = txtMeetingAgenda.Text,
                    MeetingId = Convert.ToInt32(ViewState["MeetingId"])

                };
                string inputJson = (new JavaScriptSerializer()).Serialize(meetingUpdate);

                client.UploadString(apiUrlUpdate + "/putMeeting?id=" + Convert.ToInt32(ViewState["MeetingId"]), inputJson);
              
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات الاجتماع بنجاح', 'تم')", true);

            }
            else
            {
                string fname=null;
                string meetingId = null;
                if (MinutesOfMeetingUpload.PostedFile != null && MinutesOfMeetingUpload.PostedFile.ContentLength > 0)
                {
                     fname = Path.GetFileName(MinutesOfMeetingUpload.PostedFile.FileName);
                    MinutesOfMeetingUpload.SaveAs(Server.MapPath(Path.Combine("~/Uploads/", fname)));

                }
                //object file = ReadStream(MinutesOfMeetingUpload.PostedFile.InputStream);
                string apiUrlUpdate = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";
                if (Session["fileNem"]==""|| Session["fileNem"]==null)
                {
                   // string fileName = "/" + fname;
                }
                else
                {
                    fname = Session["fileNem"].ToString();
                }
                Committee.Models.Meeting meeting = new Models.Meeting()
                {

                    MeetingTitle = txtMeetingName.Text,
                    Address = txtMeetingLocation.Text,
                    CommitteeId = Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value),
                    MeetingDate = txtMeetingDate.Text,
                    MeetingDesc = txtMeetingTopic.Text,
                    MeetingTime = txtMeetingTime.Text,
                    longitude=lng.Value,
                    Latitude= lat.Value,
                    MeetingAddress=txtMeetingLocation.Text,
                    MinutesOfMeeting="/" + fname,
                    Status=txtStatus.Text


                    // AgendaDescription = txtMeetingAgenda.Text,
                    //MinutesOfMeeting = file.ToString()

                };
                string inputJson = (new JavaScriptSerializer()).Serialize(meeting);

              meetingId= client.UploadString(apiUrlUpdate + "/PostMeeting", inputJson);
                for (int i = 0; i < Agendas.Count; i++)
                {
                    string apiUrlAgenda = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";
                    Committee.Models.Agendum agenda = new Models.Agendum()
                    {

                        AgendaDesc = Agendas[0].AgendaDesc,
                        AgendaTime = Agendas[0].AgendaTime,
                        MeetingId = Convert.ToInt32(meetingId)


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
                Committee.Models.MeetingHistory status = new Models.MeetingHistory()
                {

                    TitleAr = txtStatus.Text,
                    MeetingDate =txtMeetingDate.Text,
                    
                    MeetingId = Convert.ToInt32(meetingId)


                    // AgendaDescription = txtMeetingAgenda.Text,
                    //MinutesOfMeeting = file.ToString()

                };
                WebClient client3 = new WebClient();
                client3.Headers["Content-type"] = "application/json";
                client3.Encoding = Encoding.UTF8;
                string inputJson3 = (new JavaScriptSerializer()).Serialize(status);
                client3.UploadString(apiUrlUpdate + "/PostStatusOfMeeting", inputJson3);
               int committeeId= Convert.ToInt32(ddlCommitteeSpecified.SelectedItem.Value);
                ViewState["committeeId"] = committeeId;
                string apiUrl2 = "https://committeeapi20190806070934.azurewebsites.net/api/CommitteesMembers";
                WebClient members = new WebClient();
                members.Headers["Content-type"] = "application/json";
                members.Encoding = Encoding.UTF8;
               List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(members.DownloadString(apiUrl2 + "/GetCommitteesMember?id="+ committeeId));
                foreach (var user in users)
                {
                   
                    string apiUrlFcm = "https://committeeapi20190806070934.azurewebsites.net/api/Fcm";
                    WebClient clienfcm = new WebClient();
                    clienfcm.Headers["Content-type"] = "application/json";
                    clienfcm.Encoding = Encoding.UTF8;
                    object UserFcmo = new
                    {

                        Action_id = committeeId,
                        Body = "اشعار بخصوص اجتماع",
                        Click_action = "type_new_meeting",
                        Title = "اضافة اجتماع",
                        Action2_id=meetingId

                    };
                    string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                    clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                    string apiUrlAlert = "https://committeeapi20190806070934.azurewebsites.net/api/Committees";
                    WebClient client4 = new WebClient();

                    client4.Headers["Content-type"] = "application/json";
                    client4.Encoding = Encoding.UTF8;

                    Alert alert = new Alert()
                    {
                        Title = "لجنة جديده",
                        Message = "اشعار بخصوص اضافة لجنة",
                        UserId = user.ID,
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
                    };
                    string inputAlert = (new JavaScriptSerializer()).Serialize(alert);


                    client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);

                }
                Agendas = new List<Agendum>();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات الاجتماع بنجاح', 'تم')", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم ارسال الاشعارات بنجاح', 'تم')", true);

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
        protected void Upload(object sender,EventArgs e)
        {

        }

        private void ResetControls()
        {
            Agendas = new List<Agendum>();
            txtMeetingName.Text = "";
            txtMeetingLocation.Text = "";
           txtMeetingDate.Text = "";
            txtMeetingTopic.Text = "";
            txtMeetingTime.Text = "";
         //  txtMeetingAgenda.Text = "";
            ddlCommitteeSpecified.SelectedIndex = -1;
            //txtAgendaTime.Text = "";
            txtMeetingAgenda.Text = "";
            lat.Value = "";
            lng.Value = "";
          txtStatus.Text="";
           
            gvAgenda.Visible = false;
            gvStatus.Visible=false;


        }
        private List<Committee.Models.MeetingSearch> ShowMeetings()
        {
            List<Committee.Models.MeetingSearch> meetingResults = new List<Models.MeetingSearch>();
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

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
                        وصف_الاجتماع = meeting.MeetingDesc,
                        حالة_الاجتماع = meeting.MeetingHistories.Count==0?"": meeting.MeetingHistories.LastOrDefault(x=>x.MeetingId== meeting.MeetingId).TitleAr,
                      اللجنة = meeting.CommitteeId.ToString(),

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
            mpePopUp.Show();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Agendas = new List<Agendum>();
            mpePopUp.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpePopUp.Hide();
        }

        protected void btnOpenPopUp_Click1(object sender, EventArgs e)
        {
            if (MinutesOfMeetingUpload.PostedFile.FileName!="")
            {
                Session["fileNem"] =MinutesOfMeetingUpload.PostedFile.FileName;
            }
           
            mpePopUp.Show();
           // MinutesOfMeetingUpload.v = Session["fileNem"].ToString();
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            Agendas.Add(new Models.Agendum()
            {
                AgendaDesc = txtMeetingAgenda.Text,
                MeetingId = 4
            });
            mpePopUp.Hide();
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
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Agendum> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Agendum>>(client.DownloadString(apiUrl3 + "/GetMeetingAgendas?meetingId=" + meetingId));


            return Committees.Select(x => new Models.AgendaUpdate() { الرقم = x.id, وصف_الاجندة = x.AgendaDesc,وقت_الاجندة=x.AgendaTime.ToString() }).ToList();
        }
        private void ShowmeetingAgendaUpdate(int meetingId)
        {
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

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
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

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


                deleteButton.Visible = true;
                deleteButton.Text = "مسح";

                editButton.Visible = true;
                editButton.Text = "تعديل";
                selectButton.Text = "اختيار";
            }
        }

        protected void gvAgenda_RowEditing(object sender, GridViewEditEventArgs e)
        {
            divagendaUpdate.Visible = true;
            GridViewRow row = (GridViewRow)gvAgenda.Rows[e.NewEditIndex];
            txtAgendaUpdate.Text = row.Cells[3].Text;
            txtTimeUpdate.Text = row.Cells[2].Text;
            AgendaId.Value = row.Cells[1].Text;
            gvAgenda.SelectedIndex = -1;
        }

        protected void gvAgenda_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvAgenda.Rows[e.RowIndex];
            TextBox textAgendaTime = (TextBox)row.Cells[2].Controls[0];
            int id1 =Convert.ToInt32(row.Cells[1].Controls[0]);

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


                deleteButton.Visible = true;
                deleteButton.Text = "مسح";

                editButton.Visible = true;
                editButton.Text = "تعديل";
                selectButton.Text = "اختيار";
            }
        }

        protected void gvStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int statusId = Convert.ToInt32(gvStatus.Rows[e.RowIndex].Cells[1].Text.ToString());
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

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
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            int meetingId = v;
            List<Committee.Models.MeetingHistory> histories = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingHistory>>(client.DownloadString(apiUrl3 + "/GetMeetingHistory?meetingId=" + meetingId));


            return histories.Select(x => new Models.StatusSearch() { الرقم = x.Id, الحاله = x.TitleAr, التاريخ = x.CreatedAt }).ToList();
        }

        protected void gvStatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            divStatus.Visible = true;
            GridViewRow row = (GridViewRow)gvStatus.Rows[e.NewEditIndex];
            txtHistoryUpdate.Text = row.Cells[3].Text;
            txtHistoryTime.Text = row.Cells[2].Text;
            HistoryId.Value = row.Cells[1].Text;
            gvStatus.SelectedIndex = -1;
            //gvStatus.EditIndex = e.NewEditIndex;

            ////gvAgenda.EditIndex = e.NewEditIndex;
            //ShowmeetingHistories(Convert.ToInt32(ViewState["MeetingId"]));

        }

        protected void gvStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvStatus.Rows[e.RowIndex];
            TextBox textAgendaTime = (TextBox)row.Cells[2].Controls[0];
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
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            Committee.Models.Agendum agenda = new Models.Agendum()
            {
                id = Convert.ToInt32(AgendaId.Value),
                AgendaTime = txtTimeUpdate.Text,
                AgendaDesc = txtAgendaUpdate.Text,
                MeetingId = Convert.ToInt32(ViewState["MeetingId"])


                // AgendaDescription = txtMeetingAgenda.Text,
                //MinutesOfMeeting = file.ToString()

            };
            string inputJson = (new JavaScriptSerializer()).Serialize(agenda);


            client.UploadString(apiUrl3 + "/putMeetingAgenda?id=" + Convert.ToInt32(AgendaId.Value), inputJson);
            string apiUrl2 = "https://committeeapi20190806070934.azurewebsites.net/api/CommitteesMembers";
            WebClient members = new WebClient();
            members.Headers["Content-type"] = "application/json";
            members.Encoding = Encoding.UTF8;
            List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(members.DownloadString(apiUrl2 + "/GetCommitteesMember?id=" + Convert.ToInt32(ViewState["committeeId"])));
            foreach (var user in users)
            {

                string apiUrlFcm = "https://committeeapi20190806070934.azurewebsites.net/api/Fcm";
                WebClient clienfcm = new WebClient();
                clienfcm.Headers["Content-type"] = "application/json";
                clienfcm.Encoding = Encoding.UTF8;
                object UserFcmo = new
                {

                    Action_id = Convert.ToInt32(ViewState["committeeId"]),
                    Body = "اشعار بخصوص اجتماع",
                    Click_action = "type_new_meeting",
                    Title = "اضافة اجتماع",
                    Action2_id = Convert.ToInt32(ViewState["MeetingId"])

                };
                string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                string apiUrlAlert = "https://committeeapi20190806070934.azurewebsites.net/api/Committees";
                WebClient client4 = new WebClient();

                client4.Headers["Content-type"] = "application/json";
                client4.Encoding = Encoding.UTF8;

                Alert alert = new Alert()
                {
                    Title = "لجنة جديده",
                    Message = "اشعار بخصوص اضافة لجنة",
                    UserId = user.ID,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
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

        }

        protected void btnHistoryUpdate_Click(object sender, EventArgs e)
        {
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Meetings";

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

        }

        protected void MinutesOfMeetingUpload_Load(object sender, EventArgs e)
        {

        }
    }
}
