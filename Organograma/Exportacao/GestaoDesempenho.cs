
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Exportacao
{

    public class GestaoDesempenho
    {
        public bool gFuncionarios { get; set; }
        public bool gCargos { get; set; }
        public bool gEmpresas { get; set; }
        public bool gTipoFuncionario { get; set; }
        public bool gLocalTrabalho { get; set; }
        public bool gOrgaos { get; set; }

        //data de corte
        public DateTime DataCorte { get; set; }


        //pasta de trabalho para a operaçao 
        public string PastaTrabalho { get; set; }




        public string Erro { get; set; }





        public string CaminhoZip { get; private set; }


        public bool Exportar()
        {

            //Apaga os arquivos temporários caso existam
            

            if (File.Exists(PastaTrabalho + "/GD_Funcionarios.txt")) File.Delete(PastaTrabalho + "/GD_Funcionarios.txt");
            if (File.Exists(PastaTrabalho + "/GD_Cargos.txt")) File.Delete(PastaTrabalho + "/GD_Cargos.txt");
            if (File.Exists(PastaTrabalho + "/GD_Empresas.txt")) File.Delete(PastaTrabalho + "/GD_Empresas.txt");
            if (File.Exists(PastaTrabalho + "/GD_TipoFuncionario.txt")) File.Delete(PastaTrabalho + "/GD_TipoFuncionario.txt");
            if (File.Exists(PastaTrabalho + "/GD_LocalTrabalho.txt")) File.Delete(PastaTrabalho + "/GD_LocalTrabalho.txt");
            if (File.Exists(PastaTrabalho + "/GD_Orgaos.txt")) File.Delete(PastaTrabalho + "/GD_Orgaos.txt");
            if (File.Exists(PastaTrabalho + "/info.txt")) File.Delete(PastaTrabalho + "/info.txt");
           


            //gera os arquivos
            //para cada arquivo selecionado gera o arquivo e salva na pasta temporária 
            if (gFuncionarios)
            {

                if (DataCorte == null) DataCorte = DateTime.Now;
                TextWriter tw = new StreamWriter(PastaTrabalho + "/GD_Funcionarios.txt", false, Encoding.UTF8);
                tw.Write(Arquivos.GD_FUNCIONARIO.Carrega(this.DataCorte));
                tw.Close();
            }


            if (gCargos)
            {
                TextWriter tw = new StreamWriter(PastaTrabalho + "/GD_Cargos.txt", false, Encoding.UTF8);
                tw.Write(Arquivos.GD_CARGOS.Carrega());
                tw.Close();

            }


            if (gEmpresas)
            {
                TextWriter tw = new StreamWriter(PastaTrabalho + "/GD_Empresas.txt", false, Encoding.UTF8);
                tw.Write(Arquivos.GD_EMPRESA.Carrega());
                tw.Close();

            }

            if (gTipoFuncionario)
            {

                TextWriter tw = new StreamWriter(PastaTrabalho + "/GD_TipoFuncionario.txt", false, Encoding.UTF8);
                tw.Write(Arquivos.GD_TIPO_FUNCIONARIO.Carrega());
                tw.Close();


            }


            if (gLocalTrabalho)
            {

                TextWriter tw = new StreamWriter(PastaTrabalho + "/GD_LocalTrabalho.txt", false, Encoding.UTF8);
                tw.Write(Arquivos.GD_LOCAL_TRABALHO.Carrega());
                tw.Close();

            }


            if (gOrgaos)
            {

                TextWriter tw = new StreamWriter(PastaTrabalho + "/GD_Orgaos.txt", false, Encoding.UTF8);
                tw.Write(Arquivos.GD_ORGAOS.Carrega());
                tw.Close();
              

            }


            System.Threading.Thread.Sleep(3000);


            try
            {
                //cria o ZIP
                ICSharpCode.SharpZipLib.Zip.FastZip objZip = new ICSharpCode.SharpZipLib.Zip.FastZip();
                objZip.CreateZip(PastaTrabalho + "\\ExportacaoGD.zip", PastaTrabalho + "\\", true, @"^.+\.((txt)|(doc)|(kkk)|(rtf)|(xxx))$");
            }
            catch (Exception ex)
            {
                Erro += "Erro ao gerar o ZIP  " + ex.Message + @"<br/>";
            }


            CaminhoZip = PastaTrabalho + "\\ExportacaoGD.zip";

            if (string.IsNullOrEmpty(Erro))
            {
                return true;
            }
            else
            {
                throw new Exception(Erro);
            }





        }




    }
}

