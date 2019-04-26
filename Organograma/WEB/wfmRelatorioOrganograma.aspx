<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgRelatorios.master" AutoEventWireup="true" CodeBehind="wfmRelatorioOrganograma.aspx.cs" Inherits="WEB.wfmRelatorioOrganograma" %>

<%@ Register src="Controles/FiltroRelatoriosSecao.ascx" tagname="FiltroRelatoriosSecao" tagprefix="uc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2.Export, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpTitulo" runat="server">
    <p>
        Organograma</p>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="cpRelatorios" runat="server">



<asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">

<asp:View ID="vFiltro" runat="server" >
 <!-- Filtro -->

    <table style="width:100%; padding:0px">
    <tr>
    <td><uc1:FiltroRelatoriosSecao ID="filtroMain" runat="server" /></td>
    </tr>

    <tr>
    <td style="text-align:right">
   
    <table style="width:100%; padding:0px;">
    <tr>

    
    <td style="width:150px;">  
    
   <dx:ASPxRadioButton ID="rdSimples" runat="server"   GroupName="TIPO" Text="Organograma Simples">
   </dx:ASPxRadioButton>
    

     </td>

     <td style="width:160px">
     <dx:ASPxRadioButton ID="rdCompleto" runat="server" GroupName="TIPO" Text="Organograma Completo">
     </dx:ASPxRadioButton>

     </td>

     <td></td>

    <td style="width:100px;"><dx:ASPxButton Width="100px" ID="btnLimpar" 
            runat="server"   Text="Limpar Filtros" onclick="btnLimpar_Click"/></td>
    <td  style="width:5px">&nbsp;</td>
    <td style="width:60px;"><dx:ASPxButton  Width="60px" ID="btnGerar" runat="server"   
            Text="Gerar" onclick="btnGerar_Click" style="height: 25px" /></td>
  
   
  
    </tr>

    </table>
        
         
        
      
    
    </td>
    </tr>
    </table>
    




    <!-- fim filtro -->

</asp:View>




<asp:View ID="vResultado" runat="server"  >



<table>
<tr>
<td>



   <div style="float:left;width:32px;"><dx:ASPxButton ID="btnPDF" runat="server" 
                                EnableDefaultAppearance="false" Width="32px" Height="32px" 
                                Image-Url="~/Imagens/pdf.png" Cursor="hand"  ToolTip="Exportar para Adobe PDF" 
                                CommandArgument="PDF" 
                                OnClick="btnExportacao_Click">
<Image Url="~/Imagens/pdf.png"></Image>
                            </dx:ASPxButton>
                           </div>
                       
                        <div style="float:left;width:32px; "><dx:ASPxButton ID="btnIMG" runat="server" 
                                EnableDefaultAppearance="false" Width="32px" Height="32px" 
                                Image-Url="~/Imagens/img.png" Cursor="hand" 
                                ToolTip="Exportar para Imagem no formato PNG" CommandArgument="IMG" 
                                OnClick="btnExportacao_Click" >
<Image Url="~/Imagens/img.png"></Image>
                            </dx:ASPxButton>
                           </div>

                         
                            <div style="float:left;width:32px;"><dx:ASPxButton ID="btnVoltar" runat="server" 
                                EnableDefaultAppearance="false" Width="32px" Height="32px" 
                                Image-Url="~/Imagens/voltar.png" Cursor="hand" 
                                ToolTip="Voltar" CommandArgument="XLS" 
                                OnClick="btnVoltar_Click" >
<Image Url="~/Imagens/voltar.png"></Image>
                            </dx:ASPxButton>
                           </div>

                           
                             





</td>
</tr>

<tr>
<td>


<dx:ASPxBinaryImage ID="imgOrgChart" runat="server">
</dx:ASPxBinaryImage>


    
</td>
</tr>
</table>



















</asp:View>


</asp:MultiView>

   


</asp:Content>
