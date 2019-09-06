using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Committee
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile(@"C:\Users\mahmoud\source\repos\Committee\Committee\FireBase\legan-923c8-firebase-adminsdk-iekzq-071b49583d.json"),
            //});
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.GetApplicationDefault(),
            //});

        }
    }
}