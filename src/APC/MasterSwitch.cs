using System;
using System.IO;
using System.Net;


namespace Weavver.Vendors.APC
{
//--------------------------------------------------------------------------------------------
     public enum PowerAction
     {
          ImmediateReboot,
          DelayedReboot,
          On,
          Off
     }
//--------------------------------------------------------------------------------------------
     public enum APCModel
     {
          AP9211, // supported
          AP7900, // not supported
          AP7901, // not supported
          AP7930 // not supported
     }
//--------------------------------------------------------------------------------------------
     public class MasterSwitch
     {
          public string Host { get; set; }
          public string Username { get; set; }
          public string Password { get; set; }
//--------------------------------------------------------------------------------------------
          public bool RestartAllPorts(PowerAction action)
          {
               string immediateRebootAll = "HX=hr&HX2=hr&C2=733839f5b26682cabd0fbafc5d7c7f18&master_ctrl=4";
               return PostData("admin\r\nivycrest\r\n1\r\n1\r\n1\r\n3\r\nYES\r\n");
          }
//--------------------------------------------------------------------------------------------
          public bool RestartPort(int port, PowerAction action)
          {
               //if (port < 1 || port > 8)
               //{
               //     throw new Exception("Invalid port range.");
               //}

               //string immediateRebootPort1  = "HX=hr&HX2=hr&OutCtl=68000000&OutCtl=c9000000&OutCtl=2d010000&OutCtl=91010000&OutCtl=f5010000&OutCtl=59020000&OutCtl=bd020000&OutCtl=21030000";
               //string immediateRebootPort2  = "HX=hr&HX2=hr&OutCtl=65000000&OutCtl=cc000000&OutCtl=2d010000&OutCtl=91010000&OutCtl=f5010000&OutCtl=59020000&OutCtl=bd020000&OutCtl=21030000";
               //switch (action)
               //{
               //     case PowerAction.ImmediateReboot:
               //          return PostData(immediateRebootPort1);

               //     case PowerAction.DelayedReboot:
               //          return PostData(immediateRebootPort2);

               //     default:
               //          throw new Exception("The action you chose has not been impletmented.");
               //}

               return PostData("admin\r\nivycrest\r\n1\r\n" + port.ToString() + "\r\n1\r\n3\r\nYES\r\n");
               return false;
          }
//--------------------------------------------------------------------------------------------
          private bool PostData(string postData)
          {
               //string url = "http://" + Host + "/Forms/outlets2";

               //CredentialCache myCache = new CredentialCache();
               //NetworkCredential netCredential = new NetworkCredential(Username, Password);
               //myCache.Add(new Uri(url), "Basic", netCredential);
               //myCache.Add(new Uri("http://" + Host + "/outlets.htm"), "Basic", netCredential);

               //System.Net.ServicePointManager.Expect100Continue = false;

               //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
               //request.Method = "POST";
               //request.ContentType = "application/x-www-form-urlencoded";
               //request.Credentials = new NetworkCredential(Username, Password);
               //request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
               //request.ContentLength = postData.Length;
               //request.Referer = "http://" + Host + "/outlets.htm";
               //request.Timeout = 3000;
               //request.AllowAutoRedirect = false;
               //request.KeepAlive = true;
               //request.Headers.Add("Authorization", "Basic d2VhdnZlcjppdnljcmVzdA==");

               //Stream newStream = request.GetRequestStream();
               //StreamWriter stOut = new StreamWriter(newStream, System.Text.Encoding.ASCII);
               //stOut.Write(postData);
               //stOut.Close();

               //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               //Stream dataStream = response.GetResponseStream();
               //StreamReader reader = new StreamReader(dataStream);
               //string responseFromServer = reader.ReadToEnd();
               //reader.Close();
               //dataStream.Close();
               //response.Close();

               //if (response.StatusCode == HttpStatusCode.RedirectMethod)
               //{
               //     return true;
               //}

               System.Net.Sockets.TcpClient tclient = new System.Net.Sockets.TcpClient();
               tclient.Connect(Host, 23);
               // Translate the passed message into ASCII and store it as a Byte array.
               Byte[] data = System.Text.Encoding.ASCII.GetBytes(postData);         
               System.Net.Sockets.NetworkStream stream = tclient.GetStream();
               stream.Write(data, 0, data.Length);

               byte[] bytes = new byte[tclient.ReceiveBufferSize];
               // Read can return anything from 0 to numBytesToRead. 
               // This method blocks until at least one byte is read.
               stream.Read(bytes, 0, (int) tclient.ReceiveBufferSize);

               // Returns the data received from the host to the console.
               string returndata = System.Text.Encoding.ASCII.GetString (bytes);

               tclient.Close();

               if (returndata.Contains("Command successfully issued"))
                    return true;
               else
                    return false;
          }
//--------------------------------------------------------------------------------------------
     }
}
