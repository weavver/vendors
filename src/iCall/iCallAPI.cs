using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Weavver.Vendors.iCall
{
     public class iCallAPI
     {
//--------------------------------------------------------------------------------------------
          public static List<iCallNPA> getAvailNPA_Tier1Only(string username, string apikey)
          {
               string response = getResponse(apikey, username, "service.getAvailNPA");
               List<iCallNPA> items = new List<iCallNPA>();
               XmlDocument doc = new XmlDocument();
               doc.LoadXml(response);
               foreach (XmlNode node in doc.GetElementsByTagName("npa"))
               {
                    iCallNPA item = new iCallNPA();
                    item.Tier1 = (node.Attributes["tier1"] != null && node.Attributes["tier1"].Value == "true");
                    item.Tier2 = (node.Attributes["tier2"] != null && node.Attributes["tier2"].Value == "true");
                    item.Val = node.InnerText;

                    if (item.Tier1)
                         items.Add(item);
               }
               return items;
          }
//--------------------------------------------------------------------------------------------
          public static List<iCallDID> checkNPA_Tier1Only(string username, string apikey, string npa)
          {
               string response = getResponse(apikey, username, "service.checkNPA&npa=" + npa);
               List<iCallDID> items = new List<iCallDID>();
               XmlDocument doc = new XmlDocument();
               doc.LoadXml(response);
               foreach (XmlNode node in doc.GetElementsByTagName("number"))
               {
                    iCallDID item = new iCallDID();
                    item.Tier1 = (node.Attributes["type"] != null && node.Attributes["type"].Value == "tier1");
                    item.Tier2 = (node.Attributes["type"] != null && node.Attributes["type"].Value == "tier2");
                    item.Number = Int64.Parse(node.InnerText);

                    if (item.Tier1)
                         items.Add(item);
               }
               return items;
          }
//--------------------------------------------------------------------------------------------
          public static bool reserveDID(string username, string apikey, string number)
          {
               string response = getResponse(apikey, username, "service.reserveDID&number=" + number + "&testing=true");
               XmlDocument doc = new XmlDocument();
               doc.LoadXml(response);
               return (doc.GetElementsByTagName("did").Count > 0);
          }
//--------------------------------------------------------------------------------------------
          public static string getBalance(string username, string apikey)
          {
               string response = getResponse(apikey, username, "account.getBalance");
               return response;
          }
//--------------------------------------------------------------------------------------------
          public static bool addSubAccountAsIP(string apikey,
                                           string type,
                                           string descr,
                                           string ip = null)
          {
               string queryString = "account.addSubaccount" +
                                        "&type=" + type +
                                        "&descr=" + descr +
                                        "&ip=" + ip +
                                        "&testing=true";

               string response = getResponse(apikey, null, queryString);
               XmlDocument doc = new XmlDocument();
               doc.LoadXml(response);
               return (doc.GetElementsByTagName("did").Count > 0);
          }
//--------------------------------------------------------------------------------------------
          public static bool addSubAccountAsRegister(string apikey,
                                           string type,
                                           string descr,
                                           string register_username = null,
                                           string register_password = null)
          {
               string queryString = "account.addSubaccount" +
                                        "&type=" + type +
                                        "&descr=" + descr +
                                        "&register_username=" + register_username +
                                        "&register_password=" + register_password +
                                        "&testing=true";

               string response = getResponse(apikey, null, queryString);
               XmlDocument doc = new XmlDocument();
               doc.LoadXml(response);
               return (doc.GetElementsByTagName("did").Count > 0);
          }
//--------------------------------------------------------------------------------------------
          public static bool orderDID(string apikey,
                                      string number,
                                      string tier1or2 = "1",
                                      string routing = "")
          {
               string queryString = "service.orderDID" +
                                        "&number=" + number +
                                        "&tier=" + tier1or2 +
                                        "&routing=" + routing +
                                        "&testing=true";
           
               string response = getResponse(apikey, null, queryString);
               XmlDocument doc = new XmlDocument();
               doc.LoadXml(response);
               string stat = doc["rsp"].Attributes["stat"].Value;
               return (stat == "ok");
          }
//--------------------------------------------------------------------------------------------
          private static string getResponse(string apikey, string username, string methodName)
          {
               HttpWebRequest hwr = (HttpWebRequest) HttpWebRequest.Create("https://carriers.icall.com/api/" +
                                                       "?key=" + apikey +
                                                       "&username=" + username +
                                                       "&method=" + methodName);

               ServicePointManager.ServerCertificateValidationCallback = Validator;
               HttpWebResponse response = (HttpWebResponse)hwr.GetResponse();
               System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
               string temp = reader.ReadToEnd();
               reader.Close();
               return temp;
          }
//--------------------------------------------------------------------------------------------
          public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
          {
               return true;
          }
//--------------------------------------------------------------------------------------------
     }
}
