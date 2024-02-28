<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="HelpDesk.Usuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiandamios</title>
    <meta name="keywords" content="Fidupais" />
    <meta name="description" content="Fidupais" />
    <link href="../Imagenes/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <style type="text/css">
        body 
        {
            float: none;
            font-family: Arial, Helvetica, sans-serif;
	        margin-top: auto;
	        margin-right: auto;
	        margin-bottom: auto;
	        margin-left: auto;
        }
        .style1
        {
            width: 33px;
        }
        .style2
        {
            height: 42px;
        }
        </style>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left:60px;" >
            <tbody>
                <tr>
                    <td>
                        <img src="../Toolbar/paginationRowEdgeL.gif" alt="">
                    </td>
                    <td>
                        <img src="../Toolbar/ButtonBarEdgeL.gif" alt="">
                    </td>
                    <td>
                        <img src="../Toolbar/ButtonBarDividerL.gif" alt="">
                    </td>
                     <td class="style1">
                        <asp:ImageButton ID="Guardar" runat="server"  
                        onmouseout="this.src='../Toolbar/ButtonBarSave.gif'"
                        onmouseover="this.src='../Toolbar/ButtonBarSaveOver.gif'"
                        ToolTip="Guardar" ImageUrl="~/Toolbar/ButtonBarSave.gif" 
                        onclick="Guardar_Click" />
                    </td>
                    <td >
                        <img src="../Toolbar/ButtonBarEdgeR.gif" alt="">
                    </td>
                    <td width="100%">
                        &nbsp;
                    </td>
                </tr>
            </tbody>
          </table>
    <table style="width:445px; margin-right: 0px;">
                <tr>
                    <td colspan="3" style="text-align: left" class="style2">
                        <b>
                        <asp:Label ID="Titulo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                        <br />
                        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                        </b></td>                 
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td&nbsp;</td>
                    <td class="style5">
                        Identificación:</td>
                    <td class="style3">
             <asp:TextBox ID="TxtCodigo" runat="server" Width="200px" Height="20px" MaxLength="60" 
                AutoPostBack="True" ></asp:TextBox>
             <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                    ControlToValidate="TxtCodigo" ErrorMessage="* Incorrecto"                     
                    ValidationExpression="^[0-9 \sáéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        </td&nbsp;</td>
                        &nbsp;
                    <td class="style5">
                        Nombre:</td>
                    <td class="style3">
            <asp:TextBox ID="Nombre" runat="server" Width="200px" Height="20px" MaxLength="60" Style="text-transform:uppercase"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="Nombre" ErrorMessage="* Incorrecto"                     
                        ValidationExpression="^[A-Za-z \sáéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        E-mail: <td class="style3">
                        <asp:TextBox ID="Email" runat="server" Width="200px" Height="20px" MaxLength="100" Style="text-transform:lowercase"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                            ControlToValidate="Email" ErrorMessage="* Incorrecto" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        Login:</td>
                    <td class="style3">
            <asp:TextBox ID="Login" runat="server" Width="200px" Height="20px" MaxLength="20" Style="text-transform:uppercase"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                        ControlToValidate="Login" ErrorMessage="* Incorrecto" 
                        ValidationExpression="[A-Za-z0-9áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!()  \s]{0,300}$" 
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        Perfil:</td>
                    <td class="style3">
                        <asp:DropDownList ID="DropDownPerfil" runat="server" Width="207px" Height="20px" AutoPostBack="True"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        Estado:</td>
                    <td class="style3">
                        <asp:RadioButtonList ID="RadioButtonEstado" runat="server">
                            <asp:ListItem Value="1" Selected="True">Activo</asp:ListItem>
                            <asp:ListItem Value="2">Inactivo</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        Clave:</td>
                    <td class="style3">
                        <asp:TextBox ID="Clave" runat="server" Width="200px" Height="20px" TextMode="Password" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                
            </table>
                
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
