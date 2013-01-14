using System;
using System.IO;
using System.Net;
using System.Web;

namespace Weavver.Vendors.Google
{
     public class reCAPTCHA
     {
//-------------------------------------------------------------------------------------------
          public static bool Verify(string recaptcha_privatekey, string remoteip, string recaptcha_challenge_field, string recaptcha_response_field)
          {
               string strPost = "privatekey=" + HttpUtility.UrlEncode(recaptcha_privatekey) + "&";
               strPost += "remoteip=" + HttpUtility.UrlEncode(remoteip) + "&";
               strPost += "challenge=" + HttpUtility.UrlEncode(recaptcha_challenge_field) + "&";
               strPost += "response=" + HttpUtility.UrlEncode(recaptcha_response_field);

               StreamWriter myWriter = null;
               HttpWebRequest wClient = (HttpWebRequest) WebRequest.Create("http://www.google.com/recaptcha/api/verify");
               wClient.Method = "POST";
               wClient.ContentType = "application/x-www-form-urlencoded";
               wClient.ContentLength = strPost.Length;
               try
               {
                    myWriter = new StreamWriter(wClient.GetRequestStream());
                    myWriter.Write(strPost);
               }
               catch (Exception e)
               {
                    Console.WriteLine(e.Message);
               }
               finally
               {
                    myWriter.Close();
               }

               HttpWebResponse response = (HttpWebResponse) wClient.GetResponse();
               StreamReader reader = new StreamReader(response.GetResponseStream());
               string result = reader.ReadToEnd();
               reader.Close();
               response.Close();

               return result.Contains("true");
          }
//-------------------------------------------------------------------------------------------
     }
}
