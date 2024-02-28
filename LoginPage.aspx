<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="LoginPage.aspx.cs" Inherits="HelpDesk._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiandamios</title>
    <meta name="keywords" content="HelpDesk" />
    <meta name="description" content="HelpDesk" />
    <link href="/Imagenes/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    
</head>
<body>
    <form id="form1" runat="server">
    <p style="margin-left: 280px">
&nbsp;<a&nbsp;</p>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image 
            ID="Logo" runat="server" Height="135px" 
            ImageUrl="~/imagenes/Logo.png" Width="430px" />
    </div>
    <div style="padding: 0px 0 0 250px;">
        <div id="login-box">
            <H2>Iniciar Sesión</H2>
            &nbsp;<br />	          
            <br />
            <div id="login-box-name" style="margin-top:20px;">
                <asp:Label ID="txtUsuario" runat="server" Text="Usuario:"></asp:Label>
            </div>
            <div id="login-box-field" style="margin-top:20px;">
            <asp:TextBox ID="Usuario" runat="server" Height="30px" Width="210px" 
                    Font-Size="20px" ontextchanged="Login_TextChanged" MaxLength="60" Style="text-transform:uppercase"></asp:TextBox>
            </div>
            <div id="login-box-name">
                <asp:Label ID="txtClave" runat="server" Text="Contraseña: "></asp:Label>
            </div>
            <div id="login-box-field">
                <asp:TextBox ID="Clave" runat="server" BackColor="White" MaxLength="20" 
                    TextMode="Password" Width="210px" Height="30px" Font-Size="20px"></asp:TextBox>
            </div>
            <span class="login-box-options">
            <%--<asp:CheckBox ID="Recordarme" runat="server" Text="Recordarme" />
            <ul id="custom_content" class="gallery clearfix">
                <a href="Default.aspx?custom=true&width=260&height=270" rel="prettyPhoto" style="margin-left:30px;">
                ¿Olvidó su contraseña? 
            </ul>  --%><br />
            </span>	          
            <asp:Label ID="Mensaje" runat="server" ForeColor="#960105" Font-Bold="True" 
                Font-Italic="True" Font-Size="Medium" style="text-align: left"></asp:Label>
            <br />
            <br />
            <asp:ImageButton ID="Login" runat="server"  
                ImageUrl="imagenes/login-btn.png" style="margin-left:120px;" 
                onclick="Login_Click" />
        </div>

</div>
 </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
