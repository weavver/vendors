using System;
using System.Collections.Generic;
using System.Text;

//using Mono.Data.SqliteClient;
//using SnapService;
//using Snap.Data

namespace Weavver.Vendors.Digium.Asterisk
{
     public class AsteriskPackets
     {
//--------------------------------------------------------------------------------------------
          public static string LoginPacket(string username, string password)
          {
               return String.Format("Action: Login\r\n" +
                      "Username: {0}\r\n" +
                      "Secret: {1}\r\n\r\n", username, password);
          }
//--------------------------------------------------------------------------------------------
          public static string OriginatePacket(string channel, string phonenumber, string context, string callerid)
          {
               string packet = "Action: Originate\r\n" +
                               "ActionID: Snap Call\r\n" +
                               "Async: yes\r\n" +
                               "Priority: 1\r\n";
               packet += String.Format("Channel: {0}\r\n",    channel);
               packet += String.Format("Timeout: {0}000\r\n", "20");
               //if (variable.Value != null && variable.Value != "")
               //{
               //     packet += String.Format("Variable: {0}\r\n", variable.Value);
               //}

               // HERE WE SHOULD TRY TO ENHANCE THE CID AS MUCH AS POSSIBLE
               //if (name == cidname.Value)
               //{
               //     Snap.Library.Contacts.ContactInfo ci = Statics.Contacts.GetCallerName(destination, name);
               //     name = ci.CallerName;
               //}
               packet += String.Format("Callerid: {0} <{1}>\r\n",       callerid, callerid);
               packet += String.Format("Exten: {0}\r\n",                phonenumber);
               packet += String.Format("Context: {0}\r\n\r\n",          context);
               return packet;
               //"Variable: customid=asdfasdf\r\n" + 
          }
//--------------------------------------------------------------------------------------------
          public static string BridgeAdd(string conferenceid, string channel)
          {
               //Statics.Remote.Asterisk.SetChannelVariable("BridgeID", conferenceid, channel);
               string packet  = "Action: Originate\r\n";
               packet        += "ActionID: Snap Call\r\n";
               packet        += "Async: yes\r\n";
               packet        += String.Format("Channel: {0}\r\n",         channel);
               packet        += String.Format("Priority: {0}\r\n",        "1");
               packet        += String.Format("Timeout: {0}000\r\n",      "30");
               packet        += String.Format("Callerid: {0} <{1}>\r\n",  "Conference Call", "snap-bridge");
               packet        += String.Format("Context: {0}\r\n\r\n",     "snap");
               packet        += String.Format("Exten: {0}\r\n",           "bridgeadd");
               //packet        += String.Format("Variable: {0}\r\n",        Statics.Remote.Address.ToString());
               return packet;
          }
//--------------------------------------------------------------------------------------------
          public static string RedirectChannel(string channel, string extrachannel, string context, string extension, string priority)
          {
               string packet = "Action: Redirect\r\n" 
                             + "Channel: " + channel + "\r\n";

               if (extrachannel != null)
                    packet += "ExtraChannel: " + extrachannel + "\r\n";

               packet += "Exten: " + extension + "\r\n"
               + "Context: " + context + "\r\n"
               + "Priority: " + priority + "\r\n\r\n";
               return packet;
          }
//--------------------------------------------------------------------------------------------
          public static string MailBoxCount(string mailbox)
          {
               return String.Format("Action: MailboxCount\r\n" + 
                                    "Mailbox: {0}\r\n\r\n", mailbox);
          }
//--------------------------------------------------------------------------------------------
     }
}