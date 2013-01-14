using System;

namespace TitaniumSoft.Voip
{
	public class CiscoPhoneDirectoryEntry
	{
          private string name;
          private string number;
//-------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntry()
          {
          }
//-------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntry(string name, string number)
          {
               this.name     = name;
			this.number   = number;
          }
//-------------------------------------------------------------------------------------------
          public string Name
          {
               get
               {
                    return name;
               }
               set
               {
                    name = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Number
          {
               get
               {
                    return number;
               }
               set
               {
                    number = value;
               }
          }
//-------------------------------------------------------------------------------------------
	}
}
