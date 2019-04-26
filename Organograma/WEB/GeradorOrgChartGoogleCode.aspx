<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeradorOrgChartGoogleCode.aspx.cs" Inherits="WEB.GeradorOrgChartGoogleCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <style  type="text/css">
    .Funcionarios
    {
        text-align:center;
        vertical-align:top;
        font-size:11px;
        
    }
    </style>
    


    <style type="text/css">
    .google-visualization-orgchart-node {
        width: 240px;
    }
    .google-visualization-orgchart-node-medium {
        vertical-align: top;
    }
</style>



    <title></title>
    
    
   







</head>
<body style="padding:0px;margin:0px">
    <form id="form1" runat="server">
    <div style="min-height:300px; width:100%;"></div>


        <div><asp:Label ID="lblErro" runat="server" Font-Bold="True" ForeColor="#CC3300" 
                Visible="False">ERRO!</asp:Label>
           
           
            <div id='chart_div'></div>
           
           




    </div>
    
    <div style="min-height:300px; width:100%; "></div>




    </form>
</body>
</html>
