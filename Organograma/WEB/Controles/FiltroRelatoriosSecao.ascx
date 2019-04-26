<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroRelatoriosSecao.ascx.cs" Inherits="WEB.Controles.FiltroRelatoriosSecao" %>




<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>





<style type="text/css">
/*
Filtro Relatório
*/
.TabelaMain
{
    width:100%;
    padding:2px 2px 2px 2px;
    text-align:center;
    vertical-align:middle;
    
}

.TabelaMain td
{
    vertical-align:top;
}

.SeparadorP
{
    width:4%;
}
.SeparadorPP
{
    width:2%;
}
.ColunaConteudo
{
    width:21%;
}

.ColunaConteudo2
{
    width:22%;
}


    
</style>



    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</p>



    <table class="TabelaMain">
        <tr>
            <td  class="ColunaConteudo">
               <span class="TextoCinzaMedio">Empresas</span></td>
            <td class="SeparadorP">
                &nbsp;</td>
            <td class="ColunaConteudo">
                &nbsp;</td>
            <td class="SeparadorPP">
                </td>
            <td class="ColunaConteudo2">
                 <span class="TextoCinzaMedio">Seções</span></td>
            <td class="SeparadorP">
                &nbsp;</td>
            <td class="ColunaConteudo2">
                </td>
        </tr>


        <tr>
            <td>
                
                
                <!--inicio grid empresa -->
                <dx:ASPxGridView ID="dGridEmpresasDisp" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_EMPRESA" 
                    Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="CÓD" FieldName="COD_EMPRESA" 
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="130px" Visible="false">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Empresa" FieldName="DESCRICAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="260" />
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
                <!-- fim grid empresa -->

                
                
                
                
                
                
                </td>
            <td>
               
               <table style="text-align:center">
   
   <tr>
   <td style="text-align:center; vertical-align:middle!important;">
       <dx:ASPxButton ID="btnEmpAdd" runat="server" EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar" onclick="btnEmpAdd_Click" >
           <Image Height="50px" Url="~/Imagens/adicionar.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>



   <tr>
   <td style="text-align:center; vertical-align:middle!important;">
       <dx:ASPxButton ID="btnEmpAddAll" runat="server" EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar TODOS" onclick="btnEmpAdd_Click" CommandArgument="TODOS" >
           <Image Height="50px" Url="~/Imagens/adicionar_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>





    <tr>
   <td><br /></td></tr>



      <tr>

   <td style="text-align:center">
       <dx:ASPxButton ID="btnEmpDel" runat="server" EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover" onclick="btnEmpDel_Click" >
           <Image Height="50px" Url="~/Imagens/remover.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>


   
   <td style="text-align:center">
       <dx:ASPxButton ID="btnEmpDelAll" runat="server" EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover TODOS" onclick="btnEmpDel_Click"  CommandArgument="TODOS">
           <Image Height="50px" Url="~/Imagens/remover_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>





   </table>



                </td>

            <td>
                
               

                    
                <!--inicio grid empresa -->
                <dx:ASPxGridView ID="dGridEmpresasSel" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_EMPRESA" 
                    Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="CÓD" FieldName="COD_EMPRESA" 
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="130px" Visible="false">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Empresa" FieldName="DESCRICAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="260" />
                                        <SettingsText EmptyDataRow="Adicione alguma empresa" />
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
                <!-- fim grid empresa -->

                



                </td>
            <td>
                <br />
                </td>
            <td>

            <!-- Grid de seções disponíveis  -->

                <dx:ASPxGridView ID="dGridSecoesDisp" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" 
                    Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Empresa" FieldName="EMPRESA" 
                                                ShowInCustomizationForm="True" VisibleIndex="1"  >
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Seção" FieldName="NOME_SECAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                <Settings AutoFilterCondition="Contains" />

                                               

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="260" />
                                        <SettingsText EmptyDataRow="Adicione alguma empresa" />
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
             
             
              <!-- fim de grid de seções disponíveis -->

              
              
                </td>
            <td>
                
                
                <!-- botoes de acao -->
                <table style="text-align:center">
   
   <tr>



   <td style="text-align:center">
       <dx:ASPxButton ID="btnSecoesADD" runat="server" EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar" onclick="btnSecoesADD_Click" >
           <Image Height="50px" Url="~/Imagens/adicionar.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>

   
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSecoesADDAll" runat="server" 
           EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar TODOS" onclick="btnSecoesADD_Click" 
           CommandArgument="TODOS" >
           <Image Height="50px" Url="~/Imagens/adicionar_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>


   <tr>
   <td><br /></td></tr>



   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSecoesDel" runat="server" 
           EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover" onclick="btnSecoesDel_Click" >
           <Image Height="50px" Url="~/Imagens/remover.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>

   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSecoesDelAll" runat="server" 
           EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover TODOS" onclick="btnSecoesDel_Click"  CommandArgument="TODOS">
           <Image Height="50px" Url="~/Imagens/remover_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>




   </table>

                <!-- fim de botoes de acao -->


                
                
                </td>
            <td>

                <dx:ASPxGridView ID="dGridSecoesSel" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" 
                    Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Empresa" FieldName="EMPRESA" 
                                                ShowInCustomizationForm="True" VisibleIndex="1"  >
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Seção" FieldName="NOME_SECAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                <Settings AutoFilterCondition="Contains" />

                                               

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="260" />
                                        <SettingsText EmptyDataRow="Adicione alguma empresa" />
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
             
             
              </td>
        </tr>
    </table>

