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
    public partial class MeetingMangement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == "redirectSave")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم حفظ بيانات الاجتماع بنجاح', 'تم')", true);

                }
                if (Request.QueryString["id"] == "redirectUpdate")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('تم تعديل بيانات الاجتماع بنجاح', 'تم')", true);

                }
                if (Session["SystemRole"].ToString() == "5")
                {
                    btnaddMeeting.Visible = false;
                    LinkButton1.Visible = false;

                    
                }
                else
                {
                    btnaddMeeting.Visible = true;
                    LinkButton1.Visible = true;

                }
                LoadMeetings();
                gvMeeting.DataSource = ShowMeetings();
                gvMeeting.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvMeeting.DataSource = ShowMeetings();
            gvMeeting.DataBind();
        }

        protected void btnaddMeeting_Click(object sender, EventArgs e)
        {
            Response.Redirect("Meeting.aspx?status=new");
        }
        private List<Committee.Models.MeetingSearch> ShowMeetings()
        {
            int deptId = Convert.ToInt32(Session["DeptId"]);
            List<Committee.Models.MeetingSearch> meetingResults = new List<Models.MeetingSearch>();
            string apiUrl3 = Utilities.BASE_URL+"/api/Meetings";
            string apiUrlCommittee = Utilities.BASE_URL + "/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            
            List<Committee.Models.Meeting> meetings = serializer.Deserialize<List<Committee.Models.Meeting>>(client.DownloadString(apiUrl3 + "/GetMeeting?meetingName=" + txtSearch.Text+"&deptId="+deptId));

            if (meetings.Count != 0)
            {
                int status = 0;
                foreach (var meeting in meetings)
                {
                    Committee.Models.Committee committee = serializer.Deserialize<Committee.Models.Committee>(client.DownloadString(apiUrlCommittee + "/GetCommitteeByIdForWeb?committeeId=" +meeting.CommitteeId ));

                    status = Convert.ToInt32(meeting.Status);
                    string date = null;
                    if (!String.IsNullOrEmpty(meeting?.MeetingDate))
                    {
                        DateTime createdAt = DateTime.Parse(meeting?.MeetingDate);
                        date = createdAt.ToString("yyyy/MM/dd");
                    }
                    meetingResults.Add(new Models.MeetingSearch()
                    {
                        الرقم = meeting?.MeetingId,
                        عنوان_الاجتماع = meeting?.MeetingTitle,
                        تاريخ_الاجتماع = date,
                        وقت_الاجتماع = meeting?.MeetingTime,
                        موقع_الاجتماع = meeting?.MeetingAddress,
                        حالة_الاجتماع = meeting?.MeetingHistories?.Count == 0 ? "" : meeting?.MeetingHistories?.LastOrDefault(x => x.MeetingId == meeting?.MeetingId).TitleAr,
                        اللجنة= committee?.CommitteeName



                    });
                }



            }

            return meetingResults;

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
            gvMeeting.DataSource = ShowMeetings();
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

                if (Session["SystemRole"].ToString() == "5")
                {
                    selectButton.Visible = true;
                    selectButton.Text = "اختيار";
                    selectButton.ForeColor = System.Drawing.Color.Blue;
                    selectButton.BorderColor = System.Drawing.Color.White;
                    selectButton.Font.Size = FontUnit.Medium;
                    selectButton.Font.Underline = true;
                    editButton.Visible = false;
                    deleteButton.Visible = false;
                    btnaddMeeting.Visible = false;
                    LinkButton1.Visible = false;



                }
                else
                {
                    LinkButton1.Visible = true;

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
                    btnaddMeeting.Visible = true;

                }

                    
            }
        }

        protected void gvMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Committee.Models.StatusSearch> histories = new List<Models.StatusSearch>();
            int meetingId = Convert.ToInt32(gvMeeting.SelectedRow.Cells[1].Text);
            int committeeId = Convert.ToInt32(gvMeeting.SelectedRow.Cells[8].Text);
            GridViewRow row = gvMeeting.SelectedRow;
            List<Committee.Models.AgendaUpdate> data = ShowmeetingAgenda(meetingId);
        
            ViewState["MeetingId"] = meetingId;
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string result = webClient.DownloadString(Utilities.BASE_URL+"/api/Meetings/GetMeetingHistory?meetingId=" + meetingId);
            List<Committee.Models.MeetingHistory> Statuss = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.MeetingHistory>>(result);
            foreach (var status in Statuss)
            {
                histories.Add(new Models.StatusSearch()
                {
                    //الرقم = status.Id,
                    الحاله = status.TitleAr,
                    التاريخ = status.CreatedAt

                });
            }

           
        }

        private List<Committee.Models.AgendaUpdate> ShowmeetingAgenda(int meetingId)
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Agendum> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Agendum>>(client.DownloadString(apiUrl3 + "/GetMeetingAgendas?meetingId=" + meetingId));
            
            return Committees.Select(x => new Models.AgendaUpdate() {الاجندة = x.AgendaDesc }).ToList();
        }
        private void ShowmeetingAgendaUpdate(int meetingId)
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Meetings";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.Agendum> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Agendum>>(client.DownloadString(apiUrl3 + "/GetMeetingAgendas?meetingId=" + meetingId));



        }
        protected void gvMeeting_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvMeeting.Rows[e.NewEditIndex].Cells[1].Text);
            Response.Redirect("Meeting.aspx?meetingId=" + id + "&status=update");
         
            //WebClient client = new WebClient();
            //client.Headers["Content-type"] = "application/json";
            //client.Encoding = Encoding.UTF8;

            //Committee.Models.Meeting meeting = (new JavaScriptSerializer()).Deserialize<Committee.Models.Meeting>(client.DownloadString(apiUrl3 + "/GetMeetingForWeb?meetingId=" + gvMeeting.Rows[e.NewEditIndex].Cells[1].Text));
            //txtMeetingName.Text = meeting.MeetingTitle;
            //txtMeetingDate.Text = meeting.MeetingDate;
            //txtMeetingLocation.Text = meeting.MeetingAddress;
            //txtMeetingTopic.Text = meeting.MeetingDesc;
            //lat.Value = meeting.Latitude;
            //lng.Value = meeting.longitude;
            //ddlCommitteeSpecified.SelectedValue = meeting.CommitteeId.ToString();
            //txtMeetingTime.Text = meeting.MeetingTime;
            //txtStatus.Text = meeting.MeetingHistories.Count == 0 ? "" : meeting.MeetingHistories.Last(x => x.MeetingId == meeting.MeetingId).TitleAr;


            //btnSave.Text = "تعديل";
            //ViewState["MeetingId"] = gvMeeting.Rows[e.NewEditIndex].Cells[1].Text;
        }

        protected void gvMeeting_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMeeting.PageIndex = e.NewPageIndex;
            gvMeeting.DataSource = ShowMeetings();
            gvMeeting.DataBind();
        }

        protected void gvMeeting_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int id = Convert.ToInt32(gvMeeting.Rows[e.NewSelectedIndex].Cells[1].Text);
            Response.Redirect("Meeting.aspx?meetingId=" + id + "&status=selected");
        }

        protected void gvMeeting_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Committee.Models.MeetingSearch> result = (List<Committee.Models.MeetingSearch>)ViewState["dt"];
            if (result.Count > 0)
            {
                if (ViewState["sort"].ToString() == "Asc")
                {
                    if ("الرقم" == e.SortExpression)
                        result = result.OrderByDescending(r => r.الرقم).ToList();
                    if ("عنوان_الاجتماع" == e.SortExpression)
                        result = result.OrderByDescending(r => r.عنوان_الاجتماع).ToList();
                    if ("تاريخ_الاجتماع" == e.SortExpression)
                        result = result.OrderByDescending(r => r.تاريخ_الاجتماع).ToList();
                    if ("وقت_الاجتماع" == e.SortExpression)
                        result = result.OrderByDescending(r => r.وقت_الاجتماع).ToList();
                    if ("موقع_الاجتماع" == e.SortExpression)
                        result = result.OrderByDescending(r => r.موقع_الاجتماع).ToList();
                    if ("حالة_الاجتماع" == e.SortExpression)
                        result = result.OrderByDescending(r => r.حالة_الاجتماع).ToList();
                  
                    //...do it to all the fields

                    ViewState["sort"] = "Desc";
                }
                else
                {
                    if ("الرقم" == e.SortExpression)
                        result = result.OrderBy(r => r.الرقم).ToList();
                    if ("عنوان_الاجتماع" == e.SortExpression)
                        result = result.OrderBy(r => r.عنوان_الاجتماع).ToList();
                    if ("تاريخ_الاجتماع" == e.SortExpression)
                        result = result.OrderBy(r => r.تاريخ_الاجتماع).ToList();
                    if ("وقت_الاجتماع" == e.SortExpression)
                        result = result.OrderBy(r => r.وقت_الاجتماع).ToList();
                    if ("موقع_الاجتماع" == e.SortExpression)
                        result = result.OrderBy(r => r.موقع_الاجتماع).ToList();
                    if ("حالة_الاجتماع" == e.SortExpression)
                        result = result.OrderBy(r => r.حالة_الاجتماع).ToList();
                   
                    ViewState["sort"] = "Asc";
                }

                gvMeeting.DataSource = result;
                gvMeeting.DataBind();
                ViewState["dt"] = result;
            }
        }
        private void LoadMeetings()
        {
            this.gvMeeting.DataSource =ShowMeetings();
            this.gvMeeting.DataBind();
            ViewState["dt"] = ShowMeetings();
            ViewState["sort"] = "Asc";
        }

    }
}