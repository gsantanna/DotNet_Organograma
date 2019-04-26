using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Negocio
{
    public static class AuditTrail
    {
          public static void GravaLogAuditTrail( string Operacao, string vlAnt, string vlAtu )
          {
              //não foi feito nenhuma alteração não será necessário salvar.
              if (vlAnt == vlAtu) return;

              ORG_AUDIT_TRAIL objAT = new ORG_AUDIT_TRAIL();
              
              objAT.DATA_HORA = DateTime.Now;
              objAT.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
              objAT.OPERACAO = Operacao;
              objAT.USUARIO = System.Web.HttpContext.Current.User.Identity.Name;
              objAT.VL_ANTERIOR = vlAnt;
              objAT.VL_ATUAL = vlAtu;

              //chama o processo de inclusão em outra thread (Async.)
              IniciaEmOutraThread(objAT);
          }

          public static void IniciaEmOutraThread(object Param)
          {
              ThreadPool.QueueUserWorkItem(_ =>
              {
                  using (Negocio.gsatOrganogramaDataContext objDS = new gsatOrganogramaDataContext())
                  {
                      ORG_AUDIT_TRAIL objAT = Param as ORG_AUDIT_TRAIL;
                      objDS.ORG_AUDIT_TRAIL.AddObject(objAT);
                      objDS.SaveChanges();
                  }
              });
          }



    }
}