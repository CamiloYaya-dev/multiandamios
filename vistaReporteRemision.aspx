<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaReporteRemision.aspx.cs" Inherits="HelpDesk.vistaReporteRemision" %>


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
        .auto-style84 {
            width: 20px;
            height: 17px;
        }
        .auto-style85 {
            width: 179px;
            height: 17px;
        }
        .auto-style86 {
            width: 487px;
            height: 17px;
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
                    <td>
                        <asp:ImageButton ID="CrearClienteObra" runat="server" 
                            ImageUrl="~/Toolbar/ButtonBarAdd.gif" 
                            onmouseout="this.src='Toolbar/ButtonBarAdd.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarAddOver.gif'"
                             onclick="CrearClienteObra_Click" ToolTip="Crear Cliente-Obra" style="width: 30px" 
                            Height="30px" Visible="True"/>
                    </td>
                    <%if (perfil == 1){%>
                    <td>
                        <asp:ImageButton ID="CrearInventario" runat="server" 
                            ImageUrl="~/Toolbar/ButtonBarCopy.gif" 
                            onmouseout="this.src='Toolbar/ButtonBarCopy.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarCopyOver.gif'"
                             onclick="CrearInventario_Click" ToolTip="Crear Inventario" style="width: 30px" 
                            Height="30px" Visible="True"/>
                    </td>
                    <%}%>
                    <td>
                        <asp:ImageButton ID="CrearRemision" runat="server" 
                            ImageUrl="~/Toolbar/ButtonBarEdit.gif" 
                            onmouseout="this.src='Toolbar/ButtonBarEdit.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarEditOver.gif'"
                             onclick="CrearRemision_Click" ToolTip="Crear Remisión" style="width: 30px" 
                            Height="30px" Visible="True"/>
                    </td>
                    <td>
                        <asp:ImageButton ID="Reporte" runat="server" Enabled="false" 
                            ImageUrl="~/Toolbar/ButtonBarPDFExport.gif" ToolTip="Reporte"
                            onmouseout="this.src='Toolbar/ButtonBarPDFExport.gif'"
                            onmouseover="this.src='Toolbar/ButtonBarPDFExportOver.gif'" Height="30px" 
                            onclick="Reporte_Click" Visible="false"/>
                    </td>
                    <td>
                        <asp:ImageButton ID="Refrescar" runat="server" 
                            onmouseout="this.src='Toolbar/ButtonBarRefresh.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarRefreshOver.gif'" 
                            ToolTip="Refrescar"  ImageUrl="~/Toolbar/ButtonBarRefresh.gif" 
                            onclick="Refrescar_Click" Height="30px" />
                    </td>                    
                    <td >
                        <img src="Toolbar/ButtonBarDividerR.gif" alt=""/>
                    </td>
                    <td >
                        <img alt="" height="30" src="Toolbar/ButtonBarEdgeR.gif"/></td>
                    <td width="100%">                        
                    </td>                    
                </tr>
            </tbody>
        </table>
        </div>
    </div>
        
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
            <br />
        
        <asp:Label ID="LabelReporte" runat="server" Font-Bold="True" ForeColor="#6699FF">Reporte de remisiones</asp:Label>
            &nbsp;
            <br />
            <table id="table_cliente" runat="server">
                <tr>
                    <td class="auto-style84"></td>
                    <td class="auto-style85" style="font-style: italic; color: #808080;">Buscar </td>
                    <td class="auto-style86" style="font-style: italic; font-size: small; color: #808080">
                        <asp:TextBox ID="txtBuscarCliente" runat="server" MaxLength="50" Style="text-transform:uppercase" Width="330px"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="btnBuscarClienteObra" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Search.png" onclick="btnBuscarClienteObra_Click" ToolTip="Buscar" Width="24px" />
                    </td>
                </tr>
            </table>
            <br />
            <table id="table_cliente2" runat="server">
                <tr>
                    <td class="auto-style84"></td>
                    <td class="auto-style85">Cliente - Obra</td>
                    <td class="auto-style86" style="font-style: italic; font-size: small; color: #808080">
                        <asp:DropDownList ID="listClienteObra" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="21px" Visible="true" Width="369px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table id="table_cliente3" runat="server">
                <tr>
                    <td class="auto-style84"></td>
                    <td class="auto-style85">
                        <asp:Label ID="lbTipoRemision0" runat="server" Font-Bold="False" ForeColor="Black" Visible="true">Tipo Remisión</asp:Label>
                    </td>
                    <td class="auto-style86" style="font-style: italic; font-size: small; color: #808080">
                        <asp:DropDownList ID="listTipoRemision" runat="server" AppendDataBoundItems="false" AutoPostBack="True" DataValueField="CC" Height="25px" Visible="true" Width="95px">
                            <asp:ListItem value="1">ENTREGA</asp:ListItem>
                            <asp:ListItem value="2">RETIRO</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="btnBuscarRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/down.png" onclick="btnBuscarRemision_Click" ToolTip="Buscar remisiones" Visible="true" Width="24px" />
                    </td>
                </tr>
            </table>
            &nbsp;<br /><asp:GridView ID="GridViewReporte" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                    onrowcommand="GridViewReporte_RowCommand" 
                    onpageindexchanging="GridViewReporte_PageIndexChanging" PageSize="100000" 
                    HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
                    Width="426px"  Font-Size="Small" >
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                    <Columns>   
                        <asp:ButtonField ButtonType="Image" CommandName="Ver" HeaderText="Ver Remisión"
                            ImageUrl="~/imagenes/001_60.png" Text="Ver" />
                        <asp:BoundField DataField="Codigo" HeaderText="Remisión" /> 
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha" /> 
                        <asp:BoundField DataField="Estado" HeaderText="Estado" /> 
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:GridView ID="GridViewReporteCompleto" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                    onrowcommand="GridViewReporteCompleto_RowCommand" 
                    onpageindexchanging="GridViewReporteCompleto_PageIndexChanging" PageSize="100000" 
                    HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
                    Width="900px"  Font-Size="Small" Height="2439px" >
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                    <Columns>
                        <asp:ButtonField ButtonType="Image" CommandName="Ver" HeaderText="Ver Remisión"
                            ImageUrl="~/imagenes/001_60.png" Text="Ver" />   
                        <asp:BoundField DataField="Codigo" HeaderText="Remisión" />
                        <asp:BoundField DataField="ClienteObra" HeaderText="Cliente - Obra" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha" /> 
                        <asp:BoundField DataField="Estado" HeaderText="Estado" /> 
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
        
        </ContentTemplate>
        </asp:UpdatePanel>
        
        
       
        &nbsp;<table ID="table_listado" runat="server">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                

            </td>
        </tr>
        <tr>
            <td>
            <asp:Label ID="Mensaje2" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
    </table> 
    <table ID="table_atras" runat="server">
        <tr>
           <td rowspan="2" style="color: #0066FF">
               <asp:ImageButton ID="Atras" runat="server" Height="24px" 
                   ImageUrl="~/imagenes/001_61.png" onclick="Atras_Click" 
                   style="margin-bottom: 0px" ToolTip="Atras" Visible="True" Width="24px" />
                   Atrás</td>
           </tr>
    </table>
  
       
        
</asp:Content>

