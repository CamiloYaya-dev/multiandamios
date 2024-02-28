<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaRemision.aspx.cs" Inherits="HelpDesk.vistaRemision" %>


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
        .auto-style24 {
            width: 20px;
            height: 18px;
        }
        .auto-style28 {
            width: 349px;
            height: 18px;
        }
        .auto-style88 {
            width: 453px;
            height: 7px;
        }
        .auto-style92 {
            width: 401px;
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
        .style3 {
            width: 896px;
        }
        .auto-style98 {
            width: 314px;
            height: 27px;
        }
        .auto-style99 {
            width: 314px;
        }
        .auto-style102 {
            width: 159px;
        }
        .auto-style103 {
            width: 159px;
            height: 7px;
        }
        .auto-style104 {
            width: 190px;
            height: 27px;
        }
        .auto-style105 {
            width: 193px;
        }
        
        .auto-style106 {
            width: 451px;
            height: 27px;
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
        
            <br />
        
            <asp:Label ID="lbNuevaRemision" runat="server" Font-Bold="False" ForeColor="#FF6600" Visible="False">Nueva Remisión</asp:Label>
            <asp:ImageButton ID="btnNuevaRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_45.png" onclick="Refrescar_Click" ToolTip="Nuevo Cliente" Visible="false" Width="24px" />
            <br />
        
        <br />
        
        <asp:Label ID="LabelRemision" runat="server" Font-Bold="True" ForeColor="#6699FF">Remisión</asp:Label>
        <br />
        <table ID="table_remision" runat="server">
            <tr>
                <td class="auto-style24">
                </td>
                <td class="auto-style102">
                    </td>
                <td class="auto-style106" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <br />
                </td>
                <td class="auto-style104">
                </td>
                <td class="auto-style98" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
                <td class="auto-style105" align="center">
                    <asp:Label ID="LabelAnulado" runat="server" Font-Bold="True" ForeColor="#CC0000" Visible="false">Anulado</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style103">
                    No. Remisión
                </td>
                <td class="auto-style106" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:DropDownList ID="listTipoRemision" runat="server" AppendDataBoundItems="false" AutoPostBack="False" Height="25px" OnSelectedIndexChanged="ListTipoRemision_SelectedIndexChanged" Width="95px" DataValueField="CC">
                        <asp:ListItem value ="1">ENTREGA</asp:ListItem>
                        <asp:ListItem value ="2">RETIRO</asp:ListItem> 
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox ID="txtRemision" runat="server" MaxLength="18" Width="144px" Style="text-transform:uppercase"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtRemision" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                        &nbsp;&nbsp;<asp:ImageButton ID="btnBuscarRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Search.png" onclick="btnBuscarRemision_Click" ToolTip="Buscar Remisión" Width="24px" />
                </td>
                <td class="auto-style104" align="center">
                    Fecha Registro</td>
                <td class="auto-style99" 
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
                <td class="auto-style105">
                    <asp:Label ID="LabelAnular" runat="server" ForeColor="#CC0000" Visible="false">Anular Remisión</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style103">
                    Cliente - Obra</td>
                <td class="auto-style106" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:DropDownList ID="listClienteObra" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="22px" Width="420px" OnSelectedIndexChanged="listClienteObra_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="auto-style104">
                    &nbsp;</td>
                <td class="auto-style99" 
                    style="font-style: italic; font-size: small; color: #808080">
                    &nbsp;</td>
                <td class="auto-style105" align="center">
                    <asp:ImageButton ID="btnAnular" runat="server" Height="24px" ImageUrl="~/imagenes/delete.png" onclick="btnAnular_Click" ToolTip="Anular Remisión" Width="24px" Visible="false"/>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style103">
                    Material</td>
                <td class="auto-style106"
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:DropDownList ID="listMaterial" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="listMaterial_SelectedIndexChanged" AutoPostBack="True" Height="22px" Width="420px">
                    </asp:DropDownList>
                    </td>
                <td class="auto-style104" align="center">
                    Cantidad</td>
                <td class="auto-style99" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtCantidad" runat="server" OnTextChanged="txtCantidad_TextChanged" AutoPostBack="True" MaxLength="50" Width="47px" Style="text-transform:uppercase"></asp:TextBox> 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtCantidad" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="auto-style105">
                </td>
            </tr>
        </table>
        
        <br />
        
        <asp:GridView ID="GridViewRemisionInventario" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewRemisionInventario_RowCommand" 
            onpageindexchanging="GridViewRemisionInventario_PageIndexChanging" PageSize="10000" 
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="781px"  Font-Size="Small" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>
                <asp:BoundField DataField="NumReg" HeaderText="Ítem" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Material" HeaderText="Material" />
                <asp:ButtonField ButtonType="Image" CommandName="Remover" HeaderText="Remover" 
                    ImageUrl="~/imagenes/001_05.png" Text="Remover" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 

        <asp:GridView ID="GridViewRemisionRetiro" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewRemisionRetiro_RowCommand" 
            onpageindexchanging="GridViewRemisionRetiro_PageIndexChanging" PageSize="10000" 
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="781px"  Font-Size="Small" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>
                <asp:BoundField DataField="NumReg" HeaderText="Ítem" />
                <asp:BoundField DataField="Retiro" HeaderText="Retiro" />
                <asp:BoundField DataField="Pendiente" HeaderText="Pendiente" />
                <asp:BoundField DataField="Material" HeaderText="Material" />
                <asp:ButtonField ButtonType="Image" CommandName="Remover" HeaderText="Remover" 
                    ImageUrl="~/imagenes/001_05.png" Text="Remover" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        
        <table id="table_remision1" runat="server">
            <tr>
                <td class="auto-style24"></td>
                <td class="auto-style86"></td>
                <td class="auto-style88" style="font-style: normal; font-size: medium; color: #0000CC">&nbsp;<br /> 
                    <asp:Label ID="lbGuardarRemision" runat="server" Font-Bold="False" ForeColor="#0000CC" Visible="False">Guardar Remisión</asp:Label>
                    <asp:Label ID="lbActualizarRemision" runat="server" Font-Bold="False" ForeColor="#0000CC" Visible="False">Actualizar Remisión</asp:Label>
                    &nbsp;<asp:ImageButton ID="btnGuardarRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Guardar.png" onclick="btnGuardarRemision_Click" ToolTip="Guardar Remisión" Width="24px" Visible="False" />
                    <asp:ImageButton ID="btnActualizarRemision" runat="server" AutoPostBack="True" Height="24px" ImageUrl="~/imagenes/Guardar.png" onclick="btnActualizarRemision_Click" ToolTip="Actualizar Remisión" Width="24px" Visible="False" />
                </td>
                <td class="auto-style92"></td>
                <td class="auto-style28" style="font-style: normal; font-size: medium; color: #0000CC">&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <asp:Label ID="LabelReporte" runat="server" Font-Bold="True" ForeColor="#6699FF" Visible="false">Reporte al día</asp:Label>
        <br />
        <asp:GridView ID="GridViewReporte" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewReporte_RowCommand" 
            onpageindexchanging="GridViewReporte_PageIndexChanging" PageSize="100000" 
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="531px"  Font-Size="Small" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="" >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Material" HeaderText="Material" ItemStyle-HorizontalAlign="Left"/>
                <asp:TemplateField HeaderText="A Remisión">
                    <ItemTemplate>
                        <asp:TextBox ID="txtColCantRetiro" runat="server" MaxLength="30" Width="50px" Text=''></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtColCantRetiro" ErrorMessage="Digite número"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>

        <table id="table_reporte" runat="server">
            <tr>
                <td class="auto-style24"></td>
                <td class="auto-style86"></td>
                <td class="auto-style88" style="font-style: normal; font-size: medium; color: #0000CC">&nbsp;<br /> 
                    </td>
                <td class="auto-style92">
                    <asp:Label ID="lbCrearRemision" runat="server" Font-Bold="False" ForeColor="#006600" Visible="False">Crear Remisión</asp:Label>
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnCrearRemision" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_01.png" onclick="btnCrearRemision_Click" ToolTip="Crear Remisión" Visible="False" Width="24px" />
                </td>
                <td class="auto-style28" style="font-style: normal; font-size: medium; color: #0000CC">&nbsp;</td>
            </tr>
        </table>

        <asp:HiddenField ID="HiddenField1" runat="server" />  
        <asp:Panel ID="PnlConfirmar" runat="server" CssClass="modalPopup" style="display:none;width:400px" >
            <div class="header">
                <asp:Label ID="lb_tituloConfirmar" runat="server" Text="Anular Remisión"></asp:Label>
            </div>
            <div class="body">
                <table style="width: 400px">                    
                    <tr>
                        <td> ¿Está seguro de anular la remisión? <br />
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

