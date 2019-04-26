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
    public partial class wfmSubsecaoAdd_____ : System.Web.UI.Page
    {
        bool ModoEdicao = false;
         
        protected void Page_Load(object sender, EventArgs e)
        {
            ModoEdicao = (Request.QueryString["edicao"].Equals("true"));


            if (!IsPostBack)
            {
                CarregaSessoes(); //popula o combo de sessão pai
            }


            //binda os combos e grids
            BindaCombos();

        }


        //executado em todas as mudanças do grid pois caso o usuario adicione 
        //um funcionario alocado na seção, ele passa automaticamente 
        //a estar habilitado como gestor da mesma
        private void BindaCombos()
        {

            //carrega os funcionarios já alocados 
            if (ViewState["SELECIONADOS"] != null)
            {
                this.dGridAlocados.DataSource = (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);
                this.dGridAlocados.DataBind();
            }




            if (ViewState["FUNCIONARIOS"] != null)
            {
                //combo de gestores --retirar os duplicados 
                var cboGestorDS = (from g in (ViewState["FUNCIONARIOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>)
                                   select   new { NOME= g.NOME,  CPF= g.CPF }).Distinct();


                this.cboGestor.DataSource = cboGestorDS;
                this.cboGestor.TextField = "NOME";
                this.cboGestor.ValueField = "CPF";
                this.cboGestor.DataBind();


                //carrega os funcionarios já alocados 
                List<Negocio.ORG_FUNCIONARIO_ALOCADO> objAlocado = 
                    (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);



                //binda o grid excluindo os já alocados.
                 var dGridDS = from k in cboGestorDS
                              where   (objAlocado.Where(f => f.CPF.Equals(k.CPF)).Count())==0
                              select k;


                this.dGridDisp.DataSource = dGridDS;
                this.dGridDisp.DataBind();


            }



        }

        //deve ser executado em toda mudança do combobox de seção superior 
        private void CarregaSessoes()
        {

            //cria o contexto 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();


            //Carrega os combos de seções superiores
            var objSecoesSup = (from s in objDs.ORG_LISTA_SECOES_SIMPLES
                                select s).Distinct();


            this.cboSecaoSuperior.DataSource = objSecoesSup;
            this.cboSecaoSuperior.TextField = "NOME_SECAO";
            this.cboSecaoSuperior.ValueField = "COD_SECAO";
            this.cboSecaoSuperior.DataBind();


      

        }


        //Carrega funcionarios diponíveis! e limpa a tabela funcionarios alocados.
        private void CarregaGestores()
        {
            //cria o contexto 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            

            var Gestores = (from g in objDs.ORG_FUNCIONARIO_ALOCADO
                            where (g.COD_SECAO.Equals( txtCodSuperior.Text)) ||
                             (g.COD_SECAO_SUP == txtCodSuperior.Text) ||
                             (g.COD_SECAO == txtCodigo.Text)
                            select  g).Distinct().ToList<Negocio.ORG_FUNCIONARIO_ALOCADO>();


            //cria a tabela local de funcionarios
            ViewState.Add("FUNCIONARIOS", Gestores);


            //cria a tabela local de selecionados
            ViewState.Add("SELECIONADOS", new List<Negocio.ORG_FUNCIONARIO_ALOCADO>());



            this.cboGestor.SelectedIndex = -1;



           


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

            
            
            
            //monta os dados para inclusão  (Hierarquia) -cadastro subseção
            Negocio.ORG_HIERARQUIA objHierarquia = new Negocio.ORG_HIERARQUIA();
            objHierarquia.COD_EMPRESA = Convert.ToInt32(this.txtCodigo.Text.Substring(0, 2)).ToString();
            objHierarquia.COD_SECAO = this.txtCodigo.Text;
            objHierarquia.NOME = txtSubsecao.Text;
            objHierarquia.COD_EMPRESA_SUP = objHierarquia.COD_EMPRESA;
            objHierarquia.COD_SECAO_SUP = cboSecaoSuperior.SelectedItem.Value.ToString();
            objHierarquia.GESTOR_MOD = "1";
            objHierarquia.GESTOR =  cboGestor.SelectedItem !=null ?  cboGestor.SelectedItem.Value.ToString(): string.Empty ;
            objHierarquia.SUBSECAO = "1";

            //monta os dados para inclusão (Funcionarios)
            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objFuncionarios =
           (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);


            if (objSub.AdicionaOuRecriaSubsecao(objHierarquia, objFuncionarios))
            {
                Util.Avisos.Aviso("Subseção criada!", "wfmSubsecoes.aspx", this.Page);

            }
            else
            {
                Util.Avisos.Aviso( string.Format( "Erro ao criar a subseção o erro foi: {0}", objSub.Erro  ), this.Page);
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




            if ( dGridAlocados.Selection.Count <= 0) return;



            List<Negocio.ORG_FUNCIONARIO_ALOCADO> objAlocado =
               (ViewState["SELECIONADOS"] as List<Negocio.ORG_FUNCIONARIO_ALOCADO>);

            List<Object> objSelecao = dGridAlocados.GetSelectedFieldValues(new string[] { "CPF","NOME" });


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
            Response.Redirect("wfmSubsecoes.aspx");


        }

        protected void cboSecaoSuperior_SelectedIndexChanged(object sender, EventArgs e)
        {


            this.txtCodSuperior.Text = cboSecaoSuperior.SelectedItem.Value.ToString();


            //cria o contexto 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();
            
            //carrega as subseções com a mesma seção superior  /p gerar o codigo automaticamente
            var subsecoes = (from s in objDs.ORG_HIERARQUIA
                             where s.COD_SECAO_SUP== txtCodSuperior.Text && s.SUBSECAO.Equals("1")
                             select s).OrderByDescending(f => f.COD_SECAO ).FirstOrDefault();


            if (subsecoes == null) //primeira subseção
            {
                txtCodigo.Text = cboSecaoSuperior.SelectedItem.Value + ".01";

            }
            else //acrescentar uma subceção
            {
                txtCodigo.Text = txtCodSuperior.Text + "." +
                  (Convert.ToInt32(subsecoes.COD_SECAO.Substring(subsecoes.COD_SECAO.Length - 2)) + 1).ToString().PadLeft(2,'0');
               
            }


            //preenche a diretoria 

               var diretoria =  (from d in objDs.ORG_LISTA_SECOES
                             where d.COD_SECAO == txtCodSuperior.Text
                             select d.DIRETORIA).FirstOrDefault();

               txtDiretoria.Text = diretoria != null ? diretoria : string.Empty;


       

            //atualiza os GRIS de Pessoas disponíveis pra ser gestor 
            CarregaGestores();


            //atualiza os Grids e todo resto 
            BindaCombos();



        }


#endregion



    } //fim da classe


   





} //fim do namespace