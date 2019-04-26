using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
   public class gsatOrganogramaDataContext : gsOrganogramaModel
    {
        //carrega o proxy com a connectionstring 
       public gsatOrganogramaDataContext()
           : base(global::System.Configuration.ConfigurationManager.ConnectionStrings["gsOrganogramaModel"].ConnectionString)
       {
            
       }




    }
}
