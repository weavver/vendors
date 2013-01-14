using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Weavver.Vendors.FreeSWITCH
{
	/// <summary>
	/// Calling this should return a path to the user's mail box. If the user is not authorized it should return "error".
	/// </summary>
     //public delegate string		dAuthorizePopUser	(PopClient popclient, string user,		string		pass);
     //public delegate void		dDebugOutput		(PopClient popclient, string message,	DebugType	debugtype);

	public class EventSocket
	{
          //public event			dAuthorizePopUser	AuthorizePopUser;
          //public event			dDebugOutput		DebugOutput;

		private string			servername			= "unknown.server";
		public	ArrayList		connections			= new ArrayList();
		private bool			listening			= false;
		private WaitCallback	_OnAccept			= null;
		private Socket			listenersocket		= null;
//-------------------------------------------------------------------------------------------
		public EventSocket()
		{
			_OnAccept = new WaitCallback(OnAccept);
		}
//-------------------------------------------------------------------------------------------
		public string ServerName
		{
			get
			{
				return servername;
			}
			set
			{
				servername = value;
			}
		}
//-------------------------------------------------------------------------------------------
		public bool Listening
		{
			get
			{
				return listening;
			}
		}
//-------------------------------------------------------------------------------------------
		public void Listen(IPEndPoint ipendpoint)
		{
			Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			listenersocket	= Listener;
			try
			{
				Listener.Bind(ipendpoint);
				Listener.Listen((int) SocketOptionName.MaxConnections);
				listening = true;
				//Weavver.Common.Common.Log("Listening..", "EventSocket is listening on: " + Listener.LocalEndPoint.ToString(), DebugType.Listen);
			}
			catch(Exception e)
			{
				// DebugOut(null, "Event failed to bind: " + e.Message, DebugType.Critical);
				return;
			}

			while (Listening && !Environment.HasShutdownStarted)
			{
				try 
				{
					ThreadPool.QueueUserWorkItem(_OnAccept, Listener.Accept());
				}
				catch 
				{
					Thread.Sleep(100);
				}
			}
			Listener.Shutdown(SocketShutdown.Both);
			Listener.Close();
			listening = false;
		}
//-------------------------------------------------------------------------------------------
		private void OnAccept(Object sock)
		{
			try
			{
				//PopClient client = new PopClient((Socket) sock, this);

                    
                    Socket pSock = (Socket)sock;
                    System.Console.WriteLine("Socket connected from " + pSock.RemoteEndPoint.ToString());
                    Send(pSock, "connect\n\n");
                    HandleConnection(pSock);
                    //connections.Add(client);
                    //client.HandleConnection();
                    //connections.Remove(client);

				sock	= null;
				//client	= null;
			}
			catch (Exception e)
			{
				//DebugOut(null, "Unknown error:\r\n\r\n" + e.Message, DebugType.Critical);
			}
		}
//-------------------------------------------------------------------------------------------
          public void HandleConnection(Socket pSock)
          {
               try
               {
                    Send(pSock, "hello!" + Environment.NewLine);

                    string sREP = pSock.RemoteEndPoint.ToString();

                    int ret;
                    decimal dTimeOut = DateTime.Now.ToFileTime();

                    string pData = "";
                    string strData = "";

                    byte[] RecvBytes;

                    string uuid = "";
                    while (pSock.Connected)
                    {
                         if (pSock.Available > 0)
                         {
                              RecvBytes = new byte[pSock.Available];
                              ret = pSock.Receive(RecvBytes, 0, RecvBytes.Length, SocketFlags.None);
                              strData = strData + System.Text.Encoding.ASCII.GetString(RecvBytes).Substring(0, ret);
                              while (strData.IndexOf("\n") > 0)
                              {
                                   string line = strData.Substring(0, strData.IndexOf("\n"));

                                   Console.WriteLine(line);

                                   strData = strData.Substring(strData.IndexOf("\n") + 1);

                                   if (line.StartsWith("Channel-Unique-ID"))
                                   {
                                        uuid = line.Substring(line.IndexOf(":") + 2);

                                        string answer = "sendmsg\n" +
                                             "call-command: execute\n" +
                                             "execute-app-name: answer\n\n";

                                        Send(pSock, answer);

                                        //for (int i = 0; i < 10; i++)
                                        {
                                             string sendmsg = "SendMsg" + uuid + "\n"
                                                  + "call-command: execute\n"
                                                  + "execute-app-name: playback\n"
                                                  + "execute-app-arg: tone_stream://%(2000,4000,440,480)\n\n";

                                             Send(pSock, sendmsg);

                                        }
                                   }
                                   Console.WriteLine("------------");
                              }
                         }
                              
                         string sendget = "SendMsg " + uuid + "\n"
                              + "call-command: execute\n"
                              + "execute-app-name: getDigits\n"
                              + "execute-app-arg: 3 # 3000\n\n";
                         //+ "execute-app-arg: #\n"
                         //+ "execute-app-arg: 3000\n\n";

                         Send(pSock, sendget);
                    }
               }
               catch { }
          }
//-------------------------------------------------------------------------------------------
		public void DebugOut(string message)
		{
               //if (DebugOutput != null)
               //     DebugOutput(popclient, message, debugtype);
		}
//-------------------------------------------------------------------------------------------
		public void StopListening()
		{
			listening = false;
			try
			{
				listenersocket.Close();
			}
			catch {}
		}
//-------------------------------------------------------------------------------------------
		public string AuthorizeUser(string user, string pass)
		{
               //if (AuthorizePopUser != null)
               //     return AuthorizePopUser(popclient, user, pass);
               //else
               return "Error";
		}
//-------------------------------------------------------------------------------------------
          private void Send(Socket pSock, string strSend)
          {
               try
               {
                    Byte[] bSend = System.Text.Encoding.ASCII.GetBytes(strSend);
                    pSock.Send(bSend, 0, bSend.Length, SocketFlags.None);
               }
               catch
               {
                    // Parent.DebugOut(this, "Error: Unable to send data", DebugType.Error);
               }
          }
//-------------------------------------------------------------------------------------------
	}
}