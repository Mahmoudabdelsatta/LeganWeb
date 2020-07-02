using Committee.Models;
using Fleck;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;


namespace Committee.Controller
{
  public  class SocketForCommittee
    {
      public  static void OpenSocket()
        {
          
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:"+Utilities.Chat_Port);
            server.RestartAfterListenError = true;
            server.Start(socket =>
                {
                    socket.OnOpen = () =>
                        {
                            allSockets.Add(socket);
                        };
                    socket.OnClose = () =>
                        {
                            
                            allSockets.Remove(socket);
                       

                        };
                    socket.OnMessage = message =>
                        {
                            try
                            {

                                ChatMessage chatMessage = (new JavaScriptSerializer()).Deserialize<Committee.Models.ChatMessage>(message);

                                bool valid = WebApiConsume.CheckAccessToken(chatMessage.AccessToken);
                                if (valid)
                                {
                                   

                             ChatMessage  chat=  Utilities.SaveChatMessage(chatMessage);
                               string chatMessageString= (new JavaScriptSerializer()).Serialize(chat);
                             allSockets.ToList().ForEach(s => s.Send(chatMessageString));

                                    Utilities.SendChatAlert(chatMessage.CommitteeId);


                                }
                                else
                                {
                                  
                                    allSockets.ToList().ForEach(s => s.Send("invalid token"));

                                }
                            }
                            catch (Exception ex)
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
                          
                           


                        };
                });


            

        }
    }
}
