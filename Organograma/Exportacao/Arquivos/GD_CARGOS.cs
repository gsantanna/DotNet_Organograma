using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Negocio;

namespace Exportacao.Arquivos
{
    public static class GD_CARGOS 
    {
      

        //setar no constructor se tiver  algum filtro.
        public static string  Carrega()
        {
            //primeira linha é o cabeçalho 
            StringBuilder objStrSaida = new StringBuilder();
            objStrSaida.AppendLine("COD_CARGO;COD_MACRO_CARGO;DSC_CARGO"); //CABECALHO


            //Cria o data context
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //linhas nativas
            foreach (Negocio.ORG_CARGO o in objDs.ORG_CARGO.Where( f=> f.DSC_TIPO_FUNC != "14"))
            {
                objStrSaida.AppendLine(string.Format("{0};{1};{2}", o.COD_CARGO, 1, o.DSC_CARGO));

            }

            //linhas avulsas
            foreach (Negocio.ORG_FUNCIONARIO_EXPORT_GD_MDL o in objDs.ORG_FUNCIONARIO_EXPORT_GD_MDL.Where(f=> f.CARGO_COD_CARGO != null  && f.CARGO_DSC_CARGO!= null))
            {
                objStrSaida.AppendLine(string.Format("{0};{1};{2}", o.CARGO_COD_CARGO, 1, o.CARGO_DSC_CARGO));
                
            }


            


            return objStrSaida.ToString();

        }
       

    }
}
