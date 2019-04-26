using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using EvoPdf.HtmlToPdf;
using System.Drawing;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace WEB
{


    public partial class wfmRelatorioOrganograma : System.Web.UI.Page
    {

        #region METODOS_PAGINA
        protected void Page_Load(object sender, EventArgs e)
        {

          
      
                if (Request.QueryString["tipo"] == "Completo")
                {
                    rdCompleto.Checked = true;
                }
                else
                {
                    rdSimples.Checked = true;
                }
            


            
            if (mvMain.ActiveViewIndex == 1)  BindaImagemGrafico();


            


        }
        #endregion

        #region ACOES_FILTRO


        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            filtroMain.Limpar();
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
        
            //verifica se pelo menos 1 nível  foi selecionado.
            if (!filtroMain.PossuiSelecao())
            {
                Util.Avisos.Aviso("Você deve selecionar algum filtro para continuar", this.Page);
                return;
            }


            

            //cria o contexto de dados.
            Negocio.gsatOrganogramaDataContext objDs = new Negocio.gsatOrganogramaDataContext();

            //carrega a lista de seções
            List<Negocio.ORG_LISTA_SECOES_SIMPLES> Secoes = objDs.ORG_LISTA_SECOES_SIMPLES.ToList();

            
            //carregar as seções do filtro.
            //carregar as filhas
            //carregar as filhas das filhas (10 níveis)

            
            //cria a lista principal que receberá o merge depois.
            List<Negocio.OrgChartDataSource> objDsPrincipal = new List<Negocio.OrgChartDataSource>();


            //por seções
            if (filtroMain.SecoesSelecionadas.Count > 0)
            {
                objDsPrincipal.AddRange((from s in Secoes
                                         join filtro in filtroMain.SecoesSelecionadas on s.COD_SECAO equals filtro.COD_SECAO
                                         select new Negocio.OrgChartDataSource
                                         {
                                             id = Convert.ToInt64(s.COD_SECAO.Replace(".", "")),
                                            idPai = s.COD_SECAO_SUP ==null ? 0 : Convert.ToInt64( s.COD_SECAO_SUP.Replace(".","")),
                                             Gestor = s.GESTOR,
                                             Cod_secao = s.COD_SECAO,
                                             Cod_secao_sup = s.COD_SECAO_SUP,
                                             Secao = s.NOME_SECAO
                                         }).ToList());
            }
            else if (filtroMain.EmpresasSelecionadas.Count > 0)
            {
                objDsPrincipal.AddRange((from s in Secoes
                                         join filtro in filtroMain.EmpresasSelecionadas on s.COD_EMPRESA equals  filtro.COD_EMPRESA
                                         select new Negocio.OrgChartDataSource
                                         {
                                             id = Convert.ToInt64(s.COD_SECAO.Replace(".", "")),
                                             idPai = s.COD_SECAO_SUP == null ? 0 : Convert.ToInt64(s.COD_SECAO_SUP.Replace(".", "")),
                                             Gestor = s.GESTOR,
                                             Cod_secao = s.COD_SECAO,
                                             Cod_secao_sup = s.COD_SECAO_SUP,
                                             Secao = s.NOME_SECAO, 
                                             Processado=true
                                         }).ToList());
            }


            




           

            //Carrega todas as Filhas!
            for (int i = 0; i < 10; i++) //percorre 10 níveis adicionando as filhas.
            {
                List<Negocio.OrgChartDataSource> objTmp = new List<Negocio.OrgChartDataSource>();
                //para cada Item carrega todos os seus filhos.
                foreach (Negocio.OrgChartDataSource c in objDsPrincipal.Where( f=>f.Processado == false ))
                {
                    //seta o ítem como processado.
                    c.Processado = true;

                    //adiciona todas as filhas desta seção.
                    objTmp.AddRange((from s in Secoes
                                             where   s.COD_SECAO_SUP != null &&  s.COD_SECAO_SUP.Equals(c.Cod_secao)
                                             select new Negocio.OrgChartDataSource
                                                   {
                                                       id = Convert.ToInt64(s.COD_SECAO.Replace(".", "")),
                                                       idPai = s.COD_SECAO_SUP == null ? 0 : Convert.ToInt64(s.COD_SECAO_SUP.Replace(".", "")),
                                                       Gestor = s.GESTOR,
                                                       Cod_secao = s.COD_SECAO,
                                                       Cod_secao_sup = s.COD_SECAO_SUP,
                                                       Secao = s.NOME_SECAO,
                                                       Processado = false
                                                   }).ToList()); 

                } // Fim do Foreach 

               

                //adiciona o otmp pra lista principal
                objDsPrincipal.AddRange(objTmp);



            } //fim do FOR de 10 Loops



            //Adiciona as SUBSEÇÕES
            List<Negocio.OrgChartDataSource> oTmpSubsecao;
            
            //Carrega o objeto 
            oTmpSubsecao = (from s in objDs.ORG_LISTA_SUBSECAO.ToList()
                            join filtro in objDsPrincipal on s.COD_SECAO_SUP equals filtro.Cod_secao
                            select new Negocio.OrgChartDataSource
                            {
                                id = Convert.ToInt64(s.COD_SECAO.Replace(".", "")),
                                idPai = s.COD_SECAO_SUP == null ? 0 : Convert.ToInt64(s.COD_SECAO_SUP.Replace(".", "")),
                                Gestor = s.GESTOR,
                                Cod_secao = s.COD_SECAO,
                                Cod_secao_sup = s.COD_SECAO_SUP,
                                Secao = s.NOME,
                                Processado = true
                            }).ToList();




            //Adiciona as subseções no objPrincipal
            objDsPrincipal.AddRange(oTmpSubsecao);







            //verifica se teve alguma seção selecionada
            if (objDsPrincipal.Count() <= 0)
            {
                Util.Avisos.Aviso("Nenhuma seção encontrada, selecione alguma seção ou empresa e tente novamente", this.Page);
                return;

            }
             

            //Retira uma eventual duplicidade 
            objDsPrincipal = objDsPrincipal.Distinct().ToList();




            //caso tenha sido selecionado habilitar funcionários, 
            //carrega TODOS os funcionários lotados nas seções seleciondas no filtro
            //caso contrário mantem o campo Funcionários em branco.
            if (this.rdCompleto.Checked)
            {
                //Carrega todos os funcionários alocados!
                var ListaFuncionarios =( from f in objDs.ORG_FUNCIONARIO_ALOCADO.ToList()
                                        join filtro in objDsPrincipal on f.COD_SECAO equals filtro.Cod_secao
                                        select new { COD_SECAO = f.COD_SECAO, f.NOME  }).Distinct();
                //monta as strings [Funcionário] no obj principal.

                //para cada seção no objeto principal..
                foreach (Negocio.OrgChartDataSource s in objDsPrincipal)
                {

                   

                   //percorre os funcionários preenchendo.
                    foreach (var func in ListaFuncionarios.Where(f => f.COD_SECAO.Equals(s.Cod_secao)))
                    {
                        s.Funcionarios += string.Format(@"{0}<br />", func.NOME);
                    }
                }

            
            }//fim do IF do rdcompleto 



            //marca itens "sem pai" com o id_pai 0"
            foreach (var i in objDsPrincipal)
            {
                if (objDsPrincipal.Where(f => f.Cod_secao.Equals(i.Cod_secao_sup)).Count() == 0)
                {
                    i.idPai = 0;
                }

            }

         

            //adiciona o resultado 
            Guid objGuid = Guid.NewGuid();

            //Joga o  resultado para o Application
            Application.Add("OC_" + objGuid.ToString(), objDsPrincipal);

           

            //Cria o objeto do GRID principal. (Imagem)
            ResultadoOrganograma objResultado = new ResultadoOrganograma();
            
            //seta a referencia
            objResultado.Referencia = objGuid;


            //preencher a imagem com o resultado do EXPORT page to PDF
            
            //cria o conversor de imagem 
            ImgConverter objImgConv = new ImgConverter();
            
           
            
        
            //formata o caminho do gerador de organograma
            string strCaminho = string.Format("{0}://{1}{2}{3}",
                                    Context.Request.Url.Scheme,
                                    Context.Request.Url.Host,
                                    Context.Request.Url.Port == 80
                                        ? string.Empty
                                        : ":" + Context.Request.Url.Port,
                                    Context.Request.ApplicationPath);
            if (!strCaminho.EndsWith("/")) strCaminho += "/";




            //carrega a Imagem Original (sem o corte da remoção da marca d'agua)
            System.Drawing.Image objImgOriginal =
            objImgConv.GetImageFromUrl(string.Format("{0}GeradorOrgChartGoogleCode.aspx?ID_ORG_CHART_DS={1}",  strCaminho , objGuid.ToString()));


            //cria um Bitmap com a imagem original /p permitir o corte.
            Bitmap objBmp = new Bitmap(objImgOriginal);

            //cria o retangulo de corte da imagem, (remoção da marca d'agua)
            Rectangle objCorte = new Rectangle(0, 300, objImgOriginal.Width, objImgOriginal.Height - 599);

            //corta a imagem
            objBmp = Util.ImgUtil.cropAtRect(objBmp, objCorte);

            //Cria um Memorystrem para guardar o array de saída (imagem Já pronta
            MemoryStream msSaida = new MemoryStream();

            //Salva o bitmap já sem a marca d'agua em um memorystream
            objBmp.Save(msSaida, System.Drawing.Imaging.ImageFormat.Png);

            //grava a imagem do gráfico no objeto do GRID
            objResultado.Imagem = msSaida.ToArray();
            
            //adiciona o DataSource(Imagem) para o viewstate (binda messmo com postback)
            ViewState.Add("OC_IMG", objResultado);


            //Binda o grid 
            BindaImagemGrafico();



            //muda para a view do resultado
            this.mvMain.ActiveViewIndex = 1; 
        }


        void BindaImagemGrafico()
        {

            ResultadoOrganograma objResultado = (ViewState["OC_IMG"] as ResultadoOrganograma);
            this.imgOrgChart.Value = objResultado.Imagem;


           


        }



        #endregion




        #region METODOS_RESULTADO

    

        protected void btnExportacao_Click(object sender, EventArgs e)
        {

            //carrega o botão
            ASPxButton btn = (sender as ASPxButton);
            //Carrega a Imagem.
            ResultadoOrganograma objResultado = (ViewState["OC_IMG"] as ResultadoOrganograma);


            if (btn.CommandArgument.Equals("PDF"))
            {
                //carrega a Imagem
                System.Drawing.Image objImg = System.Drawing.Image.FromStream(new MemoryStream(objResultado.Imagem));
                


               
                //Cria o PDFSHARP
                PdfDocument doc = new PdfDocument();
              
                //adiciona uma página
                doc.Pages.Add(new PdfPage());

                //Configura o tamanho de acordo com a imagem 
                doc.Pages[0].Width = objImg.Width;
                doc.Pages[0].Height = objImg.Height;


                //Carrega o Drawner do PDF 
                XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);

                //Cria o objeto de imagem XImage (PDF)
                XImage img = XImage.FromGdiPlusImage(objImg);
                
                //Printa a imagem no PDF 
                xgr.DrawImage(img, 0, 0);
                

                //Cria o memorystrem de saída.
                MemoryStream msSaida = new MemoryStream();

                //Joga o PDF criado para o msSaida SEM fechar o MemoryStream
                doc.Save(msSaida, false );

                //carrega o response
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();//limpa
                //passa os headers
                response.AddHeader("Content-Type", "binary/octet-stream");
                response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}; size={1}", "ExportOrganograma.pdf", msSaida.Length.ToString()));
                //carrega a imagem
                response.BinaryWrite( msSaida.ToArray());

                //doc.Save
                doc.Close();

                //finaliza a response.
                response.End(); 


              
               
            }
            else if (btn.CommandArgument.Equals("IMG"))
            {
               
               
                //carrega o response
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();//limpa
                //passa os headers
                response.AddHeader("Content-Type", "binary/octet-stream");
                response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}; size={1}", "ExportOrganograma.png",  objResultado.Imagem.Length.ToString()));
                //carrega a imagem
                response.BinaryWrite( objResultado.Imagem);
                //finaliza a response.
                response.End(); 

            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.mvMain.ActiveViewIndex = 0;
        }



        #endregion


        #region CLASSES_AUXILIARES
        [Serializable]
        protected internal class ResultadoOrganograma
        {
            public Guid Referencia { get; set; }
            public byte[] Imagem { get; set; }
        }

        #endregion


    }









}