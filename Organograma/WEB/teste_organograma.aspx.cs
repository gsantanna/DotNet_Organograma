using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data; 
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class teste_organograma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Data.DataTable dtAplication = new System.Data.DataTable();
            dtAplication.Columns.AddRange(new DataColumn[] { new DataColumn("ID"), new DataColumn ("NOME") });



            for (int i = 0; i < Application.Count; i++)
            {
                dtAplication.Rows.Add(Application.Keys[i], Application[i].ToString());
            }

            this.GridView1.DataSource = dtAplication;
            this.GridView1.DataBind();





        }














    }
}