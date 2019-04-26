using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data.OracleClient;

namespace Negocio.Validacoes
{

   

    public class Validador
    {

        private string strEmpresa {get;set;}
        private List<ORG_EMPRESA> empresas { get; set; }

        public Validador()
        {
            //cria o contexto de dados
            Negocio.gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();
            empresas = (from s in objDs.ORG_EMPRESA
                        select s).ToList<ORG_EMPRESA>();
            objDs.Dispose();
        }

        /// <summary>
        /// Carrega a lista de [Validacao] com os ítens de Validações a ser exibidas da home
        /// </summary>
        /// <returns></returns>
        public List<Validacao> CarregaValidacoesHome()
        {
            return CarregaValidacoesHome(string.Empty);
        }

        /// <summary>
        /// Carrega a lista de [Validacao] com os ítens de Validações a ser exibidas da home
        /// </summary>
        /// <returns></returns>
        public List<Validacao> CarregaValidacoesHome( string Empresa)
        {
            strEmpresa = Empresa;

            List<Validacao> objRetorno = new List<Validacao>();
            
            //adiciona as 4 validações da home.

            Validacao v1 = ConsultaSecaoSemSuperior();
            Validacao v2 = ConsultaSecaoSemGestor();
            Validacao v3 = ConsultaFuncionarioAlocadoSubsecaoNaoFilha();
            Validacao v4 = ConsultaInconsistenciaCadastroFuncionario();

            if (v1 != null) objRetorno.Add(v1);
            if (v2 != null) objRetorno.Add(v2);
            if (v3 != null) objRetorno.Add(v3);
            if (v4 != null) objRetorno.Add(v4);



            return objRetorno;

        }


        /// <summary>
        /// Carrega a lista de validações de um determinado item de validação (detalhamento)
        /// </summary>
        /// <param name="Empresa">Código da Empresa</param>
        /// <param name="ID">ID do item de validação</param>
        /// <returns></returns>
        public List<Validacao> CarregaValidacoesDetalhe(string Empresa, int ID)
        {
            strEmpresa = Empresa;

            List<Validacao> objRetorno = new List<Validacao>();
            //adiciona a respectiva validação.

         Validacao vDET ;

            switch( ID)
            {
                case 1 : vDET= ConsultaSecaoSemSuperior();
                    break;
                case 2 : vDET= ConsultaSecaoSemGestor();
                    break;
                case 3 : vDET= ConsultaFuncionarioAlocadoSubsecaoNaoFilha();
                    break;
                case 4: vDET = ConsultaInconsistenciaCadastroFuncionario();
                    break;
                default: vDET = null;
                     break;

            }

            if (vDET != null) objRetorno.Add(vDET);
            return objRetorno;


        }


        #region Carga_das_valid  

        /// <summary>
        /// Executa a consulta referente a regra: 
        /// 5.16.1.  O sistema deverá permitir a visualização em tela  
        /// e a extração de relatórios, com as seções e subseções que não 
        /// possuem seção superior e diretoria definidas, com exceção das 
        /// seções que possuem o flag marcado, indicando que não possuem seção superior 
        /// e diretoria. 
        /// </summary>
        /// <returns>Validacao carregada</returns>
        public Validacao ConsultaSecaoSemSuperior()
        {
            //cria o contexto de dados
            Negocio.gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //carrega a validação 
            var lst = (from s in objDs.ORG_LISTA_SECOES_SIMPLES
                      where (s.COD_SECAO_SUP == null || s.COD_SECAO_SUP == "") &&
                      (s.POSSUI_SUP == null || s.POSSUI_SUP == "0")
                      && s.COD_EMPRESA.Contains(strEmpresa)
                      select s).OrderBy(f=>f.COD_EMPRESA);

           

            if (lst.Count() <= 0) return null;

            //cria o objeto de saída 
            Validacao objValid = new Validacao();
            objValid.Id = 1; //validacao de seções sem seção superior
            objValid.dtAtualizacao = DateTime.Now; 
            //adiciona o título 
            if (lst.Count() > 1)
            {
                objValid.Titulo = string.Format("Existem {0} seções/subseções sem seção superior/diretoria definida. Atualizado em: {1}", lst.Count(), DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }
            else
            {
                objValid.Titulo = string.Format("Existe {0} seção/subseção sem seção superior/diretoria definida. Atualizado em: {1}", lst.Count(), DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
          
            }

            foreach (var s in lst)
            {

                //adiciona os itens 
                string emp = empresas.Where(f => f.COD_EMPRESA.Equals(s.COD_EMPRESA)).First().DESCRICAO;


                objValid.ListaValidacaoDetalhe.Add(
                    new ValidacaoDetalhe(  objValid.Id ,
                         string.Format("{0} - {1} - {2}", emp, s.NOME_SECAO, s.COD_SECAO)
                        ,
                        ""
                        ));

            }





            objValid.ListaValidacaoDetalhe = objValid.ListaValidacaoDetalhe.OrderBy(f => f.Titulo).ToList();
            return objValid;


        }




        /// <summary>
        ///5.16.2.  O sistema deverá permitir a visualização em tela e a extração
        ///de relatórios, com as seções que não possuem gestor definido. 
        ///Para os casos das seções que possuem gestor modificado em relação ao sistema de RH,
        ///o sistema deverá verificar se existe um gestor definido neste campo. 
        ///Para os casos das seções sem gestores modificados, o sistema deverá verificar se
        ///existe um gestor definido nos dados disponibilizados pelo sistema de RH
        /// </summary>
        /// <returns></returns>
        public Validacao ConsultaSecaoSemGestor()
        {

            //cria o contexto de dados 
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();

            //carrega as seções sem gestor definido 
            var secoes = from s in objDs.ORG_LISTA_SECOES_SIMPLES
                         where (s.CPF_GESTOR == null || s.CPF_GESTOR.Equals(string.Empty))
                           && s.COD_EMPRESA.Contains(strEmpresa)
                         select s;



            //carrega as subseções sem gestor definido.
            var subSecoes = from s in objDs.ORG_HIERARQUIA
                            where ( s.SUBSECAO.Equals("1") && (s.GESTOR == null || s.GESTOR.Equals(string.Empty))
                             ) && s.COD_EMPRESA.Contains(strEmpresa)
                            select s;


            //caso não tenha nenhuma info volta zerada
            if (secoes.Count() <= 0 && subSecoes.Count() <= 0) return null;
            

            //cria o objeto de saída 
            Validacao objValid = new Validacao();
            objValid.Id = 2; //validacao de seções sem gestor
            objValid.dtAtualizacao = DateTime.Now;
            //adiciona o título 
            objValid.Titulo = string.Format("Existe{2} {0} seções/subseções sem gestor definido. Atualizado em: {1}",   secoes.Count() + subSecoes.Count(), DateTime.Now.ToString("dd/MM/yyyy HH:mm"),  (secoes.Count() + subSecoes.Count())  > 1 ? "s": string.Empty  );


            //adiciona as SEÇÕES no obj saída.
            foreach (var s in secoes)
            {
                //Carrega a empresa (descrição) 
                string emp = empresas.Where(f => f.COD_EMPRESA.Equals(s.COD_EMPRESA)).First().DESCRICAO;

                //Adciona os itens de detalhe 
                objValid.ListaValidacaoDetalhe.Add(
                    new ValidacaoDetalhe(objValid.Id,
                         string.Format("{0} - {1} - {2}", emp, s.NOME_SECAO, s.COD_SECAO), ""));

            }

            //adiciona as SUBSEÇÕES 
            foreach (var s in subSecoes)
            {
                //Carrega a empresa (descrição) 
                string emp = empresas.Where(f => f.COD_EMPRESA.Equals(s.COD_EMPRESA)).First().DESCRICAO;

                //Adciona os itens de detalhe 
                objValid.ListaValidacaoDetalhe.Add(
                    new ValidacaoDetalhe(objValid.Id,
                         string.Format("{0} - {1} - {2}", emp, s.NOME, s.COD_SECAO), ""));

            }


            objValid.ListaValidacaoDetalhe = objValid.ListaValidacaoDetalhe.OrderBy(f => f.Titulo ).ToList();
            return objValid;


        }




        /// <summary>
        /// Executa a consulta referente a regra:
        /// 5.16.3.  O sistema deverá permitir a visualização em  tela e
        /// a extração de relatórios, identificando os funcionários que possuam
        /// subseções que não são filhas da seção associada aos mesmos.
        /// </summary>
        /// <returns></returns>
        public Validacao ConsultaFuncionarioAlocadoSubsecaoNaoFilha()
        {

            //cria o contexto de dados 
            gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //carrega os funcionários 
            var funcionarios = from f in objDs.ORG_FUNCIONARIO
                               join fs in objDs.ORG_FUNCIONARIO_SUBSECAO on f.CPF equals fs.CPF
                               join hi in objDs.ORG_HIERARQUIA on fs.COD_SECAO equals hi.COD_SECAO
                               where (!(hi.COD_SECAO_SUP.Equals(f.COD_SECAO)) && hi.SUBSECAO.Equals("1")
                                ) && hi.COD_EMPRESA.Contains(strEmpresa)
                               select new
                               {
                                   f.NOME_EMPRESA,
                                   f.CPF,
                                   f.NOME,
                                   SECAO_ALOCADO = hi.NOME + " " + hi.COD_SECAO,
                                   SECAO_ALOCADO_SUP = hi.COD_SECAO_SUP,
                                   COD_SECAO_RM = f.COD_SECAO,
                                   NOME_SECAO = hi.NOME,
                                   NOME_SECAO_RM = f.NOME_SECAO + " " + f.COD_SECAO
                               };



            if (funcionarios.Count() <= 0) return null;



            //cria o objeto de saída 
            Validacao objValid = new Validacao();
            objValid.Id = 3; //validacao funcionario em subsecao não filha da que vem do rm
            objValid.dtAtualizacao = DateTime.Now;
            //adiciona o título 
            if (funcionarios.Count() > 1)
            {
                objValid.Titulo = string.Format("Existem {0} Funcionários alocados a subseções não filhas das seções originais. Atualizado em: {1}", funcionarios.Count(), DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }
            else
            {
                objValid.Titulo = string.Format("Existe {0} Funcionário alocado a uma subseção não filha da seção original. Atualizado em: {1}", funcionarios.Count(), DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }


            //adiciona as SEÇÕES no obj saída.
            foreach (var s in funcionarios)
            {
            
                //Adciona os itens de detalhe 
                objValid.ListaValidacaoDetalhe.Add(
                    new ValidacaoDetalhe(objValid.Id,
                         string.Format("{0} - Funcionário: {1} - Subseção: {2} - Seção RH: {3} ",  s.NOME_EMPRESA , s.NOME, s.SECAO_ALOCADO, s.NOME_SECAO_RM), 
                         ""
                         ));

            }



            objValid.ListaValidacaoDetalhe = objValid.ListaValidacaoDetalhe.OrderBy(f => f.Descricao).ToList();
            return objValid;


        }

        /// <summary>
        /// Executa a validação do suposto item 4.2.2
        /// </summary>
        /// <returns></returns>
        public Validacao ConsultaInconsistenciaCadastroFuncionario()
        {

            //cria o  contexto de dados 
            Negocio.gsatOrganogramaDataContext objDs = new gsatOrganogramaDataContext();


            //monta uma lista de gestores
            var funcionarios=( from fu in objDs.ORG_VALIDACAO_04 
                              where fu.COD_EMPRESA.Contains(strEmpresa)
                                select new
                                {
                                    NOME_EMPRESA = fu.EMPRESA,
                                    NOME = fu.NOME,
                                    NOME_SECAO =fu.NOME_SECAO
                                }).Distinct();


            if (funcionarios.Count() <= 0) return null;

            //cria o objeto de saída 
            Validacao objValid = new Validacao();
            objValid.Id = 4; //validacao funcionario cadastrado errado 
            objValid.dtAtualizacao = DateTime.Now;
            //adiciona o título 
            

            objValid.Titulo = string.Format("Existem {0} problema{2} no cadastro de gestores das seções/subseções {1}", funcionarios.Count(), DateTime.Now.ToString("dd/MM/yyyy HH:mm")  , funcionarios.Count() > 1 ? "s":"" );



            //adiciona as SEÇÕES no obj saída.
            foreach (var s in funcionarios)
            {

                //Adciona os itens de detalhe 
                objValid.ListaValidacaoDetalhe.Add(
                    new ValidacaoDetalhe(objValid.Id,
                         string.Format("{0} - Subseção: {1} - Funcionário: {2}", s.NOME_EMPRESA, s.NOME_SECAO, s.NOME),
                         ""
                         ));

            }



            objValid.ListaValidacaoDetalhe = objValid.ListaValidacaoDetalhe.OrderBy(f => f.Descricao).ToList();
            return objValid;


        }


        #endregion








































































































    }//eoc
  

}//eon
