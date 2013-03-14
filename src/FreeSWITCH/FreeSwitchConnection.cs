using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace Weavver.Vendors.FreeSWITCH
{
//-------------------------------------------------------------------------------------------
     public class FreeSwitchConnection
     {
          public Socket workSocket = null;
          public const int BUFFER_SIZE = 1024;
          public byte[] buffer = new byte[BUFFER_SIZE];
          public StringBuilder IncomingData = new StringBuilder(1000);
          public Queue<FreeSwitchPacket> Packets = new Queue<FreeSwitchPacket>();
          public string portStr = "5000";
          public string FreeSwitchPassword;
          public StreamWriter logFile;
//-------------------------------------------------------------------------------------------
          public FreeSwitchConnection()
          {
          }
//-------------------------------------------------------------------------------------------
          public FreeSwitchConnection(Socket client)
          {
               workSocket = client;
               QueueRead();
          }
//-------------------------------------------------------------------------------------------
          public void ConnectFreeSwitch(string host, int port, string password)
          {
               logFile = new StreamWriter("C:\\freeswitch.log", true);

               FreeSwitchPassword = password;
               workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               workSocket.Connect(host, port);
               if (!workSocket.Connected)
                    throw new Exception("Could not connect to FreeSwitch.");
               QueueRead();
          }
//-------------------------------------------------------------------------------------------
          private void QueueRead()
          {
               Send("auth " + FreeSwitchPassword + "\n\n");
               Send("event plain ALL\n\n");
               workSocket.BeginReceive(buffer, 0, BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), null);
          }
//-------------------------------------------------------------------------------------------
          public void Read_Callback(IAsyncResult ar)
          {
               if (workSocket.Connected)
               {
                    int read = workSocket.EndReceive(ar);
                    if (read > 0)
                    {
                         string newData = Encoding.ASCII.GetString(buffer, 0, read);
                         logFile.Write(newData.Replace("\n", "\r\n"));
                         IncomingData.Append(newData);
                         while (IncomingData.ToString().IndexOf("\n\n") > -1)
                         {
                              string packet = Regex.Split(IncomingData.ToString(), "\n\n")[0];
                              FreeSwitchPacket fsPacket = new FreeSwitchPacket(packet);
                              Packets.Enqueue(fsPacket);
                              IncomingData.Remove(0, IncomingData.ToString().IndexOf("\n\n") + 2);
                         }
                         workSocket.BeginReceive(buffer, 0, BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), workSocket);
                    }
                    else
                    {
                         // socket closed
                    }
               }
          }
//-------------------------------------------------------------------------------------------
          public void ExpectRinging()
          {
               Expect("Answer-State: ringing");
          }
//-------------------------------------------------------------------------------------------
          public void Answer()
          {
               string packet = "sendmsg\n" +
                    "call-command: execute\n" +
                    "execute-app-name: answer\n\n";

               Send(packet);
          }
//-------------------------------------------------------------------------------------------
          public void SendDTMF(string uuid, string digits)
          {
               string packet = "SendMsg " + uuid + "\n" +
                    "call-command: execute\n" +
                    "execute-app-name: send_dtmf\n" +
                    "execute-app-arg: " + digits + "\n\n";

               Send(workSocket, packet);
          }
//-------------------------------------------------------------------------------------------
          public void Hangup(string uuid)
          {
               string packet = "SendMsg " + uuid + "\n" +
                    "call-command: execute\n" +
                    "execute-app-name: hangup\n\n";

               Send(packet);
          }
//-------------------------------------------------------------------------------------------
          public FreeSwitchPacket Expect(string packet)
          {
               Stopwatch swTimeOut = new Stopwatch();
               swTimeOut.Start();
               while (swTimeOut.ElapsedMilliseconds < 30000) // 30 seconds
               {
                    if (Packets.Count > 0)
                    {
                         FreeSwitchPacket fsPacket = Packets.Dequeue();
                         if (fsPacket.Body.Contains(packet))
                         {
                              return fsPacket;
                         }
                    }
                    Thread.Sleep(10);
               }
               throw new Exception("The expected message from FreeSwitch could not be found: " + packet);
               return null;
          }
//-------------------------------------------------------------------------------------------
          public void Send(string packet)
          {
               Send(workSocket, packet);
          }
//-------------------------------------------------------------------------------------------
          private void Send(Socket socket, string data)
          {
               if (socket.Connected)
               {
                    Byte[] bSend = System.Text.Encoding.ASCII.GetBytes(data);
                    socket.Send(bSend);
                    bSend = null;
                    data = null;
               }
          }
//-------------------------------------------------------------------------------------------
          public void Disconnect()
          {
               if (workSocket != null)
                    workSocket.Close();

               if (logFile != null)
                    logFile.Dispose();
          }
//-------------------------------------------------------------------------------------------
     }
}