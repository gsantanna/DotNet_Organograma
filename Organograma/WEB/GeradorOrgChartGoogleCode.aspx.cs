using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WEB
{
    public partial class GeradorOrgChartGoogleCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

 
            //caso não encontre a session, gera um erro na imagem
            if (Application["OC_" + Request.QueryString["ID_ORG_CHART_DS"]] == null)
            {
                this.lblErro.Text = "Erro, a imagem não pode ser gerada. #APP_N_ENC";
                this.lblErro.Visible = true;
                return;
            }

           

                 List<    
            Negocio.OrgChartDataSource> objDs = (Application["OC_" + Request.QueryString["ID_ORG_CHART_DS"]] as List< Negocio.OrgChartDataSource>);


                 if (System.Configuration.ConfigurationManager.AppSettings["ORGCHART_MODE"] == "DEBUG")
                 {
                     StringBuilder strDebug = new StringBuilder();


                     strDebug.AppendLine(@"<body> <h1>Debug</h1><table>");
                     strDebug.AppendLine(@"<tr><td>SECAO</td><td>GESTOR</td><td>Cod_secao</td><td>cod_secao_sup</td><td>ID</td><td>ID_PAI</td></tr>");

                     foreach (Negocio.OrgChartDataSource ds in objDs)
                     {
                         strDebug.AppendLine(string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", ds.Secao, ds.Gestor, ds.Cod_secao, ds.Cod_secao_sup, ds.id, ds.idPai));


                     }

                     strDebug.AppendLine("</table></body>");

                     this.lblErro.Text = strDebug.ToString();
                     this.lblErro.Visible = true;

                 }





            
            //gera o script do Google
            StringBuilder objStrGoogle = new StringBuilder();

            objStrGoogle.AppendLine("<script type='text/javascript' src='https://www.google.com/jsapi'></script>");
            objStrGoogle.AppendLine("<script type='text/javascript'>");
            objStrGoogle.AppendLine("google.load('visualization', '1', { packages: ['orgchart'] });");
            objStrGoogle.AppendLine("google.setOnLoadCallback(drawChart);");
            objStrGoogle.AppendLine("function drawChart() {");

            objStrGoogle.AppendLine("var data = new google.visualization.DataTable();");
            objStrGoogle.AppendLine("data.addColumn('string', 'Name');");
            objStrGoogle.AppendLine("data.addColumn('string', 'Manager');");
            objStrGoogle.AppendLine("data.addColumn('string', 'ToolTip');");
            objStrGoogle.AppendLine("data.addRows([ ");

            foreach (Negocio.OrgChartDataSource ds in objDs)
            {
                string strIdPai = ds.idPai > 0 ? ds.idPai.ToString() : string.Empty;
                objStrGoogle.AppendLine("[{ v: '" + ds.id.ToString() + "', f: '" + ds.Secao + "<div style=\"color:red; font-style:italic\">" + ds.Gestor + "</div><div class=\"Funcionarios\">" + ds.Funcionarios +"</div>' }, '"+ strIdPai +"', '{"+ds.Secao+"}'],");

           
            }


            objStrGoogle = objStrGoogle.Remove(objStrGoogle.Length - 3, 3);
            objStrGoogle.AppendLine(" ]);");
            objStrGoogle.AppendLine("var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));");
            objStrGoogle.AppendLine("chart.draw(data, { allowHtml: true });");
            objStrGoogle.AppendLine("}");


            objStrGoogle.AppendLine(@"</script>");


            //registra o script 
            ClientScript.RegisterStartupScript
            ( Page.GetType() ,"Inicio", objStrGoogle.ToString());




            //destroi o objeto da application
           Application.Remove("OC_" + Request.QueryString["ID_ORG_CHART_DS"]);














        }
    }
}