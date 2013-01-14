using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

namespace TitaniumSoft.Voip
{
	public class CiscoPhone
	{
		private		string	address		= string.Empty;
		Socket				connection	= null;

		public CiscoPhone()
		{
		}

		public void Connect(string address)
		{
			this.address			= address;

			IPEndPoint	endpoint	= new IPEndPoint(IPAddress.Parse(address), 23);
						connection	= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				connection.Connect(endpoint);
			}
			catch
			{
			}

			
			byte[] RecvBytes;

			int		ret		= 0;

			string	data	= "";
			string	pData	= "";
			while(connection.Connected)
			{	
				if (connection.Available > 0)
				{
					RecvBytes	= new byte[connection.Available];
					ret			= connection.Receive(RecvBytes, 0, RecvBytes.Length, SocketFlags.None);
					data		= data + Encoding.ASCII.GetString(RecvBytes).Substring(0, ret);

					while (data.IndexOf(Environment.NewLine) > -1 || data.EndsWith("Password :"))
					{
						pData		= Regex.Split(data, Environment.NewLine)[0].ToString();
						HandleData(pData);
						data		= data.Substring((data.IndexOf(Environment.NewLine) + 2));
						if (data.EndsWith("Password : "))
						{
							data = "";
						}
					}
				}
			}
			connection.Close();
		}

		private void HandleData(string line)
		{
			if (line.EndsWith("Password :"))
			{
				Send("1", connection);
				Send("2", connection);
				Send("3", connection);
				Send("4", connection);
				Send("\r", connection);
				//Send("1234\r", connection);
			}
		}

		/// <summary>
		/// This method implies "Disconnect()" because the phone will automatically close the session.
		/// </summary>
		public void RebootPhone()
		{
            
		}

		#region Send
		private void Send(string line, Socket socket)
		{
			try
			{
				if (socket.Connected)
				{
					Byte[] bSend = System.Text.Encoding.ASCII.GetBytes(line);
					socket.Send(bSend, 0, bSend.Length, SocketFlags.None);
					bSend = null;
				}
			}
			catch {}
		}
		#endregion
	}
}
