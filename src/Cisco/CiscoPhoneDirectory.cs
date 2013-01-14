using System;

namespace TitaniumSoft.Voip
{
	public class CiscoPhoneDirectory
	{
          private string title;
          private string prompt;
          private CiscoPhoneDirectoryEntryCollection items = new CiscoPhoneDirectoryEntryCollection();
//-------------------------------------------------------------------------------------------
          public CiscoPhoneDirectory(string title, string prompt)
          {
               this.title     = title;
			this.prompt    = prompt;
          }
//-------------------------------------------------------------------------------------------
          public string Title
          {
               get
               {
                    return title;
               }
               set
               {
                    title = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public string Prompt
          {
               get
               {
                    return prompt;
               }
               set
               {
                    prompt = value;
               }
          }
//-------------------------------------------------------------------------------------------
          public CiscoPhoneDirectoryEntryCollection Items
          {
               get
               {
                    return items;
               }
          }
//-------------------------------------------------------------------------------------------
          public string RenderXml()
          {
               string phonedirectory  = "<CiscoIPPhoneDirectory>";
               phonedirectory        += "<Title>" + Title + "</Title>";
               phonedirectory        += "<Prompt>" + Prompt + "</Prompt>";
               for (int i = 0; i < Items.Count; i++)
               {
                    phonedirectory        += "<DirectoryEntry>";
                    phonedirectory        += "    <Name>" + Items[i].Name + "</Name>";
                    phonedirectory        += "    <Telephone>" + Items[i].Number + "</Telephone>";
                    phonedirectory        += "</DirectoryEntry>";
               }
               phonedirectory        += "</CiscoIPPhoneDirectory>";
               
               return phonedirectory;
          }
//-------------------------------------------------------------------------------------------
	}
}
