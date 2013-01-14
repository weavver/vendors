using System;
using System.Collections;
using System.Data;

namespace TitaniumSoft.Voip
{
     public class CiscoPhoneDirectoryEntryCollection : CollectionBase
     {
//--------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntry Add()
          {
               CiscoPhoneDirectoryEntry directoryentry = new CiscoPhoneDirectoryEntry();
               return Add(directoryentry);
          }
//--------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntry Add(CiscoPhoneDirectoryEntry directoryentry)
          {			
               List.Add(directoryentry);
               return directoryentry;
          }
//--------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntry Add(string name, string number)
          {			
               CiscoPhoneDirectoryEntry directoryentry = new CiscoPhoneDirectoryEntry();
               directoryentry.Name      = name;
               directoryentry.Number    = number;
               return Add(directoryentry);
          }
//--------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntry this[int index]
          {
               get
               {
                    return (CiscoPhoneDirectoryEntry) List[index];
               }
               set
               {
                    base.List[index] = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public int IndexOf(CiscoPhoneDirectoryEntry directoryentry)
          {
               return List.IndexOf(directoryentry);
          }
//--------------------------------------------------------------------------------------------
     }
}