using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class GD_LOCAL_TRABALHO 
    {
      


        //setar no constructor se tiver  algum filtro.
        public static string  Carrega()
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
            objStrSaida.AppendLine("COD_LOCAL_TRABALHO;NOM_LOCAL_TRABALHO"); //CABECALHO


            //Cria o data context
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();

             
            //linhas nativas
            foreach (Negocio.ORG_LOCAL_TRABALHO o in objDs.ORG_LOCAL_TRABALHO)
            {
                objStrSaida.AppendLine(string.Format("{0};{1}", o.COD_LOCAL_TRABALHO, o.NOM_LOCAL_TRABALHO));

            }

            //linhas avulsas
            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_GD_MDL o in objDs.ORG_FUNCIONARIO_EXPORT_GD_MDL.Where(f=> f.LOC_TRAB_COD_LOCAL!=null && f.LOC_TRAB_NOM_LOCAL_TRABALHO !=null ))
            {
                objStrSaida.AppendLine(string.Format("{0};{1}", o.LOC_TRAB_COD_LOCAL, o.LOC_TRAB_NOM_LOCAL_TRABALHO));

            }


            return objStrSaida.ToString();

        }
       

    }
}
