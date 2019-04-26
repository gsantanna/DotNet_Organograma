
<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgMaster.Master" AutoEventWireup="true" CodeBehind="wfmDiretoria.aspx.cs" Inherits="WEB.wfmDiretoria" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="Subtitulo">Definição de Diretoria</div>

   


    <table  style="width:100%">
        <tr>
            <td>
                <dx:ASPxSplitter ID="spliterMain" runat="server" Height="510px" Width="100%" 
                    AllowResize="False">
                    <panes>
                        <dx:SplitterPane>
                            <ContentCollection>
<dx:SplitterContentControl ID="SplitterContentControl1"  runat="server" SupportsDisabledAttribute="True">
   

  
  <div class="Colunastop">
    SEÇÕES
</div>


  

    <dx:ASPxGridView ID="dGridDisp" runat="server" AutoGenerateColumns="False" 
        KeyFieldName="COD_SECAO" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
        CssPostfix="DevEx" Width="97%">
        <Settings GridLines="Horizontal" ShowFilterRow="True" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Código" FieldName="COD_SECAO" 
                ShowInCustomizationForm="True" VisibleIndex="2" Width="130px">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Seção" FieldName="NOME_SECAO" 
                ShowInCustomizationForm="True" VisibleIndex="1">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
        </Columns>

         <SettingsBehavior AllowSelectByRowClick="true"   />


        <SettingsPager Mode="ShowAllRecords" Visible="False">
        </SettingsPager>
           <Settings VerticalScrollableHeight="410" ShowVerticalScrollBar="true" />


        <SettingsText EmptyDataRow="Nenhuma informação encontrada" />
        <SettingsLoadingPanel Text="Carregando&amp;hellip;" />
        <Images SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
            <LoadingPanelOnStatusBar Url="~/App_Themes/DevEx/GridView/StatusBarLoading.gif">
            </LoadingPanelOnStatusBar>
            <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
            </LoadingPanel>
        </Images>
        <ImagesFilterControl>
            <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
            </LoadingPanel>
        </ImagesFilterControl>
        <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
            <LoadingPanel ImageSpacing="5px">
            </LoadingPanel>
        </Styles>
        <StylesEditors ButtonEditCellSpacing="0">
            <ProgressBar Height="21px">
            </ProgressBar>
        </StylesEditors>


    </dx:ASPxGridView>
  
  


                </dx:SplitterContentControl>
</ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane Name="Separador" Size="80px">
                            <PaneStyle HorizontalAlign="Center">
                                <Paddings Padding="0px" />
                            </PaneStyle>
                            <ContentCollection>
<dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True"><br />
   

   <table style="text-align:center">
   
   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnAdicionar" runat="server" EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar" OnClick="btnAdicionar_Click">
           <Image Height="50px" Url="~/Imagens/adicionar.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>

      <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnRemover" runat="server" EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover" OnClick="btnRemover_Click">
           <Image Height="50px" Url="~/Imagens/remover.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>

   </table>
   

   <!-- botoes de acao -->

  
                                </dx:SplitterContentControl>
</ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane>
                            <contentcollection>
                                <dx:SplitterContentControl ID="SplitterContentControl3" runat="server" SupportsDisabledAttribute="True">
                                    
                                    <div class="Colunastop">
                                    DIRETORIAS
                                    </div>
                                    


                                    <dx:ASPxGridView ID="dGridDiretorias" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" Width="97%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Código" FieldName="COD_SECAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="130px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Diretoria" FieldName="NOME_SECAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="410" />
                                        <SettingsText EmptyDataRow="Nenhuma informação encontrada" />
                                        <SettingsLoadingPanel Text="Carregando&amp;hellip;" />
                                        <Images SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                                            <LoadingPanelOnStatusBar Url="~/App_Themes/DevEx/GridView/StatusBarLoading.gif">
                                            </LoadingPanelOnStatusBar>
                                            <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </Images>
                                        <ImagesFilterControl>
                                            <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </ImagesFilterControl>
                                        <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx">
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                            <LoadingPanel ImageSpacing="5px">
                                            </LoadingPanel>
                                        </Styles>
                                        <StylesEditors ButtonEditCellSpacing="0">
                                            <ProgressBar Height="21px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </dx:SplitterContentControl>
                            </contentcollection>
                        </dx:SplitterPane>
                    </panes>
                </dx:ASPxSplitter>
            </td>
            <td style="width:10px">
                
            </td>
        </tr>
        <tr>
            <td>
                
                <table style="padding:10px 10px 10px 10px;"> 
                <tr>
                <td>
                    <dx:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" Width="100px" 
                        onclick="btnSalvar_Click">
                    </dx:ASPxButton>
                    </td>
                    <td class="s10"></td>
                    <td>
                        <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100px" 
                            onclick="btnCancelar_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
                 </table>
                
                </td>
            <td style="width:10px">
                &nbsp;</td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
</asp:Content>

