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
    public partial class wfmDiretoria : System.Web.UI.Page
    {
        string Empresa;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["EMPRESA"] == null || string.IsNullOrEmpty(Session["EMPRESA"].ToString())) Response.Redirect("default.aspx");
            Empresa = Session["EMPRESA"].ToString();
            
         

            //carrega os dados 
            if (!IsPostBack) Carrega();


            BindaListas();


        }


        private void BindaListas()
        {
             dGridDiretorias.DataSource = ViewState["diretorias"];
            dGridDiretorias.DataBind();


            dGridDisp.DataSource = ViewState["diretoriasDisp"];
            dGridDisp.DataBind();

          
            

        }


        private void Carrega()
        {
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            
            //carrega as diretorias selecionadas
            var listaDiretorias = from d in objDs.ORG_LISTA_DIRETORIAS    
                                  where d.COD_EMPRESA.Equals(Empresa)
                                  select d;

            //grava no viewstate o objeto como array para ser editável 
            ViewState.Add("diretorias", listaDiretorias.Where(f => f.DIRETORIA.Equals("1")).OrderBy(f=>f.NOME_SECAO).ToList<Negocio.ORG_LISTA_DIRETORIAS>());
            ViewState.Add("diretoriasDisp", listaDiretorias.Where(f => !f.DIRETORIA.Equals("1")).OrderBy(f=>f.NOME_SECAO).ToList<Negocio.ORG_LISTA_DIRETORIAS>());


          


        }




        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            


            //tenta gravar alterações
            //caso nao consiga devolve um erro.

            Negocio.DiretoriaManager objDm = new Negocio.DiretoriaManager();
            if (objDm.AtualizaDiretoria(ViewState["diretorias"] as List<Negocio.ORG_LISTA_DIRETORIAS> , Empresa ) )
            {
                Avisos.Aviso("Diretorias Salvas!", this);
                Carrega();
                BindaListas();

            }
            else
            {
                //exibir uma mensagem de erro para o usuario.
                Avisos.Aviso(string.Format("Erro ao salvar as diretorias, o erro foi: {0}", objDm.Erro), this);

            }
             
            


            
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (dGridDisp.Selection.Count <= 0) return; //nenhum item selecionado.

            //carrega as listas do viewstate
            List<Negocio.ORG_LISTA_DIRETORIAS> objDiretorias = ViewState["diretorias"] as List<Negocio.ORG_LISTA_DIRETORIAS>;
            List<Negocio.ORG_LISTA_DIRETORIAS> objDiretoriasDisp = ViewState["diretoriasDisp"] as List< Negocio.ORG_LISTA_DIRETORIAS>;


            //carrega a seleção do grid
            List<Object> objSelecao =
            dGridDisp.GetSelectedFieldValues(new string[] { "COD_EMPRESA","COD_SECAO","NOME_SECAO","TT" });


            //para cada item selecionado 
            foreach (object o in objSelecao)
            {
                
                //adiciona na lista de diretorias
                Negocio.ORG_LISTA_DIRETORIAS ld = new Negocio.ORG_LISTA_DIRETORIAS();

                ld.COD_EMPRESA = (o as object[])[0].ToString();
                ld.COD_SECAO = (o as object[])[1].ToString();
                ld.DIRETORIA = "1";
                ld.NOME_SECAO = (o as object[])[2].ToString();
                ld.TT =   (o as object[])[3] !=null ? (o as object[])[3].ToString() : string.Empty;

                objDiretorias.Add(ld);

                //remove da lista de disponíveis
                objDiretoriasDisp.RemoveAll(f => f.COD_SECAO.Equals(ld.COD_SECAO));

            }


            //atualia o viewstate 
            ViewState["diretorias"]  = objDiretorias;
            ViewState["diretoriasDisp"]= objDiretoriasDisp;



            BindaListas();


        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {


            if (dGridDiretorias.Selection.Count <= 0) return; //nenhum item selecionado.

            //carrega as listas do viewstate
            List<Negocio.ORG_LISTA_DIRETORIAS> objDiretorias = ViewState["diretorias"] as List<Negocio.ORG_LISTA_DIRETORIAS>;
            List<Negocio.ORG_LISTA_DIRETORIAS> objDiretoriasDisp = ViewState["diretoriasDisp"] as List<Negocio.ORG_LISTA_DIRETORIAS>;


            //carrega a seleção do grid
            List<Object> objSelecao =
             dGridDiretorias.GetSelectedFieldValues(new string[] { "COD_EMPRESA", "COD_SECAO", "NOME_SECAO", "TT" });


            //para cada item selecionado 
            foreach (object o in objSelecao)
            {

                //adiciona na lista de Disponiveis
                Negocio.ORG_LISTA_DIRETORIAS ld = new Negocio.ORG_LISTA_DIRETORIAS();

                ld.COD_EMPRESA = (o as object[])[0].ToString();
                ld.COD_SECAO = (o as object[])[1].ToString();
                ld.DIRETORIA = "1";
                ld.NOME_SECAO = (o as object[])[2].ToString();
                ld.TT = (o as object[])[3] != null ? (o as object[])[3].ToString() : string.Empty;

                objDiretoriasDisp.Add(ld);

                //remove da lista de diretorias 
                objDiretorias.RemoveAll(f => f.COD_SECAO.Equals(ld.COD_SECAO));


            }


            //atualia o viewstate 
            ViewState["diretorias"] = objDiretorias;
            ViewState["diretoriasDisp"] = objDiretoriasDisp;

            BindaListas();

             




        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Carrega();
            BindaListas();

        }
    }
}