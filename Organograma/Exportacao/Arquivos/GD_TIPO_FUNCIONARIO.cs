using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class GD_TIPO_FUNCIONARIO 
    {
      


        //setar no constructor se tiver  algum filtro.
        public static string  Carrega()
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
            objStrSaida.AppendLine("COD_TIPO_FUNCIONARIO;NOM_TIPO_FUNCIONARIO"); //CABECALHO


            //Cria o data context
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //linhas nativas
            foreach (Negocio.ORG_TIPO_FUNCIONARIO o in objDs.ORG_TIPO_FUNCIONARIO)
            {
                objStrSaida.AppendLine(string.Format("{0};{1}", o.COD_TIPO_FUNCIONARIO, o.NOM_TIPO_FUNCIONARIO));

            }

            //linhas avulsas
            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_GD_MDL o in objDs.ORG_FUNCIONARIO_EXPORT_GD_MDL.Where(f=> f.TIPO_FUNC_COD_TIPO_FUNCIONARIO!=null && f.TIPO_FUNC_NOM_TIPO_FUNCIONARIO !=null  ))
            {
                objStrSaida.AppendLine(string.Format("{0};{1}", o.TIPO_FUNC_COD_TIPO_FUNCIONARIO, o.TIPO_FUNC_NOM_TIPO_FUNCIONARIO));

            }

           


            return objStrSaida.ToString();

        }
       

    }
}
