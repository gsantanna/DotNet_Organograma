using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WEB
{
    public partial class wfmExportTreinamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //verifica se tem o dicionario no application 
                if (Application["ExportGestaoTreinamentoConfig"] != null)
                {
                    //carrega o dicionario e binda os campos
                    Dictionary<string, string> objConfig =
                    (Application["ExportGestaoTreinamentoConfig"] as Dictionary<string, string>);


                  
                    //ftp 
                    string t= string.Empty;

                   objConfig.TryGetValue( "txtFtpHost", out t);
                    this.txtFtpHost.Text=t;
                    t=string.Empty;


                    objConfig.TryGetValue( "txtFtpUsuario", out t);
                    this.txtFtpUsuario.Text=t;
                    t=string.Empty;

                    objConfig.TryGetValue( "txtFtpPasta", out t);
                    this.txtFtpPasta.Text=t;
                    t=string.Empty;

                    objConfig.TryGetValue( "txtFtpSenha", out t);
                    this.txtFtpSenha.Text=t;
                    t=string.Empty;

                    //diretorio
                    objConfig.TryGetValue( "txtDiretorio", out t);
                    this.txtDiretorio.Text=t;
                    t=string.Empty;



                    this.chkSalvarParametros.Checked = true;




                    
                   objConfig.TryGetValue( "txtFtpHost", out t);
                    this.txtFtpHost.Text=t;










                }
            }

        }


        protected void MudaVisualizacao( object sender, EventArgs e)
        {
            pnlFTP.Visible = rdFtp.Checked;
            pnlDiretorio.Visible = rdDiretorio.Checked;
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {

            if (chkSalvarParametros.Checked)
            {
                //grava a confi 

                Dictionary<string, string> objConfig = new Dictionary<string, string>();
               

                objConfig.Add("txtFtpHost", txtFtpHost.Text);
                objConfig.Add("txtFtpPasta", txtFtpPasta.Text);
                objConfig.Add("txtFtpSenha", txtFtpSenha.Text);
                objConfig.Add("txtFtpUsuario", txtFtpUsuario.Text);
                objConfig.Add("txtDiretorio", txtDiretorio.Text);

                Application.Add("ExportGestaoTreinamentoConfig", objConfig);



            }
            else
            {
                if (Application["ExportGestaoTreinamentoConfig"] != null) Application.Remove("ExportGestaoTreinamentoConfig");

            }
          

            if (rdDiretorio.Checked && string.IsNullOrEmpty(txtDiretorio.Text))
            {
                Util.Avisos.Aviso("Você deve selecioar um diretório!", this.Page);

                return;
            }

            if (rdFtp.Checked && string.IsNullOrEmpty(txtFtpHost.Text))
            {
                Util.Avisos.Aviso("Você deve selecionar um host FTP válido", this.Page);
                return;

            }



          

            Exportacao.Treinamento objExport = new Exportacao.Treinamento();
          
            //binda as propriedades de arquivos
            objExport.PastaTrabalho = Server.MapPath("/Temp").ToString();


            //tenta gerar o arquivo 
            if (!objExport.Exportar())
            {
                Util.Avisos.Aviso("Erro ao gerar o arquivo, o erro foi " + objExport.Erro,this.Page);
                throw objExport.exx;

            }


            

            if (rdDiretorio.Checked) //gravar o arquivo ZIP no diretório selecionado.
            {
                try
                {
                    //copiar o arquivo ZIP para o diretorio selecionado.
                    //se já existir deleta
                    if (File.Exists(txtDiretorio.Text + "\\TR_Exportacao.zip"))
                    {
                        File.Delete(txtDiretorio.Text + "\\TR_Exportacao.zip");

                    }

                    //copia o arquivo para pasta 
                    File.Copy(objExport.CaminhoZip, txtDiretorio.Text + "\\TR_Exportacao.zip");
                    Util.Avisos.Aviso("O arquivo: TR_Exportacao.zip foi criado! na pasta solicitada!", Page);
                }
                catch (Exception ex)
                {
                    Util.Avisos.Aviso("Erro ao salvar o arquivo na pasta. o erro foi " + ex.Message, this.Page);
                }

            }
            else if
              (rdDownload.Checked)
            {
                Response.Clear();
                Response.TransmitFile(objExport.CaminhoZip);
                Response.End();

            }
            else if (rdFtp.Checked)
            {
                try
                {
                    //subir o arquivo no ftp 
                    //
                    FtpLib.FtpConnection conn;

                    if (string.IsNullOrEmpty(txtFtpUsuario.Text))
                    {
                        conn = new FtpLib.FtpConnection(txtFtpHost.Text);
                    }
                    else
                    {
                        conn = new FtpLib.FtpConnection(txtFtpHost.Text, txtFtpUsuario.Text, txtFtpSenha.Text);

                    }

                    conn.Open();
                    conn.Login();

                    if (!string.IsNullOrEmpty(txtFtpPasta.Text))
                        conn.SetCurrentDirectory(txtFtpPasta.Text);



                    conn.PutFile(objExport.CaminhoZip, "ExportacaoTR.zip");

                    Util.Avisos.Aviso("O Arquivo foi salvo no FTP com sucesso!", this.Page);

                }
                catch (Exception ex)
                {
                    Util.Avisos.Aviso("Erro ao subir o arquivo para o FTP, não foi possivel se conectar" + ex.Message, this.Page);
                }

            }

            







        }

       
    }
}