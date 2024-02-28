<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaInicio.aspx.cs" Inherits="HelpDesk.vistaInicio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="scripts/EventosComunes.js" type="text/javascript"></script>
    <%--TOOLBAR FLOTANTE <script src="scripts/EventosComunes.js" type="text/javascript"></script>
    <script src="scripts/mootools-1.2.2-core-nc.js" type="text/javascript"></script>
    <script src="scripts/e24scrollevents-1.0.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .style12
        {
            width: 128px;
        }
        .style13
        {
            width: 78px;
        }
        .style14
        {
            width: 335px;
        }
        .style2
        {
            width: 40px;
        }
        .style17
        {
            width: 77px;
        }
        .style21
        {
            width: 618px;
        }
    .style22
    {
        width: 20px;
            height: 69px;
        }
    .style23
    {
        width: 94px;
            height: 69px;
        }
        .style25
        {
            width: 618px;
            height: 69px;
        }
        .style26
        {
            width: 76px;
        }
        .style27
        {
            width: 74px;
        }
        .style28
        {
            width: 246px;
        }
        .style29
        {
            width: 49px;
        }
    .style30
    {
        width: 902px;
    }
        .auto-style41 {
            width: 177px;
            height: 40px;
        }
        .auto-style43 {
            width: 118px;
            height: 35px;
        }
        .auto-style44 {
            width: 8px;
            height: 35px;
        }
        .auto-style45 {
            width: 195px;
            height: 35px;
        }
        .auto-style58 {
            width: 19px;
            height: 40px;
        }
        .auto-style96 {
            width: 8px;
            height: 40px;
        }
        .auto-style97 {
            width: 195px;
            height: 40px;
        }
        .auto-style98 {
            width: 8px;
        }
        .auto-style99 {
            width: 263px;
            height: 40px;
        }
        .auto-style100 {
            width: 177px;
        }
        .auto-style101 {
            width: 118px;
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div id="Inicial" style="position:fixed; height:30px; top:0px; left:0px;">
        <div id="Menu">        
            <table cellpadding="0" cellspacing="0" border="0" style="z-index:800; width:1024px">
            
            <tbody>
                <tr>
                    <td>
                        <img src="Toolbar/paginationRowEdgeL.gif" alt=""/>
                    </td>
                    <td>
                        <img alt="" height="30" src="Toolbar/ButtonBarEdgeL.gif"/></td>
                    <td>
                        <img src="Toolbar/ButtonBarDividerL.gif" alt=""/>
                    </td>
                    <td>
                        <asp:ImageButton ID="Inicio" runat="server" Height="30px" 
                            ImageUrl="~/Toolbar/ButtonBarHome.gif" onclick="Inicio_Click" 
                            onmouseout="this.src='Toolbar/ButtonBarHome.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarHomeOver.gif'" 
                            ToolTip="Inicio" Visible="True" />
                    </td>                  
                    <td >
                        <img src="Toolbar/ButtonBarDividerR.gif" alt=""/>
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
        style="top:130px; z-index:800; width:1000px; left: 0px; height: 75px;">
            <tbody>
                <tr>
                    <td class="style13">
                    </td>
                    <td >
                        </td>
                    <td class="style30">
                    </td>
                    <td>                        
                        <ul class="gallery clearfix" 
                            style="font-style: italic; color: #000066; font-size: medium;"/>
                            <%if (perfil == 1){%>Usuarios
                            <li><a href="Parametrizacion/Usuarios.aspx?iframe=true&amp;width=490&amp;height=440&amp;SCROLLING=no" rel="prettyPhoto[iframe]">
                            Crear</a></li>
                            <li><a href="Parametrizacion/ModificarUsuarios.aspx?iframe=true&amp;width=1100&amp;height=450&amp;SCROLLING=no" rel="prettyPhoto[iframe]">
                            Modificar</a></li><%}%>
	                    
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
        
        <asp:UpdatePanel ID="upPagina" runat = "server" UpdateMode ="Conditional" >
        <ContentTemplate>
        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        
            <br />
        
        <asp:Label ID="Titulo1" runat="server" Font-Bold="False" ForeColor="#6699FF" Font-Size="Large">Seleccione una opción:</asp:Label>
            <br />
            <br />
        <br />
        <table ID="table_factura" runat="server">
            <tr>
                <td class="auto-style43">
                </td>
                <td class="auto-style44">
                    </td>
                <td class="auto-style45" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;">
                    <a href="vistaCrearClienteObra.aspx?iframe=false" rel="prettyPhoto[iframe]">Crear Cliente - Obra</a>
                    <br />
                </td>
                <td class="auto-style58">
                    <asp:ImageButton ID="btnCrearClienteObra" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_60.png" onclick="btnCrearClienteObra_Click" ToolTip="Crear Cliente-Obra" Width="24px" />
                </td>
                <td class="auto-style100">

                </td>
                <td class="auto-style99" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;" >                   
                    <a href="vistaReporteRemision.aspx?iframe=false" rel="prettyPhoto[iframe]" style="font-style: italic; font-weight: bold">Reporte de remisiones</a></td>
                <td class="auto-style98">

                    <asp:ImageButton ID="btnReporteRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/application_search.png" onclick="btnReporteRemision_Click" ToolTip="Reporte de remisiones" Width="24px" />

                </td>
            </tr>
            
            <tr>
                <td class="auto-style101">
                </td>
                <td class="auto-style96">
                    &nbsp;</td>
                <td class="auto-style97" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;">
                     <a href="vistaCrearInventario.aspx?iframe=false" rel="prettyPhoto[iframe]">Inventario</a><br />
                </td>
                <td class="auto-style58">
                    <asp:ImageButton ID="btnCrearInventario" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_60.png" onclick="btnCrearInventario_Click" ToolTip="Crear Inventario" Width="24px" />
                </td>
                <td class="auto-style100">

                </td>
                <td class="auto-style99" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;" >                   
                    <a href="vistaReporteDia.aspx?iframe=false" rel="prettyPhoto[iframe]" style="font-style: italic; font-weight: bold">Reporte al día</a></td>
                <td class="auto-style98">

                    <asp:ImageButton ID="btnReporteDia" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/application_search.png" onclick="btnReporteDia_Click" ToolTip="Reporte al día por obra" Width="24px" />

                </td>
            </tr>
            
            <tr>
                <td class="auto-style101">
                </td>
                <td class="auto-style96">
                    &nbsp;</td>
                <td class="auto-style97" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;">
                     <a href="vistaRemision.aspx?iframe=false" rel="prettyPhoto[iframe]">Remisiones</a><br />
                </td>
                <td class="auto-style58">
                    <asp:ImageButton ID="btnNuevaRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_60.png" onclick="btnNuevaRemision_Click" ToolTip="Nueva Remisión" Width="24px" />
                </td>
                <td class="auto-style100">

                </td>
                <td class="auto-style99" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;" >                   
                    <a href="vistaReporteHistorico.aspx?iframe=false" rel="prettyPhoto[iframe]" style="font-style: italic; font-weight: bold">Reporte histórico</a></td>

                <td class="auto-style98">

                    <asp:ImageButton ID="btnReporteReporteHistorico" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/application_search.png" onclick="btnReporteHistorico_Click" ToolTip="Reporte de facturación" Width="24px" />

                </td>
            </tr>

            <tr>
                <td class="auto-style101">
                </td>
                <td class="auto-style96">
                    &nbsp;</td>
                <td class="auto-style97" 
                    style="font-style: normal; font-size: large; color: #000066; font-family: Verdana, Geneva, Tahoma, sans-serif;">
                     <a href="vistaRecordatorio.aspx?iframe=false" rel="prettyPhoto[iframe]">Recordatorio</a><br />
                </td>
                <td class="auto-style58">
                    <asp:ImageButton ID="btnRecordatorio" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_60.png" onclick="btnRecordatorio_Click" ToolTip="Recordatorio" Width="24px" />
                </td>
                <td class="auto-style100">

                </td>
                <td class="auto-style101">
                </td>
                <td class="auto-style101">
                </td>
            </tr>


        </table>
        

        </ContentTemplate>
        </asp:UpdatePanel>
        
        
       
        <br />
        

       
        
</asp:Content>

