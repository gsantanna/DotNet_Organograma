<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgMaster.Master" AutoEventWireup="true" CodeBehind="wfmSecoes.aspx.cs" Inherits="WEB.wfmSecoes" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="Subtitulo" style="margin-bottom:3px !important;">Cadastro de Seções</div>


                <dx:ASPxGridView ID="dGridMain" runat="server" AutoGenerateColumns="False" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    Width="100%" KeyFieldName="COD_SECAO">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Seção" FieldName="NOME_RM" 
                            VisibleIndex="0">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="Mod. Nome" FieldName="NOME_MOD" 
                            ReadOnly="True" VisibleIndex="5">
                            <PropertiesCheckEdit DisplayTextChecked="Alterado" 
                                DisplayTextUnchecked="Não Alterado" ValueChecked="1" ValueType="System.String" 
                                ValueUnchecked="0">
                            </PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="Nome Modificado" VisibleIndex="6" 
                            Width="50px" FieldName="NOME_MODIFICADO">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>



                          <dx:GridViewDataCheckColumn Caption="Mod. Gestor" FieldName="GESTOR_MOD" 
                            ReadOnly="True" VisibleIndex="7">
                            <PropertiesCheckEdit DisplayTextChecked="Alterado" 
                                DisplayTextUnchecked="Não Alterado" ValueChecked="1" ValueType="System.String" 
                                ValueUnchecked="0">
                            </PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>



                        <dx:GridViewDataTextColumn Caption="Gestor Modificado" 
                            FieldName="NOME_GESTOR_MOD" VisibleIndex="8">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>

                




                        <dx:GridViewDataCheckColumn Caption="Não possui SUP." 
                            FieldName="POSSUI_SUPERIOR" VisibleIndex="11" Width="40px">
                            <PropertiesCheckEdit AllowGrayed="false" ValueChecked="1" ValueUnchecked="0" ValueType="System.String"
                             DisplayTextChecked="Possui" DisplayTextUnchecked="Não possui" ></PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="Gestor RM" FieldName="NOME_GESTOR_RM" 
                            VisibleIndex="2">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="Visível Todos" FieldName="PUBLICO" 
                            VisibleIndex="10">
                            <PropertiesCheckEdit  ValueType="System.String" ValueChecked="1" ValueUnchecked="0" ValueGrayed="K" DisplayTextChecked="Visível"  DisplayTextUnchecked="Não visível"   ></PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="Diretoria" FieldName="DIRETORIA" 
                            VisibleIndex="9">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Editar"  Name="Editar"
                            VisibleIndex="12" Width="30px">
                            <PropertiesTextEdit>
                                <Style HorizontalAlign="Center">
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                                AllowSort="False" />
                            <DataItemTemplate>
                                <table style="border:none;width:100%;padding:0px 0px 0px 0px; text-align:center">
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width:25px; text-align:center">
                                            <!-- botões de ação -->
                                            <dx:ASPxButton ID="btnEditar" runat="server"  
                                                CommandArgument='<%# Eval("COD_SECAO") %>' Cursor="hand" 
                                                EnableDefaultAppearance="false" Image-Url="" Width="25px" 
                                                onclick="btnEditar_Click">
                                                <Image Height="25px" Url="~/Imagens/editar.jpg">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " FieldName="COD_SECAO_SUP" 
                            VisibleIndex="4" Width="10px">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Seção Superior" FieldName="NOME_SECAO_SUP" 
                            VisibleIndex="3">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " FieldName="COD_SECAO" VisibleIndex="1" 
                            Width="80px">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="10">
                        <Summary AllPagesText="Páginas: {0} - {1} ({2} items)" 
                            Text="Página {0} of {1} ({2} itens)" />
                    </SettingsPager>
                    <Settings ShowFilterRow="True" GridLines="Horizontal" />
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
                        <Cell>
                            <Paddings Padding="0px" />
                        </Cell>
                        <LoadingPanel ImageSpacing="5px">
                        </LoadingPanel>
                    </Styles>
                    <StylesEditors ButtonEditCellSpacing="0">
                        <ProgressBar Height="21px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
