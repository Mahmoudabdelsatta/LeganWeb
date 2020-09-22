using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Committee.Controller
{
    public static class SMS
    {
        public static void SendSms(string text, string phone)
        {

            SMSV2.ApplicantMessagesServiceSoapClient service = new SMSV2.ApplicantMessagesServiceSoapClient(SMSV2.ApplicantMessagesServiceSoapClient.EndpointConfiguration.ApplicantMessagesServiceSoap);
            service.SendVerificationCodeFortawteenAsync(phone, text);


        }
    }
}