<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistausuario.aspx.cs" Inherits="HelpDesk.vistausuario" %>





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
        .auto-style11 {
            width: 461px;
            height: 8px;
        }
        .auto-style17 {
            width: 54px;
            height: 8px;
        }
        .auto-style18 {
            width: 71px;
            height: 8px;
        }
        .auto-style19 {
            width: 126px;
            height: 8px;
        }
        .auto-style20 {
            width: 532px;
            height: 8px;
        }
        .auto-style21 {
            width: 173px;
            height: 8px;
        }
        .auto-style22 {
            width: 51px;
            height: 8px;
        }
        .auto-style23 {
            width: 229px;
        }
        .auto-style24 {
            width: 20px;
            height: 40px;
        }
        .auto-style26 {
            width: 461px;
            height: 40px;
        }
        .auto-style27 {
            width: 54px;
            height: 40px;
        }
        .auto-style28 {
            width: 349px;
            height: 40px;
        }
        .auto-style29 {
            width: 246px;
            height: 40px;
        }
        .auto-style31 {
            width: 631px;
            height: 40px;
        }
        .auto-style32 {
            width: 631px;
        }
        .auto-style33 {
            width: 258px;
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
                            ToolTip="Nuevo Soporte" Visible="False" />
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
                        <asp:ImageButton ID="Guardar" runat="server" Enabled="True"
                        onmouseout="this.src='Toolbar/ButtonBarSave.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarSaveOver.gif'"
                        ToolTip="Guardar" ImageUrl="~/Toolbar/ButtonBarSave.gif" Height="30px" 
                        onclick="Guardar_Click" Visible="False" />
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
        
        <asp:UpdatePanel ID="upPagina" runat = "server" UpdateMode ="Conditional" >
        <ContentTemplate>
        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        
            <br />
        
        <asp:Label ID="Titulo1" runat="server" Font-Bold="True" ForeColor="#6699FF">Factura de Venta</asp:Label>
            <br />
        <br />
        <table ID="table_factura" runat="server">
            <tr>
                <td class="auto-style24">
                </td>
                <td class="auto-style33">
                    Factura de Venta No.</td>
                <td class="auto-style26" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txt_nNumFactura" runat="server" MaxLength="10"></asp:TextBox>
                    <br />
                </td>
                <td class="auto-style27">
                </td>
                <td class="auto-style28" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style33">
                    Identificación
                </td>
                <td class="auto-style11" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txt_nIdentificacion" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:DropDownList ID="list_TipoIdentificacion" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="ListTipoIdentificacion_SelectedIndexChanged" Width="52px" DataValueField="CC">
                        <asp:ListItem>CC</asp:ListItem>
                        <asp:ListItem>NIT</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
                <td class="auto-style17">
                </td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style33">
                    Señor(es)
                </td>
                <td class="auto-style11" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="TextBox5" runat="server" MaxLength="10" Width="362px" Style="text-transform:uppercase"></asp:TextBox>
                    <br />
                </td>
                <td class="auto-style17">
                </td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style33">
                    Dirección Obra</td>
                <td class="auto-style11" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" Width="364px" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style17">
                    Teléfono</td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="TextBox4" runat="server" MaxLength="10" Width="133px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style33">
                    Dirección Oficina</td>
                <td class="auto-style11" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="10" Width="364px" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style17">
                    Teléfono</td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="10" Width="133px"></asp:TextBox>
                </td>
            </tr>

        </table>
        
        <table>
            <tr>
                <td class="auto-style23">
                </td>
                <td class="auto-style32">
                </td>                
            </tr>
            <tr>
                <td class="auto-style23">
                </td>
                <td class="auto-style31">
                    Detalle de equipos alquilados:</td>                
            </tr>
        </table>
        
        <table>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style18">
                    Cantidad</td>
                <td class="auto-style19" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtCantidad" runat="server" MaxLength="10" Width="46px"></asp:TextBox>
                </td>
                <td class="auto-style22">
                    Descripción</td>
                <td class="auto-style20" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" Width="375px" Height="38px" TextMode="MultiLine" Style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="auto-style18">
                    Valor Unitario</td>
                <td class="auto-style21" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtValUnitario" runat="server" MaxLength="10" Width="118px"></asp:TextBox>
                </td>
                <td class="auto-style18">
                    Valor Total</td>
                <td class="auto-style19" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtValTotal" runat="server" MaxLength="10" Width="118px"></asp:TextBox>
                </td>
                <td class="style29">
                    <asp:ImageButton ID="btn_Agregar" runat="server" Height="24px" AutoPostBack="False"
                        ImageUrl="~/imagenes/001_06.png" onclick="btnAgregar_Click" ToolTip="Agregar" 
                        Width="24px"/>
                </td>
            </tr>
        </table>
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
                <asp:GridView ID="GridViewObra" runat="server" AllowPaging="True" 
                    CellPadding="4"   DataKeyNames="Codigo"
                    onselectedindexchanged="GridViewObra_SelectedIndexChanged" onrowcommand="GridViewObra_RowCommand" 
                    onpageindexchanging="GridViewObra_PageIndexChanging" PageSize="12" AutoGenerateColumns="False" 
                    HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
                    Width="1018px"  Font-Size="Small" >
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                    <Columns>                
                        <asp:BoundField DataField="Codigo" HeaderText="Ref." />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="ValUnitario" HeaderText="Valor Unitario" />
                        <asp:BoundField DataField="ValTotal" HeaderText="Valor Total" />
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

