<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModificarUsuarios.aspx.cs" Inherits="ModificarUsuarios" %>

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
        .style3
        {
            width: 391px;
        }
        .style4
        {
            width: 80px;
        }
        .style5
        {
            width: 93px;
        }
        .style6
        {
            width: 80px;
            height: 56px;
        }
        .style7
        {
            width: 93px;
            height: 56px;
        }
        .style8
        {
            width: 391px;
            height: 56px;
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
        <b>Modificar Usuarios<br />
        </b>
          
 
            <asp:Label ID="Mensaje" runat="server" ForeColor="#CC0000" 
        style="margin-left:25px;" Font-Bold="True"></asp:Label>
            <br />
            <br />
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" 
            AllowPaging="True" 
            DataKeyNames="codigo" onpageindexchanging="GridView1_PageIndexChanging" 
            onrowcommand="GridView1_RowCommand" PageSize="1000" Width="1038px" Font-Size="Small">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Modificar" 
                    ImageUrl="~/imagenes/001_52.png" Text="Modificar" />
                <asp:BoundField DataField="CODIGO" HeaderText="ID" 
                    SortExpression="CODIGO" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Perfil" HeaderText="Perfil" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Login" HeaderText="Login" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha Ingreso" 
                    DataFormatString="{0:d}" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
    
        <br />
    
    
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

    </form>
</body>
</html>
