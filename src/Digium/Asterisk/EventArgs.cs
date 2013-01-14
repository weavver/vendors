using System;

namespace Weavver.Vendors.Digium.Asterisk
{
//--------------------------------------------------------------------------------------------
     public class CallStartedEventArgs : EventArgs
     {
          public CallStartedEventArgs(string uniqueid, string channel, string state, string callerid)
          {
               this.uniqueid  = uniqueid;
               this.channel   = channel;
               this.state     = state;
               this.callerid  = callerid;
          }    
          public readonly string uniqueid;
          public readonly string channel;
          public readonly string state;
          public readonly string callerid;
     }
     //CallStateChangeEventArgs
//--------------------------------------------------------------------------------------------
     public class CallStateChangeEventArgs : EventArgs
     {
          public CallStateChangeEventArgs(string uniqueid, string channel, string state, string callerid)
          {
               this.uniqueid  = uniqueid;
               this.channel   = channel;
               this.state     = state;
               this.callerid  = callerid;
          }    
          public readonly string uniqueid;
          public readonly string channel;
          public readonly string state;
          public readonly string callerid;
     }
//--------------------------------------------------------------------------------------------
     public class PeerStatusChangeEventArgs : EventArgs
     {
          public PeerStatusChangeEventArgs(string peer, string peerstatus, string time)
          {
               this.Peer       = peer;
               this.PeerStatus = peerstatus;
               if (time == "")
               {
                    this.Time = "-1";
               }
               else
               {
                    this.Time = time;
               }
          }
          public readonly string Peer;
          public readonly string PeerStatus;
          public readonly string Time;
     }
//--------------------------------------------------------------------------------------------
     public class CallHangupEventArgs : EventArgs
     {
          public CallHangupEventArgs(string channel, string uniqueid, string cause)
          {
               this.Channel  = channel;
               this.UniqueID = uniqueid;
               this.Cause    = cause;
          }
          public readonly string Channel;
          public readonly string UniqueID;
          public readonly string Cause;
     }
//--------------------------------------------------------------------------------------------
     public class CallNextPriorityEventArgs : EventArgs
     {
          public CallNextPriorityEventArgs(string uniqueid, string channel, string extension, string priority, string context, string application, string appdata)
          {
               this.UniqueID       = uniqueid;
               this.Channel        = channel;
               this.Extension      = extension;
               this.Priority       = priority;
               this.Context        = context;
               this.Application    = application;
               this.AppData        = appdata;
          }
          public readonly string UniqueID;
          public readonly string Channel;
          public readonly string Extension;
          public readonly string Priority;
          public readonly string Context;
          public readonly string Application;
          public readonly string AppData;
     }
//--------------------------------------------------------------------------------------------
}