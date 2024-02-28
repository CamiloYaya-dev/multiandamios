<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="vistaRecordatorio.aspx.cs" Inherits="HelpDesk.vistaRecordatorio" %>


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
                
        .auto-style88 {
            width: 150px;
        }
        .auto-style89 {
            width: 154px;
        }
        .auto-style90 {
            width: 250px;
        }
                
        .auto-style91 {
            width: 249px;
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
        
        <asp:UpdatePanel ID="upPagina" runat = "server" UpdateMode ="Conditional" >
        <ContentTemplate>
        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnAceptar" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_06.png" onclick="btnAceptar_Click" ToolTip="Aceptar" Width="24px" Visible="False" />
            &nbsp;&nbsp;<asp:ImageButton ID="btnCancelar" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/001_05.png" onclick="btnCancelar_Click" ToolTip="Cancelar" Width="24px" Visible="False" />
        
            <br />
        
        <asp:Label ID="LabelRecordatorio" runat="server" Font-Bold="True" ForeColor="#6699FF">Recordatorio</asp:Label>
            <br />
        <br />
        <table ID="table_recordatorio" runat="server">
            <tr>
                <td class="auto-style84">
                </td>
                <td class="auto-style91" style="font-style: italic; color: #808080;">
                    Buscar Recordatorio
                </td>
                <td class="auto-style90" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <asp:TextBox ID="txtBuscarRecordatorio" runat="server" MaxLength="100" Style="text-transform:uppercase" Width="121px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnBuscarRecordatorio" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Search.png" onclick="btnBuscarRecordatorio_Click" ToolTip="Buscar Recordatorio" Width="24px" />
                </td>
                <td class="auto-style87">
                    &nbsp;</td>
                <td class="auto-style88">                    
                </td>
                <td class="auto-style87">
                </td>
                <td class="auto-style88">                    
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style91">
                    Fecha
                </td>
                <td class="auto-style90" 
                    style="font-style: italic; font-size: small; color: #808080">
                    <%--                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtDescripcion" ErrorMessage="Digite el material de inventario"                     
                        ValidationExpression="^[0-9A-Za-z \sáéíóúÁÉÍÓÚñÑ:;'&quot;,.&amp;?!() ]{0,200}$" 
                        Display="Dynamic">
                     </asp:RegularExpressionValidator>--%>
                    <asp:TextBox ID="txtFechaRegistro" runat="server" MaxLength="50" Style="text-transform:uppercase" Width="121px"></asp:TextBox>
                    <asp:MaskedEditExtender ID="txtFechaRegistro_MaskedEditExtender" runat="server" Mask="99/LLL/9999" MaskType="None" TargetControlID="txtFechaRegistro">
                    </asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="txtFechaRegistro_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="txtFechaRegistro">
                    </asp:CalendarExtender>
                </td>
                <td class="auto-style88">
                    Título</td>
                <td class="auto-style87" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                    <asp:TextBox ID="txtTitulo" runat="server" MaxLength="100" Style="text-transform:uppercase" Width="100px"></asp:TextBox>
                </td>
                <td class="auto-style84">
                    Descripción</td>
                <td class="auto-style10" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="500" Style="text-transform:uppercase" Width="364px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style24">   
                </td>
                <td class="auto-style91">
                    </td> 
                <td class="auto-style90" 
                    style="font-style: normal; font-size: medium; color: #0000CC">
                    &nbsp;<asp:Label ID="lbGuardarRecordatorio" runat="server" Font-Bold="False" ForeColor="#0000CC">Guardar Recordatorio</asp:Label>
                    &nbsp;<asp:ImageButton ID="btnGuardarRecordatorio" runat="server" AutoPostBack="False" Height="24px" ImageUrl="~/imagenes/Guardar.png" onclick="btnGuardarRecordatorio_Click" ToolTip="Guardar Recordatorio" Width="24px" />
                    <br /> 
                </td> 
                <td class="auto-style89">
                </td>
                <td class="auto-style83">
                </td>
                <td class="auto-style83">
                </td>
                <td class="auto-style28" 
                    style="font-style: italic; font-size: small; color: #808080">                    
                </td>
            </tr>
        </table>  
            <br />
        <asp:GridView ID="GridViewBuscarRecordatorio" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewBuscarRecordatorio_RowCommand"
            onpageindexchanging="GridViewBuscarRecordatorio_PageIndexChanging" PageSize="100000"               
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="1018px"  Font-Size="Small" EnableModelValidation="True" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaRecordatorio" HeaderText="Fecha Recordatorio"  /> 
                <asp:BoundField DataField="Titulo" HeaderText="Título" />    
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />                                   
                <asp:ButtonField ButtonType="Image" CommandName="Editar" HeaderText="Editar"
                    ImageUrl="~/imagenes/001_45.png" Text="Editar" />
                <asp:ButtonField ButtonType="Image" CommandName="Remover" HeaderText="Remover"
                    ImageUrl="~/imagenes/001_05.png" Text="Remover" />                    
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#66CCFF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
            <br />
            <br />
        <asp:GridView ID="GridViewRecordatorio" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
            onrowcommand="GridViewRecordatorio_RowCommand"
            onpageindexchanging="GridViewRecordatorio_PageIndexChanging" PageSize="100000"               
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            Width="1018px"  Font-Size="Small" EnableModelValidation="True" >
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <Columns>   
                <asp:BoundField DataField="Codigo" HeaderText=""  >           
                    <ItemStyle BackColor="#507CD1" ForeColor="#507CD1" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaRecordatorio" HeaderText="Fecha Recordatorio"  /> 
                <asp:BoundField DataField="Titulo" HeaderText="Título" />    
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />                                   
                <asp:ButtonField ButtonType="Image" CommandName="Editar" HeaderText="Editar"
                    ImageUrl="~/imagenes/001_45.png" Text="Editar" />
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

