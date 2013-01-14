using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace TitaniumSoft.Voip
{
     class GroupMessage
     {
          private   string              m_id                = "";
          private   StringCollection    m_groupmembers      = new StringCollection();
          private   string              m_body              = "";

//--------------------------------------------------------------------------------------------
          public GroupMessage()
          {}
          public GroupMessage(string id)
          {
               m_id                = id;
          }
          public GroupMessage(string id, StringCollection groupmembers)
          {
               m_id                = id;
               m_groupmembers      = groupmembers;
          }
          public GroupMessage(string id, StringCollection groupmembers, string body)
          {
               m_id                = id;
               m_groupmembers      = groupmembers;
               m_body              = body;
          }
//--------------------------------------------------------------------------------------------
          public string Id
          {
               get
               {
                    return         m_id;
               }
               set
               {
                    m_id           = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public StringCollection GroupMembers
          {
               get
               {
                    return         m_groupmembers;
               }
               set
               {
                    m_groupmembers = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public string Body
          {
               get
               {
                    return         m_body;
               }
               set
               {
                    m_body         = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public bool MemberExists(string member)
          {
               for( int i = 0; i < m_groupmembers.Count; i++ )
               {
                    if( m_groupmembers[i] == member )
                    {
                         return true;
                    }
               }
               return false;
          }
//--------------------------------------------------------------------------------------------
     }
}
