<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgMaster.Master" AutoEventWireup="true" CodeBehind="wfmSubsecoes.aspx.cs" Inherits="WEB.wfmSubsecoes" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  


  <div class="Subtitulo">
  Cadastro de Subseções
  </div>


    <table cellpadding="2" cellspacing="2" style="width:97%" >
        <tr>
            <td>
                <dx:ASPxButton ID="btnAdicionar" EnableDefaultAppearance="false"  
                    runat="server" Text="Adicionar" 
                    Image-Url="~/Imagens/AdicionarNew.png" Width="110px"  Image-Height="20" 
                     ImagePosition="Left"
                    Height="40px" Font-Bold="True" Font-Names="Calibri" ForeColor="#000048" 
                    HorizontalAlign="Left" ImageSpacing="10px" VerticalAlign="Middle" 
                    Cursor="hand" onclick="btnAdicionar_Click">
<Image Height="20px" Url="~/Imagens/AdicionarNew.png"></Image>
                    <Paddings PaddingLeft="5px" />
                    <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="dGridMain" runat="server" AutoGenerateColumns="False" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    Width="100%" KeyFieldName="COD_SECAO" >
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Subseção" FieldName="NOME" VisibleIndex="0" 
                            SortIndex="0" SortOrder="Ascending">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " FieldName="COD_SECAO_SUP" 
                            VisibleIndex="3" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Seção Superior" FieldName="NOME_SECAO_SUP" 
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Gestor" FieldName="GESTOR" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Diretoria" FieldName="DIRETORIA" 
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " FieldName="COD_SECAO" VisibleIndex="1" 
                            Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ações" VisibleIndex="6" Width="50px">
                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                                AllowSort="False" />
                            <DataItemTemplate>
                               

                               <table style="border:none;width:100%;padding:0px 0px 0px 0px;">
                               <tr>
                               
                               <td>
                               <!-- botões de ação -->
                               <dx:ASPxButton ID="btnEditar" runat="server" Image-Url=""  CommandArgument='<%# Eval("COD_SECAO") %>'
                                    EnableDefaultAppearance="false" Cursor="hand" Width="40px" 
                                       onclick="btnEditar_Click" >
                                   
                                    
                                   <Image Height="40px" Url="~/Imagens/editar.jpg">
                                   </Image>
                               </dx:ASPxButton>
</td> 
<td>

 
                                <dx:ASPxButton ID="btnExcluir" runat="server" Cursor="hand"  CommandArgument='<%# Eval("COD_SECAO") %>'
                                    EnableDefaultAppearance="False" Image-Url="" Width="40px" 
                                    onclick="btnExcluir_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer = confirm('Deseja mesmo remover esta subseção?');

}" />
                                    <Image Height="40px" Url="~/Imagens/deletar.jpg">
                                    </Image>
                                </dx:ASPxButton>
</td>
</tr>
</table>



                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="10">
                        <Summary AllPagesText="Páginas: {0} - {1} ({2} items)" 
                            Text="Página {0} of {1} ({2} itens)" />
                    </SettingsPager>
                    <Settings ShowFilterRow="True" />
                    <SettingsText EmptyDataRow="Nenhum registro encontrado" />
                    <SettingsLoadingPanel Text="Carregando&amp;hellip;" />
                    <Paddings Padding="0px" />
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
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
