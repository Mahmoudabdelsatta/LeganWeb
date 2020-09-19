using System;

namespace SMSlegan
{
    public class SMSGateway
    {

        public static void SendSms(string phone, string text)
        {

            SmsLL.smsSoapClient service = new SmsLL.smsSoapClient(SmsLL.smsSoapClient.EndpointConfiguration.smsSoap);
            service.StcsmsAsync(phone, "EMARARIYADH", text);

         
        }
    }
}
