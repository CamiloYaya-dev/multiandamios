<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="listadotecnicos.aspx.cs" Inherits="HelpDesk.listadotecnicos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register TagPrefix="tecf" TagName="Tecfinsa" Src="~/Controls/EscalarSoporte.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="scripts/EventosComunes.js" type="text/javascript"></script>
    <script src="scripts/mootools-1.2.2-core-nc.js" type="text/javascript"></script>
    <script src="scripts/e24scrollevents-1.0.js" type="text/javascript"></script>
    <style type="text/css">
        .style8
        {
            width: 22px;
        }
        .style9
        {
            width: 491px;
        }
        #table_tecnico
        {
            width: 632px;
        }
        .style11
        {
            width: 22px;
            height: 34px;
        }
        .style12
        {
            width: 65px;
            height: 34px;
        }
        .style13
        {
            width: 491px;
            height: 34px;
        }
        .style14
        {
            width: 65px;
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
    
    <asp:UpdatePanel ID="upMenu" runat="server" UpdateMode="Always" 
        onload="upMenu_Load">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Guardar" EventName="Click" />
        </Triggers>
    <ContentTemplate>
    
    <div id="Inicial" style="position:fixed; height:30px; top:0px; left:0px;">
        <table id="Menu" cellpadding="0" cellspacing="0" border="0" style="top:0px; position:fixed; z-index:800;">
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
                        <asp:ImageButton ID="Inicio" runat="server" 
                        onmouseout="this.src='Toolbar/ButtonBarHome.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarHomeOver.gif'"
                        ToolTip="Inicio" ImageUrl="~/Toolbar/ButtonBarHome.gif" Height="30px" 
                        onclick="Atras_Click" Visible="True" />
                    </td>
                    <td>
                        <asp:ImageButton ID="Nuevo" runat="server" Height="30px" 
                            ImageUrl="~/Toolbar/ButtonBarNew.gif" onclick="Nuevo_Click" 
                            onmouseout="this.src='Toolbar/ButtonBarNew.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarNewOver.gif'" 
                            ToolTip="Nuevo" Visible="False" />
                    </td>
                    <td>
                        <asp:ImageButton ID="Modificar" runat="server" 
                            ImageUrl="~/Toolbar/ButtonBarEdit.gif" 
                            onmouseout="this.src='Toolbar/ButtonBarEdit.gif'" 
                            onmouseover="this.src='Toolbar/ButtonBarEditOver.gif'"
                             onclick="Modificar_Click" ToolTip="Modificar" style="width: 30px" 
                            Height="30px" Visible="False" />
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
                        ToolTip="Guardar" onclick="Guardar_Click" ImageUrl="~/Toolbar/ButtonBarSave.gif" Height="30px" 
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
                        <asp:ImageButton ID="ExportarExcel" runat="server"
                        onmouseout="this.src='Toolbar/ButtonBarExcelExport.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarExcelExportOver.gif'"
                        ToolTip="Exportar en Excel" 
                        ImageUrl="~/Toolbar/ButtonBarExcelExport.gif" onclick="ExportarExcel_Click" 
                            Height="30px" Visible="False" />
                    </td>
                    
                   
                    <td >
                        <img src="Toolbar/ButtonBarDividerR.gif" alt="">
                    </td>
                    <td >
                        <img alt="" height="30" src="Toolbar/ButtonBarEdgeR.gif"></img></td>
                    <td width="100%">
                        &nbsp;<br />
                        </td>
                </tr>
            </tbody>
        </table>  
    </div>
        <%--<div id="MenuInicial" style="height:30px; position:absolute; top:1;"> </div>                     
                    <script language="javascript" type="text/javascript">
                        var Menu = $('Menu');
                        var Contenedor = $('Centro');
                        var MenuInicial = $('MenuInicial');
                        MenuInicial.addEvents({
                            'visible': function() {
                                Menu.style.position = 'relative';
                                Menu.style.top = 0;
                            },
                            'hidden': function() {
                                Menu.style.position = 'fixed';
                                Menu.style.top = '-1px';
                            }
                        });
                        new e24ScrollEvents({
                            container: window,
                            mode: 'vertical',
                            elements: [MenuInicial]
                        });
                    </script>  --%>
        
        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
       
       </ContentTemplate>
       </asp:UpdatePanel>
        
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Titulo" runat="server" Font-Bold="True" ForeColor="#6699FF">Listado Técnicos</asp:Label>
        
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <br />
    
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                        ForeColor="#333333" GridLines="None" Height="153px" HorizontalAlign="Center" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                        onrowcommand="GridView1_RowCommand" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="12" 
                        Width="986px" Font-Size="Small">
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Cod_Tecnico"
                                ImageUrl="~/Imagenes/001_52.png" Text="Modificar parametros" />
                            <asp:BoundField DataField="Codigo" HeaderText="Código" />
                            <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="Area" HeaderText="Área" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                            <asp:BoundField DataField="Login" HeaderText="Login" />
                            <asp:BoundField DataField="Canasta" HeaderText="Canasta" />
                            <asp:BoundField DataField="Escalamiento" HeaderText="Escalamiento" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        <br />
        
        <table ID="table_tecnico" runat="server">
            <tr>
                    <td class="style11">
                    </td>
                    <td class="style12">
                        <img alt="" height="30" src="Toolbar/ButtonBarEdgeL.gif"><asp:ImageButton 
                            ID="ImageButton1" runat="server"
                            onmouseout="this.src='Toolbar/ButtonBarSave.gif'"
                            onmouseover="this.src='Toolbar/ButtonBarSaveOver.gif'"
                            ToolTip="Guardar" ImageUrl="~/Toolbar/ButtonBarSave.gif" Height="30px" 
                            onclick="Guardar_Click" /><img alt="" height="30" src="Toolbar/ButtonBarEdgeR.gif">
                        </td>
                    <td class="style13">
                        
                        <b>
                        <asp:Label ID="Nombre_Tecnico" runat="server" Font-Bold="True" 
                            ForeColor="Black"></asp:Label>
                        <asp:Label ID="Lb_Cod_Tecnico" runat="server" Font-Bold="True" 
                            ForeColor="Black" Visible="False"></asp:Label>
                        </b>
                        
                    </td>
            </tr>
            <tr>
                    <td class="style8">
                        </td>
                    <td class="style14">
                        Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                    <td class="style9">
                        <asp:DropDownList ID="DropDownEmpresa" runat="server" Height="20px" Width="207px"
                            AppendDataBoundItems="true">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList>
                        
                        <b>
                        <asp:Label ID="Lb_Cod_Empresa" runat="server" Font-Bold="True" 
                            ForeColor="Black" Visible="False"></asp:Label>
                        </b>
                    </td>
            </tr>
            <tr>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style14">
                        Area:</td>
                    <td class="style9">
                        <asp:DropDownList ID="DropDownArea" runat="server" Height="20px" Width="207px"
                            AppendDataBoundItems="true">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList>
                        
                        <b>
                        <asp:Label ID="Lb_Cod_Area" runat="server" Font-Bold="True" 
                            ForeColor="Black" Visible="False"></asp:Label>
                        </b>
                    </td>
            </tr>
            <tr>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style9">
                        <asp:CheckBox ID="CheckCanasta" runat="server" Text="Canasta Inicial" />
                        &nbsp;</td>
            </tr>
            <tr>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style9">
                        <asp:CheckBox ID="CheckAutoescala" runat="server" 
                            Text="Escalamiento automático" />
                    </td>
            </tr>
        </table>
        <br />
        
        <table ID="table_atras" runat="server">
             <tr>
                 <td rowspan="2" style="color: #0066FF">
                     <asp:ImageButton ID="Atras" runat="server" Height="24px" 
                         ImageUrl="~/imagenes/001_61.png" onclick="Atras_Click" 
                         style="margin-bottom: 0px" ToolTip="Atras" Visible="True" Width="24px" />
                     Atrás</td>
             </tr>
         </table>
        <table style="width:100%;">
            <tr>
                <td style="text-align: Center; color: #0066CC;">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
   
       </ContentTemplate>
       </asp:UpdatePanel>
        
</asp:Content>

