<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EscalarSoporte.ascx.cs" Inherits="HelpDesk.EscalarSoporte" %>

    <script src="<%= ResolveClientUrl("~/")%>scripts/EventosComunes.js" type="text/javascript"></script>
        <asp:UpdatePanel ID="upEncontrar" runat="server" ChildrenAsTriggers="true" UpdateMode="Always"><ContentTemplate>
    <div id="Fondo" runat="server" style="display:none; background-color:White;filter:alpha(opacity=70);opacity:0.7;position:fixed; top:0px; left:0px; right:0px; bottom:0px; 
                                        z-index:1000; ">                                                  
     </div>
     
    <div id="divEncontrar" runat="server" style="display:none; z-index:1001; position:absolute;box-shadow: #111 0 .15em .17em;-webkit-box-shadow: #111 0 .15em .17.em; -moz-box-shadow: #111 0 .15em .17em; " onmousedown="comienzoMovimiento(event, this.id);">
        <asp:Panel ID="PanelEncontrar" runat="server"></asp:Panel>    
            <table align="center" style="width: 576px; background-image: url('~/imagenes/login-box-backg.png');
                    background-repeat: no-repeat; position: absolute; background-attachment: fixed; 
                    top: -6px; left: 8px; height: 402px; margin-bottom: 0px; background-color: #DCE4F9;" visible="True">
                    <tr>
                        <td bgcolor="#0099CC" colspan="5" style="background-color: #3A93D2; font-size: medium;color: #000000;font-weight: bold;height: 25px;">
                            <table border="0 0 0 0" style="width:100%"><tr><td style="text-align:left; width:100%">Escalamiento</td><td style="text-align:right; width:20px">
                                <asp:ImageButton ID="Cerrar" runat="server" ImageUrl="~/imagenes/001_05.png" 
                                    onclick="Cerrar_Click" /></td></tr></table> 
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px;height:177px;">
                        </td>
                        <td style="width: 359px;height: 177px; text-align:left">
                        
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                        ForeColor="#333333" GridLines="None" Height="153px" HorizontalAlign="Center" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                        onrowcommand="GridView1_RowCommand" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="8" 
                        Width="559px" Font-Size="Small" >
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Cod_Area" 
                                ImageUrl="~/Imagenes/001_06.png" Text="Seleccione Area" />
                            <asp:BoundField DataField="Codigo" HeaderText="Ref." />
                            <asp:BoundField DataField="Descripcion" HeaderText="Área" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            
            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" au
                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" 
                        ForeColor="#333333" GridLines="None" Height="153px" HorizontalAlign="Center" 
                        onpageindexchanging="GridView2_PageIndexChanging" 
                        onrowcommand="GridView2_RowCommand" 
                        onselectedindexchanged="GridView2_SelectedIndexChanged" PageSize="12" 
                        Width="826px" Font-Size="Small" >
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Cod_Tecnico" 
                                ImageUrl="~/Imagenes/001_06.png" Text="Seleccione Técnico" />
                            <asp:BoundField DataField="Codigo" HeaderText="Cod." />
                            <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="Area" HeaderText="Área" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                            <asp:BoundField DataField="Login" HeaderText="Login" />
                            <asp:BoundField DataField="Canasta" HeaderText="Canasta" Visible="false" />
                            <asp:BoundField DataField="Escalamiento" HeaderText="Escalamiento" Visible="false" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Label ID="Mensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                        
                        
                        </td>
                        <td style="height: 177px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px;height: 8px;">
                        </td>
                        <td style="width: 359px;height: 8px;">
                        </td>
                        <td style="height: 8px;">
                            &nbsp;
                        </td>
                        <td style="height: 8px;">
                            &nbsp;
                        </td>
                        <td style="height: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px;height: 24px;">
                        </td>
                        <td style="width: 359px;height: 24px;">
                        </td>
                        <td style="height: 24px;">
                        </td>
                        <td style="height: 24px;">
                        </td>
                        <td style="height: 24px;">
                        </td>
                    </tr>
                </table>
                <br />
            

            
            
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>