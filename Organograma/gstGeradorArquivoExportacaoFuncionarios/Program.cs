using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

using System.Data;


namespace gstGeradorArquivoExportacaoFuncionarios
{
    class Program
    {


        static void Main(string[] args)
        {


         


            //host, ftp, senha , pasta 
            try
            {
                
                Console.Clear();

                Console.WriteLine("");
                Console.WriteLine("            EXPORTADOR ARQUIVOS GLOBOSAT ORGANOGRAMA                ");
                Console.WriteLine("                SISTEMA DE GESTÃO DE DESEMPENHO                     ");
                Console.WriteLine("");

                
                if (args.Length != 4)
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Erro de linha de comando, o uso correto:");
                    Console.WriteLine("Gerador.exe  [host FTP] [usuario FTP] [SENHA FTP] [PASTA FTP]");
                    return;
                }

            }
            catch
            {
                return;
            }


            Console.WriteLine("Iniciando exportação");

            //cria o objeto de exportação 
            Exportacao.Treinamento objExportTr = new Exportacao.Treinamento();
            objExportTr.PastaTrabalho = Directory.GetCurrentDirectory();
          

            if (objExportTr.Exportar())
            {
                Console.WriteLine("\nArquivo: " + objExportTr.CaminhoTxt + " criado!");
            }
            else
            {
                Console.WriteLine("Erro ao executar o processo de exportação, o erro foi: " + objExportTr.Erro);
                Console.WriteLine(objExportTr.exx.ToString());
                throw (objExportTr.exx);

            }



            Console.WriteLine("Conectando ao FTP");


            string HostFtp = args[0];
            string UsuarioFtp = args[1];
            string SenhaFTP = args[2];
            string PastaFTP = args[3];



            FtpLib.FtpConnection conn;

            conn = new FtpLib.FtpConnection(HostFtp, 21, UsuarioFtp, SenhaFTP);
           
            Console.WriteLine("Abrindo conexão FTP..");
            conn.Open();
            Console.WriteLine("Efetuando Login..");
            conn.Login();
            Console.WriteLine("Mudando para a pasta : " + PastaFTP);
            conn.SetCurrentDirectory(PastaFTP);
            Console.WriteLine("Realizando upload do arqivo");
            conn.PutFile(objExportTr.CaminhoTxt, "dados.csv");
            Console.WriteLine("Operação concluída!");
            conn.Close();

            


        }
    }
}
