using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;


namespace Util
{
   public  static class Avisos 
    {

         public static void  Aviso(string strMensagem, Page p)
        {


            StringBuilder strScript = new StringBuilder();
            strScript.AppendLine(@"<script>");
            strScript.AppendFormat("alert('{0}');", strMensagem);
            strScript.AppendLine(@"</script>");

            p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Inicio", strScript.ToString());

        }


         public static void Aviso(string strMensagem, string strRedirect, Page p)
         {
             Aviso(strMensagem, p);

             StringBuilder strScript = new StringBuilder();
             strScript.AppendLine(@"<script>");
             strScript.AppendFormat("window.location =  \"{0}\"", strRedirect);
             strScript.AppendLine(@"</script>");

             p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Inicio2", strScript.ToString());


         }

    }
}
