using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class GeradorOrgChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // this.lblTitulo.Text =  ("Gerado o ORG chart do Item:  " + Request.QueryString["ID_ORG_CHART_DS"]);

           // "OC_" + objGuid.ToString(), objDsPrincipal
         
               //  Application["OC_" + Request.QueryString["ID_ORG_CHART_DS"]] == null ?

            //caso não encontre a session, gera um erro na imagem
            if (Application["OC_" + Request.QueryString["ID_ORG_CHART_DS"]] == null)
            {
                this.lblErro.Text = "Erro, a imagem não pode ser gerada. #APP_N_ENC";
                this.lblErro.Visible = true;
                return;
            }

                 List<    
            Negocio.OrgChartDataSource> objDs = (Application["OC_" + Request.QueryString["ID_ORG_CHART_DS"]] as List< Negocio.OrgChartDataSource>);
            
            this.RadOrgChart1.DataSource = objDs;
            this.RadOrgChart1.DataTextField = "Secao";
            this.RadOrgChart1.DataFieldID = "id";
            this.RadOrgChart1.DataFieldParentID = "idPai";

           
            this.RadOrgChart1.DataBind();

           
          



        }
    }
}