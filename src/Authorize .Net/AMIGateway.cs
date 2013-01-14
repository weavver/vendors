using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;

namespace Weavver.Vendors.Authorize.Net
{
//-------------------------------------------------------------------------------------------
     /// <summary>
     /// possibly obsoleted by adding in the official authorize.net sdk
     /// </summary>
     public class AMIGateway
     {
          // test server for developer accounts: https://test.authorize.net/gateway/transact.dll
          // server for real accounts: https://secure.authorize.net/gateway/transact.dll
          //                                 ^--------------- use also if real account is in testing mode
          public string post_url = "https://secure.authorize.net/gateway/transact.dll";
          public string x_login_id = ""; //
          public string x_tran_key = ""; //
          public string x_delim_data = ""; // TRUE
          public string x_delim_char = ""; // |
          public string x_relay_response = ""; // FALSE
          public string x_type = ""; // AUTH_CAPTURE
          public string x_method = ""; // CC
          public string x_card_num = ""; // 4111111111111111
          public string x_exp_date = ""; // 0115
          public string x_amount = ""; // 0115
          public string x_description = ""; // Sample Transaction
          public string x_first_name = ""; // John
          public string x_last_name = ""; // Doe
          public string x_address = ""; // 1234 Street
          public string x_state = ""; // WA
          public string x_zip = ""; // 98004
          private bool _testmode = false;
//-------------------------------------------------------------------------------------------
          public bool TestMode
          {
               get
               {
                    return _testmode;
               }
               set
               {
                    _testmode = value;
                    if (_testmode)
                         post_url = "https://test.authorize.net/gateway/transact.dll";
                    else
                         post_url = "https://secure.authorize.net/gateway/transact.dll";
               }
          }
//-------------------------------------------------------------------------------------------
          public AMIGatewayResponse BillCard()
          {
               Dictionary<string, string> post_values = new Dictionary<string, string>();
               //the API Login ID and Transaction Key must be replaced with valid values
               post_values.Add("x_login", x_login_id);
               post_values.Add("x_tran_key", x_tran_key);
               post_values.Add("x_delim_data", x_delim_data);
               post_values.Add("x_delim_char", x_delim_char);
               post_values.Add("x_relay_response", x_relay_response);

               post_values.Add("x_type", x_type);
               post_values.Add("x_method", x_method);
               post_values.Add("x_card_num", x_card_num);
               post_values.Add("x_exp_date", x_exp_date);

               post_values.Add("x_amount", x_amount);
               post_values.Add("x_description", x_description);

               post_values.Add("x_first_name", x_first_name);
               post_values.Add("x_last_name", x_last_name);
               post_values.Add("x_address", x_address);
               post_values.Add("x_state", x_state);
               post_values.Add("x_zip", x_zip);
               // Additional fields can be added here as outlined in the AIM integration
               // guide at: http://developer.authorize.net

               // This section takes the input fields and converts them to the proper format
               // for an http post.  For example: "x_login=username&x_tran_key=a1B2c3D4"
               String post_string = "";

               foreach (KeyValuePair<string, string> post_value in post_values)
               {
                    post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&";
               }
               post_string = post_string.TrimEnd('&');

               // The following section provides an example of how to add line item details to
               // the post string.  Because line items may consist of multiple values with the
               // same key/name, they cannot be simply added into the above array.
               //
               // This section is commented out by default.
               /*
               string[] line_items = {
                "item1<|>golf balls<|><|>2<|>18.95<|>Y",
                "item2<|>golf bag<|>Wilson golf carry bag, red<|>1<|>39.99<|>Y",
                "item3<|>book<|>Golf for Dummies<|>1<|>21.99<|>Y"};

               foreach( string value in line_items )
               {
                post_string += "&x_line_item=" + HttpUtility.UrlEncode(value);
               }
               */

               // create an HttpWebRequest object to communicate with Authorize.net
               HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(post_url);
               objRequest.Method = "POST";
               objRequest.ContentLength = post_string.Length;
               objRequest.ContentType = "application/x-www-form-urlencoded";

               // post data is sent as a stream
               StreamWriter myWriter = null;
               myWriter = new StreamWriter(objRequest.GetRequestStream());
               myWriter.Write(post_string);
               myWriter.Close();

               // returned values are returned as a stream, then read into a string
               String post_response;
               HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
               using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
               {
                    post_response = responseStream.ReadToEnd();
                    responseStream.Close();
               }

               // the response string is broken into an array
               // The split character specified here must match the delimiting character specified above


               AMIGatewayResponse response = new AMIGatewayResponse();
               response.ParseResponse(post_response);

               return response;
          }
//-------------------------------------------------------------------------------------------
     }
//-------------------------------------------------------------------------------------------
}


// Old code, obseleted by adding the AuthorizeNet C# SDK..
//AMIGateway gateway = new AMIGateway();
//// gateway.TestMode = true;
//gateway.x_login_id = ConfigurationManager.AppSettings["authorize.net_loginid"]; 
//gateway.x_tran_key = ConfigurationManager.AppSettings["authorize.net_transactionkey"];
//gateway.x_first_name = NameFirst;
//gateway.x_last_name = NameLast;
//gateway.x_address = AddressLine1;
//gateway.x_state = State;
//gateway.x_zip = ZipCode;
//gateway.x_amount = amount.ToString();
//gateway.x_description = description;
//gateway.x_method = "CC";
//gateway.x_card_num = CCNumber;
//AMIGatewayResponse billingResponse = gateway.BillCard();