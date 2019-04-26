
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Globalization;

namespace Exportacao
{

    public class Treinamento
    {

        //pasta de trabalho para a operaçao 
        public string PastaTrabalho { get; set; }




        public string Erro { get; set; }

        public string CaminhoZip { get; private set; }
        public string CaminhoTxt { get; private set; }

        public Exception exx { get; private set; }

        public bool Exportar()
        {

            try
            {

                //limpa os arquivos existentes
                foreach (string s in Directory.GetFiles(PastaTrabalho))
                {
                    if( s.EndsWith(".txt") || s.EndsWith(".csv") || s.EndsWith(".zip"))
                    { 
                        File.Delete(s);
                    }
                }

                

            }
            catch (Exception ex)
            {
                Erro = "Erro ao apagar os arquivos temporários. " + ex.Message;

                return false;
            }


            try
            {

                CultureInfo ci = new CultureInfo("pt-BR");


                //para cada arquivo selecionado gera o arquivo e salva na pasta temporária 
                TextWriter tw = new StreamWriter(PastaTrabalho + "/dados.csv", false, Encoding.GetEncoding("iso-8859-1"));
                tw.Write(Arquivos.TR_FUNCIONARIOS.Carrega());
                tw.Close();


            }
            catch (Exception ex)
            {
                Erro = "Erro interno ao gerar o arquivo de funcionários, o erro foi: " + ex.Message;
                exx = ex;

                return false;
            }

            System.Threading.Thread.Sleep(500);


            try
            {

                //cria o ZIP
                ICSharpCode.SharpZipLib.Zip.FastZip objZip = new ICSharpCode.SharpZipLib.Zip.FastZip();
                objZip.CreateZip(PastaTrabalho + "\\ExportacaoTR.zip", PastaTrabalho + "\\", true, @"^.+\.((csv)|(doc)|(docx)|(rtf)|(kkk))$");


                CaminhoTxt = PastaTrabalho + "\\dados.csv";
                CaminhoZip = PastaTrabalho + "\\ExportacaoTR.zip";
            }
            catch (Exception ex)
            {
                Erro = "Erro ao criar arquivo ZIP o erro foi: " + ex.Message;
                return false;
            }



            return true;
        }







    }
}


