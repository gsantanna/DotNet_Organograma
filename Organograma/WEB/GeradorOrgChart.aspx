<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeradorOrgChart.aspx.cs" Inherits="WEB.GeradorOrgChart" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="padding:0px;margin:0px">
    <form id="form1" runat="server">
    <div style="min-height:300px; width:100%; background-color:Red"></div>
        <div><asp:Label ID="lblErro" runat="server" Font-Bold="True" ForeColor="#CC3300" 
                Visible="False">ERRO!</asp:Label>
            <telerik:RadOrgChart ID="RadOrgChart1" runat="server"    
                AllowGroupItemDragging="True" DisableDefaultImage="False" LoadOnDemand="None">
           
           
           <ItemTemplate>

           <div style="width:100%;text-align:center; font-size:11px; font-weight:bold; ">
           <asp:Label ID="lblF" runat="server" Text='<%# Eval("Secao") %>'/>
           </div>

             <div style="width:100%;text-align:center; font-size:10px; font-weight:bold; ">
           <asp:Label ID="Label1" runat="server" Text='<%# Eval("Gestor") %>'/>
           </div>
         
         <br />


         <div style="width:100%; border-top:1px solid #EEEEEE; padding:3px 0px 0px 0px;">
       

           <div style="width:100%;text-align:center; font-size:10px; font-weight:normal; ">
           RODRIGO RAMOS GUIMARAES <br />
            RODRIGO RAMOS GUIMARAES <br />
             RODRIGO RAMOS GUIMARAES <br />
              RODRIGO RAMOS GUIMARAES <br />
           <asp:Label ID="Label2" runat="server" Text='<%# Eval("Funcionarios") %>'/>
           </div>
         


  </div>

           

           </ItemTemplate>
           
           
           
            </telerik:RadOrgChart>




            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" 
                        Name="Telerik.Web.UI.Common.Core.js">
                    </asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" 
                        Name="Telerik.Web.UI.Common.jQuery.js">
                    </asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" 
                        Name="Telerik.Web.UI.Common.jQueryInclude.js">
                    </asp:ScriptReference>
                </Scripts>
            </telerik:RadScriptManager>
    </div>
    
    <div style="min-height:300px; width:100%; background-color:Red"></div>




    </form>
</body>
</html>
