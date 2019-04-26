using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Negocio.Validacoes;
using System.Data;
using DevExpress.Web.ASPxEditors;



namespace WEB
{
	public partial class _default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
           

            if (!IsPostBack)
            {

                ExecutaManutencao();
                CarregaImagens();
                CarregaDados();
                BindaGrid();
            }

          


		}


        #region ImagensHome

        private void CarregaImagens()
        {

            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            var ListaEmpresas = objDs.ORG_EMPRESA;
            this.rptMenuInicial.DataSource = ListaEmpresas;
            this.rptMenuInicial.DataBind();

        }

        protected void lbkbtnMain_click(object sender, EventArgs e)
        {

            Session.Add("EMPRESA", (sender as LinkButton).CommandArgument);
            Response.Redirect("wfmMain.aspx");




        }

        #endregion


        #region Validacoes
        
        private void CarregaDados()
        {

            Negocio.Validacoes.Validador objValidador = new Negocio.Validacoes.Validador();
            List<Negocio.Validacoes.Validacao>
            ListaValidacoes = objValidador.CarregaValidacoesHome();
            Session.Add("objValid", ListaValidacoes);

        }

        private void BindaGrid()
        {
            List<Negocio.Validacoes.Validacao>
          objValid = Session["objValid"] as List<Negocio.Validacoes.Validacao>;

            this.dGridValidacao1.DataSource = objValid;
            this.dGridValidacao1.DataBind();

        }

        protected void dGridValidacao1Detalhe_BeforePerformDataSelect(object sender, EventArgs e)
        {

            if (Session["objValid"] == null) return;


            ASPxGridView dGridDet = (sender as ASPxGridView);
            int Id = Convert.ToInt32(dGridDet.GetMasterRowKeyValue());


            List<Negocio.Validacoes.Validacao>
          objValid = Session["objValid"] as List<Negocio.Validacoes.Validacao>;




            List<ValidacaoDetalhe> objDetDs =
                objValid.Where(f => f.Id.Equals(Id)).First().ListaValidacaoDetalhe;

            dGridDet.DataSource = objDetDs;







        }

        protected void btnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {

            //monta o banco de daodos da exportção.
            int idl = 1;

            //carrega o banco atual 
            List<Negocio.Validacoes.Validacao> objValid = Session["objValid"] as List<Negocio.Validacoes.Validacao>;


            //cria o objeto que será a base da exportação.
            DataTable dtExport = new DataTable();
            dtExport.Columns.AddRange(
                new DataColumn[] {
                new DataColumn("ORDEM", System.Type.GetType("System.Int32",true,true)),
                new DataColumn("CONTEUDO") });




            //para cada linha do grid adiciona um regisro na base
            //de exportação 
            foreach (Negocio.Validacoes.Validacao validacao in objValid)
            {
                //adiciona a linha do cabecalho 
                dtExport.Rows.Add(idl, validacao.Titulo);

                foreach (ValidacaoDetalhe detalhe in validacao.ListaValidacaoDetalhe)
                {
                    //incrementa o contador da linha 
                    idl++;
                    //adiciona as linhas do detalhe 
                    dtExport.Rows.Add(idl, detalhe.Titulo);

                }
            }


            //cria o grid modelo 

            dGridExportacao.DataSource = dtExport;
            dGridExportacao.DataBind();


            this.exprtMain.WritePdfToResponse("Validacoes_export");




        }



        protected void ExecutaManutencao()
        {

            Manutencao.Manutencao objManut = new Manutencao.Manutencao();
            objManut.ExecutaManutencao();

           
        }




        #endregion




    }
}