using System;
using System.Collections.Generic;
using System.Web;

using Weavver.Data;

namespace Weavver.Sys
{
     public class View
     {
//-------------------------------------------------------------------------------------------
          public string Id_asString { get; set; }
          public Guid ObjectId { get; set;}
          public Guid EntityId { get; set;}
          public string views { get; set;}
          public string DocName { get; set; }
//-------------------------------------------------------------------------------------------
          public View() : base()
          {
               //Type = "Weavver.System.Permission";
          }
//-------------------------------------------------------------------------------------------
     }
}