<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaCrearInventario.aspx.cs" Inherits="HelpDesk.vistaCrearInventario" %>


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
        .auto-style1 {
            width: 20px;
            height: 8px;
        }
        .auto-style10 {
            width: 349px;
            height: 8px;
        }
        .auto-style24 {
            width: 20px;
            height: 18px;
        }
        .auto-style28 {
            width: 349px;
            height: 18px;
        }
        .auto-style78 {
            width: 155px;
            height: 18px;
        }
        .auto-style79 {
            width: 155px;
            height: 40px;
        }
        .auto-style81 {
            width: 401px;
            height: 18px;
        }
        .auto-style82 {
            width: 401px;
            height: 8px;
        }
        .auto-style83 {
            width: 117px;
            height: 18px;
        }
        .auto-style84 {
            width: 117px;
            height: 8px;
        }
        .auto-style85 {
            width: 20px;
            height: 7px;
        }
        .auto-style87 {
            width: 401px;
            height: 7px;
        }
        .auto-style88 {
            width: 117px;
            height: 7px;
        }
        .auto-style89 {
            width: 349px;
            height: 7px;
        }
        .auto-style90 {
            width: 155px;
            height: 7px;
        }
        .auto-style91 {
            width: 20px;
            height: 27px;
        }
        .auto-style92 {
            width: 401px;
            height: 27px;
        }
        .auto-style93 {
            width: 117px;
            height: 27px;
        }
        .auto-style94 {
            width: 349px;
            height: 27px;
        }
        .auto-style95 {
            width: 155px;
            height: 27px;
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
        .auto-style2 {
            width: 329px;
        }
        .auto-style5 {
            width: 240px;
        }
        .auto-style9 {
            width: 62px;
        }
        .auto-style11 {
            width: 456px;
        }
        .auto-style12 {
            width: 163px;
        }
        .auto-style13 {
            width: 200px;
        }
        .style3 {
            width: 896px;
        }
        .auto-style14 {
            width: 428px;
        }
        .auto-style15 {
            width: 456px;
            height: 26px;
        }
        .auto-style16 {
            width: 240px;
            height: 26px;
        }
        .auto-style17 {
            width: 163px;
            height: 26px;
        }
        .auto-style18 {
            width: 428px;
            height: 26px;
        }
        .auto-style19 {
            width: 329px;
            height: 26px;
        }
        .auto-style20 {
            width: 221px;
        }
        .auto-style21 {
            width: 24px;
        }
        .auto-style22 {
            width: 180px;
        }
        .auto-style23 {
            width: 21px;
        }
        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div id="Inicial" style="position:fixed; height:30px; top:0; left:0;">
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
                        <asp:ImageButton ID="btnCrearInventarioTool" runat="server" 
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
                        <asp:ImageButton ID="Reporte" runat="server" 
                            ImageUrl="~/Toolbar/ButtonBarPDFExport.gif" ToolTip="Reporte"
                            onmouseout="this.src='Toolbar/ButtonBarPDFExport.gif'"
                            onmouseover="this.src='Toolbar/ButtonBarPDFExportOver.gif'" Height="30px" Visible="false"
                            onclick="Reporte_Click" />
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
        <%--<asp:ImageButton ID="btnAceptar" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_06.png" onclick="btnAceptar_Click" ToolTip="Aceptar" Width="24px" Visible="False" />
        <asp:ImageButton ID="btnCancelar" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_05.png" onclick="btnCancelar_Click" ToolTip="Cancelar" Width="24px" Visible="False" />       --%>
        <br />       
        <asp:Label ID="LabelInventario" runat="server" Font-Bold="True" ForeColor="#6699FF">Inventario</asp:Label>
        <br />
        <br />
        <table ID="table_inventario" runat="server">
            <tr>
                <td class="auto-style85">
                </td>
                <td class="auto-style90" style="color: #666666; font-style: italic">
                    Buscar material</td>
                <td class="auto-style87" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtBuscar" runat="server" MaxLength="50" Style="text-transform:uppercase" Width="194px"></asp:TextBox>
                    &nbsp;<asp:ImageButton ID="btnBuscarMaterial" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Search.png" onclick="btnBuscarMaterial_Click" ToolTip="Buscar material" Width="24px" />
                    <br />
                </td>
                <td class="auto-style88">
                </td>
                <td class="auto-style89" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
            <tr>
                <td class="auto-style85">
                </td>
                <td class="auto-style90" style="color: #666666; font-style: italic">
                    &nbsp;</td>
                <td class="auto-style87" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <br />
                </td>
                <td class="auto-style88">
                </td>
                <td class="auto-style89" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
            <tr>
                <td class="auto-style91">
                </td>
                <td class="auto-style95">
                    Cantidad</td>
                <td class="auto-style92" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtCantidad" runat="server" MaxLength="5" Width="61px" Style="text-transform:uppercase"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtCantidad" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="([0-9]|-)*"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    &nbsp;&nbsp;
                    <br />
                </td>
                <td class="auto-style93">
                    </td>
                <td class="auto-style94" 
                    style="font-style: italic; font-size: small; color: #808080">
                    </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style79">
                    Descripción</td>
                <td class="auto-style82" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" Width="364px" Style="text-transform:uppercase"></asp:TextBox>
<%--                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtDescripcion" ErrorMessage="Digite el material de inventario"                     
                        ValidationExpression="^[0-9A-Za-z \sáéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                     </asp:RegularExpressionValidator>--%>
                </td>
                <td class="auto-style84">
                    Fecha Registro</td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtFechaRegistro" runat="server" MaxLength="50" Width="121px" Style="text-transform:uppercase"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        MaskType="None"
                        Mask="99/LLL/9999"
                        TargetControlID="txtFechaRegistro">
                    </asp:MaskedEditExtender>                    
                    <asp:CalendarExtender ID="txtFechaRegistro_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="txtFechaRegistro" Format="dd/MMM/yyyy" >
                    </asp:CalendarExtender> 
                </td>
            </tr>
            <tr>
                <td class="auto-style24">   
                </td>
                <td class="auto-style78">
                    </td> 
                <td class="auto-style81" 
                    style="font-style: normal; font-size: medium; color: #0000CC">
                    &nbsp;<asp:Label ID="lbCrearInventario" runat="server" Font-Bold="False" ForeColor="#0000CC" Visible="True">Crear registro de inventario</asp:Label>
                    &nbsp;<asp:ImageButton ID="btnCrearInventario" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Guardar.png" onclick="btnCrearInventario_Click" ToolTip="Crear Inventario" Width="24px" />
                    <br /> 
                </td> 
                <td class="auto-style83">
                </td>
                <td class="auto-style28" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
        </table> 
            
            <br />
            <asp:Label ID="LabelBodega" runat="server" Font-Bold="True" ForeColor="#6699FF">Bodega</asp:Label>
            <br />
            <br />
        
       
    <asp:GridView ID="GridViewInventario" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewInventario_RowCommand" EnableEventValidation="false"
            onpageindexchanging="GridViewInventario_PageIndexChanging" PageSize="100000"               
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="1018px"  Font-Size="Small"  >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>

                <asp:BoundField DataField="CantidadEnBodega" HeaderText="En Bodega" />                          
                <asp:BoundField DataField="CantidadAlquilado" HeaderText="Alquilado" />
                <asp:ButtonField ButtonType="Image" CommandName="Detalle" HeaderText="" 
                     ImageUrl="~/imagenes/001_61.png" Text="Detalle" />
                <asp:BoundField DataField="Cargado" HeaderText="Existencia" />
                <asp:ButtonField ButtonType="Image" CommandName="DetallePerdida" HeaderText="" 
                     ImageUrl="~/imagenes/001_60.png" Text="DetallePerdida" />
                <asp:BoundField DataField="Perdida" HeaderText="Pérdida" />

                <asp:BoundField DataField="Material" HeaderText="Material" />
                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />                    
                <asp:ButtonField ButtonType="Image" CommandName="Editar" HeaderText="Editar"
                    ImageUrl="~/imagenes/001_45.png" Text="Editar" />
                <asp:ButtonField ButtonType="Image" CommandName="Remover" HeaderText="Remover" 
                    ImageUrl="~/imagenes/001_05.png" Text="Remover" />  
                <%--<asp:TemplateField HeaderText="Remover">
                    <ItemTemplate>
                        <asp:ImageButton ID="IMBtt_Remover" runat="server" CommandArgument='<%#Eval("Codigo")%>' 
                            CommandName="Remover" ImageUrl="~/imagenes/001_05.png" Height="16px" Width="16px" 
                            OnClientClick="return confirm('¿Desea eliminar el registro del inventario?');" >
                        </asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField> --%>                 
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:GridView ID="GridViewDetalleInventario" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewDetalleInventario_RowCommand"
            onpageindexchanging="GridViewDetalleInventario_PageIndexChanging" PageSize="100000"               
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="697px"  Font-Size="Small" EnableModelValidation="True" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField> 
                <asp:BoundField DataField="ClienteObra" HeaderText="Cliente - Obra" ItemStyle-HorizontalAlign="Left" />              
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
        <asp:GridView ID="GridViewDetallePerdida" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewDetallePerdida_RowCommand"
            onpageindexchanging="GridViewDetallePerdida_PageIndexChanging" PageSize="100000"               
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="697px"  Font-Size="Small" EnableModelValidation="True" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>
                <asp:BoundField DataField="Obra" HeaderText="Obra" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" /> 
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
        <asp:HiddenField ID="HiddenField1" runat="server" />  
        <asp:Panel ID="PnlConfirmar" runat="server" CssClass="modalPopup" style="display:none;width:400px" >
            <div class="header">
                <asp:Label ID="lb_tituloConfirmar" runat="server" Text="Eliminar Registro"></asp:Label>
            </div>
            <div class="body">
                <table style="width: 400px">                    
                    <tr>
                        <td> ¿Está seguro de eliminar el registro del inventario? <br />
                            <asp:ImageButton ID="btnAceptarModal" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_06.png" onclick="btnAceptar_Click" ToolTip="Aceptar" Width="24px" Visible="True" />&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="btnCancelarModal" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_05.png" onclick="btnCancelar_Click" ToolTip="Cancelar" Width="24px" Visible="True" /><br />
                            <asp:Label ID="MensajeModal" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label><br />
                            <asp:ImageButton ID="btnSalirModal" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_07.png" onclick="btnCancelar_Click" ToolTip="Salir" Width="24px" Visible="False" />
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
            
        <asp:HiddenField ID="HiddenField2" runat="server" />  
        <asp:Panel ID="PanelOK" runat="server" CssClass="modalPopup" style="display:none;width:400px" >
            <div class="header">
                <asp:Label ID="lb_tituloOK" runat="server" Text="Remisión"></asp:Label>
            </div>
            <div class="body">
                <table style="width: 400px">                    
                    <tr>
                        <td> Información guardada correctamente. <br />
                            <asp:ImageButton ID="btnSalirModalOK" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_07.png" onclick="btnSalirModalOK_Click" ToolTip="Salir" Width="24px" Visible="True" />
                        </td>
                    </tr>
                </table>  
            </div>
            <br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupOK" runat="server" BackgroundCssClass="modalBackground"
            Enabled="False" PopupControlID="PanelOK" RepositionMode="RepositionOnWindowResizeAndScroll"
            TargetControlID="HiddenField2">
        </asp:ModalPopupExtender>      
    </ContentTemplate>
    </asp:UpdatePanel> 
        <br />
        
    <table ID="table_listado" runat="server">
        <tr>
            <td>
                &nbsp;
            </td>
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
                   style="margin-bottom: 0" ToolTip="Atras" Visible="True" Width="24px" />
                   Atrás</td>
           </tr>
    </table>
        
</asp:Content>

