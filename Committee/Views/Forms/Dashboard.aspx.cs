using Committee.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Committee.Views.Forms
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["SystemRole"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                lblUserName2.Text = Session["Name"].ToString();
                lblDate.Text = DateTime.Now.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));
                GetMemeberCount();
                GetCommitteeCount();
                GetDepartmentCount();
            }
        }

        [WebMethod]
        public static List<object> GetChartData()
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var DeptId = HttpContext.Current.Session["DeptId"];
            //if (DeptId==null)
            //{

            //}
            List<int> Charts = (new JavaScriptSerializer()).Deserialize<List<int>>(client.DownloadString(apiUrl3 + "/GetActiveAndNonActiveCommittee?deptId=" + DeptId));
            
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "اللجان النشطة", Charts[0] });
            chartData.Add(new object[] { "اللجان الغير نشطة", Charts[1] });
          
            return chartData;
        }
        [WebMethod]
        public static List<object> GetTypeCommittee()
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var DeptId = HttpContext.Current.Session["DeptId"];
            List<int> Charts = (new JavaScriptSerializer()).Deserialize<List<int>>(client.DownloadString(apiUrl3 + "/GetTypeCommittee?deptId=" + DeptId));

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "اللجان العسكرية", Charts[0] });
            chartData.Add(new object[] { "اللجان المدنية", Charts[1] });

            return chartData;
        }
        [WebMethod]
        public static List<object> AcceptedAndRejectedCount()
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var DeptId = HttpContext.Current.Session["DeptId"];
            List<int> Charts = (new JavaScriptSerializer()).Deserialize<List<int>>(client.DownloadString(apiUrl3 + "/AcceptedAndRejectedCount?deptId=" + DeptId));

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "عدد مرات قبول المحاضر", Charts[0] });
            chartData.Add(new object[] { "عدد مرات رفض المحاضر", Charts[1] });

            return chartData;
        }
        [WebMethod]
        public  void GetMemeberCount()
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";
            var DeptId = HttpContext.Current.Session["DeptId"];

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            int Charts = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/GetMemeberCount?deptId=" + DeptId));

            lblMembersCount.Text = Charts.ToString();
        }
        [WebMethod]
        public void GetCommitteeCount()
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";
            var DeptId = HttpContext.Current.Session["DeptId"];

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            int Charts = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/GetCommitteeCount?deptId=" + DeptId));
            lblCommitteeCount.Text = Charts.ToString();
        }

        [WebMethod]
        public void GetDepartmentCount()
        {
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";
            var DeptId = HttpContext.Current.Session["DeptId"];

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            int Charts = (new JavaScriptSerializer()).Deserialize<int>(client.DownloadString(apiUrl3 + "/GetDepartmentCount?deptId=" + DeptId));
            lblDeptCount.Text = Charts.ToString();
        }

        [WebMethod]
        public static List<object> GetDepartmentsOfEachCommittee()
        {
            List<DepartmentCommittees> departmentCommittees = new List<DepartmentCommittees>();
            List<object> chartData = new List<object>();
            string apiUrl3 = Utilities.BASE_URL+"/api/Chart";
            var DeptId = HttpContext.Current.Session["DeptId"];

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List <DepartmentCommittees> Charts = (new JavaScriptSerializer()).Deserialize<List<DepartmentCommittees>>(client.DownloadString(apiUrl3 + "/GetDepartmentsOfEachCommittee?deptId=" + DeptId));

            foreach (var chart in Charts)
            {
             
                chartData.Add(new object[] {chart.DeptName, chart.CommitteesCount });

            }



            return chartData;
        }
    }
    }
