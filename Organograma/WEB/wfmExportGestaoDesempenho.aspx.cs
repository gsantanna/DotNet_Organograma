using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WEB
{
    public partial class wfmExportGestaoDesempenho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //verifica se tem o dicionario no application 
                if (Application["ExportGestaoDesempemhoConfig"] != null)
                {
                    //carrega o dicionario e binda os campos
                    Dictionary<string, string> objConfig =
                    (Application["ExportGestaoDesempemhoConfig"] as Dictionary<string, string>);


                  
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

 Application.Add("ExportGestaoDesempemhoConfig", objConfig);



            }
            else
            {
                if (Application["ExportGestaoDesempemhoConfig"] != null) Application.Remove("ExportGestaoDesempemhoConfig");

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



            //verifica se selecionou algum arquivo
            if (!(gFuncionarios.Checked || gCargos.Checked || gEmpresas.Checked || gTipoDeFuncionario.Checked || gLocalDeTrabalho.Checked || gOrgaos.Checked))
            {
                lblErro.Text = "Você deve selecionar algum arquivo para ser gerado!";
                lblErro.Visible = true;
                return;
            }
            else
            {
                lblErro.Visible = false;
            }


            

            Exportacao.GestaoDesempenho objExport = new Exportacao.GestaoDesempenho();
          
            //binda as propriedades de arquivos
            objExport.gFuncionarios = this.gFuncionarios.Checked;
            objExport.gCargos = this.gCargos.Checked;
            objExport.gEmpresas = this.gEmpresas.Checked;
            objExport.gTipoFuncionario = this.gTipoDeFuncionario.Checked;
            objExport.gLocalTrabalho = this.gLocalDeTrabalho.Checked;
            objExport.gOrgaos = this.gOrgaos.Checked;

            //data de corte
            objExport.DataCorte = cboDtCorte.Date;
            objExport.PastaTrabalho = Server.MapPath("/Temp").ToString();

            //tenta gerar o arquivo 
            if (!objExport.Exportar())
            {
                Util.Avisos.Aviso("Erro ao gerar o arquivo, o erro foi " + objExport.Erro,this.Page); 
                return;
            }


            

            if (rdDiretorio.Checked) //gravar o arquivo ZIP no diretório selecionado.
            {
                try
                {
                    //copiar o arquivo ZIP para o diretorio selecionado.
                    //se já existir deleta
                    if (File.Exists(txtDiretorio.Text + "\\GD_Exportacao.zip"))
                    {
                        File.Delete(txtDiretorio.Text + "\\GD_Exportacao.zip");

                    }

                    //copia o arquivo para pasta 
                    File.Copy(objExport.CaminhoZip, txtDiretorio.Text + "\\GD_Exportacao.zip");
                    Util.Avisos.Aviso("O arquivo: GD_Exportacao.zip foi criado! na pasta solicitada!", Page);
                }
                catch (Exception ex)
                {
                    Util.Avisos.Aviso("Erro ao salvar o arquivo na pasta. o erro foi " + ex.Message, this.Page);
                }

            }
            else if
              (rdDownload.Checked)
            {

                Response.TransmitFile(objExport.CaminhoZip);

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



                    conn.PutFile(objExport.CaminhoZip, "ExportacaoGD.zip");

                    Util.Avisos.Aviso("O Arquivo foi salvo no FTP com sucesso!", this.Page);

                }
                catch
                {
                    Util.Avisos.Aviso("Erro ao subir o arquivo para o FTP, não foi possivel se conectar ao FTP", this.Page);

                }

            }
            
             
             

             


        }

       
    }
}