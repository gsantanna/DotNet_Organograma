using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using Util;

namespace WEB
{
    public partial class wfmSubsecaoEdit : System.Web.UI.Page
    {

        string Empresa;

        protected void Page_Load(object sender, EventArgs e)
        {

            //... poe o menu laranja
            DevExpress.Web.ASPxMenu.ASPxMenu menuPrincipal = Master.FindControl("mnuPrincipal") as DevExpress.Web.ASPxMenu.ASPxMenu;
            menuPrincipal.Items[2].Selected = true;



            if (Session["EMPRESA"] == null || string.IsNullOrEmpty(Session["EMPRESA"].ToString())) Response.Redirect("default.aspx");
            Empresa = Session["EMPRESA"].ToString();
            
         
          
            

            if (!IsPostBack)
            {
                CarregaSubsecao();  //carrega as tabelas base para a viewstate
                CarregaSessoes(); //popula o combo de sessão pai e o de edição
                CarregaGestores();//popula o combo de gestores com todos os gestores disponíveis e o gestor atual
              
               

            }


            //binda os combos e grids
            BindaCombos();

        }




        //deve ser executado uma unica vez para trazer os dados base da subseção
        private void CarregaSubsecao()
        {

            //cria o contexto 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            string id_hierarquia = Request.QueryString["id_subsecao"];
            
            //Carrega o item que está sendo editado.
            Negocio.ORG_HIERARQUIA objHierarquia = (from h in objDs.ORG_HIERARQUIA
                                                    where h.COD_SECAO.Equals(id_hierarquia) 
                                                    select h).FirstOrDefault();

            //se o item não foi encontrado sai da pagina 
            if (objHierarquia == null) Response.Redirect("wfmSubsecoes.aspx?erro=SUBSECAO NAO LOCALIZADA");

            //adiciona o objeto na viewstate para ser manipulado no grid
            ViewState.Add("HIERARQUIA", objHierarquia);

            //adiciona os funcionários alocados na viewstate.
            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objFuncionarioAlocado =
                (from f in objDs.ORG_FUNCIONARIO_ALOCADO
                 where f.COD_SECAO.Equals(objHierarquia.COD_SECAO) && f.COD_EMPRESA.Equals(objHierarquia.COD_EMPRESA)
                 select f).ToList<Negocio.ORG_FUNCIONARIO_ALOCADO>();

            ViewState.Add("SELECIONADOS", objFuncionarioAlocado);



            this.txtSubsecao.Text = objHierarquia.NOME;
            this.txtCodigo.Text = objHierarquia.COD_SECAO;

            //grava no viewstate os valores iniciais de seção superior e código
            ViewState.Add("COD_SECAO_SUP_INICIAL", objHierarquia.COD_SECAO_SUP);
            ViewState.Add("COD_EMPRESA_SUP_INICIAL", objHierarquia.COD_EMPRESA_SUP);

            ViewState.Add("COD_SECAO_INICIAL", objHierarquia.COD_SECAO);
            ViewState.Add("COD_EMPRESA_INICIAL", objHierarquia.COD_EMPRESA);




        }

        //deve ser executado em toda mudança do combobox de seção superior 
        private void CarregaSessoes()
        {

             //traz a hierarquia 
            Negocio.ORG_HIERARQUIA objHierarquia = (ViewState["HIERARQUIA"] as Negocio.ORG_HIERARQUIA);
           



            //cria o contexto 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            //Carrega os combos de seções superiores
            var objSecoesSup = (from s in objDs.ORG_LISTA_SECOES_SIMPLES
                                where s.COD_EMPRESA.Equals(Empresa) || s.PUBLICO=="1"
                                select s).Distinct();


            this.cboSecaoSuperior.DataSource = objSecoesSup.OrderBy(f=>f.NOME_SECAO);
            this.cboSecaoSuperior.TextField = "NOME_SECAO";
            this.cboSecaoSuperior.ValueField = "COD_SECAO";
            this.cboSecaoSuperior.DataBind();

           


        }

        private void CarregaGestores()
        {

            //traz a hierarquia 
            Negocio.ORG_HIERARQUIA objHierarquia = (ViewState["HIERARQUIA"] as Negocio.ORG_HIERARQUIA);
            



            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();


            var Gestores = (from g in objDs.ORG_FUNCIONARIO_DISPONIVEL_SUB
                            where g.COD_SECAO.Equals(objHierarquia.COD_SECAO_SUP)
                            select g).Distinct().ToList<Negocio.ORG_FUNCIONARIO_DISPONIVEL_SUB>();



            var Funcionarios = (from f in objDs.ORG_FUNCIONARIO_ALOCADO
                                where
                                  f.COD_SECAO.Equals( objHierarquia.COD_SECAO_SUP)
                                select f).Distinct().ToList<Negocio.ORG_FUNCIONARIO_ALOCADO>();





            //cria a tabela local de funcionarios
            ViewState.Add("FUNCIONARIOS", Funcionarios);
            ViewState.Add("GESTORES", Gestores);







        }













        //executado em todas as mudanças do grid pois caso o usuario adicione 
        //um funcionario alocado na seção, ele passa automaticamente 
        //a estar habilitado como gestor da mesma
        private void BindaCombos()
        {

            //traz a hierarquia 
            Negocio.ORG_HIERARQUIA objHierarquia = (ViewState["HIERARQUIA"] as Negocio.ORG_HIERARQUIA);
            //traz os funcionarios 

            //carrega a lista de funcionários disponíveis 
            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objFuncionarios =
                (ViewState["FUNCIONARIOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);

            
            //Carrega a lista de gestores disponíveis 
            List<Negocio.ORG_FUNCIONARIO_DISPONIVEL_SUB> objGestores =
                (ViewState["GESTORES"] as List<Negocio.ORG_FUNCIONARIO_DISPONIVEL_SUB>);



            //carrega a lista de funcionários alocados 
            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objAlocados =
                (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);



            //Carrega a seção superior 
            if (!IsPostBack)
            {
                this.cboSecaoSuperior.SelectedItem = cboSecaoSuperior.Items.FindByValue(objHierarquia.COD_SECAO_SUP);
                this.txtCodSuperior.Text = objHierarquia.COD_SECAO_SUP;
               
            }
            //preenche a diretoria

            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            var diretoria = (from d in objDs.ORG_LISTA_SECOES
                             where d.COD_SECAO == txtCodSuperior.Text 
                             select d.DIRETORIA).FirstOrDefault();

            txtDiretoria.Text = diretoria != null ? diretoria : string.Empty;





            //Carrega a lista de gestores e binda.
            //alterado dia 25/02/2012 - Adicionado o orderby
            var dsGestores = 
            (from g in (ViewState["GESTORES"] as List<Negocio.ORG_FUNCIONARIO_DISPONIVEL_SUB>)
             select new { NOME = g.NOME, CPF = g.CPF }).Distinct().OrderBy(f=>f.NOME);
            
            

            this.cboGestor.DataSource = dsGestores;
            this.cboGestor.TextField = "NOME";
            this.cboGestor.ValueField = "CPF";
            
            this.cboGestor.DataBind();
            this.cboGestor.Items.Insert(0, new ListEditItem("(VAZIO)", string.Empty));

            

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(objHierarquia.GESTOR))
                {
                    
                }
                else
                {
                    this.cboGestor.SelectedItem = this.cboGestor.Items.FindByValue(objHierarquia.GESTOR);
                }
            }


            //bind o grid com os funcionários alocados 
            this.dGridAlocados.DataSource = objAlocados;
            dGridAlocados.DataBind();

            //binda o grid com os funcionários disponíveis 
            //e que ainda não foram alocados

            //binda o grid excluindo os já alocados.
            var dGridDispDs = from k in objFuncionarios
                          where (objAlocados.Where(f => f.CPF.Equals(k.CPF)).Count()) == 0
                          select k;


            this.dGridDisp.DataSource = dGridDispDs;
            this.dGridDisp.DataBind();



            



        }





        #region Controles

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            
            bool erro = false;
            string strErro = string.Empty;
            //cria o contexto de dados 
            Negocio.SubSecaoManager objSub = new Negocio.SubSecaoManager();



            //valida campos.
            if (txtSubsecao.Text.Length < 3)
            {
                erro = true;
                strErro += "Você precisa preencher o nome da subseção";

            }


            if (erro)
            {
                lblErro.Text = strErro;
                lblErro.Visible = true;
                return;
            }
            else
            {
                lblErro.Visible = false;
            }




            //monta os dados para ATUALIZAÇÃO  (Hierarquia) -cadastro subseção
            Negocio.ORG_HIERARQUIA objHierarquia = new Negocio.ORG_HIERARQUIA();
            objHierarquia.COD_EMPRESA = Convert.ToInt32(this.txtCodigo.Text.Substring(0, 2)).ToString();
            objHierarquia.COD_SECAO = this.txtCodigo.Text;
            objHierarquia.NOME = txtSubsecao.Text;
            objHierarquia.NOME_MOD = "1";
            objHierarquia.COD_EMPRESA_SUP = objHierarquia.COD_EMPRESA;
            objHierarquia.COD_SECAO_SUP = cboSecaoSuperior.SelectedItem.Value.ToString();
            objHierarquia.GESTOR_MOD = "1";
            objHierarquia.GESTOR = cboGestor.SelectedItem != null ? cboGestor.SelectedItem.Value.ToString() : string.Empty;
            objHierarquia.SUBSECAO = "1";

            //monta os dados para inclusão (Funcionarios)
            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objFuncionarios =
           (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);


            if (objSub.AtualizaSubsecao(  ViewState["COD_SECAO_INICIAL"].ToString(),  ViewState["COD_EMPRESA_INICIAL"].ToString(), objHierarquia, objFuncionarios))
            {
                Util.Avisos.Aviso("Subseção alterada!", string.Format("wfmSubsecoes.aspx?fe={0}", Request.QueryString["fe"]) , this.Page);

            }
            else
            {
                Util.Avisos.Aviso(string.Format("Erro ao alterar a subseção o erro foi: {0}", objSub.Erro), this.Page);
            }

        }




        protected void btnAdicionar_Click(object sender, EventArgs e)
        {

            if (dGridDisp.Selection.Count <= 0) return;



            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objAlocado =
               (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);

            List<Object> objSelecao = dGridDisp.GetSelectedFieldValues(new string[] { "CPF", "NOME" });


            //para cada item adicionado, adicionar na tabela de selecionados.
            foreach (object o in objSelecao)
            {

                //verificar se já existe antes de adicionar 
                string CPF = (o as object[])[0].ToString();
                string NOME = (o as object[])[1].ToString();

                if (objAlocado.Where(f => f.CPF.Equals(CPF)).FirstOrDefault() == null)
                {

                    Negocio.ORG_FUNCIONARIO_ALOCADO fa = new Negocio.ORG_FUNCIONARIO_ALOCADO();
                    //carrega os campos 
                    fa.CPF = CPF;
                    fa.NOME = NOME;
                    fa.COD_SECAO = this.txtCodigo.Text;
                    fa.COD_EMPRESA = Convert.ToInt32(this.txtCodigo.Text.Substring(0, 2)).ToString();
                    fa.COD_SECAO_SUP = cboSecaoSuperior.SelectedItem.Value.ToString();
                    fa.COD_EMPRESA_SUP = fa.COD_EMPRESA;
                    objAlocado.Add(fa);

                }

            }


            //atualiza o viewstate com a tabela adicionada 
            ViewState["SELECIONADOS"] = objAlocado;

            BindaCombos();





        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {




            if (dGridAlocados.Selection.Count <= 0) return;



            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objAlocado =
               (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);

            List<Object> objSelecao = dGridAlocados.GetSelectedFieldValues(new string[] { "CPF", "NOME" });


            //para cada item adicionado, adicionar na tabela de selecionados.
            foreach (object o in objSelecao)
            {

                //verificar se já existe antes de adicionar 
                string CPF = (o as object[])[0].ToString();
                objAlocado.RemoveAll(f => f.CPF.Equals(CPF));

            }


            //atualiza o viewstate com a tabela adicionada 
            ViewState["SELECIONADOS"] = objAlocado;

            BindaCombos();











        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("wfmSubsecoes.aspx?fe={0}", Request.QueryString["fe"]));


        }


        
        protected void cboSecaoSuperior_SelectedIndexChanged(object sender, EventArgs e)
        {

            //atualiza o objeto da viewstate.
            Negocio.ORG_HIERARQUIA objHierarquia = (ViewState["HIERARQUIA"] as Negocio.ORG_HIERARQUIA);
                objHierarquia.COD_SECAO_SUP = cboSecaoSuperior.SelectedItem.Value.ToString();
                objHierarquia.COD_EMPRESA_SUP = Convert.ToInt32(cboSecaoSuperior.SelectedItem.Value.ToString().Substring(0, 2)).ToString();




            this.txtCodSuperior.Text = cboSecaoSuperior.SelectedItem.Value.ToString();


            
            //cria o contexto 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            //carrega as subseções com a mesma seção superior  /p gerar o codigo automaticamente
            var subsecoes = (from s in objDs.ORG_HIERARQUIA
                             where s.COD_SECAO_SUP == objHierarquia.COD_SECAO_SUP && s.SUBSECAO.Equals("1")
                             select s).OrderByDescending(f => f.COD_SECAO).FirstOrDefault();

            if (objHierarquia.COD_SECAO_SUP == ViewState["COD_SECAO_SUP_INICIAL"].ToString())
            {
                //não mudou, não precisa re-gerar nenhum código. 
                txtCodigo.Text = objHierarquia.COD_SECAO;
            }
            else //comportamento padrão para mudar o código 
            {

                if (subsecoes == null) //primeira subseção
                {
                    txtCodigo.Text = cboSecaoSuperior.SelectedItem.Value + ".01";

                }
                else //acrescentar uma subceção
                {
                    txtCodigo.Text = txtCodSuperior.Text + "." +
                      (Convert.ToInt32(subsecoes.COD_SECAO.Substring(subsecoes.COD_SECAO.Length - 2)) + 1).ToString().PadLeft(2, '0');

                }
            }




            //preenche a diretoria 

            var diretoria = (from d in objDs.ORG_LISTA_SECOES
                             where d.COD_SECAO == objHierarquia.COD_SECAO_SUP
                             select d.DIRETORIA).FirstOrDefault();

            txtDiretoria.Text = diretoria != null ? diretoria : string.Empty;

            ViewState["HIERARQUIA"] = objHierarquia;

            CarregaGestores();
            BindaCombos();

            /*
            //atualiza os GRIS de Pessoas disponíveis pra ser gestor 
            CarregaGestores();


            //atualiza os Grids e todo resto 
            BindaCombos();
             */




           
        }
        

        protected void cboGestor_SelectedIndexChanged(object sender, EventArgs e)
        {

            //atualiza o gestor no obj atual 
            //traz a hierarquia 
            Negocio.ORG_HIERARQUIA objHierarquia = (ViewState["HIERARQUIA"] as Negocio.ORG_HIERARQUIA);
            objHierarquia.GESTOR = cboGestor.SelectedItem.Value.ToString();

            ViewState["HIERARQUIA"] = objHierarquia;

        }


        #endregion

       




    }




} //fim do namespace