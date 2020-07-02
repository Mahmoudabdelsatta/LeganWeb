using Committee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Committee.Controller
{
    public class WebApiConsume
    {
        public static bool login(string url, string UserName, string Password, out string Name, out int? SystemRole, out string DepartmentId)
        {

            Password = Encryptor.MD5Hash(Password);
            string apiUrlUser = Utilities.BASE_URL + "/api/Users";

            object input = new
            {
                Name = UserName.Trim(),
                Pass = Password.Trim()
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrlUser + "/ValidateUserNameAndPassword?userName=" + UserName + "&password=" + Password, inputJson);
            bool validate = (new JavaScriptSerializer()).Deserialize<bool>(json);
            Committee.Models.User user = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client.DownloadString(apiUrlUser + "/GetUser?userName=" + UserName.ToLower() + "&password=" + Password.Trim().ToLower()));

            Name = user?.UserName;
            SystemRole = user == null ? SystemRole = 0: user.SystemRole;
            DepartmentId = user?.ManagerOfDepartment;
            return validate;
        }
        public static bool CheckAccessToken(string accessToken)
        {


            string apiUrlUser = Utilities.BASE_URL + "/api/Users";
            
            WebClient client = new WebClient();
            client.Headers["Authorization"] = "bearer " + accessToken;

            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(apiUrlUser + "/CheckAccessToken");
            bool check = (new JavaScriptSerializer()).Deserialize<bool>(json);

            return check;
        }
        public static bool ValidateUserPhone(string Phone)
        {

            string apiUrlUser = Utilities.BASE_URL + "/api/Users";
            //string apiUrl2 = Utilities.BASE_URL+"/api/Users";

            object input = new
            {
                Phone = Phone.Trim(),
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrlUser + "/ValidateUserPhone?Phone=" + Phone, inputJson);
            bool validate = (new JavaScriptSerializer()).Deserialize<bool>(json);
            return validate;
        }
        public static List<CommitteeSearch> ShowCommittees(string CommitteeName,int deptId)
        {

            List<CommitteeSearch> committeesUpdate = new List<CommitteeSearch>();
            string apiUrl3 = Utilities.BASE_URL + "/api/Committees";
            string apiUrl2 = Utilities.BASE_URL + "/api/Users";

            WebClient client2 = new WebClient();
            client2.Headers["Content-type"] = "application/json";
            client2.Encoding = Encoding.UTF8;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveData> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveData>>(client.DownloadString(apiUrl3 + "/GetCommitteeDetails?committeeName=" + CommitteeName.Trim().ToLower() + "&commiteeDate=" + CommitteeName+"&deptId="+deptId));
            foreach (var committee in Committees)
            {
                Committee.Models.User userman = new User();
                Committee.Models.User userssec = new User();

                if (committee.CommitteeManager != null && committee.CommitteeSecretary != null)
                {


                    userman = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client2.DownloadString(apiUrl2 + "/GetUserById?id=" + committee.CommitteeManager));
                    userssec = (new JavaScriptSerializer()).Deserialize<Committee.Models.User>(client2.DownloadString(apiUrl2 + "/GetUserById?id=" + committee.CommitteeSecretary));
                }
                string date = null;
                if (committee.CommitteeDate!=null)
                {
               DateTime createdAt = DateTime.Parse(committee.CommitteeDate);
                 date = Utilities.ConvertDateCalendar(createdAt, "Hijri", "ar-sa");
                }
              
                committeesUpdate.Add(new CommitteeSearch()
                {
                    رقم_اللجنه = committee.CommitteeId,
                    اسم_اللجنه = committee.CommitteeName,
                    تاريخ_اللجنه = date,
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
        private static List<CommitteeSearch> ShowCommitteesAdd(string CommitteeName)
        {
            List<CommitteeSearch> committeesUpdate = new List<CommitteeSearch>();
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveData> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveData>>(client.DownloadString(apiUrl3 + "/GetCommitteeDetails?committeeName=" + CommitteeName.Trim().ToLower() + "&commiteeDate=" + CommitteeName.Trim().ToLower()));
            foreach (var committee in Committees)
            {
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
                    رئيس_اللجنه = committee.CommitteeManager,
                    سكرتير_اللجنه = committee.CommitteeSecretary,
                    //جهة_الوارد = committee.CommitteeInbox1,
                    //رقم_القيد = committee.CommitteeEnrollmentNumber,
                    //سنة_القيد = committee.CommitteeEnrollmentDate,
                    //تاريخ_التعديل = committee.CreatedAt,
                    //تاريخ_اللإنشاء = committee.UpdatedAt
                });
            }

            return committeesUpdate;
        }
        public static List<CommitteeSearchUpdate> ShowsCommitteesForUpdate(int committeeId, string CommitteeName)
        {
            List<CommitteeSearchUpdate> committeesUpdate = new List<CommitteeSearchUpdate>();
            string apiUrl3 = "https://committeeapi20190806070934.azurewebsites.net/api/Committees";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.CommitteeRetrieveUpdate> Committees = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.CommitteeRetrieveUpdate>>(client.DownloadString(apiUrl3 + "/GetCommitteeDetails?committeeName=" + CommitteeName.Trim().ToLower()));
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
        public static List<UserGrid> ShowCommitteeMembers(int committeeId)
        {
            List<UserGrid> userGrids = new List<UserGrid>();
            string apiUrl3 = Utilities.BASE_URL + "/api/CommitteesMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(client.DownloadString(apiUrl3 + "/GetCommitteesMember?id=" + committeeId)).Where(x=>x.ID!=0).ToList();
           
            foreach (var user in users)
            {
                CommitteesMember committeesMember = (new JavaScriptSerializer()).Deserialize<CommitteesMember>(client.DownloadString(apiUrl3 + "/GetMemberDataFromCommitteeMember?committeeId=" + committeeId + "&userId="+user.ID));
                string roleName  = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrl3 + "/GetRoleName?committeeRole=" + committeesMember?.CommitteeRole));

                userGrids.Add(new UserGrid() { رقم_العضو = committeesMember.User.ID, اسم_العضو = committeesMember?.User?.UserName, رقم_التليقون = committeesMember?.User?.Phone, البريد_الالكترونى = committeesMember?.User?.UserEmailId, جهة_العمل = committeesMember?.User?.WorkSide, الدور = roleName });
            }


            return userGrids;

        }
        public static List<UserGrid> ShowCommitteeMembersForNew(int userId)
        {
            List<UserGrid> UserGrid = new List<UserGrid>();
            string apiUrl3 = Utilities.BASE_URL + "/api/CommitteesMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

           Committee.Models.User user = (new JavaScriptSerializer()).Deserialize<User>(client.DownloadString(apiUrl3 + "/GetMemberForGrid?userId=" + userId));

            UserGrid.Add(new UserGrid() { رقم_العضو = user.ID, اسم_العضو = user.UserName, رقم_التليقون = user.Phone, البريد_الالكترونى = user.UserEmailId, جهة_العمل = user.WorkSide,الدور="عضو"});
            return UserGrid ;

        }
        public static List<User> ShowCommitteeMembersForChat (int committeeId)
        {
            string apiUrl3 = Utilities.BASE_URL + "/api/CommitteesMembers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            List<Committee.Models.User> users = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(client.DownloadString(apiUrl3 + "/GetCommitteesMember?id=" + committeeId));


            return users.ToList();

        }
        public static string DeleteCommittee(string url,int CommitteeId)
        {
            int committeeId = CommitteeId;
            string apiUrlDeleteCommittee = url;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = committeeId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            string CommitteeExist = (new JavaScriptSerializer()).Deserialize<string>(client.DownloadString(apiUrlDeleteCommittee + "/DeleteCommittee?id=" + committeeId.ToString()));
            return CommitteeExist;
        }
        public static List<User> GetUsers(string Url)
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string result = webClient.DownloadString(Url);
            List<Committee.Models.User> Members = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.User>>(result);
            return Members;

        }
        public static List<Department> GetDepartments(string Url)
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string result = webClient.DownloadString(Url);
            List<Committee.Models.Department> Departments = (new JavaScriptSerializer()).Deserialize<List<Committee.Models.Department>>(result);
            return Departments;

        }
        public static void UpdateCommittee(string url, int CommitteeId,string inputJson)
        {
            int committeeId = CommitteeId;
            string apiUrlUpdateCommittee = url;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                id = committeeId,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            client.UploadString(apiUrlUpdateCommittee + "/PutCommittee?id=" + committeeId, inputJson);
          
        }
        public static void PostCommittee(string url, string inputJson)
        {
         
            string apiUrlPostCommittee = url;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            client.UploadString(apiUrlPostCommittee + "/PostCommitteeForWeb", inputJson);

        }
        public static bool IsCommitteeExist(string url, string  CommitteeName)
        {
         
            string apiUrlCheckCommittee = url;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                Name = CommitteeName,
            };
            string inputJson3 = (new JavaScriptSerializer()).Serialize(input);
            bool CommitteeExist = (new JavaScriptSerializer()).Deserialize<bool>(client.DownloadString(apiUrlCheckCommittee + "/IsCommitteeExist?committeeName=" + CommitteeName));
            return CommitteeExist;
        }
        public static int GetCommitteeId(string Url, string CommitteeName)
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            int Committee = (new JavaScriptSerializer()).Deserialize<int>(webClient.DownloadString(Url + "/GetCommitteeId?committeeName=" + CommitteeName));
            return Committee;

        }
        public static void PostCommitteeMembers(string url, string inputJson)
        {

            string apiUrlPostCommitteeMembers = url;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            client.UploadString(apiUrlPostCommitteeMembers + "/PostListOfCommitteesMember", inputJson);

        }
        public static User GetUserById(string Url, int MemberId)
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            User userFcm = (new JavaScriptSerializer()).Deserialize<User>(webClient.DownloadString(Url + "/GetUser?id=" + MemberId));
            return userFcm;

        }
        public static void SendUserNotification(string url, int CommitteeId,string CommitteeName,string FcmToken)
        {
            try
            {
                string apiUrlFcm = url;
                WebClient clienfcm = new WebClient();
                clienfcm.Headers["Content-type"] = "application/json";
                clienfcm.Encoding = Encoding.UTF8;
                object UserFcmo = new
                {
                    Action_Id1 = CommitteeId,
                    Title = "اضافة عضو",
                    Action_Id2 = 0,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Action_Type = "type_join_committee",
                    Body = "عضو جديد",
                };
                string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + FcmToken, inputFcm);
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
        }
        public static void PostAlert(string url, int CommitteeId, int memberId)
        {
            WebClient client4 = new WebClient();

            client4.Headers["Content-type"] = "application/json";
            client4.Encoding = Encoding.UTF8;

            Alert alert = new Alert()
            {
                Title = "لجنة جديده",
                Message = "اشعار بخصوص اضافة لجنة",
                UserId = memberId,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Action_Id1=CommitteeId,
                Action_Id2=null,
                Action_Type=1
            };
            string inputAlert = (new JavaScriptSerializer()).Serialize(alert);


            client4.UploadString(url + "/PostAlert", inputAlert);
        }
        public static int GetUserId(string Url, string UserName)
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            int memberId = (new JavaScriptSerializer()).Deserialize<int>(webClient.DownloadString(Url + "/GetUserId?memberName=" + UserName));
            return memberId;

        }
        public static int? GetMeetingId(string Url, int committeeId,string createdAt)
        {
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            int? meetingId = (new JavaScriptSerializer()).Deserialize<int?>(webClient.DownloadString(Url + "/GetMeetingId?committeeId=" + committeeId+ "&createdAt="+createdAt));
            return meetingId;

        }
        public static void PostCommitteeMembersUpdate(string url, string inputJson)
        {

            string apiUrlPostCommitteeMembers = url;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            client.UploadString(apiUrlPostCommitteeMembers + "/PostCommitteesMember", inputJson);

        }

    }
}