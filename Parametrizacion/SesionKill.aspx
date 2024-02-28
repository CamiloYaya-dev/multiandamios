<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SesionKill.aspx.cs" Inherits="Parametrizacion_SesionKill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Multiandamios</title>
    <meta name="keywords" content="Fidupais" />
    <meta name="description" content="Fidupais" />
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
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
        </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        &nbsp;<asp:Panel ID="Panel1" runat="server" style="margin-left:100px;margin-top:50px;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img alt="" src="../imagenes/user_delete.png" 
                style="width: 48px; height: 48px" />
            <br />
            &nbsp;Su sesión a terminado<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="../LoginPage.aspx" 
    target="_parent" style="font-size:small;color:#3A93D2;">[Iniciar Sesión]</a>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
