﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class gsOrgMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["EMPRESA"] != null)
            {
                imgEmpresa.ImageUrl = string.Format("~/Imagens/Logos_sup/{0}.jpg", Session["EMPRESA"]);
                imgEmpresa.Visible = true;

            }
        }







    }
}