using System;
using System.Collections;
using System.Data;

namespace TitaniumSoft.Voip
{
     public class GroupCollection : CollectionBase
     {
//--------------------------------------------------------------------------------------------
          public Group Add()
          {
               Group group = new Group();
               return Add(group);
          }
//--------------------------------------------------------------------------------------------
          public Group Add(Group group)
          {
               List.Add(group);
               return group;
          }
//--------------------------------------------------------------------------------------------
          public Group this[int index]
          {
               get
               { 
                    return (Group) base.List[index]; 
               }
               set
               {
                    base.List[index] = value;
               }
          }
//--------------------------------------------------------------------------------------------
          public int IndexOf(Group group)
          {
               return List.IndexOf(group);
          }
//--------------------------------------------------------------------------------------------
     }
}
