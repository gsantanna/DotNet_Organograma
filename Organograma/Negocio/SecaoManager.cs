using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
  public  class SecaoManager
    {

      public string Erro { get; set; }

      public bool AtualizaSecao( ORG_LISTA_SECOES objSecao )
      {

          string strAtu = "", strAnt = "";



          //cria o datacontext
          gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();
          

          //verifica se existe a entrada desta secao na tabela de hierarquia.
          var objHierarquia = (from h in objDs.ORG_HIERARQUIA
                               where h.COD_SECAO.Equals(objSecao.COD_SECAO) &&
                                     h.COD_EMPRESA.Equals(objSecao.COD_EMPRESA)
                               select h).FirstOrDefault();

          bool add = false;

          if (objHierarquia != null)
          {
              strAnt = string.Format("COD_SECAO:{0}, NOME_MOD:({1} {2}), GESTOR_MOD:({3} {4}), PUBLICO:{5}, POSSUI_SUPERIOR?:{6} " , 
                  objHierarquia.COD_SECAO, 
                  objHierarquia.NOME_MOD.Equals("1") ? "Sim": "Não",
                  objHierarquia.NOME, 
                  objHierarquia.GESTOR_MOD.Equals("1")? "Sim": "Não",
                  objHierarquia.GESTOR, 
                  objHierarquia.PUBLICO.Equals("1")? "Sim" : "Não",
                  objHierarquia.POSSUI_SUPERIOR.Equals("1")? "Sim" : "Não");
          }
          else
          {
              add = true;
              objHierarquia = new ORG_HIERARQUIA();
          }

           objHierarquia.COD_EMPRESA = objSecao.COD_EMPRESA;
           objHierarquia.COD_SECAO = objSecao.COD_SECAO;
           objHierarquia.COD_SECAO_SUP = objSecao.COD_SECAO_SUP;
           objHierarquia.COD_EMPRESA_SUP = objSecao.COD_EMPRESA_SUP;
           objHierarquia.GESTOR = objSecao.CPF_GESTOR_MOD;
           objHierarquia.GESTOR_MOD = objSecao.GESTOR_MOD;
           objHierarquia.NOME = objSecao.NOME_MODIFICADO;
           objHierarquia.NOME_MOD = objSecao.NOME_MOD;
           objHierarquia.PUBLICO = objSecao.PUBLICO;
           objHierarquia.POSSUI_SUPERIOR = objSecao.POSSUI_SUPERIOR;


            strAtu = string.Format("COD_SECAO:{0}, NOME_MOD:({1} {2}), GESTOR_MOD:({3} {4}), PUBLICO:{5}, POSSUI_SUPERIOR?:{6} ",
                   objHierarquia.COD_SECAO,
                   objHierarquia.NOME_MOD.Equals("1") ? "Sim" : "Não",
                   objHierarquia.NOME,
                   objHierarquia.GESTOR_MOD.Equals("1") ? "Sim" : "Não",
                   objHierarquia.GESTOR,
                   objHierarquia.PUBLICO.Equals("1") ? "Sim" : "Não",
                   objHierarquia.POSSUI_SUPERIOR.Equals("1") ? "Sim" : "Não");


          //caso o objeto tenha sido adicionado 
          if (add) objDs.ORG_HIERARQUIA.AddObject(objHierarquia);

          //commit nas mudanças
          objDs.SaveChanges();

          AuditTrail.GravaLogAuditTrail("ALTERAÇÃO DE SEÇÃO", strAnt, strAtu);


          return true;

        

      }





    }
}
