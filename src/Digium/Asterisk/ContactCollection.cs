using System;
using System.Collections;
using System.Text;

namespace TitaniumSoft.Voip
{
     public class ContactCollection : CollectionBase
     {
//--------------------------------------------------------------------------------------------
          public Contact Add()
          {
               Contact contact = new Contact();
               return Add(contact);
          }
//--------------------------------------------------------------------------------------------
          public Contact Add(Contact contact)
          {
               List.Add(contact);
               return contact;
          }
//--------------------------------------------------------------------------------------------
          public Contact this[int index]
          {
               get
               { 
                    return (Contact) base.List[index]; 
               }
               set
               {
                    base.List[index] = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public int IndexOf(Contact contact)
          {
               return List.IndexOf(contact);
          }
//--------------------------------------------------------------------------------------------
     }
}
