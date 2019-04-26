using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WEB
{
    public partial class wfmRelatorioSecoesSuperiores : System.Web.UI.Page
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
                dGridExptrMain.WritePdfToResponse(string.Format("RelatSecoesSupPDF{0:dd_MM_yyyy}", DateTime.Now));
            }
            else if (btn.CommandArgument.Equals("XLS"))
            {
                dGridExptrMain.WriteXlsxToResponse(string.Format("RelatSecoesSupXLS{0:dd_MM_yyyy}", DateTime.Now));

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
            List<Negocio.ORG_LISTA_SECOES> Secoes = (from s in objDs.ORG_LISTA_SECOES
                                                     where  s.POSSUI_FILHAS.Equals("1")
                                                     select s).ToList();

            


            //verificar se o relatório é por SEÇÃO
            if (filtroMain.SecoesSelecionadas.Count > 0) //por seção 
            {

                objResultado = (from s in  Secoes
                                join f in filtroMain.SecoesSelecionadas on s.COD_SECAO equals f.COD_SECAO
                                select new ResultadoRelatorio
                                {
                                   
                                    Gestor = s.GESTOR_MOD.Equals("1") ? s.NOME_GESTOR_MOD : s.NOME_GESTOR_RM,
                                    GestorEmail = s.GESTOR_MOD.Equals("1") ? s.EMAIL_GESTOR_MOD : s.EMAIL_GESTOR_RM,
                                    Empresa = f.EMPRESA,
                                    Secao = s.NOME_SECAO_FINAL + " - " + s.COD_SECAO,
                                    SubSecoes =  chkSubsecao.Checked ?  (objDs.ORG_LISTA_SUBSECAO.Where( fss => fss.COD_SECAO_SUP.Equals(s.COD_SECAO)).ToList()) : null ,
                                    Secoes = (objDs.ORG_LISTA_SECOES_SIMPLES.Where(fff => fff.COD_SECAO_SUP.Equals(s.COD_SECAO)).ToList())
                                }).ToList<ResultadoRelatorio>();

            }
            else //por empresa
            {

                objResultado = (from s in Secoes
                                join f in filtroMain.EmpresasSelecionadas on s.COD_EMPRESA equals f.COD_EMPRESA
                                select new ResultadoRelatorio
                                {
                                    Gestor = s.GESTOR_MOD.Equals("1") ? s.NOME_GESTOR_MOD : s.NOME_GESTOR_RM,
                                    GestorEmail = s.GESTOR_MOD.Equals("1") ? s.EMAIL_GESTOR_MOD : s.EMAIL_GESTOR_RM,
                                    Empresa = f.DESCRICAO,
                                    Secao = s.NOME_SECAO_FINAL + " - " + s.COD_SECAO,
                                    SubSecoes = chkSubsecao.Checked ? (objDs.ORG_LISTA_SUBSECAO.Where(fss => fss.COD_SECAO_SUP.Equals(s.COD_SECAO)).ToList()) : null,
                                    Secoes = (objDs.ORG_LISTA_SECOES_SIMPLES.Where(fff => fff.COD_SECAO_SUP.Equals(s.COD_SECAO)).ToList())
                                }).ToList<ResultadoRelatorio>();
            }





       
            //atualiza a linha do seções
            foreach (ResultadoRelatorio r in objResultado)
            {
                //adiciona as seções
                foreach (Negocio.ORG_LISTA_SECOES_SIMPLES fu in r.Secoes)
                {
                    r.Secoes_Linha += string.Format("{0} <br />", fu.NOME_SECAO + " - " + fu.COD_SECAO);

                }



                //adiciona as subseções
                if (chkSubsecao.Checked && r.SubSecoes !=null )
                {
                    foreach (Negocio.ORG_LISTA_SUBSECAO fu in r.SubSecoes)
                    {
                        r.Secoes_Linha += string.Format("{0} <br />", fu.NOME + " - " + fu.COD_SECAO);
                    }

                }




            }





            //remove os ítens que não deveriam estar (seções sem filhas filtradas no check de subseções)
            objResultado.RemoveAll(f => string.IsNullOrEmpty(f.Secoes_Linha));




            //caso não encontre ninguem ele retorna uma mensagem
            if (objResultado.Count() <= 0)
            {
                Util.Avisos.Aviso("Nenhum item encontrado!", this.Page);
                return;
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
            public string Gestor { get; set; }
            public string GestorEmail { get; set; }
            public string Secoes_Linha { get; set; }
            public List<Negocio.ORG_LISTA_SECOES_SIMPLES> Secoes { get; set; }
            public List<Negocio.ORG_LISTA_SUBSECAO> SubSecoes { get; set; }

            public ResultadoRelatorio()
            {
                Secoes  = new List<Negocio.ORG_LISTA_SECOES_SIMPLES>();
                SubSecoes = new List<Negocio.ORG_LISTA_SUBSECAO>();


            }

        }


        #endregion

        protected void dGridExptrMain_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            e.Text = e.Text.Replace("<br />", "\n");

        }





    }
}