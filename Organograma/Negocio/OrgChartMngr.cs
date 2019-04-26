using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
    public static class OrgChartMngr
    {


    }


    /// <summary>
    /// Classe de dados modelo do OrgChart
    /// </summary>
    [Serializable]
    public class OrgChartDataSource
    {

            public Int64 id {get;set;}
            public Int64 idPai { get; set; }
            public string Secao { get; set; }
            public string Gestor { get; set; }
            public string Funcionarios { get; set; }
            public string Cod_secao { get; set; }
            public string Cod_secao_sup { get; set; }
            public bool Processado { get; set; }

            public OrgChartDataSource()
            { }

            public OrgChartDataSource(Int64 _id, Int64 _idPai, string _secao, string _gestor, string _Funcionarios)
            {
                id = _id;
                idPai = _idPai;
                Secao = _secao;
                Gestor = _gestor;
                Funcionarios = _Funcionarios;


            }



    }






}
