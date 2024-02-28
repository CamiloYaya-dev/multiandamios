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

namespace HelpDesk
{
    public partial class vistadirector : System.Web.UI.Page
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
        public string area_director;
        public string cod_accion; // Variable que se usa para el escalamiento de registros según código
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            perfil = Convert.ToInt32(Session["perfil"]);
            login = Convert.ToString(Session["login"]);
            seleccion = Convert.ToString(Session["seleccion"]);
            Mensaje.Text = Convert.ToString(Session["mensaje"]);
            Session["mensaje"] = null;
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

            Escalar.Ocultar += new EventHandler(Escalar_onOcultar);

            Traer_Info_Usuario();
            Traer_Soporte();
            Traer_Escalados_Area();
        }

        //Código que maneja la barra de herramientas
        protected void Nuevo_Click(object sender, ImageClickEventArgs e)
        {
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
        
        //Función que trae información del director
        public void Traer_Info_Usuario()
        {
            try
            {
                // 1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                // 2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Info_Usuario";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                // 3. Parámetros con valores 
                OracleCmd.Parameters.Add("nFiltro", OracleType.Number).Value = 2; // Busca usuario en la aplicación
                OracleCmd.Parameters.Add("nCodigo", OracleType.Number).Value = cod_usuario;
                OracleCmd.Parameters.Add("vNombres", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vApellidos", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nPerfil", OracleType.Number).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nEmpresa", OracleType.Number).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nArea", OracleType.Number).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vEmail", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vLogin", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("fFechaIng", OracleType.DateTime).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vCanasta", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vAutoescala", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vEstado", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                // 4. Abrir la conexión 
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();

                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

                if (datodevuelto != "1")
                {
                    Session["mensaje"] = "Error al traer información del director de área.";
                }
                else
                {
                    // Trae el área del director
                    area_director = Convert.ToString(OracleCmd.Parameters["nArea"].Value);
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["login"]) == "admin")
                {
                    Mensaje.Text = ex.ToString();
                }
                else
                {
                    Mensaje.Text = "Error al traer información del director de área.";
                }
                con.Close();
            }
            con.Close();
        }

        //Función que carga la lista principal de soportes según área del director
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
                    Mensaje.Text = "Error al cargar información de soporte.";
                }
                con.Close();
            }
            con.Close();
        }

        //Función que carga la lista principal de escalados según área del director
        public void Traer_Escalados_Area()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Escalados_Area";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores
                OracleCmd.Parameters.Add("nArea", OracleType.Number).Value = "" + area_director + "";
                OracleCmd.Parameters.Add("Cursor_Traer_Escalados_Area", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4.Abrir la conexión y crear el DataAdapter
                con.Open();
                DA = new OracleDataAdapter(OracleCmd);

                ////5.Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Escalados");
                GridView2.DataSource = ds.Tables["Escalados"];
                GridView2.DataBind();
                if (GridView2.Rows.Count == 0)
                {
                    Label3.Text = "No existen registros.";
                }
                else
                {
                    Label3.Text = "";
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["perfil"]) == "1")
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

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            Traer_Escalados_Area();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

            index = GridView2.SelectedIndex;
            Session["Seleccion"] = GridView2.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistadirector.aspx");
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
                Session["cod_soporte"] = ColumnaCodigo.Text;
                Session["usuario_solicitante_sop"] = ColumnaCodigo1.Text; // Variable de sesión para el control EscalarSoporte (Usuario que solicita)
                
                
                // Variable de sesión para habilitar o deshabilitar escalamiento automático
                Escalar.Show(Convert.ToInt32(Session["autoescala"])); 
            }
        }

        protected void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Detalle")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridView2.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[1];
                TableCell ColumnaClase = FilaSeleccionada.Cells[3];
                Session["cod_soporte"] = ColumnaCodigo.Text;
                
                Response.Redirect("detallesoporte.aspx");
            }
        }

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