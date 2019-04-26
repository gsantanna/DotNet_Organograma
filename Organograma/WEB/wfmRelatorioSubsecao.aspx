<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgRelatorios.master" AutoEventWireup="true" CodeBehind="wfmRelatorioSubsecao.aspx.cs" Inherits="WEB.wfmRelatoriosSubsecao" %>
<%@ Register src="Controles/FiltroRelatoriosSubSecao.ascx" tagname="FiltroRelatoriosSubSecao" tagprefix="uc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2.Export, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpTitulo" runat="server">
    <p>
        Relatório de Subseções</p>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="cpRelatorios" runat="server">



<asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">

<asp:View ID="vFiltro" runat="server" >
 <!-- Filtro -->

    <table style="width:100%; padding:0px">
    <tr>
    <td><uc1:FiltroRelatoriosSubSecao ID="filtroMain" runat="server" /></td>
    </tr>

    <tr>
    <td style="text-align:right">
   
    <table style="width:100%; padding:0px;">
    <tr>
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
                       
                        <div style="float:left;width:32px; "><dx:ASPxButton ID="btnXLS" runat="server" 
                                EnableDefaultAppearance="false" Width="32px" Height="32px" 
                                Image-Url="~/Imagens/xls.png" Cursor="hand" 
                                ToolTip="Exportar para MS. Excel (XLS)" CommandArgument="XLS" 
                                OnClick="btnExportacao_Click" >
<Image Url="~/Imagens/xls.png"></Image>
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
<dx:ASPxGridView ID="dGridResposta" runat="server" AutoGenerateColumns="False" 
        Width="100%">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Empresa" FieldName="Empresa" 
            VisibleIndex="0">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Subseção Código" FieldName="Nome" 
            VisibleIndex="1">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Seção Superior Código" VisibleIndex="2"  FieldName="Nome_Superior" >
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Gestor" VisibleIndex="3" FieldName="Gestor">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Diretoria" VisibleIndex="4" FieldName="Diretoria">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Funcionários Lotados" VisibleIndex="5" 
            FieldName="Funcionarios_Linha">
            <PropertiesTextEdit EncodeHtml="False">
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager>
        <Summary AllPagesText="Páginas: {0} - {1} ({2} itens)" 
            Text="Página {0} de {1} ({2} itens)" />
    </SettingsPager>
    <Settings ShowFilterRow="True" />
    <SettingsLoadingPanel Text="Carregando&amp;hellip;" />
    </dx:ASPxGridView>
    <br />
    <dx:ASPxGridViewExporter ID="dGridExptrMain" runat="server" 
        GridViewID="dGridResposta" Landscape="True" MaxColumnWidth="140" 
        onrenderbrick="dGridExptrMain_RenderBrick" PaperKind="A4">
    </dx:ASPxGridViewExporter>
</td>
</tr>
</table>



















</asp:View>


</asp:MultiView>

   


</asp:Content>
