// Copyright: 2010 Weavver, Inc. 
// Author: Mitchel Constantin <mythicalbox@weavver.com>
// License: Public Domain (Limited to this file)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CookComputing.XmlRpc;

namespace Weavver.Vendors.ProcessOne
{
//-------------------------------------------------------------------------------------------
     public struct send_message_chat
     {
          public string from;
          public string to;
          public string body;
     }
//-------------------------------------------------------------------------------------------
     public struct status {}
//-------------------------------------------------------------------------------------------
     public class ejabberdRPC
     {
//-------------------------------------------------------------------------------------------
          [XmlRpcUrl("http://192.168.10.111:4560/RPC2")]
          public interface IStateName : IXmlRpcProxy
          {
               [XmlRpcMethod("send_message_chat")]
               object SendMessageChat(send_message_chat message);

               [XmlRpcMethod("status")]
               object Status(status s);
          }
//-------------------------------------------------------------------------------------------
          public CookComputing.XmlRpc.XmlRpcStruct SendMessageChat(send_message_chat message)
          {
               IStateName proxy = XmlRpcProxyGen.Create<IStateName>();
               proxy.KeepAlive = false;
               return (CookComputing.XmlRpc.XmlRpcStruct) proxy.SendMessageChat(message);
          }
//-------------------------------------------------------------------------------------------
          public CookComputing.XmlRpc.XmlRpcStruct Status(status status)
          {
               IStateName proxy = XmlRpcProxyGen.Create<IStateName>();
               proxy.KeepAlive = false;
               return (CookComputing.XmlRpc.XmlRpcStruct) proxy.Status(status);
          }
//-------------------------------------------------------------------------------------------
     }
}