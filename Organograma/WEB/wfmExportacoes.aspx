<%@ Page Title="" Language="C#" MasterPageFile="~/gsGeracaoArquivos.master" AutoEventWireup="true" CodeBehind="wfmExportacoes.aspx.cs" Inherits="WEB.wfmExportacoes" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpGerador" runat="server">
    <div  class="Colunastop">Cadastrar linhas avulsas.</div>
      <br />
   
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
            LoadingPanelText="Carregando&amp;hellip;" Width="98%">
            <TabPages>
                <dx:TabPage Text="Gestão de Desempenho">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                           
                        

                         <div style="margin-left:5px">

                            <dx:ASPxButton ID="btnAdicionar" ToolTip="Adicionar" runat="server" Text="Adicionar"  Cursor="hand"
                                OnClick="btnAdicionar_Click" EnableDefaultAppearance="False" Height="30px" 
                                HorizontalAlign="Left" ImageSpacing="10px" VerticalAlign="Middle" Width="100px" 
                            
                            >
                                <Image Height="20px" Url="~/Imagens/AdicionarNew.png">
                                </Image>
                                <Paddings PaddingLeft="10px" />
                                <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </dx:ASPxButton></div>


                     

                            <br />
                            <dx:ASPxGridView ID="dGridGestaoDesempenho" runat="server" 
                                AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                CssPostfix="DevEx" DataSourceID="dsGridGestaoDesemp" 
                                KeyFieldName="ID_LINHA" Width="97%">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Ações" ShowInCustomizationForm="True" 
                                        VisibleIndex="0">
                                        <EditButton Visible="True">
                                        </EditButton>
                                        <DeleteButton Visible="True">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_FUNCIONARIO" 
                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                       
                                        
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit><PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NUM_MATRICULA" 
                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                       
                                       
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_EMPRESA" 
                                        ShowInCustomizationForm="True" VisibleIndex="4">
                                   
                                      
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_ORGAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="5">
                                       
                                     

                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_CARGO" 
                                        ShowInCustomizationForm="True" VisibleIndex="6">
                                      
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_SUPERVISOR" 
                                        ShowInCustomizationForm="True" VisibleIndex="7">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DSC_DOMINIO" 
                                        ShowInCustomizationForm="True" VisibleIndex="8">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DSC_LOGON" 
                                        ShowInCustomizationForm="True" VisibleIndex="9">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DSC_SENHA" 
                                        ShowInCustomizationForm="True" VisibleIndex="10">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOM_FUNCIONARIO" 
                                        ShowInCustomizationForm="True" VisibleIndex="11">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SEXO" 
                                        ShowInCustomizationForm="True" VisibleIndex="12">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DAT_ADMISSAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="13">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DSC_EMAIL" ShowInCustomizationForm="True" 
                                        VisibleIndex="14">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_TIPO_FUNC" 
                                        ShowInCustomizationForm="True" VisibleIndex="15">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_TIPO_USUARIO" 
                                        ShowInCustomizationForm="True" VisibleIndex="16">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DAT_NASCIMENTO" 
                                        ShowInCustomizationForm="True" VisibleIndex="17">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_FILIAL" 
                                        ShowInCustomizationForm="True" VisibleIndex="18">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_CPF" ShowInCustomizationForm="True" 
                                        VisibleIndex="19">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DSC_APELIDO" 
                                        ShowInCustomizationForm="True" VisibleIndex="20">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DAT_INICIO_FERIAS" 
                                        ShowInCustomizationForm="True" VisibleIndex="21">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DAT_FIM_FERIAS" 
                                        ShowInCustomizationForm="True" VisibleIndex="22">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_SUPERVISOR_FUNCIONAL" 
                                        ShowInCustomizationForm="True" VisibleIndex="23">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DSC_GRADE_SALARIAL" 
                                        ShowInCustomizationForm="True" VisibleIndex="24">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DAT_INICIO_CARGO" 
                                        ShowInCustomizationForm="True" VisibleIndex="25">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_IDIOMA_PREF" 
                                        ShowInCustomizationForm="True" VisibleIndex="26">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_PAIS" ShowInCustomizationForm="True" 
                                        VisibleIndex="27">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_UF" ShowInCustomizationForm="True" 
                                        VisibleIndex="28">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_SECAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="29">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_SECAO_SUP" 
                                        ShowInCustomizationForm="True" VisibleIndex="30">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_SECAO_SUP" 
                                        ShowInCustomizationForm="True" VisibleIndex="31">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_DIRETORIA" 
                                        ShowInCustomizationForm="True" VisibleIndex="32">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_DIRETORIA" 
                                        ShowInCustomizationForm="True" VisibleIndex="33">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                     <dx:GridViewDataTextColumn FieldName="CARGO_COD_CARGO" 
                                        ShowInCustomizationForm="True" VisibleIndex="34">
                                           <PropertiesTextEdit MaxLength="10"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn FieldName="CARGO_DSC_CARGO" 
                                        ShowInCustomizationForm="True" VisibleIndex="35">
                                           <PropertiesTextEdit MaxLength="100"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>


                                      <dx:GridViewDataTextColumn FieldName="EMPRESA_COD_EMPRESA" 
                                        ShowInCustomizationForm="True" VisibleIndex="36">
                                           <PropertiesTextEdit MaxLength="5"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                    
                                      <dx:GridViewDataTextColumn FieldName="EMPRESA_NOM_EMPRESA" 
                                        ShowInCustomizationForm="True" VisibleIndex="37">
                                        <PropertiesTextEdit MaxLength="100"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                    
                                      <dx:GridViewDataTextColumn FieldName="LOC_TRAB_COD_LOCAL"  Caption="LOC_TRAB_COD_LOCAL_TRABALHO"
                                        ShowInCustomizationForm="True" VisibleIndex="38">
                                        <PropertiesTextEdit MaxLength="10"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                    
                                      <dx:GridViewDataTextColumn FieldName="LOC_TRAB_NOM_LOCAL_TRABALHO" 
                                        ShowInCustomizationForm="True" VisibleIndex="39">
                                        <PropertiesTextEdit MaxLength="100"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>


                                    
                                      <dx:GridViewDataTextColumn FieldName="TIPO_FUNC_COD_TIPO_FUNCIONARIO" 
                                        ShowInCustomizationForm="True" VisibleIndex="40">
                                        <PropertiesTextEdit MaxLength="10"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>


                                      <dx:GridViewDataTextColumn FieldName="TIPO_FUNC_NOM_TIPO_FUNCIONARIO" 
                                        ShowInCustomizationForm="True" VisibleIndex="41">
                                        <PropertiesTextEdit MaxLength="100"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>




                                      <dx:GridViewDataTextColumn FieldName="ORGAOS_COD_ORGAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="42">
                                        <PropertiesTextEdit MaxLength="30"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                    
                                      <dx:GridViewDataTextColumn FieldName="ORGAOS_COD_ORGAO_SUPERIOR" 
                                        ShowInCustomizationForm="True" VisibleIndex="43">
                                        <PropertiesTextEdit MaxLength="30"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    
                                      <dx:GridViewDataTextColumn FieldName="ORGAOS_COD_DIRETORIA" 
                                        ShowInCustomizationForm="True" VisibleIndex="44">
                                        <PropertiesTextEdit MaxLength="30"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    
                                      <dx:GridViewDataTextColumn FieldName="ORGAOS_NOM_ORGAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="45">
                                        <PropertiesTextEdit MaxLength="100"/>
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>





                                    <dx:GridViewDataTextColumn Caption="Id" FieldName="ID_LINHA" ReadOnly="True" 
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                        <EditFormSettings Visible="False" />
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior ConfirmDelete="True" />
                                <SettingsPager>
                                    <Summary AllPagesText="Páginas: {0} - {1} ({2} items)" 
                                        Text="Página {0} de {1} ({2} itens)" />
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" 
                                    PopupEditFormHorizontalAlign="LeftSides" />
                                <Settings ShowFilterRow="True" />
                                <SettingsText CommandCancel="Cancelar" CommandClearFilter="Limpar" 
                                    CommandDelete="Deletar" CommandEdit="Editar" CommandUpdate="Salvar" 
                                    ConfirmDelete="Deseja mesmo deletar este registro?" 
                                    EmptyDataRow="Nenhum registro encontrado" />
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
                                    <Customization HorizontalAlign="Left">
                                    </Customization>
                                    <LoadingPanel ImageSpacing="5px">
                                    </LoadingPanel>
                                    <EditForm HorizontalAlign="Left">
                                    </EditForm>
                                </Styles>
                                <StylesEditors ButtonEditCellSpacing="0">
                                    <ProgressBar Height="21px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                       
                            

                                <asp:EntityDataSource ID="dsGridGestaoDesemp" runat="server" 
                               ContextTypeName="Negocio.gsatOrganogramaDataContext" 
                                DefaultContainerName="gsOrganogramaModel" EnableDelete="True" 
                                EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                                EntitySetName="ORG_FUNCIONARIO_EXPORT_GD_MDL">
                            </asp:EntityDataSource>



                           




                            <br />
                            <br />
                            <br />
                            <br />
                        
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="Treinamento">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                         
                            <div style="margin-left:5px">
                                <dx:ASPxButton ID="btnAdicionarTr" runat="server"  ToolTip="Adicionar" Cursor="hand"
                                EnableDefaultAppearance="False" Height="30px" HorizontalAlign="Left" 
                                ImageSpacing="10px" OnClick="btnAdicionarTr_Click" Text="Adicionar" 
                                VerticalAlign="Middle" Width="100px">
                                <Image Height="20px" Url="~/Imagens/AdicionarNew.png">
                                </Image>
                                <Paddings PaddingLeft="10px" />
                                <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            </dx:ASPxButton></div>


                            <br />
                            <dx:ASPxGridView ID="dGridTreinamento" runat="server" 
                                AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                                CssPostfix="DevEx" DataSourceID="dsGridTR" KeyFieldName="ID_LINHA" 
                                Width="97%">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Ações" ShowInCustomizationForm="True" 
                                        VisibleIndex="0">
                                        <EditButton Visible="True">
                                        </EditButton>
                                        <DeleteButton Visible="True">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="FIRST_NAME" 
                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="LAST_NAME" 
                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="EMAIL" 
                                        ShowInCustomizationForm="True" VisibleIndex="4">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="USERNAME" 
                                        ShowInCustomizationForm="True" VisibleIndex="5">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PASSWORD" 
                                        ShowInCustomizationForm="True" VisibleIndex="6">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PERSONSTATUS" 
                                        ShowInCustomizationForm="True" VisibleIndex="7">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="USERSTATUS" 
                                        ShowInCustomizationForm="True" VisibleIndex="8">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="CPF" ShowInCustomizationForm="True" 
                                        VisibleIndex="9">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_EMPRESA" 
                                        ShowInCustomizationForm="True" VisibleIndex="10">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_EMPRESA" 
                                        ShowInCustomizationForm="True" VisibleIndex="11">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_FILIAL" 
                                        ShowInCustomizationForm="True" VisibleIndex="12">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_FILIAL" 
                                        ShowInCustomizationForm="True" VisibleIndex="13">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DAT_ADMISSAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="14">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_SECAO" ShowInCustomizationForm="True" 
                                        VisibleIndex="15">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_SECAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="16">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="CHAPA" ShowInCustomizationForm="True" 
                                        VisibleIndex="17">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_CARGO" ShowInCustomizationForm="True" 
                                        VisibleIndex="18">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_FUNCAO" 
                                        ShowInCustomizationForm="True" VisibleIndex="19">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="CPF_GESTOR" 
                                        ShowInCustomizationForm="True" VisibleIndex="20">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_GESTOR" 
                                        ShowInCustomizationForm="True" VisibleIndex="21">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="EMAIL_GESTOR" 
                                        ShowInCustomizationForm="True" VisibleIndex="22">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SEXO" ShowInCustomizationForm="True" 
                                        VisibleIndex="23">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DATA_NASCIMENTO" 
                                        ShowInCustomizationForm="True" VisibleIndex="24">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_TIPO_FUNC" 
                                        ShowInCustomizationForm="True" VisibleIndex="25">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="DESC_TIPO_FUNC" 
                                        ShowInCustomizationForm="True" VisibleIndex="26">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="PERFIL_PROFISSIONAL"  ShowInCustomizationForm="true" VisibleIndex="26">
                                    
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn FieldName="COD_SECAO_SUP" 
                                        ShowInCustomizationForm="True" VisibleIndex="27">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_SECAO_SUP" 
                                        ShowInCustomizationForm="True" VisibleIndex="28">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="COD_DIRETORIA" 
                                        ShowInCustomizationForm="True" VisibleIndex="29">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NOME_DIRETORIA" 
                                        ShowInCustomizationForm="True" VisibleIndex="30">
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ID_LINHA" ShowInCustomizationForm="True" 
                                        VisibleIndex="1" Caption="ID" ReadOnly="True">
                                        <EditFormSettings Visible="False" />
                                    <PropertiesTextEdit Width="300px"></PropertiesTextEdit></dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior ConfirmDelete="True" />
                                <SettingsPager>
                                    <Summary AllPagesText="Páginas: {0} - {1} ({2} items)" 
                                        Text="Página {0} de {1} ({2} itens)" />
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" 
                                    PopupEditFormHorizontalAlign="LeftSides" />
                                <Settings ShowFilterRow="True" />
                                <SettingsText CommandCancel="Cancelar" CommandClearFilter="Limpar" 
                                    CommandDelete="Deletar" CommandEdit="Editar" CommandUpdate="Salvar" 
                                    ConfirmDelete="Deseja mesmo deletar este registro?" 
                                    EmptyDataRow="Nenhum registro encontrado" 
                                    CustomizationWindowCaption="Editar" />
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
                         



                                <asp:EntityDataSource ID="dsGridTR" runat="server" 
                               ContextTypeName="Negocio.gsatOrganogramaDataContext" 
                                DefaultContainerName="gsOrganogramaModel" EnableDelete="True" 
                                EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                                EntitySetName="ORG_FUNCIONARIO_EXPORT_TR_MDL">
                            </asp:EntityDataSource>



                            <br />
                            <br />
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
  
  
</asp:Content>
