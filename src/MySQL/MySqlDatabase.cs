using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.Security.Cryptography;
using System.Threading;

namespace WeavverLib.Data
{
     public class MySqlDatabase
     {
//--------------------------------------------------------------------------------------------
          public struct UserInfo
          {
               public string[] Roles;
               public string PasswordHash;
               public string Password;
          }
//--------------------------------------------------------------------------------------------
          /// <summary>
          /// This property should only be accessed if the current thread has a lock on DatabaseLock
          /// </summary>
          public MySqlConnection DBConnection
          {
               get
               {
                    if (dbconnection == null || dbconnection.State != System.Data.ConnectionState.Open)
                    {
                         Connect();
                    }
                    return dbconnection;
               }
               set
               {
                    dbconnection = value;
               }
          }
          private MySqlConnection dbconnection     = null;
          public  string          ConnectionLock   = "Lock";
          public  string          ConnectionString = "";
//--------------------------------------------------------------------------------------------
          private void Connect()
          {
               try
               {
                    if (dbconnection != null && dbconnection.State != System.Data.ConnectionState.Closed)
                    {
                         try
                         {
                              dbconnection.Close();
                         }
                         catch
                         {
                              dbconnection.Dispose();
                         }
                         dbconnection = null;
                    }
               }
               finally
               {
                    dbconnection              = new MySqlConnection(ConnectionString);
                    dbconnection.StateChange += new StateChangeEventHandler(dbconnection_StateChange);
                    dbconnection.Open();
               }
          }
//--------------------------------------------------------------------------------------------
          void dbconnection_StateChange(object sender, StateChangeEventArgs e)
          {
               
          }
//--------------------------------------------------------------------------------------------
          public bool CheckUser(string jid, string password, out string[] roles)
          {
               lock (ConnectionLock)
               {
                    int          allowedroleid = 10;
                    string       username      = (jid.IndexOf("@") > 0) ? jid.Substring(0, jid.IndexOf("@")) : null;
                    password                   = Md5Hash(password);
                    List<string> UserRoles     = new List<string>();
                    bool         authenticated = false;
                    MySqlCommand command       = new MySqlCommand("SELECT users.pass, role.rid, role.name FROM users, users_roles, role where users.uid=users_roles.uid and role.rid=users_roles.rid and users.name=?name;", DBConnection);
                    command.Parameters.Add(new MySqlParameter("?name", username));
                    MySqlDataReader reader     = command.ExecuteReader();
                    while (reader.Read())
                    {
                         string dbpassword = reader.GetString(0);
                         int    dbroleid   = reader.GetInt32(1);
                         string dbrolename = reader.GetString(2);
                         if (dbpassword == password && dbroleid == allowedroleid)
                              authenticated = true;
                         UserRoles.Add(dbrolename);
                    }
                    reader.Close();
                    roles = UserRoles.ToArray();
                    throw new Exception("BLAH! " );
                    return authenticated;
               }
          }
//--------------------------------------------------------------------------------------------
          public string[] ReadAllSecurityRoles()
          {
               lock (ConnectionLock)
               {
                    List<string> allRoles = new List<string>();
                    MySqlCommand command = new MySqlCommand("select name from role;", DBConnection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                         allRoles.Add(reader.GetString(0));
                    }
                    reader.Close();
                    return allRoles.ToArray();
               }
          }
//--------------------------------------------------------------------------------------------
          public static string Md5Hash(string pass)
          {
               MD5 md5 = MD5CryptoServiceProvider.Create ();
               byte[] dataMd5 = md5.ComputeHash (Encoding.Default.GetBytes (pass));
               StringBuilder sb = new StringBuilder();
               for (int i = 0; i < dataMd5.Length; i++)
                    sb.AppendFormat("{0:x2}", dataMd5[i]);
               return sb.ToString ();
          }
//--------------------------------------------------------------------------------------------
     }
}
