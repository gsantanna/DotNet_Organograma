using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq.Expressions;
using System.Data.Linq;
using System.Data;


namespace WEB.Controles
{

    public partial class FiltroRelatoriosSecao : System.Web.UI.UserControl 
    {

        #region propriedades

       
        /// <summary>
        /// Lista das seções selecionadas no controle
        /// </summary>
        public List<SECOES> SecoesSelecionadas
        {
            get
            {
                if (ViewState["SECOES_SEL"] == null)
                {
                    return new List<SECOES>();
                }
                else
                {
                    return (ViewState["SECOES_SEL"] as List<SECOES>);
                }
            }

        }

        /// <summary>
        /// Lista as empresas selecionadas no controle
        /// </summary>
        public List<Negocio.ORG_EMPRESA> EmpresasSelecionadas
        {
            get
            {
                if (ViewState["EMPRESA_SEL"] == null)
                {
                    return new List<Negocio.ORG_EMPRESA>();
                }
                else
                {
                    return (ViewState["EMPRESA_SEL"] as List<Negocio.ORG_EMPRESA>);
                }
            }

        }

        private bool _IncluirDiretorias = true;
        private bool _IncluirSecoes = true;
        private bool _SomenteSuperiores = false;


        /// <summary>
        /// Inclui ou não as seções (Default: SIM)
        /// </summary>
        public bool IncluirSecoes
        {
            get { return _IncluirSecoes; }
            set { _IncluirSecoes = value; }
        }

        /// <summary>
       /// Inclui ou não as diretorias (Default: SIM)
       /// </summary>
        public bool IncluirDiretorias
        {
            get { return _IncluirDiretorias; }
            set { _IncluirDiretorias = value; }
        }

        /// <summary>
        /// Somente exibe seções superiores
        /// </summary>
        public bool SomenteSuperiores
        {
            get { return _SomenteSuperiores; }
            set { _SomenteSuperiores = value; }

        }
        

        #endregion

        #region Métodos 


        public void Limpar()
        {
            ViewState["EMPRESA"] = null;
            ViewState["EMPRESA_SEL"] = null;
            ViewState["SECOES_DISP"] = null;
            ViewState["SECOES_SEL"] = null;


            //recarrega as empresas
            CarregaEmpresas();
            BindaGridEmpresas();



            //rebinda os grids 
            this.dGridEmpresasSel.DataSource = new List<Negocio.ORG_EMPRESA>();
            this.dGridEmpresasSel.DataBind();

            this.dGridSecoesSel.DataSource = new List<SECOES>();
            this.dGridSecoesSel.DataBind();
            this.dGridSecoesDisp.DataSource = new List<SECOES>();
            this.dGridSecoesDisp.DataBind();




            this.dGridEmpresasDisp.FilterExpression = string.Empty;
            this.dGridEmpresasSel.FilterExpression = string.Empty;
            this.dGridSecoesDisp.FilterExpression = string.Empty;
            this.dGridSecoesSel.FilterExpression = string.Empty;
           




        }

        /// <summary>
        /// Limpa Seleção
        /// </summary>
        private void LimparSelecao()
        {
            this.dGridEmpresasDisp.Selection.UnselectAll();
            this.dGridEmpresasSel.Selection.UnselectAll();
            this.dGridSecoesDisp.Selection.UnselectAll();
            this.dGridSecoesSel.Selection.UnselectAll();
        }


        public bool PossuiSelecao()
        {
            return (SecoesSelecionadas.Count() +  EmpresasSelecionadas.Count()) > 0;

        }


       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaEmpresas();
            }


            BindaGridEmpresas();
            BindaGridSecoes();

        }
      
        
        #endregion

        #region Empresas
     
        private void CarregaEmpresas()
        {
            //Cria o contexto de dados 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            //cria o obj de saída 
            List<Negocio.ORG_EMPRESA> objEmp = objDs.ORG_EMPRESA.ToList();

            //Grava a tabela na seção
            ViewState.Add("EMPRESA", objEmp);

            //Grava a tabela de selecionados na seção
            List<Negocio.ORG_EMPRESA> objEmpSel = new List<Negocio.ORG_EMPRESA>();
            ViewState.Add("EMPRESA_SEL", objEmpSel);


            
        }

        private void BindaGridEmpresas()
        {

                //carrega a lista
                List<Negocio.ORG_EMPRESA> emp = (ViewState["EMPRESA"] as List<Negocio.ORG_EMPRESA>);

                //binda no grid 
                dGridEmpresasDisp.DataSource = emp;
                dGridEmpresasDisp.DataBind();


                //carrega a lista
                List<Negocio.ORG_EMPRESA> emp_Sel = (ViewState["EMPRESA_SEL"] as List<Negocio.ORG_EMPRESA>);
                dGridEmpresasSel.DataSource = emp_Sel;
                dGridEmpresasSel.DataBind();


        }

        protected void btnEmpAdd_Click(object sender, EventArgs e)
        {
            //caso seja o botão adicionar todos seleciona todos antes da execução
            if ((sender as DevExpress.Web.ASPxEditors.ASPxButton).CommandArgument.Equals("TODOS"))
                dGridEmpresasDisp.Selection.SelectAll();

            
            if ( dGridEmpresasDisp.Selection.Count <= 0) return; //nenhum item selecionado.


            //Carrega as empresas da ViewState.
            List<Negocio.ORG_EMPRESA> Empresas_Sel = (ViewState["EMPRESA_SEL"] as List<Negocio.ORG_EMPRESA>);
            List<Negocio.ORG_EMPRESA> Empresas = (ViewState["EMPRESA"] as List<Negocio.ORG_EMPRESA>);



            //Carrega os itens selecionados
            List<Object> objSelecao = dGridEmpresasDisp.GetSelectedFieldValues(new string[] { "COD_EMPRESA", "DESCRICAO" });
            

            //para cada item selecionado 
            foreach (object o in objSelecao)
            {
                //Se foi adicionado deleta o existente do destino
                Empresas_Sel.RemoveAll(f => f.COD_EMPRESA.Equals((o as object[])[0].ToString()));
                //deleta o ítem da origem 
                Empresas.RemoveAll(f => f.COD_EMPRESA.Equals((o as object[])[0].ToString()));
                
                //adiciona a empresa
                Negocio.ORG_EMPRESA emp = new Negocio.ORG_EMPRESA();
                emp.COD_EMPRESA =  (o as object[])[0].ToString();
                emp.DESCRICAO =  (o as object[])[1].ToString();
                Empresas_Sel.Add(emp);

            }


            //atualiza os grids
            BindaGridEmpresas();

            //Atualiza o obj das seções
            CarregaSecoes();

            LimparSelecao();

        }

        protected void btnEmpDel_Click(object sender, EventArgs e)
        {


            //caso seja o botão adicionar todos seleciona todos antes da execução
            if ((sender as DevExpress.Web.ASPxEditors.ASPxButton).CommandArgument.Equals("TODOS"))
                dGridEmpresasSel.Selection.SelectAll();


            if (dGridEmpresasSel.Selection.Count <= 0) return; //nenhum item selecionado.


            //Carrega as empresas da ViewState.
            List<Negocio.ORG_EMPRESA> Empresas_Sel = (ViewState["EMPRESA_SEL"] as List<Negocio.ORG_EMPRESA>);
            List<Negocio.ORG_EMPRESA> Empresas = (ViewState["EMPRESA"] as List<Negocio.ORG_EMPRESA>);




            //Carrega os itens selecionados
            List<Object> objSelecao = dGridEmpresasSel.GetSelectedFieldValues(new string[] { "COD_EMPRESA", "DESCRICAO" });








            //para cada item selecionado 
            foreach (object o in objSelecao)
            {
                //Se foi adicionado deleta o existente do destino
                Empresas_Sel.RemoveAll(f => f.COD_EMPRESA.Equals((o as object[])[0].ToString()));
                //deleta o ítem da origem 
                Empresas.RemoveAll(f => f.COD_EMPRESA.Equals((o as object[])[0].ToString()));

                //adiciona a empresa
                Negocio.ORG_EMPRESA emp = new Negocio.ORG_EMPRESA();
                emp.COD_EMPRESA = (o as object[])[0].ToString();
                emp.DESCRICAO = (o as object[])[1].ToString();
                Empresas.Add(emp);

            }


            //atualiza os grids
            BindaGridEmpresas();
            //Atualiza o obj das seções
            CarregaSecoes();

            LimparSelecao();

        }
        
        #endregion

        #region sessoes

        private void CarregaSecoes()
        {
            //carrega as empresas selecionadas
            List<Negocio.ORG_EMPRESA> Empresas_Sel = (ViewState["EMPRESA_SEL"] as List<Negocio.ORG_EMPRESA>);
            

            //Carrega as seções de acordo com as empresas selecionadas.
           
            //cria o contexto de dados 
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            //carrega a lista de seções disponíveis cruzando com a seleção do grid
            var objSecoesDisp = (from s in objDs.ORG_LISTA_SECOES_SIMPLES.ToList<Negocio.ORG_LISTA_SECOES_SIMPLES>()
                                 join ep in Empresas_Sel on s.COD_EMPRESA equals ep.COD_EMPRESA
                                 where   ((s.DIRETORIA.Equals( IncluirDiretorias ? "1" : "0")) ||
                                         (s.DIRETORIA.Equals( IncluirSecoes ? "0" : "1"))) &&
                                         (objDs.ORG_HIERARQUIA.Where(f=>f.COD_SECAO_SUP.Equals(s.COD_SECAO)).Count()) >  ( SomenteSuperiores? 0 : -1)
                                 select new SECOES { COD_EMPRESA = s.COD_EMPRESA, COD_SECAO = s.COD_SECAO, EMPRESA=ep.DESCRICAO, NOME_SECAO= s.NOME_SECAO+ " " + s.COD_SECAO    }).ToList();

            //adiciona a lista de seções disponíveis (seções das empresas selecionadas) na memória
            ViewState.Add("SECOES_DISP", objSecoesDisp);







            //chama a rotina /p bindar o grid
            BindaGridSecoes();

            

        }


        private void BindaGridSecoes()
        {
            //caso não tenha nenhuma seção carregada (primeiro load).
            if (ViewState["SECOES_DISP" ] == null) return;


            //carrega a lista de seções disponíveis e selecionadas da memória (Viewstate)            
            List<SECOES> objSecoesDisp = ViewState["SECOES_DISP"] as List<SECOES>;            
            List<SECOES> objSecoesSel = ViewState["SECOES_SEL"] as List<SECOES>;

            

            //binda o grid de disponíveis 
            this.dGridSecoesDisp.DataSource = objSecoesDisp;
            this.dGridSecoesDisp.DataBind();

            
            //binda o grid e selecionadas caso tenha alguma informação
            if( objSecoesDisp!=null)
            {
                this.dGridSecoesSel.DataSource = objSecoesSel;
                this.dGridSecoesSel.DataBind();
            }



        }

        
        protected void btnSecoesADD_Click(object sender, EventArgs e)
        {

            //caso seja o botão adicionar todos seleciona todos antes da execução
            if ((sender as DevExpress.Web.ASPxEditors.ASPxButton).CommandArgument.Equals("TODOS"))
                dGridSecoesDisp.Selection.SelectAll();



            if ( dGridSecoesDisp.Selection.Count <= 0) return; //nenhum item selecionado.


            //verifica se a ViewState com as seções selecionadas existe, caso não exista cria uma nova

            if (ViewState["SECOES_SEL"] == null)            
                ViewState.Add("SECOES_SEL", new List<SECOES>());


            //carrega a lista de disponíveis e selecionadas.            
            List<SECOES> objSecoesDisp = ViewState["SECOES_DISP"] as List<SECOES>;
            List<SECOES> objSecoesSel = ViewState["SECOES_SEL"] as List<SECOES>;





            //para cada item selecionado arranca da origem e adiciona no destino,.
            //Carrega os itens selecionados
            List<Object> objSelecao =   dGridSecoesDisp.GetSelectedFieldValues(new string[] { "COD_SECAO", "NOME_SECAO","COD_EMPRESA","EMPRESA" });

            //para cada item selecionado 
            foreach (object o in objSelecao)
            {
                //Se foi adicionado deleta o existente do destino
                objSecoesSel.RemoveAll(f => f.COD_SECAO.Equals((o as object[])[0].ToString()));
                
                //adiciona a seção

                SECOES objSec = new SECOES();

                objSec.COD_SECAO = (o as object[])[0].ToString();
                objSec.NOME_SECAO = (o as object[])[1].ToString();
                objSec.COD_EMPRESA = (o as object[])[2].ToString();
                objSec.EMPRESA = (o as object[])[3].ToString();

                objSecoesSel.Add(objSec);
  
            }


            //rebinda o grid 
            BindaGridSecoes();

            LimparSelecao();


        }

        protected void btnSecoesDel_Click(object sender, EventArgs e)
        {

            //caso seja o botão adicionar todos seleciona todos antes da execução
            if ((sender as DevExpress.Web.ASPxEditors.ASPxButton).CommandArgument.Equals("TODOS"))
                dGridSecoesSel.Selection.SelectAll();



            if (dGridSecoesSel.Selection.Count <= 0) return; //nenhum item selecionado.


            //carrega a lista de disponíveis e selecionadas.                        
            List<SECOES> objSecoesSel = ViewState["SECOES_SEL"] as List<SECOES>;
            //carrega a seleção do grid
            List<Object> objSelecao =   dGridSecoesSel.GetSelectedFieldValues(new string[] { "COD_SECAO", "NOME_SECAO","COD_EMPRESA","EMPRESA" });

            //para cada item selecionado 
            foreach (object o in objSelecao)
            {
                //deleta o objeto da origem
                objSecoesSel.RemoveAll(f => f.COD_SECAO.Equals((o as object[])[0].ToString()));
                
            }


            //rebinda o grid 
            BindaGridSecoes();


            LimparSelecao();

        }

        #endregion

        #region classesAuxiliares


        [Serializable]
        public class SECOES
        {
            public string COD_SECAO {get;set;}
            public string NOME_SECAO {get;set;}
            public string COD_EMPRESA {get;set;}
            public string EMPRESA {get;set;}


        }

        #endregion



    }











}