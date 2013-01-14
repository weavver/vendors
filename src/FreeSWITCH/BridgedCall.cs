using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Weavver.Connect
{
     public class BridgedCall
     {
          public string Phone1 = "";
          public string Phone2 = "";
          public DateTime ExpireAtUTC;
//-------------------------------------------------------------------------------------------
          public void Connect()
          {
               try
               {
                    string commandURL = "http://192.168.10.20:8080/webapi/originate?sofia/gateway/icall/{0}%20&bridge(sofia/gateway/icall/{1})";
                    commandURL = String.Format(commandURL, Phone1, Phone2);

                    WebClient wc = new WebClient();
                    wc.Credentials = new NetworkCredential("freeswitch", "works");
                    string x = wc.DownloadString(commandURL);
               }
               catch
               {
               }
          }
//-------------------------------------------------------------------------------------------
     }
}
