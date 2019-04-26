<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroRelatoriosSubSecao.ascx.cs" Inherits="WEB.Controles.FiltroRelatoriosSubSecao" %>




<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>





<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>





<style type="text/css">
/*
Filtro Relatório
*/
.TabelaMain
{
   
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
  /*  width:4%;*/
}
.SeparadorPP
{
   /* width:2%;*/
   
 
 
 padding:0px 7px 0px 7px;
 
 
   
   
}
.ColunaConteudo
{
   
  /* width:21%;*/
  background-color:#EEEEEE;
  
  
   
}

.ColunaConteudo2
{
  /*  width:22%;*/
}


    
    </style>



<!-- Inicio teste nova config de filtro-->
<dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
    <TabPages>
        <dx:TabPage Text="Empresas">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    


                    <!-- EMPRESA -->
                    <table>

  <tr>

            <!--inicio grid empresa -->
            <td>
               
                <dx:ASPxGridView ID="dGridEmpresasDisp" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_EMPRESA" 
                    Width="250px">
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
              
                
            </td>
            <!-- fim grid empresa -->

            <!-- botoes acao empresa -->
            <td style="vertical-align:top">
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
           ToolTip="Adicionar TODOS" onclick="btnEmpAdd_Click" 
           CommandArgument="TODOS" >
           <Image Height="50px" Url="~/Imagens/adicionar_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>



   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnEmpDel" runat="server" EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover" onclick="btnEmpDel_Click" >
           <Image Height="50px" Url="~/Imagens/remover.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>

   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnEmpDelAll" runat="server" 
           EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover TODOS" onclick="btnEmpDel_Click" CommandArgument="TODOS" >
           <Image Height="50px" Url="~/Imagens/remover_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>


   </table>
           </td>
            <!-- fim botoes de acao empresa -->

            <!-- Inicio Grid Empresas Sel -->
            <td>
                <dx:ASPxGridView ID="dGridEmpresasSel" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_EMPRESA" 
                    Width="250px">
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
               
             </td>
            <!-- Fim Grid Empresas Sel -->
            </tr>

                    </table>
                    <!-- FIM EMPRESA -->










                    












                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Seções">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                <!-- Secoes -->
                <table>
                <tr>
                
                     <!-- inicio grid seções Disp -->
            <td>
                <dx:ASPxGridView ID="dGridSecoesDisp" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" 
                    Width="250px">
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
            <!-- fim grid secoes Disp -->
            
            <!-- Inicio botões de Ação -->
            <td style=" vertical-align:top" >
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

   
   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSEcoesADDAll" runat="server" 
           EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar TODOS" onclick="btnSecoesADD_Click" 
           CommandArgument="TODOS" >
           <Image Height="50px" Url="~/Imagens/adicionar_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>


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
           ToolTip="Remover TODOS" onclick="btnSecoesDel_Click" 
           CommandArgument="TODOS" >
           <Image Height="50px" Url="~/Imagens/remover_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>


   </table>
            </td>
            <!-- Fim botões de Ação -->

            <!-- inicio grd seções SEL -->
            <td>

                <dx:ASPxGridView ID="dGridSecoesSel" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" 
                    Width="250px">
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
            <!-- fim grd Seções SEL -->


</tr>
                </table>
                <!-- Fim Secoes -->

                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Subseções">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                <!-- Subsecao -->
                <table>
                <tr>
                         <!-- inicio subsecao disponivel -->
              <td>
           
            <dx:ASPxGridView ID="dGridSubsecoesDisp" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" 
                    Width="250px">
                                        <Columns>
                                            
                                            <dx:GridViewDataTextColumn Caption="Subseção" FieldName="DESCRICAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                <Settings AutoFilterCondition="Contains" />

                                               

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="260" />
                                        <SettingsText EmptyDataRow="Adicione alguma Seção" />
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
              <!-- fim subsecao disponivel -->

              <!-- botoes de acao -->
              <td style="vertical-align:top">
                
                <table style="text-align:center">
   
   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSubseccaoADD" runat="server" 
           EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar" onclick="btnSubSecoesADD_Click" >
           <Image Height="50px" Url="~/Imagens/adicionar.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>



    
   <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSubsecaoAddALL" runat="server"  CommandArgument="TODOS"
           EnableDefaultAppearance="False" Cursor="hand"
           ToolTip="Adicionar TODOS" onclick="btnSubSecoesADD_Click" >
           <Image Height="50px" Url="~/Imagens/adicionar_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>
    
      <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSubsecaoDEL" runat="server" 
           EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover" onclick="btnSubSecoesDel_Click" >
           <Image Height="50px" Url="~/Imagens/remover.jpg">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>

     <tr>
   <td style="text-align:center">
       <dx:ASPxButton ID="btnSubsecaoDELAll" runat="server" 
           EnableDefaultAppearance="False"  Cursor="hand"
           ToolTip="Remover TODOS" onclick="btnSubSecoesDel_Click"  
           CommandArgument="TODOS">
           <Image Height="50px" Url="~/Imagens/remover_todos.png">
           </Image>
       </dx:ASPxButton>
       </td>
   </tr>



   </table>
               
                </td>
              <!-- fim de botoes de acao -->

              <!-- inicio subsecao selecionada -->
              <td>
              
               
            <dx:ASPxGridView ID="dGridSubsecoesSel" runat="server" 
                                        AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                        CssPostfix="DevEx" KeyFieldName="COD_SECAO" 
                    Width="250px">
                                        <Columns>
                                            
                                            <dx:GridViewDataTextColumn Caption="Subseção" FieldName="DESCRICAO" 
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                <Settings AutoFilterCondition="Contains" />

                                               

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowFilterRow="True" 
                                            ShowVerticalScrollBar="True" VerticalScrollableHeight="260" />
                                        <SettingsText EmptyDataRow="Adicione alguma Seção" />
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
              <!-- fim subsecao selecionada -->

                </tr>
                </table>

                <!-- fim Subsecao -->



                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<!-- fim teste nova ocnfig de filtro -->
