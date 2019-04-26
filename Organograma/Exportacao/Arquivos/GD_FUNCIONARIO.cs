using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class GD_FUNCIONARIO 
    {
      


        //setar no constructor se tiver  algum filtro.
        public static string  Carrega( DateTime DataCorte)
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
            objStrSaida.AppendLine("COD_FUNCIONARIO;NUM_MATRICULA;COD_EMPRESA;COD_ORGAO;COD_CARGO;COD_SUPERVISOR;DSC_DOMINIO;DSC_LOGON;DSC_SENHA;NOM_FUNCIONARIO;IND_SEXO;DAT_ADMISSAO;DSC_EMAIL;COD_TIPO_FUNCIONARIO;COD_TIPO_USUARIO;DAT_NASCIMENTO;COD_LOCAL_TRABALHO;COD_CPF;DSC_APELIDO;DAT_INICIO_FERIAS;DAT_FIM_FERIAS;COD_SUPERVISOR_FUNCIONAL;DSC_GRADE_SALARIAL;DAT_INICIO_CARGO;COD_IDIOMA_PREF;COD_PAIS;COD_UF"); //CABECALHO


            //Cria o data context
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();

                



            //linhas nativas
            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_GD o in objDs.ORG_FUNCIONARIO_EXPORT_GD.Where(f => f.COD_TIPO_FUNC != "14"  ))
            {

                DateTime dtAdmissao = DateTime.ParseExact(o.DAT_ADMISSAO, "yyyyMMdd", null);

                if (dtAdmissao <= DataCorte)
                {
                    objStrSaida.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26}",
                        o.COD_FUNCIONARIO, o.NUM_MATRICULA, o.COD_EMPRESA, o.COD_ORGAO, o.COD_CARGO, o.COD_SUPERVISOR, "", o.DSC_LOGON, "", o.NOM_FUNCIONARIO, o.SEXO,
                         dtAdmissao.ToString("dd/MM/yyyy"), o.DSC_EMAIL, o.COD_TIPO_FUNC, "", o.DAT_NASCIMENTO, o.COD_FILIAL, "", "", "", "", "", "", "", "1", "", ""));
                }



            }


            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_GD_MDL o in objDs.ORG_FUNCIONARIO_EXPORT_GD_MDL.Where(f=>f.NOM_FUNCIONARIO != null ))
            {

                objStrSaida.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26}",
                       o.COD_FUNCIONARIO, o.NUM_MATRICULA, o.COD_EMPRESA, o.COD_ORGAO, o.COD_CARGO, o.COD_SUPERVISOR, "", o.DSC_LOGON, "", o.NOM_FUNCIONARIO, o.SEXO,
                       o.DAT_ADMISSAO, o.DSC_EMAIL, o.COD_TIPO_FUNC, "", o.DAT_NASCIMENTO, o.COD_FILIAL, "", "", "", "", "", "", "", "1", "", ""));
            

               
            }




            return objStrSaida.ToString();

        }
       

    }
}
