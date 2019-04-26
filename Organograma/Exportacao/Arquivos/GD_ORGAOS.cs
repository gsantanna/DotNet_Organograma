using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class GD_ORGAOS 
    {
      


        //setar no constructor se tiver  algum filtro.
        public static string  Carrega()
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
            objStrSaida.AppendLine("COD_ORGAO;COD_ORGAO_SUPERIOR;COD_DIRETORIA;SGL_ORGAO;NOM_ORGAO;IND_ATIVO"); //CABECALHO


            //Cria o data context
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //linhas nativas SEÇÕES
            foreach (ORG_LISTA_SECOES_CONSOLIDADO s in objDs.ORG_LISTA_SECOES_CONSOLIDADO)
            {

                objStrSaida.AppendLine(string.Format("{0};{1};{2};{3};{4};{5}",
                    s.COD_SECAO,
                    (  s.POSSUI_SUPERIOR != null && s.POSSUI_SUPERIOR.Equals("1")) ? "0" : s.COD_SECAO_SUP,
                          (s.POSSUI_SUPERIOR != null && s.POSSUI_SUPERIOR.Equals("1")) ? "" : s.COD_DIRETORIA,
                    "",
                    s.NOME_SECAO,
                    "S"            ));
            }


            //linhas avulsas 
            foreach (ORG_FUNCIONARIO_EXPORT_GD_MDL s in objDs.ORG_FUNCIONARIO_EXPORT_GD_MDL.Where (f => f.ORGAOS_COD_ORGAO != null && f.ORGAOS_COD_DIRETORIA!=null && f.ORGAOS_NOM_ORGAO != null))
            {

                objStrSaida.AppendLine(string.Format("{0};{1};{2};{3};{4};{5}",
                    s.ORGAOS_COD_ORGAO,
                    s.ORGAOS_COD_ORGAO_SUPERIOR, 
                        s.ORGAOS_COD_DIRETORIA,
                    "",
                  s.ORGAOS_NOM_ORGAO,
                    "S"));
            }






            return objStrSaida.ToString();

        }
       

    }
}
