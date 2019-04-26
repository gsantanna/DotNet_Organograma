using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
    public class DiretoriaManager
    {

        public string Erro { get; set; }

        public bool AtualizaDiretoria( List<ORG_LISTA_DIRETORIAS> lista  , String Empresa)
        {
            
            //atualiza todas os registros existentes desmarca como diretoria

            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();
            var dirsAtuais = from k in
                                 objDs.ORG_HIERARQUIA
                             where k.DIRETORIA.Equals("1") && k.COD_EMPRESA.Equals(Empresa)
                             select k;

            string strAnterior = "DIRETORIAS: (";
            string strAtual = "DIRETORIAS: (";

            foreach (ORG_HIERARQUIA h in dirsAtuais)
            {
                //Log de Auditoria Anterior
                strAnterior += string.Format("{0},", h.COD_SECAO);
                h.DIRETORIA = "0";
            }

            //grava a limpeza nas hierarquias
            objDs.SaveChanges();
                
          

            //adiciona a flag de diretoria aos novos
            foreach (ORG_LISTA_DIRETORIAS d in lista)
            {
                //Log de Auditoria Atual
                strAtual += string.Format("{0},", d.COD_SECAO);



                //verificar se já tem a entrada desta secao na tabela de hierarquia
                ORG_HIERARQUIA objH = objDs.ORG_HIERARQUIA.Where(f => f.COD_SECAO.Equals(d.COD_SECAO)).FirstOrDefault();

                if(objH == null ||  string.IsNullOrEmpty( objH.COD_SECAO)) //nao possui registro na tabela de hierarquia
                {
                    objH = new ORG_HIERARQUIA();

                    objH.COD_EMPRESA = d.COD_EMPRESA;
                    objH.COD_SECAO = d.COD_SECAO;
                    objH.DIRETORIA = "1";
                    objDs.ORG_HIERARQUIA.AddObject(objH);

                    
                } else //possui registro, atualizar o registro existente.
                {
                    objH.DIRETORIA = "1";
                    
                    
                }
                

            }

            //grava a hierarquia
            objDs.SaveChanges();

            //grava o audit trail 
            strAnterior += ")";
            strAtual += ")";

            strAnterior = strAnterior.Replace(",)","");
            strAtual = strAtual.Replace(",)","");

            AuditTrail.GravaLogAuditTrail("ALTERAÇÃO DE DIRETORIAS", strAnterior, strAtual);

            return true;

            
        }



    }



}

