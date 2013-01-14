using System;
using System.Collections.Specialized;
using System.Text;

namespace TitaniumSoft.Voip
{
     public class InstantMessage
     {
          private StringCollection      recipients          = new StringCollection();
          private string                chatid;
          private string                body;
//--------------------------------------------------------------------------------------------
          public StringCollection Recipients
          {
               get
               {
                    return recipients;
               }
               set
               {
                    recipients = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public string Body
          {
               get
               {
                    return body;
               }
               set
               {
                    body = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public string ChatId
          {
               get
               {
                    return chatid;
               }
               set
               {
                    chatid = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public bool RecipientExists(string recipient)
          {
               for (int i = 0; i < Recipients.Count; i++)
               {
                    if (Recipients[i] == recipient)
                    {
                         return true;
                    }
               }
               return false;
          }
//--------------------------------------------------------------------------------------------
          public static string GenerateId()
          {

               Random         r1                  = new Random(1);
               Random         r2                  = new Random(2);
               Random         r3                  = new Random(3);

               int            a                   = r1.Next(9);
               int            b                   = r2.Next(9);
               int            c                   = r3.Next(9);

               string         format              = "{0}-{1}{2}{3}";

               return String.Format(
                         format, 
                         DateTime.Now.ToFileTime().ToString(), 
                         a.ToString(), 
                         b.ToString(), 
                         c.ToString()
                    );
          }
//--------------------------------------------------------------------------------------------
     }
}