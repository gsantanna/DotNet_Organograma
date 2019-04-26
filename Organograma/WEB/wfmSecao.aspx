<%@ Page Title="" Language="C#" MasterPageFile="~/gsOrgMaster.Master" AutoEventWireup="true" CodeBehind="wfmSecao.aspx.cs" Inherits="WEB.wfmSecao" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   <div class ="Subtitulo" >
   <asp:Label ID="lblTitulo" runat="server"></asp:Label>
   </div>
   <br />


    <table  style="width:100%; vertical-align:top">
        <tr>
            <td class="s20">
                </td>
            <td style="vertical-align:top;width:40%">
            
            <table style="width:100%;padding:0px 0px 0px 0px;" >
           <tr>
           <td>
              <span class="TextoCinzaMedio">Seção:</span>  </td>
           <td class="s10">
               </td></tr>
           <tr>
           <td>
               <dx:ASPxTextBox ID="txtSecao" runat="server" ReadOnly="True" Width="100%">
               </dx:ASPxTextBox>
               </td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
             <span class="TextoCinzaMedio"> Código:</span> </td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
               <dx:ASPxTextBox ID="txtCodSecao" runat="server" ReadOnly="True" Width="100%">
               </dx:ASPxTextBox>
               </td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
             <span class="TextoCinzaMedio">Gestor RM:</span></td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
               <dx:ASPxTextBox ID="txtGestorRM" runat="server" ReadOnly="True" Width="100%">
               </dx:ASPxTextBox>
               </td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
              <span class="TextoCinzaMedio">Seção Superior:</span></td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
               <dx:ASPxComboBox ID="cboSecaoSuperior" runat="server" 
                   AutoResizeWithContainer="True" Width="100%" AutoPostBack="True" 
                   IncrementalFilteringMode="Contains" LoadingPanelText="Carregando&amp;hellip;" 
                   onselectedindexchanged="cboSecaoSuperior_SelectedIndexChanged">
               </dx:ASPxComboBox>
               </td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
              <span class="TextoCinzaMedio"> Código Seção Superior:</span></td>
           <td>
               &nbsp;</td></tr>
           <tr>
           <td>
               <dx:ASPxTextBox ID="txtCodSecaoSuperior" runat="server" ReadOnly="True" 
                   Width="100%">
               </dx:ASPxTextBox>
               </td>
           <td>
               &nbsp;</td></tr>
            </table>
            
            
            
            </td>
            <td class="s40">
                &nbsp;</td>
            <td style=" width:40%">
                
                <table>
                <tr>
                <td style="width:100%"><span class="TextoCinzaMedio">Diretoria:</span></td><td class="s10"></td>
                </tr>
                <tr>
                <td style="width:100%">
               <dx:ASPxTextBox ID="txtDiretoria" runat="server" ReadOnly="True" Width="100%">
               </dx:ASPxTextBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">
                    <dx:ASPxCheckBox ID="chkNomeModificado" runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkNomeModificado_CheckedChanged" 
                        Text="Possui nome modificado" CheckState="Unchecked">
                        <ClientSideEvents CheckedChanged="function(s, e) {
	
    if ( !s.GetChecked())
    {   if(!confirm('Deseja mesmo desmarcar esta opção e limpar o seu conteúdo?'))
        {
            e.processOnServer =  false;
            s.SetChecked(true);
        }
    }
               


}" />
                    </dx:ASPxCheckBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%"><span class="TextoCinzaMedio">Nome Modificado:</span></td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">
               <dx:ASPxTextBox ID="txtNomeModificado" runat="server" ReadOnly="True" Width="100%">
               </dx:ASPxTextBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">
                    <dx:ASPxCheckBox ID="chkGestorModificado" runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkGestorModificado_CheckedChanged" 
                        Text="Possui gestor modificado">


                          <ClientSideEvents CheckedChanged="function(s, e) {
	
    if ( !s.GetChecked())
    {   if(!confirm('Deseja mesmo desmarcar esta opção e limpar o seu conteúdo?'))
        {
            e.processOnServer =  false;
            s.SetChecked(true);
        }
    }
               


}" />





                    </dx:ASPxCheckBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%"><span class="TextoCinzaMedio">Gestor Modificado:</span></td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">
               <dx:ASPxComboBox ID="cboGestorModificado" runat="server" 
                   AutoResizeWithContainer="True" Width="100%" IncrementalFilteringMode="Contains" 
                        LoadingPanelText="Carregando&amp;hellip;" ReadOnly="True">
               </dx:ASPxComboBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">&nbsp;</td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">
                    <dx:ASPxCheckBox ID="chkPublico" runat="server" 
                        Text="Visível para todas as empresas">
                    </dx:ASPxCheckBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                <tr>
                <td style="width:100%">
                    <dx:ASPxCheckBox ID="chkPossuiDiretoria" runat="server" 
                        Text="Não possui seção superior/diretoria" AutoPostBack="True" 
                        oncheckedchanged="chkPossuiDiretoria_CheckedChanged" >
                    </dx:ASPxCheckBox>
                    </td><td class="s10">&nbsp;</td>
                </tr>
                </table>
                

                
                
                
                
                </td>
            <td style="vertical-align:bottom; width:205px;" >

            <table style="width:100%">
            <tr>
            <td>
            <span style="color:Red; font-weight:bold">
            
            <dx:ASPxLabel ID="lblErro" runat="server" EncodeHtml="false"  >
            </dx:ASPxLabel>
            
            </span>
            
            </td>
            </tr>
            </table>

               <table style="width:100%; padding:2px 2px 2px 2px;">
                <tr>
                <td> <dx:ASPxButton ID="btnSalvar" runat="server"  Text="Salvar" Width="100px" 
                        onclick="btnSalvar_Click"></dx:ASPxButton></td>
                <td> <dx:ASPxButton ID="btnCancelar" runat="server"  Text="Cancelar" Width="100px" 
                        onclick="btnCancelar_Click"></dx:ASPxButton></td>
                </tr>
                </table>
                
                
                
                </td>
        </tr>
        </table>
    </asp:Content>
