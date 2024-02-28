<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contrasena.aspx.cs" Inherits="ControlVentas.Contrasena" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiandamios</title>
    <meta name="keywords" content="ControlVentas" />
    <meta name="description" content="ControlVentas" />
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
        .style4
        {
            height: 47px;
        }
        .style6
        {
            height: 33px;
            width: 2px;
        }
        .style7
        {
            height: 33px;
            width: 132px;
        }
        .style8
        {
            height: 33px;
            width: 14px;
        }
        .style9
        {
            height: 33px;
            width: 18px;
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
    <table style="width:383px; margin-right: 0px; height: 92px;">
                <tr>
                    <td colspan="3" style="text-align: left" class="style4">
                        <b>Cambio de clave de usuario<br />
                        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                        <br />
                        </b></td>
                    
                </tr>
               
                <tr>
                    <td class="style8">
                        </td>
                    <td class="style7">
                        Clave Acual: </td>
                    <td class="style6">
                        <asp:TextBox ID="TxtClaveActual" runat="server" Height="20px" MaxLength="10" 
                            TextMode="Password" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style9">
                        </td>
                </tr>
                <tr>
                    <td class="style8">
                        </td>
                    <td class="style7">
                        Clave Nueva:</td>
                    <td class="style6">
                        <asp:TextBox ID="TxtClave" runat="server" Width="200px" Height="20px" TextMode="Password" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                    <td class="style9">
                    </td>
                </tr>
                <tr> 
                    <td class="style8">
                        </td>  
                    <td class="style7">
                        Confirmar Clave:</td>
                    <td class="style6">
                        <asp:TextBox ID="TxtConfirmar" runat="server" Height="20px" MaxLength="10" 
                            TextMode="Password" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style9">
                        </td>
                </tr>
               
            </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
