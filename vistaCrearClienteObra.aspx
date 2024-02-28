                            <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaCrearClienteObra.aspx.cs" Inherits="HelpDesk.vistaCrearClienteObra" %>


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
        .auto-style61 {
            width: 184px;
            height: 8px;
        }
        .auto-style62 {
            width: 184px;
            height: 18px;
        }
        .auto-style76 {
            width: 198px;
            height: 18px;
        }
        .auto-style77 {
            width: 198px;
            height: 8px;
        }
        .auto-style80 {
            width: 379px;
            height: 18px;
        }
        .auto-style81 {
            width: 379px;
            height: 8px;
        }
        .auto-style82 {
            width: 179px;
            height: 18px;
        }
        .auto-style83 {
            width: 179px;
            height: 40px;
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
            width: 533px;
            height: 17px;
        }
        .auto-style87 {
            width: 184px;
            height: 17px;
        }
        .auto-style88 {
            width: 349px;
            height: 17px;
        }
        .auto-style89 {
            width: 487px;
            height: 8px;
        }
        .auto-style90 {
            width: 487px;
            height: 18px;
        }
        .auto-style93 {
            width: 525px;
            height: 18px;
        }
        .auto-style94 {
            width: 525px;
            height: 8px;
        }
        .auto-style95 {
            width: 224px;
            height: 18px;
        }
        .auto-style96 {
            width: 224px;
            height: 40px;
        }
        .auto-style97 {
            width: 566px;
            height: 18px;
        }
        .auto-style98 {
            width: 566px;
            height: 8px;
        }
        .auto-style99 {
            width: 533px;
            height: 18px;
        }
        .auto-style100 {
            width: 533px;
            height: 8px;
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
            
            <asp:Label ID="lbNuevoCliente" runat="server" Font-Bold="False" ForeColor="#FF6600" Visible="False">Nuevo Cliente</asp:Label>
            &nbsp;<asp:ImageButton ID="btnNuevoCliente" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_45.png" onclick="Refrescar_Click" ToolTip="Nuevo Cliente" Width="24px" Visible = "false"/>
            <br />
        
            <br />
        
        <asp:Label ID="LabelCliente" runat="server" Font-Bold="True" ForeColor="#6699FF">Cliente</asp:Label>
            <br />
        <br />
        <table ID="table_cliente" runat="server">
            <tr>
                <td class="auto-style84"> 
                </td>
                <td class="auto-style85" style="font-style: italic; color: #808080;">
                    Buscar Cliente
                </td>
                <td class="auto-style86" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtBuscarCliente" runat="server" MaxLength="50" Style="text-transform:uppercase" Width="357px"></asp:TextBox>
                    <asp:DropDownList ID="listCliente" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="listCliente_SelectedIndexChanged" AutoPostBack="True" Height="17px" Width="369px" Visible = "false">
                    </asp:DropDownList>
                    &nbsp; <asp:ImageButton ID="btnBuscarCliente" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Search.png" onclick="btnBuscarCliente_Click" ToolTip="Buscar Cliente" Width="24px" />
                </td>
                <td class="auto-style87">
                </td>
                <td class="auto-style88" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
            <tr>
                <td class="auto-style24">
                </td>
                <td class="auto-style82">
                    </td>
                <td class="auto-style99" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                    <br />
                </td>
                <td class="auto-style62">
                </td>
                <td class="auto-style28" 
                    style="font-style: italic; font-size: small; color: #808080">
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style83">
                    Identificación
                </td>
                <td class="auto-style100" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="18" Width="144px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtIdentificacion" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                        <asp:DropDownList ID="listTipoIdentificacion" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="ListTipoIdentificacion_SelectedIndexChanged" Width="52px" DataValueField="CC">
                            <asp:ListItem>CC</asp:ListItem>
                            <asp:ListItem>NIT</asp:ListItem> 
                        </asp:DropDownList>
                    &nbsp;
                    <br />
                </td>
                <td class="auto-style61">
                </td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style83">
                    Nombre<br /> </td>
                <td class="auto-style100" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="362px" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style61">
                    Teléfono</td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="50" Width="133px" Style="text-transform:uppercase"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtTelefono" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style83">
                    Dirección Oficina</td>
                <td class="auto-style100" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtDireccionOficina" runat="server" MaxLength="50" Width="364px" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style61">
                    Correo</td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="209px" Style="text-transform:lowercase"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                            ControlToValidate="txtCorreo" ErrorMessage="Formato incorrecto" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style24">
                </td>
                <td class="auto-style82">
                    </td> 
                <td class="auto-style99" 
                    style="font-style: normal; font-size: medium; color: #0000CC">
                    &nbsp;<asp:Label ID="lbCrearCliente" runat="server" Font-Bold="False" ForeColor="#0000CC" Visible="True">Crear Cliente</asp:Label>
                &nbsp;<asp:ImageButton ID="btnCrearCliente" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Guardar.png" onclick="btnCrearCliente_Click" ToolTip="Crear Cliente" Width="24px" Visible = "true" />
                    <br /> 
                </td> 
                <td class="auto-style62">
                </td>
                <td class="auto-style28" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
        </table>
        
            <br />
        
        <asp:Label ID="LabelObra" runat="server" Font-Bold="True" ForeColor="#6699FF">Obra</asp:Label>
            <br />
        <br />
        <table ID="table_obra" runat="server" >
            <tr>
                <td class="auto-style24">
                </td>
                <td class="auto-style95" style="font-style: italic; color: #808080;">
                    Buscar Obra</td>
                <td class="auto-style97" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtBuscarObra" runat="server" MaxLength="50" Style="text-transform:uppercase" Width="341px"></asp:TextBox>
                    <asp:ImageButton ID="btnBuscarObra" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Search.png" onclick="btnBuscarObra_Click" ToolTip="Buscar Obra" Width="24px" />
                    <br />
                    <asp:DropDownList ID="listClienteObra" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="listClienteObra_SelectedIndexChanged" Visible="false" Width="354px">
                    </asp:DropDownList>
                    &nbsp;<br />
                </td>
                <td class="auto-style76">
                </td>
                <td class="auto-style80" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style96">
                    Obra</td>
                <td class="auto-style98" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtObra" runat="server" MaxLength="50" Width="362px" Style="text-transform:uppercase"></asp:TextBox>
                    <br />
                </td>
                <td class="auto-style77">
                    &nbsp;</td>
                <td class="auto-style81" 
                    style="font-style: italic; font-size: small; color: #808080">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style96">
                    Dirección Obra</td>
                <td class="auto-style98" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtDireccionObra" runat="server" MaxLength="50" Width="364px" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style77">
                    Teléfono Obra</td>
                <td class="auto-style81" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtTelefonoObra" runat="server" MaxLength="50" Width="209px" Style="text-transform:uppercase"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtTelefonoObra" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style96">
                    Contacto</td>
                <td class="auto-style98" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtContacto" runat="server" MaxLength="50" Width="364px" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style77">
                    Teléfono Contacto</td>
                <td class="auto-style81" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtTelefonoContacto" runat="server" MaxLength="50" Width="209px" Style="text-transform:uppercase"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtTelefonoContacto" ErrorMessage="Digite un valor numérico"                     
                        ValidationExpression="^[0-9 \s áéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style24">
                </td>
                <td class="auto-style95">
                    </td>
                <td class="auto-style97" 
                    style="font-style: normal; font-size: medium; color: #0000CC">
                    &nbsp;Crear Obra
                    <asp:ImageButton ID="btnCrearObra" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Guardar.png" onclick="btnCrearObra_Click" ToolTip="Crear Obra" Width="24px" />
                    <br />
                </td>
                <td class="auto-style76">
                </td>
                <td class="auto-style80" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
        </table>
        
            <asp:GridView ID="GridViewObra" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                    onrowcommand="GridViewObra_RowCommand" 
                    onpageindexchanging="GridViewObra_PageIndexChanging" PageSize="10000" 
                    HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
                    Width="949px"  Font-Size="Small" EnableModelValidation="True" >
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                    <Columns>   
                        <asp:BoundField DataField="Codigo" HeaderText=""  >           
                            <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="Obra" HeaderText="Obra" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="TelObra" HeaderText="Tel. Obra" />
                        <asp:BoundField DataField="Contacto" HeaderText="Contacto" />
                        <asp:BoundField DataField="TelContacto" HeaderText="Tel. Contacto" />
                        <asp:ButtonField ButtonType="Image" CommandName="Remover" HeaderText="Remover"
                            ImageUrl="~/imagenes/001_05.png" Text="Detalle" />
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

