using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace TitaniumSoft.Voip.Asterisk
{
     public class SipConfiguration
     {
//------------------------------------------------------------------------------------------
          #region Private Variables
          //register? => <username>@<sip client/peer id in sip.conf>/<extension> :Register with a SIP provider 
          //realm = my realm (Change
          //private string	     allow;
          //private string	     disallow;
          private SipAccountCollection users               = new SipAccountCollection();
          private bool              autocreatepeer      = true;
          private IPAddress         bindaddress         = IPAddress.None;
          private bool              canreinvite         = false;
          private string            context             = "default";
          private int               defaultexpirey      = 3600;
          private IPAddress         externip            = IPAddress.None;
          private string	           fromdomain          = "";
          private string            localnet            = "";
          private int               maxexpirey          = 3600;
          private bool              nat                 = false;
          private string            notifymimetype      = "";
          private bool              pedatic             = false;
          private int               port                = -1;
          private bool              srvlookup           = false;
          private string            tos                 = "";
          private bool              trustrpid           = false;
          private string            useragent           = "";
          private bool              videosupport        = false;
          #endregion
//------------------------------------------------------------------------------------------
          #region Public Variables
          public bool AutoCreatePeer
          {
               get
               {
                   return autocreatepeer;
               }
               set
               {
                    autocreatepeer = value;
               }
          }
//------------------------------------------------------------------------------------------
          public IPAddress BindAddress
          {
               get
               {
                    return bindaddress;
               }
               set
               {
                    bindaddress = value;
               }
          }
//------------------------------------------------------------------------------------------
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
//------------------------------------------------------------------------------------------
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
//------------------------------------------------------------------------------------------
          public int DefaultExpirey
          {
               get
               {
                    return defaultexpirey;
               }
               set
               {
                    defaultexpirey = value;
               }
          }
//------------------------------------------------------------------------------------------
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
//------------------------------------------------------------------------------------------
          public string LocalNet
          {
               get
               {
                    return localnet;
               }
               set
               {
                    localnet = value;
               }
          }
//------------------------------------------------------------------------------------------
          public int MaxExpirey
          {
               get
               {
                    return maxexpirey;
               }
               set
               {
                    maxexpirey = value;
               }
          }
//------------------------------------------------------------------------------------------
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
//------------------------------------------------------------------------------------------
          public string NotifyMIMEType
          {
               get
               {
                    return notifymimetype;
               }
               set
               {
                    notifymimetype = value;
               }
          }
//------------------------------------------------------------------------------------------
          public bool Pedatic
          {
               get
               {
                    return pedatic;
               }
               set
               {
                    pedatic = value;
               }
          }
//------------------------------------------------------------------------------------------
          public int Port
          {
               get
               {
                    return port;
               }
               set
               {
                    if (value < 1)
                    {
                         throw new Exception("Invalid port value!");
                    }
                    port = value;
               }
          }
//------------------------------------------------------------------------------------------
          public bool SrvLookup
          {
               get
               {
                    return srvlookup;
               }
               set
               {
                    srvlookup = value;
               }
          }
//------------------------------------------------------------------------------------------
          public string TOS
          {
               get
               {
                    return tos;
               }
               set
               {
                    tos = value;
               }
          }
//------------------------------------------------------------------------------------------
          public bool TrustRPID
          {
               get
               {
                    return trustrpid;
               }
               set
               {
                    trustrpid = value;
               }
          }
//------------------------------------------------------------------------------------------
          public string UserAgent
          {
               get
               {
                    return useragent;
               }
               set
               {
                    useragent = value;
               }
          }
//------------------------------------------------------------------------------------------
          public bool VideoSupport
          {
               get
               {
                    return videosupport;
               }
               set
               {
                    videosupport = value;
               }
          }
          #endregion
//------------------------------------------------------------------------------------------
          public SipAccountCollection Users
          {
               get
               {
                    return users;
               }
          }
//------------------------------------------------------------------------------------------
          public void LoadSettingsFromFile(string path)
          {
               StreamReader file = File.OpenText(path);
               string              line        = "";
               string              sectionname = "";
               NameValueCollection sectiondata = new NameValueCollection();
               while (file.Peek() > -1)
               {
                    line = file.ReadLine().Trim();
                    
                    if (line.IndexOf(";") > -1)
                         line = line.Substring(0, line.IndexOf(";"));

                    if (line == "")
                    {
                         continue;
                    }
                    else if (line.StartsWith("["))
                    {
                         if (line.ToLower().Equals("[general]"))
                         {
                              sectionname = "general";
                         }
                         else if (sectionname == "general")
                         {
                              sectionname = line.Substring(1, line.Length - 2);
                         }
                         else if (sectionname != "general")
                         {
                              HandleSectionData(sectionname, sectiondata);
                              sectiondata.Clear();
                              sectionname = line.Substring(1, line.Length - 2);
                         }
                    }
                    else
                    {
                         string propertyname  = line.Substring(0, line.IndexOf("=")).Trim().ToLower();
                         line                 = line.Replace(propertyname, "").Trim();
                         string propertyvalue = line.Substring(line.IndexOf("=") + 1).Trim().ToLower();
                         
                         if (sectionname == "general")
                         {
                              HandleSectionGeneral(propertyname, propertyvalue);
                         }
                         else
                         {
                              sectiondata.Add(propertyname, propertyvalue);
                         }
                    }
               }
               file.Close();
          }
//------------------------------------------------------------------------------------------
          private void HandleSectionGeneral(string propertyname, string propertyvalue)
          {
               
               switch (propertyname)
               {
                    case "canreinvite":      CanReinvite    = (propertyvalue == "yes");
                         break;
                    case "context":          Context        = propertyvalue;
                         break;
                    case "defaultexpirey":   defaultexpirey = Int32.Parse(propertyvalue);
                         break;
                    case "fromdomain":       FromDomain     = propertyvalue;
                         break;
                    case "localnet":         LocalNet       = propertyvalue;
                         break;
                    case "maxexpirey":       MaxExpirey     = Int32.Parse(propertyvalue);
                         break;
                    case "nat":              Nat            = (propertyvalue == "yes");
                         break;
                    case "notiftymimetype":  NotifyMIMEType = propertyvalue;
                         break;
                    case "pedatic":          Pedatic        = (propertyvalue == "yes");
                         break;
                    case "port":             Port           = Int32.Parse(propertyvalue);
                         break;
                    case "srvlookup":        SrvLookup      = (propertyvalue == "yes");
                         break;
                    case "tos":              TOS            = propertyvalue;
                         break;
                    case "trustrpid":        TrustRPID      = (propertyvalue == "yes");
                         break;
                    case "useragent":        UserAgent      = propertyvalue;
                         break;
                    case "videosupport":     VideoSupport   = (propertyvalue == "yes");
                         break;
               }
          }
//------------------------------------------------------------------------------------------
          private void HandleSectionData(string sectionname, NameValueCollection sectiondata)
          {
               for (int i = 0; i < sectiondata.Count; i++)
               {
                    if (sectiondata.Keys[i] == "type")
                    {
                         switch (sectiondata[i])
                         {
                              case "friend":
                                   break;

                              case "peer":
                                   break;

                              case "user":
                                   SipAccount sipaccount = new SipAccount();
                                   for (int x = 0; x < sectiondata.Count; x++)
                                   {
                                        sipaccount.SectionName   = sectionname;
                                        sipaccount               = HandleSectionSipTypeUser(sipaccount, sectiondata.Keys[x], sectiondata[x]);
                                   }
                                   Users.Add(sipaccount);
                                   break;
                         }
                    }
               }
          }
//------------------------------------------------------------------------------------------
          private SipAccount HandleSectionSipTypeUser(SipAccount sipaccount, string propertyname, string propertyvalue)
          {
               switch (propertyname)
               {
                    case "canreinvite": sipaccount.CanReinvite = (propertyvalue == "yes");
                         break;
                    case "context":     sipaccount.Context     = propertyvalue;
                         break;
                    case "dtmfmode":    sipaccount.DTMFMode    = propertyvalue;
                         break;
                    case "fromuser":    sipaccount.FromUser    = propertyvalue;
                         break;
                    case "fromdomain":  sipaccount.FromDomain  = propertyvalue;
                         break;
                    case "host":        sipaccount.Host        = propertyvalue;
                         break;
                    case "insecure":    sipaccount.Insecure    = propertyvalue;
                         break;
                    case "nat":         sipaccount.Nat         = (propertyvalue == "yes");
                         break;
                    case "secret":      sipaccount.Secret      = "*****"; //propertyvalue;
                         break;
                    case "username":    sipaccount.UserName    = propertyvalue;
                         break;
               }
               return sipaccount;
          }
//------------------------------------------------------------------------------------------
     }
}