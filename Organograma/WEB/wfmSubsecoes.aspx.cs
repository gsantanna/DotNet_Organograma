using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class wfmSubsecoes : System.Web.UI.Page
    {
        string Empresa;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["EMPRESA"] == null || string.IsNullOrEmpty(Session["EMPRESA"].ToString())) Response.Redirect("default.aspx");
            Empresa = Session["EMPRESA"].ToString();
            
         
            Carrega();
        }


        private void Carrega()
        {

            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            var listaSubsecao = (from f in objDs.ORG_LISTA_SUBSECAO
                                where f.COD_EMPRESA.Equals( Empresa) 
                                select f).OrderBy(f=>f.NOME);

            this.dGridMain.DataSource = listaSubsecao;
            this.dGridMain.DataBind();



            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["fe"]))
                {
                    this.dGridMain.FilterExpression = Request.QueryString["fe"];
                }

            }

        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfmSubsecaoAdd.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("wfmSubsecaoEdit.aspx?id_subsecao={0}&fe={1}", (sender as DevExpress.Web.ASPxEditors.ASPxButton).CommandArgument.ToString() ,  dGridMain.FilterExpression  )    );

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            
            //verifica a subseção 
            Negocio.SubSecaoManager objMgnr = new Negocio.SubSecaoManager();
            if (objMgnr.ExcluiSubsecao(
            (sender as DevExpress.Web.ASPxEditors.ASPxButton).CommandArgument.ToString()))
            {
                Util.Avisos.Aviso("Sub seção excluida!", this.Page);
                Carrega();

            }
            else
            {
                //não pode excluir! exibir o erro
                Util.Avisos.Aviso(objMgnr.Erro, this.Page);
            }


            


        }

    }
}