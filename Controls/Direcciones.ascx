<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Direcciones.ascx.cs" Inherits="hvpagaduria_Controls_Direcciones" %>
<script src="<%= ResolveClientUrl("~/")%>hvpagaduria/scripts/EventosComunes.js" type="text/javascript"></script>
<asp:UpdatePanel ID="upDireccionCiudad" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
<div id="FondoDireccion" runat="server" style="display:none; background-color:White;filter:alpha(opacity=70);opacity:0.7;position:fixed; 
                                        z-index:1000;	top:0px;left:0px; right:0px; bottom:0px">                                                  
     </div>

<div id="divDireccionCiudad" runat="server" style="display:none; background-color:#DCE4F9; z-index:1001; position:absolute; box-shadow: #111 0 .15em .17em;-webkit-box-shadow: #111 0 .15em .17.em; -moz-box-shadow: #111 0 .15em .17em; " onmousedown="comienzoMovimiento(event, this.id);">
    <style type="text/css">
  
        .style1
        {
            width: 20px;
            height: 30px;
        }
        .style4
        {
            width: 177px;
            height: 30px;
        }
        .style15
        {
            font-size: xx-small;
            color: #003366;
        }
               

        .style69
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #003366;
            font-size: xx-small;
        }

        .style70
        {
            font-size: medium;
            color: #003366;
            font-weight: bold;
        }
        </style>


          <table ID="tabla7" align="center" cellpadding="1" cellspacing="2"  runat="server"
              style="width: 80%;" visible="True">
              <tr>
                  <td bgcolor="#0099CC" class="style70" colspan="5" 
                      style="text-align: center; background-color: #3A93D2">
                      REGISTRO DE DIRECCIÓN</td>
              </tr>
              <tr>
                  <td class="style15">
                      &nbsp;
                  </td>
                  <td class="style69">
                      Tipo de Via:
                  </td>
                  <td class="style15">
                      <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                          Font-Names="Arial" Font-Size="XX-Small" 
                          OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="175px">
                          <asp:ListItem>Seleccione</asp:ListItem>
                          <asp:ListItem Value="AU">Autopista</asp:ListItem>
                          <asp:ListItem Value="AV">Avenida</asp:ListItem>
                          <asp:ListItem Value="AC">Avenida Calle</asp:ListItem>
                          <asp:ListItem Value="AK">Avenida Carrera</asp:ListItem>
                          <asp:ListItem Value="BL">Bulevar</asp:ListItem>
                          <asp:ListItem Value="CL">Calle</asp:ListItem>
                          <asp:ListItem Value="CN">Camino</asp:ListItem>
                          <asp:ListItem Value="KR">Carrera</asp:ListItem>
                          <asp:ListItem Value="CT">Carretera</asp:ListItem>
                          <asp:ListItem Value="CAS">Caserio</asp:ListItem>
                          <asp:ListItem Value="CQ">Circular</asp:ListItem>
                          <asp:ListItem Value="CV">Circunvalar</asp:ListItem>
                          <asp:ListItem Value="CO">Corregimiento</asp:ListItem>
                          <asp:ListItem Value="DG">Diagonal</asp:ListItem>
                          <asp:ListItem Value="CC">Cuentas Corridas</asp:ListItem>
                          <asp:ListItem Value="TC">Troncal</asp:ListItem>
                          <asp:ListItem Value="VT">Variante</asp:ListItem>
                          <asp:ListItem Value="PJ">Pasaje</asp:ListItem>
                          <asp:ListItem Value="PT">Poste</asp:ListItem>
                          <asp:ListItem Value="TV">Transversal</asp:ListItem>
                          <asp:ListItem Value="VDA">Vereda</asp:ListItem>
                          <asp:ListItem Value="VI">Vía</asp:ListItem>
                      </asp:DropDownList>
                  </td>
                  <td class="style15">
                      Letra:
                  </td>
                  <td class="style15">
                      <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" 
                          Font-Names="Arial" Font-Size="XX-Small" 
                          OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Width="175px">
                          <asp:ListItem>Seleccione</asp:ListItem>
                          <asp:ListItem>A</asp:ListItem>
                          <asp:ListItem>B</asp:ListItem>
                          <asp:ListItem>C</asp:ListItem>
                          <asp:ListItem>D</asp:ListItem>
                          <asp:ListItem>E</asp:ListItem>
                          <asp:ListItem>F</asp:ListItem>
                          <asp:ListItem>G</asp:ListItem>
                          <asp:ListItem>H</asp:ListItem>
                          <asp:ListItem>I</asp:ListItem>
                          <asp:ListItem>J</asp:ListItem>
                          <asp:ListItem>K</asp:ListItem>
                          <asp:ListItem>L</asp:ListItem>
                          <asp:ListItem>M</asp:ListItem>
                          <asp:ListItem>N</asp:ListItem>
                          <asp:ListItem>Ñ</asp:ListItem>
                          <asp:ListItem>O</asp:ListItem>
                          <asp:ListItem>P</asp:ListItem>
                          <asp:ListItem>Q</asp:ListItem>
                          <asp:ListItem>R</asp:ListItem>
                          <asp:ListItem>S</asp:ListItem>
                          <asp:ListItem>T</asp:ListItem>
                          <asp:ListItem>U</asp:ListItem>
                          <asp:ListItem>V</asp:ListItem>
                          <asp:ListItem>X</asp:ListItem>
                          <asp:ListItem>W</asp:ListItem>
                          <asp:ListItem>Y</asp:ListItem>
                          <asp:ListItem>Z</asp:ListItem>
                      </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                  <td class="style15">
                      &nbsp;
                  </td>
                  <td class="style15">
                      <span class="style15">Sufijo</span>:
                  </td>
                  <td class="style15">
                      <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" 
                          Font-Names="Arial" Font-Size="XX-Small" 
                          OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged" Width="175px">
                          <asp:ListItem>Seleccione</asp:ListItem>
                          <asp:ListItem Value="BIS">Bis</asp:ListItem>
                      </asp:DropDownList>
                  </td>
                  <td class="style15">
                      Cuadrante:
                  </td>
                  <td class="style15">
                      <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" 
                          Font-Names="Arial" Font-Size="XX-Small" 
                          OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged" Width="175px">
                          <asp:ListItem>Seleccione</asp:ListItem>
                          <asp:ListItem Value="ESTE">Este</asp:ListItem>
                          <asp:ListItem>NorEste</asp:ListItem>
                          <asp:ListItem Value="NORTE">Norte</asp:ListItem>
                          <asp:ListItem Value="OESTE">Oeste</asp:ListItem>
                          <asp:ListItem Value="SUR">Sur</asp:ListItem>
                          <asp:ListItem Value="NOROESTE">NorOeste</asp:ListItem>
                          <asp:ListItem Value="SURESTE">SurEste</asp:ListItem>
                          <asp:ListItem Value="SUROOESTE">SurOeste</asp:ListItem>
                      </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                  <td class="style15">
                      &nbsp;
                  </td>
                  <td class="style15">
                      Complemento:
                  </td>
                  <td class="style15">
                      <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" 
                          Font-Names="Arial" Font-Size="XX-Small" 
                          onselectedindexchanged="DropDownList7_SelectedIndexChanged" Width="175px">
                          <asp:ListItem>Seleccione</asp:ListItem>
                      </asp:DropDownList>
                  </td>
                  <td class="style15">
                  </td>
                  <td class="style15">
                  </td>
              </tr>
              <tr>
                  <td class="style15">
                  </td>
                  <td class="style15">
                      <span class="style15">Campo de texto:</span><br class="style15" />
                  </td>
                  <td class="style15" colspan="2">
                      <asp:TextBox ID="TextBox1" runat="server" Height="25px" 
                          ToolTip="Para: Via principal, Via generadora, Numero de placa y otros." 
                          Width="390px"></asp:TextBox>
                  </td>
                  <td class="style15">
                      &nbsp;&nbsp;&nbsp;
                      <asp:ImageButton ID="ImageButton1" runat="server" 
                          ImageUrl="~/imagenes/Guardar.png" OnClick="ImageButton1_Click" 
                          Style="padding-top: 0px" ToolTip="Agregar" />
                  </td>
              </tr>
              <tr>
                  <td class="style1">
                  </td>
                  <td class="style4" colspan="3">
                      <asp:TextBox ID="TextBox2" runat="server" BackColor="#EBEBE4"  ReadOnly="true"
                          Height="70px" TextMode="MultiLine" Width="550px"></asp:TextBox>
                  </td>
                  <td>
                    
                      &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnEnviar" 
                          runat="server" ImageUrl="~/Imagenes/001_06.png" 
                          ToolTip="Enviar Dirección" Width="26px" onclick="btnEnviar_Click"/>
                    
                      <br />
                      &nbsp;&nbsp;
                      <asp:ImageButton ID="ImageButton5" runat="server" 
                          ImageUrl="~/imagenes/001_49.png" OnClick="ImageButton2_Click" 
                          Style="padding-top: 8px" ToolTip="Borrar dirección" />
                      <br />
                      <asp:ImageButton ID="ImageButton2" runat="server" 
                          ImageUrl="~/imagenes/001_05.png" OnClick="Cerrar_Click" 
                          Style="padding-top: 8px" ToolTip="Cancelar" />
                  </td>
              </tr>
          </table>
                      <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#CC0000" 
                          Style="text-align: center"></asp:Label>
        <br />
    </div>
    
    </ContentTemplate>
    </asp:UpdatePanel>
