using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Negocio.Validacoes;
using System.Data;

namespace WEB
{
    public partial class teste : System.Web.UI.Page
    {





        protected void Page_Load(object sender, EventArgs e)
        {


            List<ItemOrganograma> siteData = new List<ItemOrganograma>();

            siteData.Add(new ItemOrganograma(1, 0, "Diretor XPTO"));
            siteData.Add(new ItemOrganograma(2, 1, "Gerente TEste"));
            siteData.Add(new ItemOrganograma(3, 1, "Gerente teste2"));
            siteData.Add(new ItemOrganograma(4, 2, "Coordenador XPT"));
            siteData.Add(new ItemOrganograma(5, 2, "Outro coordenador XPT"));
            siteData.Add(new ItemOrganograma(6, 2, "Outro coordenador XPT"));
            siteData.Add(new ItemOrganograma(7, 2, "Outro coordenador XPT"));
            siteData.Add(new ItemOrganograma(8, 7, "Analista"));
            siteData.Add(new ItemOrganograma(9, 7, "Outro analista"));
            siteData.Add(new ItemOrganograma(99999999999, 7, "analista Int64"));


            rocMain.DataTextField = "Text";
            rocMain.DataFieldID = "ID";
            rocMain.DataFieldParentID = "ParentID";
            rocMain.DataSource = siteData;
            rocMain.DataBind();









        }





        internal class ItemOrganograma
        {
            private string _text;
            private Int64 _id;
            private Int64 _parentId;
            
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }


            public Int64 ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public Int64 ParentID
            {
                get { return _parentId; }
                set { _parentId = value; }
            }

            public ItemOrganograma(Int64 id, Int64 parentId, string text)
            {
                _id = id;
                _parentId = parentId;
                _text = text;
            }
        }
















    }


  


}