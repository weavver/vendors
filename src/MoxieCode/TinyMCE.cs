using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Weavver.Vendors.MoxieCodeSystems
{
     public class TinyMCE : TextBox
     {
//------------------------------------------------------------------------------------------
          public TinyMCE()
          {
               TextMode = TextBoxMode.MultiLine;
               Text = "asdfasdf";
          }
//------------------------------------------------------------------------------------------
          protected override void OnLoad(EventArgs e)
          {
               base.OnLoad(e);
          }
//------------------------------------------------------------------------------------------
          protected override void OnPreRender(EventArgs e)
          {
               string tinymce = "<script language=\"javascript\" type=\"text/javascript\" src=\"~/vendors/moxiecode systems/tinymce/jscripts/tiny_mce/tiny_mce_src.js\"></script>"
                    + "<script language=\"javascript\" type=\"text/javascript\">"
                    + "tinyMCE.init({"
                    + "mode : \"textareas\","
                    + "theme : \"advanced\","
                    + "theme_advanced_buttons1 : \"bold,italic,underline,justifyleft,justifycenter,justifyright,justifyfull,bullist,numlist,separator,undo,redo,separator,link,unlink,code\","
                    + "theme_advanced_buttons2 : \"\","
                    + "theme_advanced_buttons3 : \"\","
                    + "theme_advanced_toolbar_location : \"top\","
                    + "theme_advanced_toolbar_align : \"left\","
                    + "theme_advanced_statusbar_location : \"bottom\""
                    + "});"
                    + "</script>";

               if (!Page.ClientScript.IsClientScriptBlockRegistered("tinymce"))
               {
                    Page.ClientScript.RegisterClientScriptBlock(tinymce.GetType(), "tinymce", tinymce);
               }
               base.OnPreRender(e);
          }
//------------------------------------------------------------------------------------------
          protected override void Render(HtmlTextWriter writer)
          {
               base.Render(writer);
          }
//------------------------------------------------------------------------------------------
     }
}