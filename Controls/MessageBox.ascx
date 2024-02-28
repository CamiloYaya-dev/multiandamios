<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="hvpagaduria_Controls_MessageBox" %>
<script src="<%= ResolveClientUrl("~/")%>hvpagaduria/scripts/EventosComunes.js" type="text/javascript"></script>
<asp:UpdatePanel ID="upMessagebox" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
<div id="FondoMensaje" runat="server" style="display:none; background-color:White;filter:alpha(opacity=70);opacity:0.7;position:fixed; 
                                        z-index:1000; 	top:0px;left:0px; right:0px; bottom:0px">                                                  
     </div>

<div id="divMensaje" runat="server" style="display:none; background-color:#DCE4F9; z-index:1001; position:absolute; max-width:80%; box-shadow: #111 0 .15em .17em;-webkit-box-shadow: #111 0 .15em .17.em; -moz-box-shadow: #111 0 .15em .17em; " onmousedown="comienzoMovimiento(event, this.id);">

<table border="0 0 0 0" style="width:100%; text-align:center">
<tr style="background-color: #3A93D2; font-size: medium;color: #003366;font-weight: bold;height: 25px;">
<td style="background-color: #3A93D2; text-align:left"><asp:Label ID="lblTitulo" runat="server"></asp:Label></td>
<td style="text-align:right; width:20px;background-color: #3A93D2; "><asp:ImageButton ID="Cerrar" runat="server" ImageUrl="~/imagenes/001_05.png" 
                                    onclick="Cerrar_Click" /></td>
</tr>
<tr>
<td colspan="2" style="padding:20px"><%= ELMensaje %></td>
</tr>
<tr>
<td colspan="2"><asp:Button ID="btSi" runat="server" Visible="false" OnClick="btSi_Click" Text="Si" /><asp:Button ID="btNo" runat="server"  Visible="false" Text="No" OnClick="btNo_Click" /><asp:Button ID="btAceptar" runat="server"  Visible="false" Text="Aceptar" OnClick="btAceptar_Click"/><asp:Button ID="btCancelar" runat="server"  Visible="false" Text="Cancelar" OnClick="btCancelar_Click"/></td>
</tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>