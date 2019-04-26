<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WEB._default" %>

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
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema de gestão de organogramas</title>
    <link rel="Stylesheet" type="text/css" href="gsOrganograma.css" />


     <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.4.1-vsdoc.js"></script>
    <script type="text/javascript" src="Scripts/cufon-yui.js"></script>
 
    <script type="text/javascript" src="Scripts/Myriad_Pro_400.font.js"></script>
    <script type="text/javascript" src="Scripts/Myriad_Pro_700.font.js"></script>
   <script type="text/javascript" src="Scripts/cufon-replace.js"></script>









</head>
<body class="PV">
    <form id="form1" runat="server">
    <dx:ASPxSplitter ID="sptlLayout" runat="server" FullscreenMode="True" Height="100%"
        Orientation="Vertical" Width="100%" AllowResize="False">
        <Panes>




            <dx:SplitterPane AllowResize="False" MaxSize="40px" MinSize="40px" Name="Topo" 
                ShowSeparatorImage="False" Separator-Visible="False">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                     
                     <div style="width:100%; text-align:center">
                     <span class="TextoAzulDestaque">Sistema Organograma</span>
                     </div>


                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>




            <dx:SplitterPane Name="Validações"  Separator-Visible="False" >
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True" >
                        <!-- inicio do grid -->
                        <div class="TextoCinzaMedio">Validações</div>

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
                                                            <asp:Label ID="lblDetalhe" runat="server" Text='<%# Eval("Titulo") %>'>
                                                            </asp:Label>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn VisibleIndex="0" Width="10px">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Visible="False" Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings GridLines="None" ShowColumnHeaders="False" VerticalScrollableHeight="70"
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
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>


            <dx:SplitterPane Name="MenuInferior"  MinSize="140px" MaxSize="140px" AllowResize="False">
                <Separator Visible="False">
                </Separator>
                <PaneStyle HorizontalAlign="Center" Wrap="False">
                </PaneStyle>
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <asp:Repeater ID="rptMenuInicial" runat="server">
                            <HeaderTemplate>
                                <table style="width: 100%; height: 140px; text-align: center; vertical-align: middle;">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td>
                                    <asp:LinkButton ID="lbkbtnMain" runat="server" CommandArgument='<%#Eval("cod_empresa") %>'
                                        OnClick="lbkbtnMain_click">
                                        <asp:Image runat="server" ID="imgLogo" ImageUrl='<%# Eval("cod_empresa","~/Imagens/Logos/{0}.jpg") %>'
                                            BorderStyle="None" AlternateText='<%# Eval("DESCRICAO") %>' />
                                    </asp:LinkButton>
                                </td>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tr></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>


        </Panes>
    </dx:ASPxSplitter>
    </form>
</body>
</html>
