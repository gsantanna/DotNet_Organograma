using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WEB
{
    public partial class wfmRelatorioSecoes : System.Web.UI.Page
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
                dGridExptrMain.WritePdfToResponse(string.Format("RelatSecoesPDF{0:dd_MM_yyyy}", DateTime.Now));
            }
            else if (btn.CommandArgument.Equals("XLS"))
            {
                dGridExptrMain.WriteXlsxToResponse(string.Format("RelatSecoesXLS{0:dd_MM_yyyy}", DateTime.Now));

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
            if ( !filtroMain.PossuiSelecao())
            {
                Util.Avisos.Aviso("Você deve selecionar valores para os filtros antes de continuar", this.Page);
                return;
            }



            //cria o contexto de dados.
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();


            //Cria o objeto para conter as seções 
            //de acordo com os filtros selecionados. 
            //(considerando sempre o menor filtro caso selecionado)
            List<ResultadoRelatorio> objResultado = new List<ResultadoRelatorio>();


            //pré carrega a lista de seções para uma única consulta ao banco.
            List<Negocio.ORG_LISTA_SECOES> Secoes = objDs.ORG_LISTA_SECOES.ToList();



            //verificar se o relatório é por SEÇÃO
            if (filtroMain.SecoesSelecionadas.Count > 0) //por seção 
            {

                objResultado = (from s in  Secoes
                                join f in filtroMain.SecoesSelecionadas on s.COD_SECAO equals f.COD_SECAO
                                select new ResultadoRelatorio
                                {
                                    Diretoria = s.DIRETORIA,
                                    Gestor = s.GESTOR_MOD.Equals("1") ? s.NOME_GESTOR_MOD : s.NOME_GESTOR_RM,
                                    GestorEmail = s.GESTOR_MOD.Equals("1") ? s.EMAIL_GESTOR_MOD : s.EMAIL_GESTOR_RM,
                                    Empresa = f.EMPRESA,
                                    Secao = s.NOME_SECAO_FINAL + " - " + s.COD_SECAO,
                                    SecaoSuperior = s.NOME_SECAO_SUP + " - " + s.COD_SECAO_SUP,
                                    Funcionarios =
                                        (objDs.ORG_FUNCIONARIO_ALOCADO.Where(fff => fff.COD_SECAO.Equals(s.COD_SECAO)).ToList())
                                }).ToList<ResultadoRelatorio>();

            }
            else //por empresa
            {

                objResultado = (from s in Secoes
                                join f in filtroMain.EmpresasSelecionadas on s.COD_EMPRESA equals f.COD_EMPRESA
                                select new ResultadoRelatorio
                                {
                                    Diretoria = s.DIRETORIA,
                                    Gestor = s.GESTOR_MOD.Equals("1") ? s.NOME_GESTOR_MOD : s.NOME_GESTOR_RM,
                                    GestorEmail = s.GESTOR_MOD.Equals("1") ? s.EMAIL_GESTOR_MOD : s.EMAIL_GESTOR_RM,
                                    Empresa = f.DESCRICAO,
                                    Secao = s.NOME_SECAO_FINAL + " " + s.COD_SECAO,
                                    SecaoSuperior = s.NOME_SECAO_SUP + " " + s.COD_SECAO_SUP,
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
            public string Secao { get; set; }
            public string SecaoSuperior { get; set; }
            public string Gestor { get; set; }
            public string GestorEmail { get; set; }
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
            e.Text = e.Text.Replace("<br />", "\n");

        }





    }
}