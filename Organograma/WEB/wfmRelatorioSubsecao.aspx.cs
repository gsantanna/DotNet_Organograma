using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WEB
{
    public partial class wfmRelatoriosSubsecao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.mvMain.ActiveViewIndex == 1)
                BindaResultado();

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            this.filtroMain.Limpar();

        }



        protected void btnExportacao_Click(object sender, EventArgs e)
        {

            //carrega o botão
            ASPxButton btn = (sender as ASPxButton);

            if (btn.CommandArgument.Equals("PDF"))
            {
                dGridExptrMain.WritePdfToResponse(string.Format("RelatSubsecaoPDF{0:dd_MM_yyyy}", DateTime.Now));
            }
            else if (btn.CommandArgument.Equals("XLS"))
            {
                dGridExptrMain.WriteXlsxToResponse(string.Format("RelatSubsecaoXLS{0:dd_MM_yyyy}", DateTime.Now));
            
            }

        }


       

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.mvMain.ActiveViewIndex = 0;
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
          
            //verifica se algum filtro foi selecionado.
            
            //nenhuma empresa/seção selecionada.
            if (this.filtroMain.EmpresasSelecionadas.Count() <= 0 && this.filtroMain.SecoesSelecionadas.Count() <=0)
            {
                Util.Avisos.Aviso("Você deve selecionar valores para os filtros antes de continuar", this.Page);
                return;
            }



            //cria o contexto de dados.
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();


            //Cria o objeto para conter as subseções 
            //de acordo com os filtros selecionados. 
            //(considerando sempre o menor filtro caso selecionado)
            List<ResultadoRelatorio> objResultado = new List<ResultadoRelatorio>();



            //verificar qual o nível do relatório
            if (filtroMain.SubsecoesSelecionadas.Count > 0)//por Subseção
            {

                objResultado = (from s in objDs.ORG_LISTA_SUBSECAO.ToList()
                                join f in filtroMain.SubsecoesSelecionadas on s.COD_SECAO equals f.COD_SECAO
                                select new ResultadoRelatorio
                                {
                                    Diretoria = s.DIRETORIA,
                                    Gestor = s.GESTOR,
                                    Empresa = f.EMPRESA,
                                    Nome = s.NOME + " - " + s.COD_SECAO,
                                    Nome_Superior = s.NOME_SECAO_SUP + " - " + s.COD_SECAO_SUP,
                                    Funcionarios =
                                        (objDs.ORG_FUNCIONARIO_ALOCADO.Where(fff => fff.COD_SECAO.Equals(s.COD_SECAO)).ToList())
                                }).ToList<ResultadoRelatorio>();

            
            }
            else if (filtroMain.SecoesSelecionadas.Count > 0 ) //por seção 
            {

                objResultado = (from s in objDs.ORG_LISTA_SUBSECAO.ToList()
                                join f in filtroMain.SecoesSelecionadas on s.COD_SECAO_SUP equals f.COD_SECAO
                                select new ResultadoRelatorio
                                {
                                    Diretoria = s.DIRETORIA,
                                    Gestor = s.GESTOR,
                                    Empresa = f.EMPRESA,
                                    Nome = s.NOME + " - " + s.COD_SECAO,
                                    Nome_Superior = s.NOME_SECAO_SUP + " - " + s.COD_SECAO_SUP,
                                    Funcionarios =
                                        (objDs.ORG_FUNCIONARIO_ALOCADO.Where(fff => fff.COD_SECAO.Equals(s.COD_SECAO)).ToList())
                                }).ToList<ResultadoRelatorio>();

            }
            else //por empresa
            {

                objResultado = (from s in objDs.ORG_LISTA_SUBSECAO.ToList()
                                join f in filtroMain.EmpresasSelecionadas on s.COD_EMPRESA equals f.COD_EMPRESA
                                select new ResultadoRelatorio
                                {
                                    Diretoria = s.DIRETORIA,
                                    Gestor = s.GESTOR,
                                    Empresa = f.DESCRICAO,
                                    Nome = s.NOME + " - " + s.COD_SECAO,
                                    Nome_Superior = s.NOME_SECAO_SUP + " - " + s.COD_SECAO_SUP,
                                    Funcionarios =
                                        (objDs.ORG_FUNCIONARIO_ALOCADO.Where(fff => fff.COD_SECAO.Equals(s.COD_SECAO)).ToList())
                                }).ToList<ResultadoRelatorio>();
            }


            //caso não encontre ninguem ele retorna uma mensagem
            if (objResultado.Count() <= 0)
            {
                Util.Avisos.Aviso("Nenhum item encontrado!", this.Page);
                return;
            }

            //atualiza a linha do funcionario
            foreach (ResultadoRelatorio r in objResultado)
            {
                foreach (Negocio.ORG_FUNCIONARIO_ALOCADO fu in r.Funcionarios)
                {
                    r.Funcionarios_Linha += string.Format("{0} <br />", fu.NOME);
 
                }
            }





            //joga o resultado para viewstate
            ViewState.Add("RESULTADO", objResultado);
            //binda o grid
            BindaResultado();



            //muda para a view de saída do relatório
            mvMain.ActiveViewIndex = 1;




        }

        private void BindaResultado()
        {
            List<ResultadoRelatorio> objResult = (ViewState["RESULTADO"] as List<ResultadoRelatorio>);
            this.dGridResposta.DataSource = objResult;
            this.dGridResposta.DataBind();

        
        }



        #region Clases_de_apoio
        
        [Serializable]
        public class ResultadoRelatorio
        {
            public int id_linha { get; set; }
            public string Empresa { get; set; }
            public string Nome { get; set; }
            public string Nome_Superior{get;set;}
            public string Gestor { get; set; }
            public string Diretoria { get; set; }
            public string Funcionarios_Linha { get; set; }
            public List<Negocio.ORG_FUNCIONARIO_ALOCADO> Funcionarios { get; set; }


            public ResultadoRelatorio()
            {
                Funcionarios = new List<Negocio.ORG_FUNCIONARIO_ALOCADO>();
            }

        }

        
        #endregion

        protected void dGridExptrMain_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            e.Text = e.Text.Replace("<br />" , "\n");

        }





    }
}