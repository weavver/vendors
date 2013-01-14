using System.Collections;

namespace TitaniumSoft.Voip.Asterisk
{
	public class SipAccountCollection : CollectionBase
	{
//-------------------------------------------------------------------------------------------
          public SipAccount Add()
          {
               SipAccount sipaccount = new SipAccount();
               return Add(sipaccount);
          }
//-------------------------------------------------------------------------------------------
          public SipAccount Add(SipAccount sipaccount)
          {			
               List.Add(sipaccount);
               return sipaccount;
          }
 //-------------------------------------------------------------------------------------------
          public SipAccount this[int index]
          {
               get { return (SipAccount) List[index]; }
               set { base.List[index] = value; }
          }
 //-------------------------------------------------------------------------------------------
          public int IndexOf(SipAccount sipaccount)
          {
               return List.IndexOf(sipaccount);
          }
 //-------------------------------------------------------------------------------------------
     }
}