<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="detallesoporte.aspx.cs" Inherits="HelpDesk.detallesoporte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register TagPrefix="tecf" TagName="Tecfinsa" Src="~/Controls/EscalarSoporte.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="scripts/EventosComunes.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="divDetalle" >

    
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
                        <asp:ImageButton ID="Guardar" runat="server"
                        onmouseout="this.src='Toolbar/ButtonBarSave.gif'"
                        onmouseover="this.src='Toolbar/ButtonBarSaveOver.gif'"
                        ToolTip="Guardar seguimiento" ImageUrl="~/Toolbar/ButtonBarSave.gif" Height="30px" 
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
                    
                    <td class="style28">
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
                        &nbsp;<br />
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
    </script> --%> 
        
        <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        <br />        
        
        <asp:Label ID="Titulo" runat="server" Font-Bold="True" ForeColor="#6699FF">Detalle de soporte</asp:Label>
        
        <br />
         &nbsp;<table style="width: 93%; margin-left: 0px;">
                    <tr>
                        <td class="style20">
                            Número de
                            <asp:Label ID="Clase" runat="server"></asp:Label>
                            :
                            <asp:Label ID="Soporte" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;</td>
                        <td class="style23">
                            Estado:
                            <asp:Label ID="Cod_Estado" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="Estado" runat="server"></asp:Label>
                            &nbsp;&nbsp;
                            <br />
                            Usuario Asignado:
                            <asp:Label ID="Escalado" runat="server"></asp:Label>
                        </td>
                        <td class="style29">
                            &nbsp;<asp:ImageButton ID="BtCerrar" runat="server" Height="23px" 
                                ImageUrl="~/Imagenes/001_07.png" OnClick="BtCerrar_Click" 
                                ToolTip="Cerrar el caso" Width="24px" />
                        
                            <asp:Label ID="LbCerrar" runat="server" Font-Bold="True" ForeColor="#6699FF">Cerrar</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style20">
                            </td>
                        <td>
                            </td>
                        <td class="style23">
                            </td>
                        <td class="style29">
                            &nbsp;&nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style24">
                            Usuario solicitante:
                            <asp:Label ID="Usuario" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Email" runat="server" Text=""></asp:Label>
                            &nbsp;</td>
                        <td class="style25">
                            </td>
                        <td class="style26">
                            Fecha de solicitud:
                            <asp:Label ID="Fecha" runat="server"></asp:Label>
                        </td>
                        <td class="style30">
                            </td>
                    </tr>
                    <tr>
                        <td class="style20">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="style23">
                            &nbsp;</td>
                        <td class="style29">
                            <asp:ImageButton ID="BtDevolver" runat="server" Height="23px" 
                                ImageUrl="~/Imagenes/001_05.png" OnClick="BtDevolver_Click" 
                                ToolTip="Devolver el caso" Width="24px" />
                        
                            <asp:Label ID="LbDevolver" runat="server" Font-Bold="True" ForeColor="#CC0000">Devolver</asp:Label>
                        </td>
                    </tr>
        
                    <tr>
                        <td class="style20">
                            Tipo de soporte:
                            <asp:Label ID="TipoSoporte" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="style23">Anexo:
                            &nbsp;<asp:Label ID="LbAnexo" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="LbMsjAnexo" runat="server" Enabled="False"></asp:Label>
                            &nbsp;
                            <asp:ImageButton ID="BtDescarga" runat="server" Height="23px" 
                                ImageUrl="~/Imagenes/001_52.png" OnClick="BtDescarga_Click"  ToolTip="Anexo" 
                                Width="24px" Visible="True" />
                            &nbsp;</td>
                        <td class="style29">
                            </td>
                    </tr>
                    <tr>
                        <td class="style20">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="SubTipoSoporte" runat="server"></asp:Label>
                        </td>
                        <td></td>
                        <td class="style23"></td>
                        <td class="style29">
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                            Detalle:</tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="Detalle" runat="server" Height="100px" ReadOnly="True" 
                                TextMode="MultiLine" Width="1010px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
        <br />
        
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            CellPadding="4"   DataKeyNames="Codigo"
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onpageindexchanging="GridView1_PageIndexChanging" PageSize="12" 
            onrowcommand="GridView1_RowCommand" AutoGenerateColumns="False" 
            HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
            onrowdatabound="GridView1_RowDataBound" Width="990px" Font-Names="Arial"  Font-Size="Small" >
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField HeaderText="Ref." DataField="Codigo" Visible="false" />
                <asp:BoundField HeaderText="Observación" DataField="Observacion"/>
                <asp:BoundField HeaderText="Fecha" DataField="Fecha" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
        <asp:Label ID="Mensaje2" runat="server" Font-Bold="True" 
        ForeColor="#CC0000"></asp:Label>
        <table id="table_seg" runat="server">
            <tr>
                    <td class="style13">
                    </td>
                    <td>
                    Seguimiento:</td>
            </tr>

 
            <tr>
                <td class="style13">
                </td>
                <td>
                    <asp:TextBox ID="Seguimiento" runat="server" Height="100px" 
                        TextMode="MultiLine" Width="511px"></asp:TextBox>
                </td>
                
                <td class="style22" 
                    style="font-size: medium; color: #0000FF; font-style: oblique" 
                    align="center">
                    &nbsp;<asp:Label ID="LbEscalar" runat="server" Font-Bold="True" ForeColor="#6699FF">Escalar</asp:Label>
        
        &nbsp;<br />
                    <asp:ImageButton ID="BtEscalar" runat="server" Height="24px" 
                        ImageUrl="~/imagenes/001_60.png" onclick="Escalar_Click" ToolTip="Escalar" 
                        Width="24px" />
                </td>
            </tr>


            <tr>
                <td class="style17" rowspan="2">
                    &nbsp;
                </td>
                <td rowspan="2">
                    &nbsp;</td>
                
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
         <br />
        
        <br />
        
        <asp:UpdatePanel ID="upCargando" runat="server" UpdateMode="Conditional" >
       <ContentTemplate>
            <div id="divCargando" runat="server"              
                style="display:none; position:absolute; top: 180px; left: 280px; width: 162px;">
                <h5 style="width: 300px; color: #FF0000">Por favor espere la descarga del archivo...</h5>      
            </div>
       </ContentTemplate>
       </asp:UpdatePanel>
     </div>  
        
</asp:Content>
        
