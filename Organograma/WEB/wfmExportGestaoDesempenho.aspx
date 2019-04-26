<%@ Page Title="" Language="C#" MasterPageFile="~/gsGeracaoArquivos.master" AutoEventWireup="true" CodeBehind="wfmExportGestaoDesempenho.aspx.cs" Inherits="WEB.wfmExportGestaoDesempenho" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpGerador" runat="server">
    
        <br />
        <table style="width:100% ">
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                   <span class="TextoCinzaMedio"> Selecione o destino:</span></td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>


                <table  style="width:300px; border:1px solid gray" >
                <tr style="padding:10px">
                <td><asp:RadioButton ID="rdDownload"  GroupName="destino" runat="server"  CssClass="TextoAzulMedio"
                        oncheckedchanged="MudaVisualizacao"  Text="Download" Checked="True" AutoPostBack="True" />
                   </td><td>
                        <asp:RadioButton ID="rdDiretorio"  GroupName="destino" runat="server"  CssClass="TextoAzulMedio"
                            Text="Diretório" AutoPostBack="True" 
                            oncheckedchanged="MudaVisualizacao" />
                    </td><td>
                        <asp:RadioButton ID="rdFtp"  GroupName="destino" runat="server"  CssClass="TextoAzulMedio"
                            oncheckedchanged="MudaVisualizacao" Text="Ftp" AutoPostBack="True" 
                           />
                    </td>
                </tr>
                </table>

                    




                </td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
          
          
          <asp:Panel ID="pnlFTP" runat="server" Width="100%" Visible="False">        
    <table style="width:100%;border:1px solid gray;">
<tr>
<td style="width:40px"><span class="TextoCinzaMedio">Host:</span></td>   <td>   <asp:TextBox ID="txtFtpHost" 
        runat="server" Width="300px" /></td>
</tr>
<tr>
<td><span class="TextoCinzaMedio">Usuário:</span></td><td> <asp:TextBox ID="txtFtpUsuario" runat="server" /></td>
</tr>

<tr>
<td><span class="TextoCinzaMedio">Senha:</span></td><td> <asp:TextBox ID="txtFtpSenha" runat="server" 
        TextMode="Password" /></td>
</tr>

<tr>
<td><span class="TextoCinzaMedio">Pasta:</span></td><td> <asp:TextBox ID="txtFtpPasta" runat="server" /></td>
</tr>


</table>
</asp:Panel>


<asp:Panel ID="pnlDiretorio" runat="server"  Visible="false">
<table style="width:100%;border:1px solid gray">
<tr>
<td><span class="TextoCinzaMedio">Diretório:</span></td>
</tr>
<tr>
<td><asp:TextBox runat="server" ID="txtDiretorio" Width="400px" ></asp:TextBox></td>
</tr>

</table>


</asp:Panel>
                   
                </td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                    
                    <table style="width:400px">
                    <tr>
                    <td style="width:110px"><span class="TextoCinzaMedio">Data de corte:</span></td><td>
                        <dx:ASPxDateEdit ID="cboDtCorte" runat="server">
                        </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    </table>
                    
                    </td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                <span class="TextoCinzaMedio">
                    Seleção dos Arquivos:</span></td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                    

                    <table style="vertical-align:middle;">
                   
                    <tr>
                    <td style="width:15px"><asp:CheckBox ID="gFuncionarios" runat="server"   Text="" 
                            Checked="True"/> </td><td><span class="TextoCinzaPequeno"> Funcionários</span></td>
                    </tr>
                     <tr>
                    <td style="width:15px"><asp:CheckBox ID="gCargos" runat="server"   Text="" 
                            Checked="True"/> </td><td><span class="TextoCinzaPequeno">Cargos</span></td>
                    </tr>
                     <tr>
                    <td style="width:15px"><asp:CheckBox ID="gEmpresas" runat="server"   Text="" 
                            Checked="True"/> </td><td><span class="TextoCinzaPequeno">Empresas</span></td>
                    </tr>
                     <tr>
                    <td style="width:15px"><asp:CheckBox ID="gTipoDeFuncionario" runat="server"   
                            Text="" Checked="True"/> </td><td><span class="TextoCinzaPequeno">Tipo de Funcionário</span></td>
                    </tr>
                     <tr>
                    <td style="width:15px"><asp:CheckBox ID="gLocalDeTrabalho" runat="server"   Text="" 
                            Checked="True"/> </td><td><span class="TextoCinzaPequeno">Local de Trabalho</span></td>
                    </tr>
                     <tr>
                    <td style="width:15px"><asp:CheckBox ID="gOrgaos" runat="server"   Text="" 
                            Checked="True"/> </td><td><span class="TextoCinzaPequeno">Orgãos</span></td>
                    </tr>

                    </table>





                    
                    
                    
                    </td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                         

                         <asp:CheckBox ID="chkSalvarParametros" Text="Salvar Parametros da Consulta" runat="server" />
                         
                         
                         </td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                         
                         <asp:Label ID="lblErro" runat="server" Text="" ForeColor="Red" Visible="false"  ></asp:Label>
                         
                         
                         </td>
            </tr>
            <tr>
                <td  class="s10">
                    &nbsp;</td>
                <td>
                      <dx:ASPxButton runat="server" ID="btnGerar" Text=" Gerar " Width="137px" 
                          onclick="btnGerar_Click"></dx:ASPxButton>           </td>
            </tr>
        </table>
    

</asp:Content>
