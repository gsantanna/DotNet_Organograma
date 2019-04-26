

<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgMaster.Master" AutoEventWireup="true" CodeBehind="wfmValidacoes.aspx.cs" Inherits="WEB.wfmValidacoes" %>


<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSplitter" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.2.Export, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.2.Export, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


 <dx:ASPxSplitter ID="sptlLayout" runat="server" FullscreenMode="True" Height="100%"
        Orientation="Vertical" Width="100%" AllowResize="False">
        <Panes>

        

            <dx:SplitterPane AllowResize="False" Name="Botões" ShowSeparatorImage="False" 
                Size="48px" MinSize="48px">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                       


                       

<div class="Subtitulo" style="padding-top:0px !important; margin-top:0px !important; width:98%!important;margin-right:0px!important;padding-right:0px !important;  vertical-align:top !important;">

<div style="float:left">
Validações
</div>

   <div style="float:right; width:32px;min-height:40px; vertical-align:top!important;"><dx:ASPxButton ID="btnPDF" runat="server" 
                                EnableDefaultAppearance="false" Width="32px" Height="32px" 
                                Image-Url="~/Imagens/pdf.png" Cursor="hand"  ToolTip="Exportar para Adobe PDF" 
                                CommandArgument="PDF" 
                                OnClick="btnExportacao_Click">
<Image Url="~/Imagens/pdf.png"></Image>  
                            </dx:ASPxButton>
                           </div>
                        <div style="float:right;width:32px;min-height:40px; vertical-align:top;"><dx:ASPxButton ID="btnXLS" runat="server" 
                                EnableDefaultAppearance="false" Width="32px" Height="32px" 
                                Image-Url="~/Imagens/xls.png" Cursor="hand" 
                                ToolTip="Exportar para MS. Excel (XLS)" CommandArgument="XLS" 
                                OnClick="btnExportacao_Click" >
<Image Url="~/Imagens/xls.png"></Image>
                            </dx:ASPxButton>
                           </div>

  <!-- Menu de Exportação-->

                       

</div><!-- fim do subtitulo -->






                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>

        

            <dx:SplitterPane Name="Validações"  Separator-Visible="False">
<Separator Visible="False"></Separator>
                <ContentCollection>
                    <dx:splittercontentcontrol runat="server" 
        SupportsDisabledAttribute="True">
                        <!-- inicio do grid -->
                        <dx:ASPxGridView ID="dGridValidacao1" runat="server" KeyFieldName="Id" AutoGenerateColumns="False"
                            Width="100%">
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="0">
                                    <DataItemTemplate>
                                        <div style="border: 1px solid black; width: 100%; padding-top: 3px; padding-left:0px;">
                                            <span style=" font-weight:bold; margin-left:10px;">
                                            <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval("Titulo") %>'></asp:Label>
                                            </span>
                                            <dx:ASPxGridView ID="dGridValidacao1Detalhe" runat="server" OnBeforePerformDataSelect="dGridValidacao1Detalhe_BeforePerformDataSelect"
                                                AutoGenerateColumns="False" Width="99%">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn VisibleIndex="1" Width="100%">
                                                      
                                                        <DataItemTemplate>
                                                       <span   class="lnkDetalhe">
                                                      <asp:HyperLink ID="lbkDetalhe" runat="server" NavigateUrl='<%# string.Format ("wfmValidacao.aspx?ID={0}", Eval("ID")) %>'  >
                                                            <asp:Label ID="lblDetalhe" runat="server" Text='<%# Eval("Titulo") %>' >
                                                            </asp:Label>
                                                    </asp:HyperLink></span>

                                                        </DataItemTemplate>


                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn VisibleIndex="0" Width="10px">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Visible="False" Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings GridLines="None" ShowColumnHeaders="False" VerticalScrollableHeight="100"
                                                    ShowVerticalScrollBar="True" />
                                                <Paddings Padding="0px" />
                                                <Border BorderStyle="None" />
                                            </dx:ASPxGridView>
                                        </div>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings GridLines="None" ShowColumnHeaders="False" />
                            <Paddings Padding="0px" />
                            <Border BorderStyle="None" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="exprtMain" runat="server" GridViewID="dGridExportacao"
                            Landscape="True" MaxColumnWidth="9999">
                            <Styles>
                                <Cell BorderSides="None" Wrap="True">
                                </Cell>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridView runat="server" ID="dGridExportacao" AutoGenerateColumns="False"
                            Width="100%" Visible="False">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="ORDEM" Visible="False" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CONTEUDO" VisibleIndex="1" Width="100%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <Settings GridLines="None" ShowColumnHeaders="False" />
                            <Border BorderStyle="None" />
                        </dx:ASPxGridView>
                        <!-- fim do grid -->
                    </dx:splittercontentcontrol>
                </ContentCollection>
            </dx:SplitterPane>



</Panes>
</dx:ASPxSplitter>








</asp:Content>




