using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Negocio;


using System.Text;
using System.ComponentModel;

namespace Manutencao
{
    public class Manutencao
    {

        public void ExecutaManutencao()
        {
                LimpaFuncionarios();
                LimpaSecoes();
        }

        /// <summary>
        /// Limpa todos os funcionários e gerentes que não estão contidos na view 
        /// org_funcionario
        /// </summary>
        private void LimpaFuncionarios()
        {   
           
            
            //cria o contexto de dados 
            using (gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext())
            {

                StringBuilder strGestores = new StringBuilder();
                strGestores.Append(" UPDATE ORG_HIERARQUIA SET GESTOR_MOD='0' , GESTOR='' ");
                strGestores.Append(" WHERE GESTOR in");
                strGestores.Append(" (Select h.GESTOR  from ORG_HIERARQUIA  h  Left outer join ORG_FUNCIONARIO fu on h.GESTOR = fu.CPF   ");
                strGestores.Append("  where h.GESTOR is not null and fu.CPF is null)  ");

                



                objDs.ExecuteStoreCommand(strGestores.ToString(),null);
               


                StringBuilder strAlocados = new StringBuilder();
                strAlocados.Append(" DELETE FROM ORG_FUNCIONARIO_SUBSECAO WHERE CPF in ");
                strAlocados.Append(" (SELECT fs.CPF FROM ORG_FUNCIONARIO_SUBSECAO fs ");
                strAlocados.Append(" Left outer join ORG_FUNCIONARIO fu on fs.CPF=fu.CPF ");
                strAlocados.Append(" where fu.CPF is null)");
               

                objDs.ExecuteStoreCommand(strAlocados.ToString(), null);

                objDs.ExecuteStoreCommand("commit");

                /*
                //apaga o funcionário da ORG_FUNCIONARIO_SUBSECAO
                var tes = from f in
                              objDs.ORG_FUNCIONARIO_SUBSECAO.Except(from f in objDs.ORG_FUNCIONARIO_SUBSECAO
                                                                    join fu in objDs.ORG_FUNCIONARIO on f.CPF equals fu.CPF
                                                                    select f)
                          select f;

                foreach (ORG_FUNCIONARIO_SUBSECAO fu in tes)
                {
                    AuditTrail.GravaLogAuditTrail("MANUTENÇÃO", string.Format("O funcionário: {0} foi excluído da seção: {1} por não estar presente na ORG_FUNCIONARIO ", fu.CPF, fu.COD_SECAO), "");
                    objDs.ORG_FUNCIONARIO_SUBSECAO.DeleteObject(fu);

                }





                //grava as mudanças.
                objDs.SaveChanges();
                */

            }
        


        }

        /// <summary>
        /// Limpa todas as subseções que não estejam contidas na lista de seções 
        /// </summary>
        private void LimpaSecoes()
        {

            using (gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext())
            {

                StringBuilder strSecoes = new StringBuilder();

                strSecoes.AppendLine("DELETE FROM ORG_FUNCIONARIO_SUBSECAO where COD_SECAO in (");
                strSecoes.AppendLine("SELECT h.COD_SECAO FROM ORG_HIERARQUIA h ");
                strSecoes.AppendLine("LEFT OUTER JOIN ORG_SECAO s on s.cod_secao=h.cod_secao_sup ");
                strSecoes.AppendLine("where h.subsecao='1' and s.cod_Secao is null)");

                objDs.ExecuteStoreCommand(strSecoes.ToString(),null);


                StringBuilder strSecoes2 = new StringBuilder();

                strSecoes2.Append("DELETE FROM ORG_HIERARQUIA where COD_SECAO in (");
                strSecoes2.Append("SELECT h.COD_SECAO FROM ORG_HIERARQUIA h ");
                strSecoes2.Append("LEFT OUTER JOIN ORG_SECAO s on s.cod_secao=h.cod_secao_sup ");
                strSecoes2.Append("where h.subsecao='1' and s.cod_Secao is null)");
         

                objDs.ExecuteStoreCommand(strSecoes.ToString(), null);

                objDs.ExecuteStoreCommand("commit");




            }

        }

       
      


        
    }
}
