using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class wfmSecoes : System.Web.UI.Page
    {

        private String Empresa;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EMPRESA"] == null || string.IsNullOrEmpty(Session["EMPRESA"].ToString())) Response.Redirect("default.aspx");
            Empresa = Session["EMPRESA"].ToString();
            
            
            Carrega();


            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["fe"]))
                {
                    this.dGridMain.FilterExpression = Request.QueryString["fe"];
                }

            }
            
          
        }


        void Carrega()
        {
            //cria o contexto de acesso a dados
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            
            //executa a consulta
            var ListaSecoes = (from s in objDs.ORG_LISTA_SECOES
                              where s.COD_EMPRESA.Equals(  Empresa)  || s.PUBLICO=="1"
                              select s).OrderBy( s=>s.NOME_SECAO_FINAL );

            this.dGridMain.DataSource = ListaSecoes;
            this.dGridMain.DataBind();


        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxEditors.ASPxButton btn = (sender as DevExpress.Web.ASPxEditors.ASPxButton);
            Response.Redirect(string.Format("wfmSecao.aspx?id_secao={0}&fe={1}",
                btn.CommandArgument, this.dGridMain.FilterExpression));

            


        }



    }
}