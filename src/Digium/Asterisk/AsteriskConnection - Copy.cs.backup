using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Weavver.Vendors.Digium.Asterisk
{
     public delegate  void PeerStatusChangeHandler(object AsteriskConnection, PeerStatusChangeEventArgs eventargs);
     public delegate  void CallHangupHandler(object AsteriskConnection, CallHangupEventArgs eventargs);
     public delegate  void CallStartedHandler(object AsteriskConnection, CallStartedEventArgs eventargs);
     public delegate  void CallStateChangeHandler(object AsteriskConnection, CallStateChangeEventArgs eventargs);
     public delegate  void CallNextPriorityHandler(object AsteriskConnection, CallNextPriorityEventArgs eventargs);

     public class AsteriskConnection : Weavver.Sockets.SocketClient
     {
          public  CallHangupHandler        OnCallHangup          = null;
          public  CallStartedHandler       OnCallStarted         = null;
          public  CallStateChangeHandler   OnCallStateChange     = null;
          public  CallNextPriorityHandler  OnCallNextPriority    = null;
          public  PeerStatusChangeHandler  OnPeerStatusChange    = null;
          public  Socket                   connection            = null;
          private string                   address               = string.Empty;
          private string                   username              = string.Empty;
          private string                   password              = string.Empty;
          private bool                     Authenticated         = false;
          public ManualResetEvent          waitForAuthenticated = new ManualResetEvent(false);
//--------------------------------------------------------------------------------------------
          public AsteriskConnection()
          {
          }
//--------------------------------------------------------------------------------------------
          public void Connect(string username, string password, string address)
          {
               this.username = username;
               this.password = password;
               this.address = address;
               Connect(username, password, address, true);
               waitForAuthenticated.WaitOne(TimeSpan.FromSeconds(4));
          }
//--------------------------------------------------------------------------------------------
          private void Connect(string username, string password, string address, bool sendevents)
          {
//               string connectionurl = String.Format("asterisk://{0}:{1}/{2}", username, password, address);
//               Connect(connectionurl, sendevents);
//          }
////--------------------------------------------------------------------------------------------
//          public void Connect(string connectionurl, bool sendevents)
//          {
//               if (!UriParser.IsKnownScheme("asterisk"))
//                    UriParser.Register(new GenericUriParser(GenericUriParserOptions.Default), "asterisk", 5038);
//               Uri uri                 = new Uri(connectionurl);
//               username                = uri.UserInfo.Substring(0, uri.UserInfo.IndexOf(":"));
//               password                = uri.UserInfo.Substring(uri.UserInfo.IndexOf(":") + 1);

               
               connection	         = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               IPHostEntry iphe        = Dns.GetHostEntry(address);
               //endpoint                = new IPEndPoint(iphe.AddressList[0], 5038);
               base.Connect(iphe.AddressList[0], 5038);
          }
//--------------------------------------------------------------------------------------------
          string buffer = "";
          public override void HandleLine(string line)
          {
               buffer += line;
               if (buffer == "Asterisk Call Manager/1.0" ||
                   buffer == "Asterisk Call Manager/1.1")
                    buffer += "\r\n";
               while (buffer.IndexOf(Environment.NewLine) > -1 || buffer.EndsWith("Password :"))
               {
                    string pData = Regex.Split(buffer, Environment.NewLine)[0].ToString();
                    Console.WriteLine("Receiving:\t" + pData);
                    if (Authenticated)
                    {
                         HandleAuthenticatedState(pData);
                    }
                    else
                    {
                         HandleAuthenticateState(pData);
                    }
                    buffer = buffer.Substring((buffer.IndexOf(Environment.NewLine) + 2));
               }
          }
//--------------------------------------------------------------------------------------------
          private void HandleAuthenticateState(string line)
          {
               if (line == "Asterisk Call Manager/1.0" || line == "Asterisk Call Manager/1.1")
               {
                    string login	 = "Action: Login\r\n";
                    login			+= "Username: " + username + "\r\n";
                    login			+= "Secret: " + password + "\r\n\r\n";
                    Send(login, connection);
               }

               if (line == "Response: Success")
               {
                    Authenticated = true;
                    waitForAuthenticated.Set();
               }
          }
//--------------------------------------------------------------------------------------------
          string packet = "";
          private void HandleAuthenticatedState(string line)
          {
               if (line == "")
               {
                    HandlePacket();
               }
               else
               {
                    packet += line + "\r\n";
               }
          }
//--------------------------------------------------------------------------------------------
          private void HandlePacket()
          {
               string eventtype = GetValue(packet, "Event");
               switch (eventtype)
               {
                    case "Newchannel":
                         ProcessCall_Started(packet);
                         break;

                    case "Newexten":
                         ProcessCall_NextPriority(packet);
                         break;

                    case "Newstate":
                         ProcessCall_StateChange(packet);
                         break;

                    case "Hangup":
                         ProcessCall_Hangup(packet);
                         break;

                    case "PeerStatus":
                         ProcessPeer_StatusChange(packet);
                         break;
               }
               packet = "";
          }
//--------------------------------------------------------------------------------------------
          public void ProcessPeer_StatusChange(string packet)
          {
               if (OnPeerStatusChange != null)
               {
                    PeerStatusChangeEventArgs args = new PeerStatusChangeEventArgs(GetValue(packet, "Peer"), GetValue(packet, "PeerStatus"), GetValue(packet, "Time"));
                    OnPeerStatusChange(this, args);
               }
          }
//--------------------------------------------------------------------------------------------
          public void ProcessCall_Hangup(string packet)
          {
               if (OnCallHangup != null)
               {
                    CallHangupEventArgs args = new CallHangupEventArgs(GetValue(packet, "Channel"), GetValue(packet, "Uniqueid"), GetValue(packet, "Cause"));
                    OnCallHangup(this, args);
               }
          }
//--------------------------------------------------------------------------------------------
          public void ProcessCall_StateChange(string packet)
          {
               if (OnCallStateChange != null)
               {
                    CallStateChangeEventArgs args = new CallStateChangeEventArgs(GetValue(packet, "Uniqueid"), GetValue(packet, "channel"), GetValue(packet, "State"), GetValue(packet, "Callerid"));
                    OnCallStateChange(this, args);
               }
          }
//--------------------------------------------------------------------------------------------
          public void ProcessCall_Started(string packet)
          {
               if (OnCallStarted != null)
               {
                    CallStartedEventArgs args = new CallStartedEventArgs(GetValue(packet, "Uniqueid"), GetValue(packet, "channel"), GetValue(packet, "State"), GetValue(packet, "Callerid"));
                    OnCallStarted(this, args);
               }
          }
//--------------------------------------------------------------------------------------------
          public void ProcessCall_NextPriority(string packet)
          {
               if (OnCallNextPriority != null)
               {
                    CallNextPriorityEventArgs args = new CallNextPriorityEventArgs(GetValue(packet, "Uniqueid"), GetValue(packet, "Channel"), GetValue(packet, "Extension"), GetValue(packet, "Priority"), GetValue(packet, "Context"), GetValue(packet, "Application"), GetValue(packet, "AppData"));
                    OnCallNextPriority(this, args);
               }
          }
//--------------------------------------------------------------------------------------------
          public string GetValue(string packet, string propertyname)
          {
               string[] lines = Regex.Split(packet, "\r\n");
               for (int i = 0; i < lines.Length; i++)
               {
                    if (lines[i].StartsWith(propertyname))
                    {
                         return lines[i].Substring(propertyname.Length + 2);
                    }
               }
               return "";
          }
//--------------------------------------------------------------------------------------------
          public void SendPacket(string packet)
          {
               Send(packet, connection);
          }
//--------------------------------------------------------------------------------------------
          public void Disconnect()
          {
               Send("Action: Logoff\r\n\r\n", connection);
               connection.Close();
          }
//--------------------------------------------------------------------------------------------
          #region Send
		private void Send(string line, Socket socket)
		{
               Console.WriteLine("Sending:\t" + line);
			try
			{
				if (base.socket.Connected)
				{
					Byte[] bSend = System.Text.Encoding.ASCII.GetBytes(line);
					base.socket.Send(bSend, 0, bSend.Length, SocketFlags.None);
					bSend = null;
				}
			}
			catch {}
		}
		#endregion
//--------------------------------------------------------------------------------------------
	}

}