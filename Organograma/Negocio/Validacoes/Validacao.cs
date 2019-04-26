using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio.Validacoes
{

    public class Validacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime dtAtualizacao { get; set; }
        public List<ValidacaoDetalhe> ListaValidacaoDetalhe { get; set; }

        public Validacao()
        {
            ListaValidacaoDetalhe = new List<ValidacaoDetalhe>();
        }

    }

    public class ValidacaoDetalhe
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public ValidacaoDetalhe(int id, string strTitulo, string strDescricao)
        {
            Id = id;
            Titulo = strTitulo;
            Descricao = strDescricao;
        }




    }



}
