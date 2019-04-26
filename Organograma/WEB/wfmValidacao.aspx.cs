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
    public partial class wfmValidacao : System.Web.UI.Page
    {

        string Empresa;


        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["EMPRESA"] == null || string.IsNullOrEmpty(Session["EMPRESA"].ToString())) Response.Redirect("default.aspx");
            Empresa = Session["EMPRESA"].ToString();


            //... poe o menu laranja
            DevExpress.Web.ASPxMenu.ASPxMenu menuPrincipal = Master.FindControl("mnuPrincipal") as DevExpress.Web.ASPxMenu.ASPxMenu;
            menuPrincipal.Items[4].Selected = true;


            


            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["ID"])) Response.Redirect("wfmValidacoes.aspx");

                
                CarregaDados();
                BindaGrid();
            }


        }


        #region Validacoes

        private void CarregaDados()
        {

            Negocio.Validacoes.Validador objValidador = new Negocio.Validacoes.Validador();
            
            int _id = int.Parse( Request.QueryString["ID"]);


            List<Negocio.Validacoes.Validacao>
            ListaValidacoes = objValidador.CarregaValidacoesDetalhe(Empresa,  _id);
            Session.Add("objValidDetalhe", ListaValidacoes);

        }

        private void BindaGrid()
        {
            List<Negocio.Validacoes.Validacao>
          objValid = Session["objValidDetalhe"] as List<Negocio.Validacoes.Validacao>;

            this.dGridValidacao1.DataSource = objValid;
            this.dGridValidacao1.DataBind();

        }

        protected void dGridValidacao1Detalhe_BeforePerformDataSelect(object sender, EventArgs e)
        {

            ASPxGridView dGridDet = (sender as ASPxGridView);
            int Id = Convert.ToInt32(dGridDet.GetMasterRowKeyValue());


            List<Negocio.Validacoes.Validacao>
          objValid = Session["objValidDetalhe"] as List<Negocio.Validacoes.Validacao>;


            List<ValidacaoDetalhe> objDetDs =
                objValid.Where(f => f.Id.Equals(Id)).First().ListaValidacaoDetalhe;

            dGridDet.DataSource = objDetDs; 

        }


       
        #endregion




        protected void btnExportacao_Click(object sender, EventArgs e)
        {

            ASPxButton btnExport = (sender as ASPxButton);



            //monta o banco de daodos da exportção.
            int idl = 1; //contador de linha do arquivo (somente para ordenar)

            //carrega o banco atual 
            List<Negocio.Validacoes.Validacao> objValid = Session["objValidDetalhe"] as List<Negocio.Validacoes.Validacao>;


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


            if (btnExport.CommandArgument.Equals("PDF"))
            {
                this.exprtMain.WritePdfToResponse(string.Format("Export_Validacoes_{0}", DateTime.Now.ToString("yyyy-MM-dd")));
            }

            if (btnExport.CommandArgument.Equals("XLS"))
            {
                this.exprtMain.WriteXlsxToResponse(string.Format("Export_Validacoes_{0}", DateTime.Now.ToString("yyyy-MM-dd")));
            } 






















           
        }






































    }
}