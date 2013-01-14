using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Weavver.Vendors.FreeSWITCH
{
     public class FreeSwitchEventSocket
     {
          public List<FreeSwitchConnection> Connections = new List<FreeSwitchConnection>();
          public Socket ServerSocket;
//-------------------------------------------------------------------------------------------
          public void ListenforEventSocket()
          {
               ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 5000));
               ServerSocket.Listen(1);
               ServerSocket.BeginAccept(new AsyncCallback(OnClientConnect), ServerSocket);
          }
//-------------------------------------------------------------------------------------------
          public void OnClientConnect(IAsyncResult asyn)
          {
               Socket server = (Socket) asyn.AsyncState;
               Socket client = server.EndAccept(asyn);
               //QueueRead(client);

               FreeSwitchConnection newConnnection = new FreeSwitchConnection(client);
               Connections.Add(newConnnection);

               //Send(client, "connect\n\n");
               //Send(freeSwitchSocket, "event plain ALL\n\n");
          }
//-------------------------------------------------------------------------------------------
     }
}
