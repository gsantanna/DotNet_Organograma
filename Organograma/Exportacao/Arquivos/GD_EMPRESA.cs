using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class GD_EMPRESA 
    {
      


        //setar no constructor se tiver  algum filtro.
        public static string  Carrega()
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
            objStrSaida.AppendLine("COD_EMPRESA;NOM_EMPRESA"); //CABECALHO


            //Cria o data context
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //linhas nativas
            foreach (Negocio.ORG_EMPRESA o in objDs.ORG_EMPRESA)
            {
                objStrSaida.AppendLine( string.Format("{0};{1}", o.COD_EMPRESA,  o.DESCRICAO));
            }

            //linhas avulsas
            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_GD_MDL o in objDs.ORG_FUNCIONARIO_EXPORT_GD_MDL.Where(f=> f.EMPRESA_COD_EMPRESA !=null && f.EMPRESA_NOM_EMPRESA!=null))
            {
                objStrSaida.AppendLine(string.Format("{0};{1}", o.EMPRESA_COD_EMPRESA, o.EMPRESA_NOM_EMPRESA));
            }
             


           



            return objStrSaida.ToString();

        }
       

    }
}
