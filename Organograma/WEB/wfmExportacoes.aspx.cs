using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
	public partial class wfmExportacoes : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        #region GESTAO_DESEMPENHO
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {

            dGridGestaoDesempenho.AddNewRow();
        }

        #endregion 

        protected void btnAdicionarTr_Click(object sender, EventArgs e)
        {
            dGridTreinamento.AddNewRow();


        }

       
    }
}