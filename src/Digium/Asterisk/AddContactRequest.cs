using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace TitaniumSoft.Voip
{
     public class AddContactRequest
     {
          private string chatid;
          private StringCollection members = new StringCollection();

          public StringCollection Members
          {
               get
               {
                    return members;
               }
               set
               {
                    members = value;
               }
          }

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



          public bool RecipientExists(string member)
          {
               for( int i = 0; i < Members.Count; i++ )
               {
                    if( Members[i] == member )
                    {
                         return true;
                    }
               }
               return false;
          }

     }
}
