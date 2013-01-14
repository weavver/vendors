using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weavver.Vendors.Authorize.Net
{
//-------------------------------------------------------------------------------------------
     /// <summary>
     /// possibly obsoleted by adding in the official authorize.net sdk
     /// </summary>
     public class AMIGatewayResponse
     {
          public int ResponseCode;
          public string ResponseSubCode;
          public string ResponseReasonCode;
          public string ResponseReasonText;
          public string AuthorizationCode;
          public string AVSResponse;
          public string TransactionID;
          public string InvoiceNumber;
          public string Description;
          public string Amount;
          public string Method;
          public string TransactionType;
          public string CustomerId;
          public string NameFirst;
          public string NameLast;
          public string Company;
          public string Address;
          public string City;
          public string State;
          public string ZipCode;
          public string Country;
          public string Phone;
          public string Fax;
          public string EmailAddress;
          public string ShipToFirstName;
          public string ShipToLastName;
          public string ShipToCompany;
          public string ShipToAddress;
          public string ShipToCity;
          public string ShipToState;
          public string ShipToZipCode;
          public string ShipToCountry;
          public string Tax;
          public string Duty;
          public string Freight;
          public string TaxExempt;
          public string PurchaseOrderNumber;
          public string MD5Hash;
          public string CardCodeResponse;
          public string CardholderAVR;
          public string SplitTenderId;
          public string RequestedAmount;
          public string BalanceOnCard;
          public string AccountNumber;
          public string CardType;
//-------------------------------------------------------------------------------------------
          public void ParseResponse(string post_response)
          {
               string[] response_array = post_response.Split('|');
               Int32.TryParse(response_array[0], out ResponseCode);
               ResponseSubCode = response_array[1];
               ResponseReasonCode = response_array[2];
               ResponseReasonText = response_array[3];
               AuthorizationCode = response_array[4];
               AVSResponse = response_array[5];
               TransactionID = response_array[6];
               InvoiceNumber = response_array[7];
               Description = response_array[8];
               Amount = response_array[9];
               Method = response_array[10];
               TransactionType = response_array[11];
               CustomerId = response_array[12];
               NameFirst = response_array[13];
               NameLast = response_array[14];
               Company = response_array[15];
               Address = response_array[16];
               City = response_array[17];
               State = response_array[18];
               ZipCode = response_array[19];
               Country = response_array[20];
               Phone = response_array[21];
               Fax = response_array[22];
               EmailAddress = response_array[23];
               ShipToFirstName = response_array[24];
               ShipToLastName = response_array[25];
               ShipToCompany = response_array[26];
               ShipToAddress = response_array[27];
               ShipToCity = response_array[28];
               ShipToState = response_array[29];
               ShipToZipCode = response_array[30];
               ShipToCountry = response_array[31];
               Tax = response_array[32];
               Duty = response_array[33];
               Freight = response_array[34];
               TaxExempt = response_array[35];
               PurchaseOrderNumber = response_array[36];
               MD5Hash = response_array[37];
               CardCodeResponse = response_array[38];
               CardholderAVR = response_array[39];
               SplitTenderId = response_array[42];
               RequestedAmount = response_array[43];
               BalanceOnCard = response_array[44];
               AccountNumber = response_array[50];
               CardType = response_array[51];
          }
//-------------------------------------------------------------------------------------------
     }
}
