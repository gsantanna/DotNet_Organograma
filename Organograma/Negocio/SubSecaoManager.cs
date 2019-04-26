using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
   public  class SubSecaoManager
    {

       public string Erro { get; set; }


       public bool ExcluiSubsecao(string id_subsecao)
       {

           //verifica se a sunseção tem funcionários alocados 
           //caso tenha não permite a exclusão
          using (  gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext())
          {




           var funcionarios = (from f in objDs.ORG_FUNCIONARIO_ALOCADO
                               where f.COD_SECAO.Equals(id_subsecao)
                               select new { f.NOME }).Distinct();





           //não poderá excluir!
           if (funcionarios.Count() > 0)
           {
               StringBuilder objStrErro = new StringBuilder();
               objStrErro.Append(@"Você não pode excluir esta subseção pois existem funcionários alocados.\n");
               foreach (object f in funcionarios)
               {
                   objStrErro.Append(string.Format(@"{0}\n", f.ToString().Replace("{ NOME =", "").Replace("}", "")));
               }


               Erro = objStrErro.ToString();
               return false;
           }
           else
           {
               //Log no Audit 
               AuditTrail.GravaLogAuditTrail("SUBSEÇÃO EXCLUIDA", id_subsecao, "");

               //deleta a hierarquia da subseção.
               objDs.ORG_HIERARQUIA.DeleteObject(
                   objDs.ORG_HIERARQUIA.Where(f => f.COD_SECAO.Equals(id_subsecao)).FirstOrDefault());

               objDs.SaveChanges();
               return true;
           }

           }

       }


       public bool AdicionaOuRecriaSubsecao(ORG_HIERARQUIA secao, List< ORG_FUNCIONARIO_ALOCADO> funcionarios , bool Logar)
       {


           if (Logar)
           {
               string strAtu = "", strAnt = ""; 

               //cria a linha de novos registros
               strAtu = string.Format("SUBSEÇÃO:{0} CÓDIGO:{1} SEÇÃO_SUP: {2} GESTOR: {3} - FUNCIONÁRIOS: (",
                   secao.NOME,
                   secao.COD_SECAO,
                   secao.COD_SECAO_SUP,
                   secao.GESTOR
                   );

               foreach (ORG_FUNCIONARIO_ALOCADO fu in funcionarios)
                   strAtu += string.Format("{0} ,", fu.CPF);


               strAtu += ")";
               strAtu = strAtu.Replace(",)", ")");


               AuditTrail.GravaLogAuditTrail("CRIAÇÃO DE SUBSEÇÃO", strAtu, strAnt);

           }

           using (gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext())
           {

               //remove os funcionarios alocados na hierarquia (caso exista)
               foreach (ORG_FUNCIONARIO_SUBSECAO fu in
                               objDs.ORG_FUNCIONARIO_SUBSECAO.Where(f => f.COD_EMPRESA.Equals(secao.COD_EMPRESA) && f.COD_SECAO.Equals(secao.COD_SECAO)))
               {


                   //verifica se o funcionário é gestor de alguma subseção/secao com gestor modificado
                   var funcAloc = from h in objDs.ORG_HIERARQUIA
                                  where h.GESTOR_MOD.Equals("1") && h.GESTOR.Equals(fu.CPF)
                                  select h;

                   //caso encontre algum resultado, atualiza a tabela de hierarquia setando 
                   //o flag de gestor modificado como 0 e o CPF do gestor como vazio.
                   foreach (ORG_HIERARQUIA fGest in funcAloc)
                   {
                       fGest.GESTOR = string.Empty;
                       fGest.GESTOR_MOD = "0";
                       objDs.SaveChanges();

                   }


                   //remove ele da  tabela de hierarquia
                   objDs.ORG_FUNCIONARIO_SUBSECAO.DeleteObject(fu);

               }

               //remove o registro da hierarquia (caso exista)
               foreach (ORG_HIERARQUIA h in
                   objDs.ORG_HIERARQUIA.Where(f => f.COD_SECAO.Equals(secao.COD_SECAO) &&
                                                   f.COD_EMPRESA.Equals(secao.COD_EMPRESA)))
               {
                   objDs.ORG_HIERARQUIA.DeleteObject(h);

               }


               //adiciona a nova hierarquia 

               objDs.ORG_HIERARQUIA.AddObject(secao);


               //adiciona os funcionarios
               foreach (ORG_FUNCIONARIO_ALOCADO fu in funcionarios)
               {
                   ORG_FUNCIONARIO_SUBSECAO objSub = new ORG_FUNCIONARIO_SUBSECAO();
                   objSub.COD_SECAO = secao.COD_SECAO;
                   objSub.COD_EMPRESA = secao.COD_EMPRESA;
                   objSub.CPF = fu.CPF;

                   objDs.ORG_FUNCIONARIO_SUBSECAO.AddObject(objSub);
               }



               objDs.SaveChanges();

               return true;

           }
       }

       public bool AtualizaSubsecao( string strCodSecaoOrignal, string strCodEmpresaOriginal, ORG_HIERARQUIA secaoNova, List<ORG_FUNCIONARIO_ALOCADO> funcionarios)
       {
           string strAtu = "", strAnt="";
           

         //cria o contexto de dados 
           using (gsatOrganogramaDataContext objDS = new gsatOrganogramaDataContext())
           {

               //deleta a hierarquia
               ORG_HIERARQUIA objHierarquia = objDS.ORG_HIERARQUIA.Where(f => f.COD_SECAO.Equals(strCodSecaoOrignal) && f.COD_EMPRESA.Equals(strCodEmpresaOriginal)).FirstOrDefault();

               if (objHierarquia == null)
               {
                   Erro = "Sub Seção não encontrada ou modificada por outro usuário";
                   return false;

               }
               else
               {
                   strAnt=
                   string.Format("SUBSEÇÃO:{0} CÓDIGO:{1} SEÇÃO_SUP: {2} GESTOR: {3} - FUNCIONÁRIOS: (",
                   objHierarquia.NOME,
                   objHierarquia.COD_SECAO,
                   objHierarquia.COD_SECAO_SUP,
                   objHierarquia.GESTOR);

                   
               }

               //exclui os funcionarios alocados 
               foreach (ORG_FUNCIONARIO_SUBSECAO fu in
               objDS.ORG_FUNCIONARIO_SUBSECAO.Where
                   (f => f.COD_SECAO.Equals(strCodSecaoOrignal) &&
                       f.COD_EMPRESA.Equals(strCodEmpresaOriginal)))
               {
                   strAnt += string.Format("{0} ,", fu.CPF);
                   objDS.ORG_FUNCIONARIO_SUBSECAO.DeleteObject(fu);
               }

               strAnt += ")";
               strAnt = strAnt.Replace(",)", ")");

               

               //exclui a hierarquia (original)
               objDS.ORG_HIERARQUIA.DeleteObject(objHierarquia);

               //salva info no bd
               objDS.SaveChanges();



               //cria a linha de novos registros
               strAtu = string.Format("SUBSEÇÃO:{0} CÓDIGO:{1} SEÇÃO_SUP: {2} GESTOR: {3} - FUNCIONÁRIOS: (",
                   secaoNova.NOME,
                   secaoNova.COD_SECAO,
                   secaoNova.COD_SECAO_SUP,
                   secaoNova.GESTOR
                   );

               foreach (ORG_FUNCIONARIO_ALOCADO fu in funcionarios)
                  strAtu += string.Format("{0} ,", fu.CPF);


               strAtu += ")";
               strAtu = strAtu.Replace(",)", ")");

               //grava o audit trail 
               AuditTrail.GravaLogAuditTrail("ALTERAÇÃO DE SUBSEÇÃO", strAnt, strAtu);


               //cria a nova 
               return AdicionaOuRecriaSubsecao(secaoNova, funcionarios, false);

           }

       }









       class NomeTemp
       {
           NomeTemp( string strNome)
           {
               Nome = strNome;
           }
           string Nome { get; set; }
       }


    }
}
