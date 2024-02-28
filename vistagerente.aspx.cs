using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace HelpDesk
{
    public partial class vistagerente : System.Web.UI.Page
    {

        tec_user.funciones Funciones = new tec_user.funciones();
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public OracleDataAdapter DA;
        public DataRow DR;
        public string datodevuelto;
        public string seleccion;
        int index;
        public int perfil;
        public string login;
        public int cod_usuario;
        public string cod_accion; // Variable que se usa para el escalamiento de registros según código
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            perfil = Convert.ToInt32(Session["perfil"]);
            login = Convert.ToString(Session["login"]);
            Mensaje.Text = Convert.ToString(Session["mensaje"]); // Variable de sesión para mostrar mensaje del control EscalarSoporte
            Session["mensaje"] = null;
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

            Escalar.Ocultar += new EventHandler(Escalar_onOcultar);

            //Traer_Soporte();
            //Traer_Escalados();
        }

        //Código que maneja la barra de herramientas
        protected void Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            Session["mensaje"] = " ";
            Response.Redirect("vistausuario.aspx");
        }

        protected void Modificar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void Eliminar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Copiar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void InformePDF_Click(object sender, ImageClickEventArgs e)
        {

        }
        
        //Función que carga la lista principal de soportes

        public void Traer_Soporte()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Soporte";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores
                OracleCmd.Parameters.Add("Cursor_Traer_Soporte", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4.Abrir la conexión y crear el DataAdapter
                con.Open();
                OracleDataAdapter DA = new OracleDataAdapter(OracleCmd);

                ////5.Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Soporte");
                GridView1.DataSource = ds.Tables["Soporte"];
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    Label2.Text = "No existen registros de soporte.";
                }
                else
                {
                    Label2.Text = "";
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["perfil"]) == "0")
                {
                    Response.Write("Error:" + ex.ToString());
                }
                else
                {
                    Mensaje.Text = "Error al cargar información.";
                }
                con.Close();
            }
            con.Close();
        }

        //public void Traer_Escalados()
        //{
        //    try
        //    {
        //        //1. Establecer la cadena de conexion
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        //2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Traer_Escalados";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        //3.parametros con valores
        //        OracleCmd.Parameters.Add("Cursor_Traer_Escalados", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        //4.Abrir la conexión y crear el DataAdapter
        //        con.Open();
        //        DA = new OracleDataAdapter(OracleCmd);

        //        ////5.Salida de los resultados y cerrar la conexión.
        //        DataSet ds = new DataSet();
        //        DA.Fill(ds, "Escalados");
        //        GridView2.DataSource = ds.Tables["Escalados"];
        //        GridView2.DataBind();
        //        if (GridView2.Rows.Count == 0)
        //        {
        //            Label3.Text = "No existen registros.";
        //        }
        //        else
        //        {
        //            Label3.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToString(Session["perfil"]) == "1")
        //        {
        //            Response.Write("Error:" + ex.ToString());
        //        }
        //        else
        //        {
        //            Mensaje.Text = "Error al cargar información.";
        //        }
        //        con.Close();
        //    }
        //    con.Close();
        //}

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Traer_Soporte();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            index = GridView1.SelectedIndex;
            Session["Seleccion"] = GridView1.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        //protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView2.PageIndex = e.NewPageIndex;
        //    Traer_Escalados();
        //}

        //protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    index = GridView2.SelectedIndex;
        //    Session["Seleccion"] = GridView2.DataKeys[index].Value.ToString();
        //    Mensaje.Text = "";
        //}

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistagerente.aspx");
         }

        protected void GridView1_RowCommand(Object sender,  GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Escalar'
            if (e.CommandName == "Escalar")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridView1.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[1];
                TableCell ColumnaCodigo1 = FilaSeleccionada.Cells[2];
                Session["cod_soporte"] = ColumnaCodigo.Text; // Variable de sesión para el control EscalarSoporte
                Session["usuario_solicitante_sop"] = ColumnaCodigo1.Text; // Variable de sesión para el control EscalarSoporte (Usuario que solicita)
                
                // Variable de sesión para habilitar o deshabilitar escalamiento automático
                Escalar.Show(Convert.ToInt32(Session["autoescala"]));
           
            }
        }

        //protected void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
        //{
        //    // Determina el evento del botón 'Detalle'
        //    if (e.CommandName == "Detalle")
        //    {
        //        // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
        //        int index = Convert.ToInt32(e.CommandArgument);

        //        // Obtiene el código del registro
        //        GridViewRow FilaSeleccionada = GridView2.Rows[index];
        //        TableCell ColumnaCodigo = FilaSeleccionada.Cells[1];
        //        TableCell ColumnaClase = FilaSeleccionada.Cells[3];
        //        Session["cod_soporte"] = ColumnaCodigo.Text;
                
        //        Response.Redirect("detallesoporte.aspx");
        //    }
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Escalar_onOcultar(object sender, EventArgs e)
        {
            //txtDireccionInfoBasic.Text = dirCiudadInfoBasica.Direccion;
        }

        protected void List_Tecnicos_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("listadotecnicos.aspx");
        }

        // Función para TOOLBAR FLOTANTE (Se cambió por conflicto en prettyPhoto con [iframe])
        /*protected void upMenu_Load(object sender, EventArgs e)
        {
            String Script = "var Menu = $('Menu'); " +
                        " var Contenedor = $('Centro'); " +
                        " var MenuInicial = $('MenuInicial'); " +
                        " MenuInicial.addEvents({ " +
                            " 'visible': function() { " +
                                " Menu.style.position = 'relative'; " +
                                " Menu.style.top = 0; " +
                            " }, " +
                            " 'hidden': function() { " +
                                " Menu.style.position = 'fixed'; " +
                                " Menu.style.top = '-1px'; " +
                            "} " +
                        " }); " +
                        " new e24ScrollEvents({ " +
                            " container: window, " +
                            " mode: 'vertical', " +
                            " elements: [MenuInicial] " +
                        " });";

            ScriptManager.RegisterClientScriptBlock(upMenu, typeof(UpdatePanel), "MenuFlotante", Script, true);
        }
        */
        protected void ManualPDF_Click(object sender, ImageClickEventArgs e)
        {
            string nom_archivo_pdf = "HelpDesk_ManualUsuario.pdf";
            string archivo_pdf = HttpContext.Current.Server.MapPath(".").ToString();
            archivo_pdf += "\\Manuales\\" + nom_archivo_pdf;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = ContentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + nom_archivo_pdf + "\"");
            Response.TransmitFile(archivo_pdf);
            Response.Flush();
            Response.Close();
        }
        
    }
}