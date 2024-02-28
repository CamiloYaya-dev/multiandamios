<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sign.ascx.cs" Inherits="sign" %>
<div style="border:solid 1px Blue">
<p><asp:Label ID="TituloFirma" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label></p>
<asp:Panel ID="PanelFirma" runat="server">
<table border="0 0 0 0">
<tr>
<td><asp:Label ID="lblNombrel" runat="server" Text="Nombre: "></asp:Label></td>
<td><asp:Label ID="lblNombre" runat="server"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="lblCargol" runat="server" Text="Cargo: "></asp:Label></td>
<td><asp:Label ID="lblCargo" runat="server"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="lblTelefono_Celularl" runat="server" Text="Telefono/Celular: "></asp:Label></td>
<td><asp:Label ID="lblTelefono_Celular" runat="server"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="lblemaill" runat="server" Text="E-mail: "></asp:Label></td>
<td><asp:Label ID="lblemail" runat="server"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="lblCedulal" runat="server" Text="Cedula: "></asp:Label></td>
<td><asp:Label ID="lblCedula" runat="server"></asp:Label></td>
</tr>
<tr>
<td colspan="2">
    <asp:Image ID="Firma" runat="server" AlternateText="Sin firma registrada" ImageAlign="AbsMiddle"/></td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="PanelFirmar" runat="server">
<div align="center" style="text-align:center">
    <asp:Button ID="btFirmar" runat="server" Text="Firmar"
        onclick="btFirmar_Click" />
</div>
</asp:Panel>
</div>


