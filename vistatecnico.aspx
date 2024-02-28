<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistatecnico.aspx.cs" Inherits="HelpDesk.vistatecnico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register TagPrefix="tecf" TagName="Tecfinsa" Src="~/Controls/EscalarSoporte.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="scripts/EventosComunes.js" type="text/javascript"></script>
    <%--TOOLBAR FLOTANTE <script src="scripts/EventosComunes.js" type="text/javascript"></script>
    <script src="scripts/mootools-1.2.2-core-nc.js" type="text/javascript"></script>
    <script src="scripts/e24scrollevents-1.0.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .style8
        {
            width: 972px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:UpdatePanel ID="upEscalar" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Escalar" EventName="Ocultar" />
        </Triggers>
    </asp:UpdatePanel>
    <tecf:Tecfinsa ID="Escalar" runat="server" />

    <div id="Inicial" style="position:fixed; height:30px; top:0px; left:0px;">
        <div id="Menu">        
            <table cellpadding="0" cellspacing="0" border="0" style="z-index:800; width:1024px">
            
            <tbody>
                <tr>
                    <td>
                        <img src="Toolbar/paginationRowEdgeL.gif" alt="">
                    </td>
                    <td>
                        <img alt="" height="30" src="Toolbar/ButtonBarEdgeL.gif"></img></td>
                    <td>
                        <img src="Toolbar/ButtonBarDividerL.gif" alt="">
                    </td>
                    <td>
                        <asp:ImageButton ID="Nuevo" runat="server" Height="30px" 
                            ImageUrl="~/Toolbar/ButtonBarNew.gif" onclick="Nuevo_Click" 
                            onmouseout="this.src='Toolbar/ButtonBarNew.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarNewOver.gif'" 
                            ToolTip="Nuevo Soporte" />
                    </td>
                    <td>
                        <asp:ImageButton ID="Modificar" runat="server" 
                            ImageUrl="~/Toolbar/ButtonBarEdit.gif" 
                            onmouseout="this.src='Toolbar/ButtonBarEdit.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarEditOver.gif'"
                             onclick="Modificar_Click" ToolTip="Modificar" style="width: 30px" 
                            Height="30px" Visible="False"/>
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="Eliminar" runat="server" Enabled="false"
                        ImageUrl="~/Toolbar/ButtonBarDelete.gif" 
                        onmouseout="this.src='Toolbar/ButtonBarDelete.gif'"  
                        onmouseover="this.src='Toolbar/ButtonBarDeleteOver.gif'" 
                            onclick="Eliminar_Click" ToolTip="Eliminar" Height="30px" 
                            Visible="False" />
                       
                            
                    </td>
                     <td>
                        <asp:ImageButton ID="Guardar" runat="server" Enabled="false"
                        onmouseout="this.src='Toolbar/ButtonBarSave.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarSaveOver.gif'"
                        ToolTip="Guardar" ImageUrl="~/Toolbar/ButtonBarSave.gif" Height="30px" 
                             Visible="False" />
                    </td>
                    <td>
                        <asp:ImageButton ID="Refrescar" runat="server" 
                        onmouseout="this.src='Toolbar/ButtonBarRefresh.gif'" 
                        onmouseover="this.src='Toolbar/ButtonBarRefreshOver.gif'" 
                        ToolTip="Refrescar"  ImageUrl="~/Toolbar/ButtonBarRefresh.gif" 
                            onclick="Refrescar_Click" Height="30px" />
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="Copiar" runat="server" Enabled="false"
                        onmouseout="this.src='Toolbar/ButtonBarCopy.gif'" 
                        onmouseover="this.src='Toolbar/ButtonBarCopyOver.gif'" 
                        ImageUrl="~/Toolbar/ButtonBarCopy.gif" ToolTip="Copiar" onclick="Copiar_Click" 
                            Height="30px" Visible="False" />
                        
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="InformePDF" runat="server" Enabled="false" 
                            ImageUrl="~/Toolbar/ButtonBarPDFExport.gif" ToolTip="Reporte"
                            onmouseout="this.src='Toolbar/ButtonBarPDFExport.gif'"
                            onmouseover="this.src='Toolbar/ButtonBarPDFExportOver.gif'" Height="30px" 
                            onclick="InformePDF_Click" Visible="False"/>
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="InformeWord" runat="server" Enabled="false" ToolTip="Exportar en Word"
                        onmouseout="this.src='Toolbar/ButtonBarWordExport.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarWordExportOver.gif'" 
                        ImageUrl="~/Toolbar/ButtonBarWordExport.gif" Height="30px" Visible="False" />                      
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="ExportarExcel" runat="server" Visible="False"
                        onmouseout="this.src='Toolbar/ButtonBarExcelExport.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarExcelExportOver.gif'"
                        ToolTip="Exportar en Excel" 
                        ImageUrl="~/Toolbar/ButtonBarExcelExport.gif" onclick="ExportarExcel_Click" 
                            Height="30px" />
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="ManualPDF" runat="server"
                        onmouseout="this.src='Toolbar/ButtonBarManual.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarManualOver.gif'"
                        ToolTip="Manual de Usuario en PDF" 
                        ImageUrl="~/Toolbar/ButtonBarManual.gif" onclick="ManualPDF_Click" 
                            Height="30px" Visible="True" />
                    </td>  
                    <td >
                        <img src="Toolbar/ButtonBarDividerR.gif" alt="">
                    </td>
                    <td >
                        <img alt="" height="30" src="Toolbar/ButtonBarEdgeR.gif"></img></td>
                    <td width="100%">
                        
                    </td>
                    
                </tr>
            </tbody>
        </table>
        </div>
    </div>
        
    <table id="AdUsuarios" cellpadding="0" cellspacing="0" border="0"             
        style="top:130px; z-index:800; width:1000px; left: 0px; height: 42px;">
            <tbody>
                <tr>
                    <td class="style13">
                    </td>
                    <td >
                        </td>
                    <td class="style8">
                    </td>
                    <td>                        
                        <ul class="gallery clearfix" 
                            style="font-style: italic; color: #000066; font-size: medium;">
                            <%if (perfil == 1){%>Usuarios
                            <li><a href="Parametrizacion/Usuarios.aspx?iframe=true&amp;width=490&amp;height=440&amp;SCROLLING=no" rel="prettyPhoto[iframe]">
                            Crear</a></li>
                            <li><a href="Parametrizacion/ModificarUsuarios.aspx?iframe=true&amp;width=1100&amp;height=450&amp;SCROLLING=no" rel="prettyPhoto[iframe]">
                            Modificar</a></li><%}%>
                            <li style="width: 160px"><a href="Parametrizacion/Contrasena.aspx?iframe=true&amp;width=450&amp;height=200&amp;SCROLLING=no" rel="prettyPhoto[iframe]">
                            Cambiar contraseña</a></li>
	                    </ul>
	                </td>
                </tr>
            </tbody>
        </table>
        
    <%--CONTROL DE BARRA FLOTANTE <script language="javascript" type="text/javascript">
        window.onscroll = function() {
        var PosicionMenu = getAbsoluteElementPosition(document.getElementById('Inicial')); 
            if (navigator.userAgent.indexOf('MSIE') >= 0) {
                if (document.documentElement.scrollTop > PosicionMenu.top + document.getElementById('Menu').offsetHeight) {
                    document.getElementById('Menu').style.position = 'fixed';
                    document.getElementById('Menu').style.top = '1px';
                    
                    
                } else {
                document.getElementById('Menu').style.position = 'relative';
                document.getElementById('Menu').style.top = '1px';
                
                };
            }
            else {
                if (document.body.scrollTop > PosicionMenu.top + $('Menu').height) {
                    document.getElementById('Menu').style.position = 'fixed';
                    document.getElementById('Menu').style.top = '1px';
                

                } else {
                document.getElementById('Menu').style.position = 'relative';
                document.getElementById('Menu').style.top = '1px';
                
                };
            }
        }
        </script>--%>
        
        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        
        <br />
        <asp:Label ID="TituloCanasta" runat="server" Font-Bold="True" 
            ForeColor="#6699FF">Listado de Tickets y/o Solicitudes por escalar</asp:Label>
        
        <br />
        <br />
        
            <asp:GridView ID="GridCanasta" runat="server" AllowPaging="True" 
            CellPadding="4"   DataKeyNames="Codigo"
            onselectedindexchanged="GridCanasta_SelectedIndexChanged" onrowcommand="GridCanasta_RowCommand" 
            onpageindexchanging="GridCanasta_PageIndexChanging" PageSize="12" AutoGenerateColumns="False" 
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="1018px"  Font-Size="Small" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>
                <asp:ButtonField ButtonType="Image" HeaderText="Escalar" CommandName="Escalar" 
                    ImageUrl="~/Imagenes/001_60.png" Text="Escalar" />
                <asp:BoundField DataField="Codigo" HeaderText="Ref." />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                <asp:BoundField DataField="Clase" HeaderText="Clase" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo de soporte" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        
        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#CC0000" 
            Text=""></asp:Label>
        
        <br />
        <asp:Label ID="Titulo3" runat="server" Font-Bold="True" ForeColor="#6699FF">Escalados</asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            ForeColor="#333333" GridLines="None" HorizontalAlign="Center" 
            onpageindexchanging="GridView1_PageIndexChanging" onrowcommand="GridView1_RowCommand" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="12" 
            Width="1018px" Font-Size="Small">
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="Detalle" HeaderText="Detalle"
                    ImageUrl="~/imagenes/001_60.png" Text="Detalle" />
                <asp:BoundField DataField="Codigo" HeaderText="Ref." />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                <asp:BoundField DataField="Clase" HeaderText="Clase" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo de soporte" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#CC0000" 
            Text=""></asp:Label>
        <br />
        <br />
        </asp:Content>

