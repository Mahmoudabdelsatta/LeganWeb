using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Committee
{
    public static class Utilities
    {
        #region Proberties
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

        public static int SendAlert(int CommiteeId, List<Committee.Models.User> Users)
        {


            foreach (var user in Users)
            {
                try
                {


                    string apiUrlFcm = "https://committeeapi20190806070934.azurewebsites.net/api/Fcm";
                    WebClient clienfcm = new WebClient();
                    clienfcm.Headers["Content-type"] = "application/json";
                    clienfcm.Encoding = Encoding.UTF8;
                    object UserFcmo = new
                    {

                        Action_id = CommiteeId,
                        Body = "اشعار بخصوص اجتماع",
                        Click_action = "type_new_meeting",
                        Title = "اضافة اجتماع",
                        Action2_id = ""

                    };
                    string inputFcm = (new JavaScriptSerializer()).Serialize(UserFcmo);
                    clienfcm.UploadString(apiUrlFcm + "/SendMessage?_to=" + user.FCMToken, inputFcm);
                   
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
        public static void SendMail(List<Committee.Models.User> Users)
        {
            try
            {
                MailMessage eMail = new MailMessage();
                SmtpClient smtp = new SmtpClient(SmtpServer);
                eMail.From = new MailAddress(MailFrom);
                eMail.Subject = MailSubjectToMembers;

                eMail.Body = MembersMailBody;
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
        public static void SendMailToOnePerson(string mailFrom, string mailTo, string mailSubject, string mailbodymessage, string mailServer, string smtpPort, string userName, string password)
        {
            try
            {


                MailAddressCollection toMailAddressCollection = new MailAddressCollection();

                #region MailBody
                #endregion


                MailMessage eMail = new MailMessage();
                SmtpClient smtp = new SmtpClient(mailServer);
                eMail.From = new MailAddress(mailFrom);
                eMail.To.Add(mailTo);
                eMail.Subject = MailSubjectToMembers;

                eMail.Body = mailbodymessage;
                eMail.IsBodyHtml = true;
                smtp.Port = Convert.ToInt32(smtpPort);
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
    }
}