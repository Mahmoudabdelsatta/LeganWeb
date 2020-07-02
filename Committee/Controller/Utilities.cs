using Committee.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Committee.Controller;
using System.IO;

namespace Committee
{
    public static class Utilities
    {
        #region Proberties

       
        
        public static string BASE_URL
        {
            get { return Utilities.GetAppSettings("BASE_URL"); }
        }
        public static string LogError_Path
        {
            get { return Utilities.GetAppSettings("LogError_Path"); }
        }
        public static string Chat_Port
        {
            get { return Utilities.GetAppSettings("Chat_Port"); }
        }
        public static string MailFrom
        {
            get { return Utilities.GetAppSettings("mailFrom"); }
        }

        public static string MailSubjectToMembers
        {
            get { return Utilities.GetAppSettings("mailSubjectToMembers"); }
        }
        public static string MailSubject
        {
            get { return Utilities.GetAppSettings("mailSubject"); }
        }
        public static string SmtpServer
        {
            get { return Utilities.GetAppSettings("smtpServer"); }
        }
        public static string SmtpPort
        {
            get { return Utilities.GetAppSettings("smtpPort"); }
        }
        public static string MailTo
        {
            get { return Utilities.GetAppSettings("mailTo"); }
        }
        public static string OnHoldMailBody
        {
            get { return Utilities.GetAppSettings("OnHoldMailBody"); }
        }

        public static string PendingMailBody
        {
            get { return Utilities.GetAppSettings("PendingMailBody"); }
        }
        public static string ScheduledMailBody
        {
            get { return Utilities.GetAppSettings("ScheduledMailBody"); }
        }
        public static string RescheduledMailBody
        {
            get { return Utilities.GetAppSettings("RescheduledMailBody"); }
        }
        public static string SuspendedMailBody
        {
            get { return Utilities.GetAppSettings("SuspendedMailBody"); }
        }

        public static string MembersMailBody
        {
            get { return Utilities.GetAppSettings("MembersMailBody"); }
        }

        public static string UserName
        {
            get { return Utilities.GetAppSettings("userName"); }
        }
        public static string Password
        {
            get { return Utilities.GetAppSettings("password"); }
        }

        #endregion
        //SendMailToOnePerson(MailFrom, "HIT@57357.org", MailSubject, MembersMailBody, SmtpServer, SmtpPort, UserName, Password);

        public static int SendAlert(int CommiteeId, List<Committee.Models.User> Users,string text)
        {


            foreach (var user in Users)
            {
                try
                {


                    string apiUrlFcm = BASE_URL+"/api/Fcm";
                    WebClient clienfcm = new WebClient();
                    clienfcm.Headers["Content-type"] = "application/json";
                    clienfcm.Encoding = Encoding.UTF8;
                    object UserFcmo = new
                    {
                        Action_Id1 = CommiteeId,
                        Action_Id2 = "",
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
                        Action_Type = "type_new_notification",
                        Body = text,
                        Title = "اشعار"
                    };
                    string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                    clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                    string apiUrlAlert = Utilities.BASE_URL + "/api/Committees";
                    WebClient client4 = new WebClient();

                    client4.Headers["Content-type"] = "application/json";
                    client4.Encoding = Encoding.UTF8;

                    Alert alert = new Alert()
                    {
                        Action_Id1 = CommiteeId,
                        Action_Id2 = null,
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
                        Action_Type = 6,
                        Message = text,
                        Title = "اشعار"


                    };
                    string inputAlert = (new JavaScriptSerializer()).Serialize(alert);
                    client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);

                }
                catch (Exception ex)
                {

                    return 0;
                }
            }
            return 1;
        }
        public static int SendChatAlert(int CommiteeId)
        {
            List<User> users = WebApiConsume.ShowCommitteeMembersForChat(CommiteeId);

            foreach (var user in users)
            {
                try
                {


                    string apiUrlFcm = BASE_URL+"/api/Fcm";
                    WebClient clienfcm = new WebClient();
                    clienfcm.Headers["Content-type"] = "application/json";
                    clienfcm.Encoding = Encoding.UTF8;
                    object UserFcmo = new
                    {
                        Action_Id1 = CommiteeId,
                        Action_Id2 = "",
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
                        Body = "رسالة جديدة",
                        Title = "غرفة النقاش",
                        Action_Type= "type_chat_notification"


                    };
                    string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                    clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                    string apiUrlAlert = Utilities.BASE_URL + "/api/Committees";
                    WebClient client4 = new WebClient();

                    client4.Headers["Content-type"] = "application/json";
                    client4.Encoding = Encoding.UTF8;

                    Alert alert = new Alert()
                    {
                        Action_Id1 = CommiteeId,
                        Action_Id2 = null,
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
                        Action_Type = 7,
                        Message = "رسالة جديدة",
                        Title = "غرفة النقاش",



                    };
                    string inputAlert = (new JavaScriptSerializer()).Serialize(alert);
                    client4.UploadString(apiUrlAlert + "/PostAlert", inputAlert);

                }
                catch (Exception ex)
                {

                    return 0;
                }
            }
            return 1;
        }
        public static string GetAppSettings(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                return string.Empty;

            }
        }
        public static void SendMail(List<Committee.Models.User> Users,string message)
        {
            try
            {
                MailMessage eMail = new MailMessage();
                SmtpClient smtp = new SmtpClient(SmtpServer);
                eMail.From = new MailAddress(MailFrom);
                eMail.Subject = MailSubjectToMembers;

                eMail.Body = message;
                eMail.IsBodyHtml = true;
                smtp.Port = Convert.ToInt32(SmtpPort);
                smtp.Credentials = new NetworkCredential(UserName, Password);
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = false;
                MailAddressCollection toMailAddressCollection = new MailAddressCollection();
                foreach (var user in Users)
                {
                    if (user.UserEmailId!=""||user.UserEmailId!=null)
                    {
                        eMail.To.Add(user.UserEmailId);
                       smtp.Send(eMail);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in sending mail");
            }
        }
        public static void SendMailToOnePerson(string mailTo, string mailSubject, string mailbodymessage)
        {
            try
            {


                MailAddressCollection toMailAddressCollection = new MailAddressCollection();

                #region MailBody
                #endregion


                MailMessage eMail = new MailMessage();
                SmtpClient smtp = new SmtpClient(SmtpServer);
                eMail.From = new MailAddress(MailFrom);
                eMail.To.Add(mailTo);
                eMail.Subject = MailSubjectToMembers;

                eMail.Body = mailbodymessage;
                eMail.IsBodyHtml = true;
                smtp.Port = Convert.ToInt32(SmtpPort);
                smtp.Credentials = new NetworkCredential(UserName, Password);
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = false;
                smtp.Send(eMail);
                Console.WriteLine(" email sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in sending mail");
            }
        }
        public static void LogError(Exception ex)
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
        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            System.Globalization.DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;

                default:
                    return "";
            }

            /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
            DTFormat.ShortDatePattern = "d";
            return (DateConv.Date.ToString("yyyy/MM/dd", DTFormat));
        }

        public static ChatMessage SaveChatMessage(ChatMessage chatMessage)
        {
            string apiUrl2 = Utilities.BASE_URL + "/api/Users";
            WebClient client2 = new WebClient();
            client2.Headers["Content-type"] = "application/json";
            client2.Encoding = Encoding.UTF8;
            Committee.Models.ChatMessage cm = new Models.ChatMessage()
            {

                UserId = chatMessage.UserId,
                UserName = chatMessage.UserName,
                Type = chatMessage.Type,
                Message = chatMessage.Message,
                CommitteeId = chatMessage.CommitteeId,
                CreatedAt = chatMessage.CreatedAt,
                Extra=chatMessage.Extra

            };



            string inputJson = (new JavaScriptSerializer()).Serialize(cm);


            string json = client2.UploadString(apiUrl2 + "/PostUserChatMessage", inputJson);
            ChatMessage chat = (new JavaScriptSerializer()).Deserialize<ChatMessage>(json);
            return chat;

        }
    }
}