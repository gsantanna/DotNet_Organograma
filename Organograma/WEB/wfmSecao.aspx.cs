using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class wfmSecao : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            //... poe o menu laranja
            DevExpress.Web.ASPxMenu.ASPxMenu menuPrincipal = Master.FindControl("mnuPrincipal") as DevExpress.Web.ASPxMenu.ASPxMenu;
            menuPrincipal.Items[0].Selected = true;




            if (string.IsNullOrEmpty(Request.QueryString["id_secao"])) //caso acesse direto a página
                Response.Redirect("wfmSecoes.aspx");





            if (!IsPostBack)
            {
                CarregaSecao(); //1 - carrega o obj da secao no banco de dados

                CarregaCombos(); //2 - carrega os dados dos comboboxes

                BindaCampos(); //3 - binda os campos e os combos


            }


        }





        private void CarregaSecao()
        {

            string id = Request.QueryString["id_secao"];

            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            Negocio.ORG_LISTA_SECOES objSecao = new Negocio.ORG_LISTA_SECOES();
            objSecao = (from f in objDs.ORG_LISTA_SECOES
                        where f.COD_SECAO.Equals(id)
                        select f).FirstOrDefault<Negocio.ORG_LISTA_SECOES>();

            if (objSecao == null || string.IsNullOrEmpty(objSecao.COD_SECAO))
                Response.Redirect("wfmSecoes.aspx?erro=SESSAO_NAO_ENCONTRADA");



            //adiciona o registro no viewstate.
            ViewState.Add("objSecao", objSecao);

            //seta o título da pagina
            this.lblTitulo.Text = string.Format("Informações da Seção: {0}", objSecao.NOME_SECAO_FINAL);

           



        }


        private void CarregaCombos()
        {
            //cria o contexto de acesso a dados.
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            //carrega o objeto da secao 
            Negocio.ORG_LISTA_SECOES objSecao = ViewState["objSecao"] as Negocio.ORG_LISTA_SECOES;



            //seção superior 
            var listaSecoes = (from s in objDs.ORG_LISTA_SECOES_SIMPLES
                               where (s.COD_EMPRESA == objSecao.COD_EMPRESA || s.PUBLICO == "1") && s.COD_SECAO != objSecao.COD_SECAO
                               select s).OrderBy(f => f.NOME_SECAO);



            this.cboSecaoSuperior.DataSource = listaSecoes;
            this.cboSecaoSuperior.TextField = "NOME_SECAO";
            this.cboSecaoSuperior.ValueField = "COD_SECAO";
            this.cboSecaoSuperior.DataBind();


            CarregaGestores();


        }


        private void CarregaGestores()
        {

            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            //carrega o objeto da secao 
            Negocio.ORG_LISTA_SECOES objSecao = ViewState["objSecao"] as Negocio.ORG_LISTA_SECOES;



            //Gestores
            var listaGestores = (from s in objDs.ORG_FUNCIONARIO_DISPONIVEL
                                 where
                                 s.COD_SECAO == objSecao.COD_SECAO ||  ( s.COD_SECAO == objSecao.COD_SECAO_SUP && !objSecao.POSSUI_SUPERIOR.Equals("1")  )
                                 select new { s.NOME, s.CPF }).Distinct().OrderBy(f => f.NOME);

            this.cboGestorModificado.DataSource = listaGestores;
            this.cboGestorModificado.TextField = "NOME";
            this.cboGestorModificado.ValueField = "CPF";
            this.cboGestorModificado.DataBind();

           // this.cboGestorModificado.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem("Vazio", "0"));


        }


        private void BindaCampos()
        {

            //carrega a seção do viewstate.
            Negocio.ORG_LISTA_SECOES objSecao = ViewState["objSecao"] as Negocio.ORG_LISTA_SECOES;


            this.txtSecao.Text = objSecao.NOME_RM;
            this.txtCodSecao.Text = objSecao.COD_SECAO;
            this.txtGestorRM.Text = objSecao.NOME_GESTOR_RM;

            //aponta o combo para a superior selecionada.
            if (objSecao.COD_SECAO_SUP != null)
            {
                cboSecaoSuperior.SelectedItem = cboSecaoSuperior.Items.FindByValue(objSecao.COD_SECAO_SUP);

            }


            this.txtDiretoria.Text = objSecao.DIRETORIA;

            this.chkNomeModificado.Value = objSecao.NOME_MOD != null && objSecao.NOME_MOD.Equals("1");
            this.txtNomeModificado.Text = (this.chkNomeModificado.Checked) ? objSecao.NOME_MODIFICADO : string.Empty;
            this.txtCodSecaoSuperior.Text = objSecao.COD_SECAO_SUP;


            //se for gestor modificado 
            if (objSecao.GESTOR_MOD != null && objSecao.GESTOR_MOD.Equals("1"))
            {
                this.chkGestorModificado.Value = true;
                this.cboGestorModificado.SelectedItem = this.cboGestorModificado.Items.FindByValue(objSecao.CPF_GESTOR_MOD);
                this.cboGestorModificado.ReadOnly = false;
            }
            else
            {
                this.cboGestorModificado.ReadOnly = true;
                this.cboGestorModificado.SelectedIndex = -1;
            }


            //se não possuir PAI, limpa o combos 
            if (objSecao.POSSUI_SUPERIOR != null && objSecao.POSSUI_SUPERIOR.Equals("1"))
            {
                this.txtCodSecaoSuperior.Text = string.Empty;
                this.cboSecaoSuperior.SelectedIndex = -1;
                this.cboSecaoSuperior.ReadOnly = true;
                this.chkPossuiDiretoria.Checked = true;

            }

            this.chkPublico.Checked = objSecao.PUBLICO != null && objSecao.PUBLICO.Equals("1");
            this.cboSecaoSuperior.ReadOnly = this.chkPublico.Checked;



        }








        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("wfmSecoes.aspx?fe={0}", Request.QueryString["fe"]));


        }


        protected void chkNomeModificado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNomeModificado.Checked)
            {
                this.txtNomeModificado.ReadOnly = false;
            }
            else
            {
                this.txtNomeModificado.Text = string.Empty;
                this.txtNomeModificado.ReadOnly = true;

            }
        }

        protected void chkGestorModificado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGestorModificado.Checked)
            {
                this.cboGestorModificado.ReadOnly = false;
            }
            else
            {
                this.cboGestorModificado.ReadOnly = true;
                this.cboGestorModificado.SelectedIndex = -1;
            }
        }

        protected void cboSecaoSuperior_SelectedIndexChanged(object sender, EventArgs e)
        {

            //mudou a secao superior, atualizar no objeto e rebindar os campos.
            Negocio.ORG_LISTA_SECOES objSecao = ViewState["objSecao"] as Negocio.ORG_LISTA_SECOES;


            objSecao.COD_SECAO_SUP = cboSecaoSuperior.SelectedItem.Value.ToString();
            objSecao.NOME_SECAO_SUP = cboSecaoSuperior.SelectedItem.Text.ToString();
            //setar o cod da empresa 
            objSecao.COD_EMPRESA_SUP = Convert.ToInt32(objSecao.COD_SECAO_SUP.Substring(0, 2)).ToString();


            this.txtCodSecaoSuperior.Text = objSecao.COD_SECAO_SUP;


            //atualiza o viestate.
            ViewState["objSecao"] = objSecao;


            CarregaGestores();


        }

        protected void chkPossuiDiretoria_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPossuiDiretoria.Checked)
            {
                this.cboSecaoSuperior.SelectedIndex = -1;
                this.txtCodSecaoSuperior.Text = string.Empty;

                
            }

            this.cboSecaoSuperior.ReadOnly = chkPossuiDiretoria.Checked;

            //atualiza  viewstate com a decisão do usuário

            //carrega a seção do viewstate.
            Negocio.ORG_LISTA_SECOES objSecao = ViewState["objSecao"] as Negocio.ORG_LISTA_SECOES;
            objSecao.POSSUI_SUPERIOR = chkPossuiDiretoria.Checked ? "1" : "0";
            objSecao.COD_SECAO_SUP = "";
            CarregaGestores();

        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool erro = false;
            string strErro = string.Empty;


            //validar os campos
            if (chkNomeModificado.Checked && txtNomeModificado.Text.Length < 3)
            {
                erro = true;
                strErro += @"O nome modificado não foi informado! <br>";
            }


            if (cboGestorModificado.SelectedItem != null && !chkGestorModificado.Checked)
            {
                erro = true;
                strErro = "Você deve selecionar um gestor modificado ou desmarcar a opção \" Gestor Modificado \"<br>";
            }

            if (chkGestorModificado.Checked && cboGestorModificado.SelectedItem == null)
            {
                erro = true;
                strErro += @"O gestor modificado precisa ser informado! <br>";

            }

            if (cboSecaoSuperior.SelectedItem == null & !chkPossuiDiretoria.Checked)
            {
                erro = true;
                strErro = @"Você precisa selecionar uma seção superior";
            }

            if (erro)
            {
                lblErro.Visible = true;
                lblErro.Text = "Não foi possível salvar <br> " + strErro;
                return;
            }
            else
            {
                lblErro.Visible = false;
            }


            //gravar



            //carrega a seção do viewstate.
            Negocio.ORG_LISTA_SECOES objSecao = ViewState["objSecao"] as Negocio.ORG_LISTA_SECOES;



            objSecao.GESTOR_MOD = chkGestorModificado.Checked ? "1" : "0";
            objSecao.CPF_GESTOR_MOD = chkGestorModificado.Checked ?
                cboGestorModificado.SelectedItem.Value.ToString() : string.Empty;


            objSecao.NOME_MOD = chkNomeModificado.Checked ? "1" : "0";
            objSecao.NOME_MODIFICADO = chkNomeModificado.Checked ?
                txtNomeModificado.Text : string.Empty;


            //consertar 
            objSecao.POSSUI_SUPERIOR = chkPossuiDiretoria.Checked ? "1" : "0";

            if (objSecao.POSSUI_SUPERIOR.Equals("1"))
            {
                objSecao.COD_SECAO_SUP = string.Empty;
                objSecao.COD_EMPRESA_SUP = string.Empty;

            }



            objSecao.PUBLICO = chkPublico.Checked ? "1" : "0";



            Negocio.SecaoManager objMngn = new Negocio.SecaoManager();

            if (objMngn.AtualizaSecao(objSecao))
            {
                Util.Avisos.Aviso("Seção salva!", string.Format( "wfmSecoes.aspx?fe={0}",  Request.QueryString["fe"])  , this.Page);
            }
            else
            {
                Util.Avisos.Aviso(string.Format("Não foi possível salvar a seção o erro foi: {0}", objMngn.Erro), this.Page);
                this.lblErro.Text = string.Format("Não foi possível salvar a seção o erro foi: {0}", objMngn.Erro);


            }




        }





    }
}