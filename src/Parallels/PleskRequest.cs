using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Weavver.Vendors.Parallels
{
     public class PleskRequest
     {
          public string CustomerAdd(string companyname, string personname, string username, string password, string phone, string email, string address, string city)
          {
               string customeraddxml = "<packet version=\"1.6.3.0\">" + @"
               <customer>
               <add>
                  <gen_info>
                      <cname>{0}</cname>
                      <pname>{1}</pname>
                      <login>{2}</login>
                      <passwd>{3}</passwd>
                      <status>{4}</status>
                      <phone>{5}</phone>
                      <fax>{6}</fax>
                      <email>{7}</email>
                      <address>{8}</address>
                      <city>Fullerton</city>
                      <state>CA</state>
                      <pcode>{9}</pcode>
                      <country>US</country>
                  </gen_info>
               </add></customer></packet>";

               customeraddxml = string.Format(customeraddxml, companyname, personname, username, password, "0", phone, "", email, address, city);

               XmlDocument doc = SendXml(customeraddxml);
               XmlNodeList list = doc.GetElementsByTagName("errtext");
               if (list.Count > 0)
                    return list[0].InnerText;
               else
                    return null;
          }

          public void SubscriptionAdd(string domainname, string username, string password)
          {
               //XmlDocument doc = SendXml(dataxml);

               //DebugOut(doc.GetElementsByTagName("status")[0].InnerText, true);

               string subscriptionadd = "<packet version=\"1.6.3.0\">" + @"
                    <webspace>
                    <add>
                         <gen_setup>
                              <name>{0}</name>
                              <owner-login>{1}</owner-login>
                              <htype>vrt_hst</htype>
                              <ip_address>205.134.225.26</ip_address>
                              <status>0</status>
                         </gen_setup>
                         <hosting>
                          <vrt_hst>
                              <property>
                                <name>ftp_login</name>
                                <value>{2}</value>
                              </property>
                              <property>
                                <name>ftp_password</name>
                                <value>{3}</value>
                              </property>
                              <ip_address>205.134.225.26</ip_address>
                          </vrt_hst>
                         </hosting>
                         <plan-name>Basic</plan-name>
                    </add>
                    </webspace>
                    </packet>";
               subscriptionadd = string.Format(domainname, username, username, password);
               SendXml(subscriptionadd);

          }

               

//               string dataxml = "<packet version=\"1.6.3.0\">" + @"
//                    <customer>
//                    <add>
//                       <gen_info>
//                           <cname>LogicSoft Ltd.</cname>
//                           <pname>Stephen Lowell</pname>
//                           <login>stevelow</login>
//                           <passwd>Jhtr66fBB</passwd>
//                           <status>0</status>
//                           <phone>416 907 9944</phone>
//                           <fax>928 752 3905</fax>
//                           <email>host@logicsoft.net</email>
//                           <address>105 Brisbane Road, Unit 2</address>
//                           <city>Toronto</city>
//                           <state/>
//                           <pcode/>
//                           <country>CA</country>
//                       </gen_info>
//                    </add>
//                    </customer>
//                    </packet>";

//               XmlDocument doc3 = SendXml(subscriptionadd);
//               //DebugOut(doc3.InnerXml, true);
//               // Debug.Text = doc3.InnerXml;

//               string servicePlan = "<packet version=\"1.6.3.0\">" + @"
//               <service-plan>
//               <get>
//                  <filter>
//                     <id>14</id>
//                  </filter>
//               </get>
//               </service-plan>
//               </packet>";

//               XmlDocument doc2 = SendXml(servicePlan);
               //doc2.GetElementsByTagName("status")[0].InnerText
               // DebugOut(doc2.InnerXml, true);

               //d.gen_info.cname = "weavver";
               //Weavver.Vendors.Parallels.client
               // https://darkhorse.weavver.com:8443/enterprise/control/agent.php
               //System.IO.StreamReader str = new System.IO.StreamReader("webSearch.xml");
               //System.Xml.Serialization.XmlSerializer xSerializer = new System.Xml.Serialization.XmlSerializer(typeof(clientAddGenInfo));
               //clientAddGenInfo res = (clientAddGenInfo)xSerializer.Deserialize(str);

               //clientGetGenInfo ginfo = new clientGetGenInfo();
               //System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(clientData));
               //System.IO.StringWriter sw = new System.IO.StringWriter();
               //ser.Serialize(sw, d);
               //Response.Write(sw.ToString());
               //DebugOut(sw.ToString(), true);
     //}
//--------------------------------------------------------------------------------------------
     public XmlDocument SendXml(string xml)
     {
          byte[] data = System.Text.Encoding.ASCII.GetBytes(xml);
          string url = "https://darkhorse.weavver.com:8443/enterprise/control/agent.php";
          HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
          hwr.Method = "POST";
          hwr.Headers.Add("HTTP_AUTH_LOGIN", "admin");
          hwr.Headers.Add("HTTP_AUTH_PASSWD", "(w3avv3r)");
          hwr.ContentType = "text/xml";
          hwr.ContentLength = data.Length;
          ServicePointManager.ServerCertificateValidationCallback = Validator;
          System.IO.Stream newStream = hwr.GetRequestStream();

          newStream.Write(data, 0, data.Length);
          HttpWebResponse response = (HttpWebResponse) hwr.GetResponse();

          System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
          string responsexml = reader.ReadToEnd();
          reader.Close();

          XmlDocument doc = new XmlDocument();
          doc.LoadXml(responsexml);
          return doc;
     }
//--------------------------------------------------------------------------------------------
     public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
     {
          return true;
     }
//--------------------------------------------------------------------------------------------
     }
}