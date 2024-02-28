<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaReporteDia.aspx.cs" Inherits="HelpDesk.vistaReporteDia" %>


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
            width: 117px;
            height: 8px;
        }
        .auto-style85 {
            width: 140px;
            height: 7px;
        }
        .auto-style87 {
            width: 119px;
            height: 7px;
        }
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup {
            background-color: #FFFFFF;
            width: 600px;
            border: 2px solid #3399FF;
            border-radius: 12px;
            padding: 0;
        }
            .modalPopup .header {
                background-color: #337AB7;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }
            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }
            .modalPopup .footer {
                padding: 6px;
            }
            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }
            .modalPopup .yes {
                background-color: #337AB7;
                border: 1px solid #0DA9D0;
            }
            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
        .style3 {
            width: 896px;
        }
        .auto-style96 {
            width: 141px;
            height: 7px;
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
        
        <asp:Label ID="LabelReporte" runat="server" Font-Bold="True" ForeColor="#6699FF">Reporte al día</asp:Label>
        &nbsp;
        <br />
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
        <table id="table_cliente2" runat="server">
            <tr>
                <td class="auto-style84"></td>
                <td class="auto-style96" >Cliente - Obra</td>
                <td class="auto-style86" style="font-style: italic; font-size: small; color: #808080">
                    <asp:DropDownList ID="listClienteObra" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="21px" OnSelectedIndexChanged="listClienteObra_SelectedIndexChanged" Visible="true" Width="369px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        &nbsp;&nbsp;&nbsp;<br /><asp:GridView ID="GridViewReporte" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                onrowcommand="GridViewReporte_RowCommand" 
                onpageindexchanging="GridViewReporte_PageIndexChanging" PageSize="100000" 
                HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
                Width="531px"  Font-Size="Small" >
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                <Columns>   
                    <asp:BoundField DataField="Codigo" HeaderText=""  >           
                        <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="Material" HeaderText="Material" ItemStyle-HorizontalAlign="Left"/>
                    <asp:ButtonField ButtonType="Image" CommandName="Perdida" HeaderText="Reposición" 
                        ImageUrl="~/imagenes/001_49.png" Text="Detalle" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        <br />        
        &nbsp;
        <table ID="table_listado" runat="server">
            <tr>
                <td class="auto-style87">
                </td>
                <td> 
                    <asp:Label ID="Mensaje2" runat="server" Font-Bold="True" ForeColor="#CC0000" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtPerdida" runat="server" MaxLength="50" Style="text-transform:uppercase" Width="40px" Visible="false"></asp:TextBox>
                    <asp:ImageButton ID="btnGuardarInventarioReposicion" runat="server" AutoPostBack="True" Height="24px" Visible="false" ImageUrl="~/imagenes/001_49.png" onclick="btnGuardarInventarioReposicion_Click" ToolTip="Guardar Pérdida" Width="24px" />
                </td> 
                <td>
                </td>
            </tr>
        </table>
         
    <asp:HiddenField ID="HiddenField1" runat="server" />  
    <asp:Panel ID="PnlConfirmar" runat="server" CssClass="modalPopup" style="display:none;width:400px" >
        <div class="header">
            <asp:Label ID="lb_tituloConfirmar" runat="server" Text="Eliminar Registro"></asp:Label>
        </div>
        <div class="body">
            <table style="width: 400px">                    
                <tr>
                    <td> ¿Está seguro de registrar pérdida de inventario? <br />
                        <asp:ImageButton ID="btnAceptarModal" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_06.png" onclick="btnAceptar_Click" ToolTip="Aceptar" Width="24px" Visible="True" />&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="btnCancelarModal" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_05.png" onclick="btnCancelar_Click" ToolTip="Cancelar" Width="24px" Visible="True" /><br />
                        <asp:Label ID="MensajeModal" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label><br />
                        <asp:ImageButton ID="btnSalirModal" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_07.png" onclick="btnSalir_Click" ToolTip="Salir" Width="24px" Visible="False" />
                    </td>
                </tr>
            </table>  
        </div>
        <br />
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupConfirmar" runat="server" BackgroundCssClass="modalBackground"
        Enabled="False" PopupControlID="PnlConfirmar" RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="HiddenField1">
    </asp:ModalPopupExtender>

    </ContentTemplate>
    </asp:UpdatePanel>

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

