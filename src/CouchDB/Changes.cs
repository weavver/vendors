using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Weavver.Data
{
//-------------------------------------------------------------------------------------------
     public class Changes
     {
          /// <summary>
          /// 
          /// </summary>
          /// <returns>Returns a list of documents that changed and need to be processed.</returns>
          //public List<DbRev> GetChanges(Uri dburl, string sinceRev)
          //{
          //     if (dburl == null)
          //     {
          //          dburl = new Uri("http://192.168.10.111:5984/weavverdb/_changes?since=" + sinceRev + "&filter=weavver.voicescribe.objects.audiofiles/converted");
          //     }
          //     string getrequest = String.Format("GET {2} HTTP/1.1\r\nHost: {0}:{1}\r\n\r\n", dburl.Host, dburl.Port, dburl.PathAndQuery);

          //     WebClient wc = new WebClient();
          //     byte[] punk = wc.DownloadData(dburl);
          //     string json = Encoding.ASCII.GetString(punk, 0, punk.Length);

          //     Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
          //     List<DbRev> ChangeList = new List<DbRev>();
          //     foreach (var doc in o["results"])
          //     {
          //          DbRev rev = new DbRev();
          //          rev.seq = doc["seq"].ToString();
          //          rev.docId = doc["id"].ToString().Replace("\"", "");
          //          ChangeList.Add(rev);
          //     }
          //     return ChangeList;
          //}
     }

     //System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.OpenRead(filepath));
     //HttpWebRequest hwr = (HttpWebRequest) HttpWebRequest.Create("http://" + db.Server.Host + ":" + db.Server.Port + "/weavverdb/" + app.Id + "/" + System.IO.Path.GetFileName(filepath) + "?rev=" + app.Rev);

     //hwr.Method = "PUT";
     //hwr.SendChunked = true;
     //System.IO.Stream stream = hwr.GetRequestStream();
     //byte[] fileB = null;
     //fileB = br.ReadBytes((int) br.BaseStream.Length);  // needs to be optimzed later so it can handle large files... i.e. chunk it in pieces and flush the buffer
     //stream.Write(fileB, 0, fileB.Length);
     //stream.Close();
     //HttpWebResponse response = (HttpWebResponse) hwr.GetResponse();
     //System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
     //string temp = reader.ReadToEnd();
     //reader.Close();
     //br.Close();

     //// write a check to check that the attachment was uploaded successfully
     ////Response.Write(temp);


//-------------------------------------------------------------------------------------------
     public class DbRev
     {
          public string seq;
          public string docId;
          public string changes;
     }
//-------------------------------------------------------------------------------------------
}
