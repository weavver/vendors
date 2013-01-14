using System;

namespace TitaniumSoft.Voip.Asterisk
{
	public class SipAccount
	{
//-------------------------------------------------------------------------------------------
          //private string disallow;
          //private string allow;

          private enum    TUserAccess  {uaPermit, uaDeny, uaMask, uaNone}

          private string sectionname        = "";
          private string accountcode    = "";
          private string amaflags       = "";
          private string callgroup      = "";
          private bool   canreinvite    = false;
          private string context       = "";
          private string defaultip      = "";
          private string dtmfmode       = "";
          private string fromuser       = "";
          private string fromdomain     = "";
          private string host           = "";
          private string incominglimit  = "";
          private string outgoinglimit  = "";
          private string insecure       = "";
          private string language       = "";
          private string mailbox        = "";
          private string md5secret      = "";
          private bool   nat            = false;
          //private TUserAccess useraccess  ; // uaPermit, uaDeny, uaMask
          private string pickupgroup    = "";
          private string port           = "";
          private bool   qualify        = false;
          private string restrictcid    = "";
          private string rtptimeout     = "";
          private string rtpholdtimeout = "";
          private string secret         = "";
          private string type           = "";
          private string username       = "";
          private string useraccess2    = ""; //????allow - disallow
          private string musiconhold    = "";

//-------------------------------------------------------------------------------------------
          public string SectionName
          {
               get
               {
                    return sectionname;
               }
               set
               {
                    sectionname = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string AccountCode
          {
               get
               {
                    return accountcode;
               }
               set
               {
                    accountcode = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string AMAFlags
          {
               get
               {
                    return amaflags;
               }
               set
               {
                    amaflags = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string CallGroup
          {
               get
               {
                    return callgroup;
               }
               set
               {
                    callgroup = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public bool CanReinvite
          {
               get
               {
                    return canreinvite;
               }
               set
               {
                    canreinvite = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Context
          {
               get
               {
                    return context;
               }
               set
               {
                    context = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string DefaultIp
          {
               get
               {
                    return defaultip;
               }
               set
               {
                    defaultip = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string DTMFMode
          {
               get
               {
                    return dtmfmode;
               }
               set
               {
                    dtmfmode = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string FromUser
          {
               get
               {
                    return fromuser;
               }
               set
               {
                    fromuser = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string FromDomain
          {
               get
               {
                    return fromdomain;
               }
               set
               {
                    fromdomain = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Host
          {
               get
               {
                    return host;
               }
               set
               {
                    host = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string IncomingLimit
          {
               get
               {
                    return incominglimit;
               }
               set
               {
                    incominglimit = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string OutgoingLimit
          {
               get
               {
                    return outgoinglimit;
               }
               set
               {
                    outgoinglimit = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Insecure
          {
               get
               {
                    return insecure;
               }
               set
               {
                    insecure = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Language
          {
               get
               {
                    return language;
               }
               set
               {
                    language = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string MailBox
          {
               get
               {
                    return mailbox;
               }
               set
               {
                    mailbox = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Md5Secret
          {
               get
               {
                    return md5secret;
               }
               set
               {
                    md5secret = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public bool Nat
          {
               get
               {
                    return nat;
               }
               set
               {
                    nat = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string PickupGroup
          {
               get
               {
                    return pickupgroup;
               }
               set
               {
                    pickupgroup = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Port
          {
               get
               {
                    return port;
               }
               set
               {
                    port = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public bool Qualify
          {
               get
               {
                    return qualify;
               }
               set
               {
                    qualify = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string RestrictCId
          {
               get
               {
                    return restrictcid;
               }
               set
               {
                    restrictcid = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string RtpTimeout
          {
               get
               {
                    return rtptimeout;
               }
               set
               {
                    rtptimeout = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string RtpHoldTimeout
          {
               get
               {
                    return rtpholdtimeout;
               }
               set
               {
                    rtpholdtimeout = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Secret
          {
               get
               {
                    return secret;
               }
               set
               {
                    secret = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Type
          {
               get
               {
                    return type;
               }
               set
               {
                    type = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string UserName
          {
               get
               {
                    return username;
               }
               set
               {
                    username = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string UserAccess2
          {
               get
               {
                    return useraccess2;
               }
               set
               {
                    useraccess2 = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string MusicOnHold
          {
               get
               {
                    return musiconhold;
               }
               set
               {
                    musiconhold = value;
               }
          }
//-------------------------------------------------------------------------------------------
 
     }
 }    
