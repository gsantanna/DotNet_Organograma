using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class TR_FUNCIONARIOS 
    {

        //setar no constructor se tiver  algum filtro.
        public static string  Carrega()
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
           
           // objStrSaida.AppendLine("FIRSTNAME;LASTNAME;EMAIL;USERNAME;PASSWORD;PERSONSTATUS;USERSTATUS;CPF;COD_EMPRESA;EMPRESA;COD_FILIAL;NOME_FILIAL;DATA_ADMISSAO;SECAO;NOME_SECAO;CHAPA;CÓDIGO_FUNCAO;FUNCAO;CPF_GESTOR;NOME_GESTOR;EMAIL_GESTOR;SEXO;DTNASCIMENTO;CODTIPO;DESCRICAO_TIPO_FUNCIONARIO;PERFIL_PROFISSIONAL;COD_SECAO_SUPERIOR;NOME_SECAO_SUPERIOR;COD_DIRETORIA;NOME_DIRETORIA"); //CABECALHO orig

            objStrSaida.AppendLine("FIRST_NAME;LAST_NAME;EMAIL;USERNAME;PASSWORD;PERSONSTATUS;USERSTATUS;CPF;COD_EMPRESA;NOME_EMPRESA;COD_FILIAL;NOME_FILIAL;DAT_ADMISSAO;COD_SECAO;NOME_SECAO;CHAPA;COD_CARGO;NOME_FUNCAO;CPF_GESTOR;NOME_GESTOR;EMAIL_GESTOR;SEXO;DATA_NASCIMENTO;COD_TIPO_FUNC;DESC_TIPO_FUNC;COD_SECAO_SUP;NOME_SECAO_SUP;COD_DIRETORIA;NOME_DIRETORIA;PERFIL_PROFISSIONAL"); //cab novo 

                
            //Cria o data context
            Negocio.gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();

            try
            {

                //linhas nativas
                foreach (Negocio.ORG_FUNCIONARIO_EXPORT_TR o in   objDs.ORG_FUNCIONARIO_EXPORT_TR)
                {
                    objStrSaida.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26};{27};{28};{29}",
                        o.FIRST_NAME,
                        o.LAST_NAME,
                        o.EMAIL, 
                        o.USERNAME, 
                        o.PASSWORD,
                        o.PERSONSTATUS,
                        o.USERSTATUS,
                        o.CPF, 
                        o.COD_EMPRESA, 
                        o.NOME_EMPRESA,
                        o.COD_FILIAL, 
                        o.NOME_FILIAL, 
                        DateTime.ParseExact(o.DAT_ADMISSAO,"yyyyMMdd",null).ToString("dd/MM/yyyy") ,
                        o.COD_SECAO,
                        o.NOME_SECAO, 
                        o.CHAPA, 
                        o.COD_CARGO,
                        o.NOME_FUNCAO,
                        o.CPF_GESTOR,
                        o.NOME_GESTOR,
                        o.EMAIL_GESTOR,
                        o.SEXO,
                        o.DATA_NASCIMENTO,
                        o.COD_TIPO_FUNC,
                        o.DESC_TIPO_FUNC,
                        o.COD_SECAO_SUP,
                        o.NOME_SECAO_SUP,
                        o.COD_DIRETORIA,
                        o.NOME_DIRETORIA,
                        o.PERFIL_PROFISSIONAL
                        ));
                    
                }

            }
            catch  (Exception ex)
            {

                throw (ex);
            }

                

            //linhas Avulsas
            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_TR_MDL o in objDs.ORG_FUNCIONARIO_EXPORT_TR_MDL)
            {

                objStrSaida.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26};{27};{28};{29}",
                        o.FIRST_NAME,
                        o.LAST_NAME,
                        o.EMAIL,
                        o.USERNAME,
                        o.PASSWORD,
                        o.PERSONSTATUS,
                        o.USERSTATUS,
                        o.CPF,
                        o.COD_EMPRESA,
                        o.NOME_EMPRESA,
                        o.COD_FILIAL,
                        o.NOME_FILIAL,
                        o.DAT_ADMISSAO, 
                        o.COD_SECAO,
                        o.NOME_SECAO,
                        o.CHAPA,
                        o.COD_CARGO,
                        o.NOME_FUNCAO,
                        o.CPF_GESTOR,
                        o.NOME_GESTOR,
                        o.EMAIL_GESTOR,
                        o.SEXO,
                        o.DATA_NASCIMENTO,
                        o.COD_TIPO_FUNC,
                        o.DESC_TIPO_FUNC,
                        o.COD_SECAO_SUP,
                        o.NOME_SECAO_SUP,
                        o.COD_DIRETORIA,
                        o.NOME_DIRETORIA,
                        o.PERFIL_PROFISSIONAL
                        ));



            }
            





            return objStrSaida.ToString();

        }
       

    }
}
