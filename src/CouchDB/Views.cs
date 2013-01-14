using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;



namespace Weavver.Sys
{
     public class Views
     {
//-------------------------------------------------------------------------------------------
          //public string DeployAll(ICouchDatabase db, Guid updatedBy)
          //{
               //System.Reflection.Assembly assembly = Assembly.GetExecutingAssembly();
               //string[] names = assembly.GetManifestResourceNames();
               //SortedList<string, string> debugOut = new SortedList<string, string>();
               //foreach (string name in names)
               //{
               //     string log = "";
               //     if (name.StartsWith("Weavver.Views") && name.EndsWith(".txt"))
               //     {
               //          string ddid = "_design/" + name.Substring(14).ToLower().Replace(".txt", "");
               //          Weavver.Sys.View view = db.GetDocument<Weavver.Sys.View>(ddid);
               //          if (view == null)
               //          {
               //               view = new Weavver.Sys.View();
               //               view.DocName = ddid;
               //               view.CreatedAt = DateTime.UtcNow;
               //               view.CreatedBy = updatedBy;
               //          }
               //          view.UpdatedAt = DateTime.UtcNow;
               //          view.UpdatedBy = updatedBy;

               //          StreamReader stream = new StreamReader(assembly.GetManifestResourceStream(name));
               //          view.views = stream.ReadToEnd();
               //          try
               //          {
               //               view.Commit();
               //               log += "Deployed " + ddid + "..<br />\r\n";
               //          }
               //          catch (Exception ex)
               //          {
               //               log += "<span style='color: red'>Deploy failed for " + ddid + "..</span><br />";
               //          }
               //          debugOut.Add(name, log);
               //     }
               //}
               //string html = "";
               //for (int i = 0; i < debugOut.Count; i++)
               //{
               //     html += debugOut.Values[i];
               //}

               //Console.WriteLine(html);
               //return html;

          //     return "FAILED";
          //}
//-------------------------------------------------------------------------------------------
     }
}
