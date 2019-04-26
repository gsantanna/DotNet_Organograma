using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WEB
{
    public partial class wfmRelatorioFuncionariosAlocados : System.Web.UI.Page
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
                dGridExptrMain.WritePdfToResponse(string.Format("RelatFuncionariosLotadosP{0:dd_MM_yyyy}", DateTime.Now));
            }
            else if (btn.CommandArgument.Equals("XLS"))
            {
                dGridExptrMain.WriteXlsxToResponse(string.Format("RelatFuncionariosLotadosX{0:dd_MM_yyyy}", DateTime.Now));
            
            }

        }


       

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.mvMain.ActiveViewIndex = 0;
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            //verifica se pelo menos 1 nível  foi selecionado.
            if (!filtroMain.PossuiSelecao())
            {
                Util.Avisos.Aviso("Você deve selecionar algum filtro para continuar", this.Page);
                return;
            }
            
            //cria  o objeto de saída.
            List<ResultadoRelatorio> objResultado = new List<ResultadoRelatorio>();

            //cria o contexto de dados
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();


            //carrega a lista de funcionarios alocados para agilizar a consulta
            List<Negocio.ORG_FUNCIONARIO_ALOCADO>  Funcionarios = objDs.ORG_FUNCIONARIO_ALOCADO.ToList();





            //verifica se é por seção.
            if (filtroMain.SecoesSelecionadas.Count() > 0) //por seções
            {


               objResultado = (from f in Funcionarios
                             join filtro in filtroMain.SecoesSelecionadas on f.COD_SECAO equals filtro.COD_SECAO
                             select new ResultadoRelatorio
                             {
                                 Email = f.GESTOR_EMAIL,
                                 Empresa = filtro.EMPRESA,
                                 Funcionario = f.NOME,
                                 Gestor = f.GESTOR,
                                  COD_SECAO = f.COD_SECAO, 
                                 Secao = f.NOME_SECAO + " - " + f.COD_SECAO,
                                 SecaoSuperior = f.NOME_SECAO_SUP + " - " + f.COD_SECAO_SUP
                             }).ToList();

            }
            else if (filtroMain.EmpresasSelecionadas.Count() > 0) // por empresa 
            {
                objResultado = ( from f in Funcionarios
                                 join filtro in filtroMain.EmpresasSelecionadas on f.COD_EMPRESA equals filtro.COD_EMPRESA
                                 select new ResultadoRelatorio
                                 {
                                     Email = f.GESTOR_EMAIL,
                                     Empresa = f.NOME_EMPRESA,
                                     Funcionario = f.NOME,
                                     Gestor = f.GESTOR,
                                     Secao = f.NOME_SECAO + " - " + f.COD_SECAO,
                                     SecaoSuperior = f.NOME_SECAO_SUP + " - " + f.COD_SECAO_SUP,
                                     COD_SECAO =  f.COD_SECAO
                                 }).ToList();

            }




            //se for selecionado, deve se incluir as subseções.
            if (chkSubsecao.Checked)
            {
              
                    //List<ResultadoRelatorio>  
                List<ResultadoRelatorio> Resu = (from f in Funcionarios
                                                 join filtro in
                                                     (from sec in objResultado select new { COD_SECAO= sec.COD_SECAO } ).Distinct() on f.COD_SECAO_SUP equals filtro.COD_SECAO
                                                 join hie in objDs.ORG_HIERARQUIA on f.COD_SECAO equals hie.COD_SECAO 
                                                 where hie.SUBSECAO !=null && hie.SUBSECAO=="1"
                                                 select new ResultadoRelatorio
                                              {
                                                  Email = f.GESTOR_EMAIL,
                                                  Empresa = f.NOME_EMPRESA,
                                                  Funcionario = f.NOME,
                                                  Gestor = f.GESTOR,
                                                   Secao = f.NOME_SECAO + " - " + f.COD_SECAO,
                                 SecaoSuperior = f.NOME_SECAO_SUP + " - " + f.COD_SECAO_SUP,
                                                  COD_SECAO = f.COD_SECAO
                                              }).ToList();

                        objResultado.AddRange(Resu);

            }


            //se obteve algum resultado..
            if (objResultado.Count > 0)
            {
                ViewState.Add("RESULTADO", objResultado);
                BindaResultado();
                mvMain.ActiveViewIndex = 1;

            }
            else
            {
                Util.Avisos.Aviso("Nenhum registro localizado! Verifique os filtros e tente novamente", this.Page);

            }
                   


           

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
            public string Funcionario { get; set; }
            public string Empresa { get; set; }
            public string Secao { get; set; }
            public string SecaoSuperior { get; set; }
            public string Gestor { get; set; }
            public string Email { get; set; }
            public string COD_SECAO { get; set; }
            

        }

        
        #endregion

        protected void dGridExptrMain_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            e.Text = e.Text.Replace("<br />" , "\n");

        }





    }
}