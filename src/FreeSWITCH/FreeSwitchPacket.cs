using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Weavver.Vendors.FreeSWITCH
{
     public class FreeSwitchPacket
     {
          private string packet    = "";
//--------------------------------------------------------------------------------------------
          public FreeSwitchPacket(string packet)
          {
               this.packet = packet;
          }
//--------------------------------------------------------------------------------------------
          public string this[string property]
          {
               get
               {
                    return GetValue(property);
               }
               set
               {
                    bool      setprop   = false;
                    string[]  line      = Regex.Split(packet, Environment.NewLine);
                    string    newpacket = "";
                    for (int i = 0; i < line.Length; i++ )
                    {
                         if (line[i].StartsWith(property))
                         {
                             newpacket += property + ": " + value + "\n";
                             setprop = true;
                         }
                         else if (line[i] == "")
                         {
                              continue;
                         }
                         else
                         {
                              newpacket += line[i] + "\n";
                         }
                    }
                    if (setprop)
                    {
                         packet = newpacket;
                    }
                    else
                    {
                         packet = property  + ": " + value + "\n" + newpacket;
                    }
               }
          }
//--------------------------------------------------------------------------------------------
          public string Body
          {
               get
               {
                    string temp = "";
                    if (packet.StartsWith("\n\n"))
                         temp = packet.Substring(packet.IndexOf("\n\n") + 2);
                    else
                         temp = packet;


                    if (!temp.EndsWith("\n\n"))
                    {
                         if (temp.EndsWith("\n"))
                              temp += "\n";
                         else
                              temp += "\n\n";
                    }
                    return temp;
               }
          }
//--------------------------------------------------------------------------------------------
          private string GetValue(string property)
          {
               string[]  line      = Regex.Split(packet, "\n");
               string    value     = "";

               for( int i = 0; i < line.Length; i++ )
               {
                    if( line[i].StartsWith(property) )
                    {
                         value = line[i].Substring(line[i].IndexOf(":") + 2);
                         break;
                    }
               }
               return value;
          }
//--------------------------------------------------------------------------------------------
          public override string ToString()
          {
               return packet;               
          }
//--------------------------------------------------------------------------------------------
     }
}
