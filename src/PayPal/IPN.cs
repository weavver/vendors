using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Weavver.Data;

namespace Weavver.Vendors.PayPal
{
     public class IPN
     {
          public string Receiver_email { get; set; }
          public string Item_name { get; set; }
          public string Item_number { get; set; }
          public string Quantity { get; set; }
          public string Invoice { get; set; }
          public string Custom { get; set; }
          public string Payment_status { get; set; }
          public string Pending_reason { get; set; }
          public string Payment_date { get; set; }
          public string Payment_fee { get; set; }
          public decimal Payment_gross { get; set; }
          public string Txn_id { get; set; }
          public string Txn_type { get; set; }
          public string First_name { get; set; }
          public string Last_name { get; set; }
          public string Address_street { get; set; }
          public string Address_city { get; set; }
          public string Address_state { get; set; }
          public string Address_zip { get; set; }
          public string Address_country { get; set; }
          public string Address_status { get; set; }
          public string Payer_email { get; set; }
          public string Payer_status { get; set; }
          public string Payment_type { get; set; }
          public string Notify_version { get; set; }
          public string Verify_sign { get; set; }
//-------------------------------------------------------------------------------------------
          public IPN() : base()
          {
               //Type = "Weavver.Vendors.PayPal.IPN";
          }
//-------------------------------------------------------------------------------------------
     }
}
//-------------------------------------------------------------------------------------------
// if (paymentmethod == paypal)
//// try
//{
//     string fullname = ""; // tbFullName.Text
//     if (tbFullName.Text == "")
//     {
//          tbFullName.BackColor = System.Drawing.Color.LightPink;
//          return;
//     }
//     string strPayPalURL  = "https://www.paypal.com/xclick/business=mythicalbox@weavver.com";
//     strPayPalURL        += "&item_name=Weavver Product(s)";
//     strPayPalURL        += "&item_number=01";
//     strPayPalURL        += "&quantity=" + order.Quantity; 
//     strPayPalURL        += "&amount=" + order.Total.ToString();
//     strPayPalURL        += "&custom=" + order.Id ;
//     strPayPalURL        += "&return=http://www.weavver.com/company/sales/thank%20you.aspx";
//     strPayPalURL        += "&cancel_return=http://www.weavver.com";
//     strPayPalURL        += "&notify_url=http://www.weavver.com/company/sales/check%20out.aspx";
//     strPayPalURL        += "&undefined_quantity=&no_note=1&no_shipping=1";
//     Response.Redirect(strPayPalURL);
//}
////catch (Exception ex)
////{
////    Weavver.Common.Common.Log(ex.Message, ex.ToString());
////    Quantity.Text   = "1";
////    Total.Text      = order.UnitPrice.ToString();
////}