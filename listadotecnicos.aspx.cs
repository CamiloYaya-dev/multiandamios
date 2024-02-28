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
    public partial class listadotecnicos : System.Web.UI.Page
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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            perfil = Convert.ToInt32(Session["perfil"]);
            login = Convert.ToString(Session["login"]);
            Mensaje.Text = Convert.ToString(Session["mensaje"]);
            Session["mensaje"] = null;
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

            Traer_Tecnicos();
            Titulo.Visible = true;
            GridView1.Visible = true;
            table_tecnico.Visible = false;

            if (Page.IsPostBack == false)
            {
                Traer_Empresas();
                Traer_Areas();
            }

            table_atras.Visible = false;
        }

        //Código que maneja la barra de herramientas
        protected void Nuevo_Click(object sender, ImageClickEventArgs e)
        {

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

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("listadotecnicos.aspx");
        }
        
        //Función que carga la lista de técnicos

        public void Traer_Tecnicos()
        {
            try
            {
                // 1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                // 2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Tecnicos";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                // 3.parametros con valores 
                OracleCmd.Parameters.Add("Cursor_Traer_Tecnicos", OracleType.Cursor).Direction = ParameterDirection.Output;

                // 4.Abrir la conexión y crear el DataAdapter
                con.Open();
                OracleDataAdapter DA = new OracleDataAdapter(OracleCmd);

                // 5.Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Tecnicos");
                GridView1.DataSource = ds.Tables["Tecnicos"];
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    Mensaje.Text = "No existen registros.";
                }
                else
                {
                    Mensaje.Text = "";
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

        public void Traer_Empresas()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                string var = "Pro_Traer_Empresas";

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = var;//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores 
                OracleCmd.Parameters.Add("Cursor_Traer_Empresas", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4.Abrir la conexión y crear el DataAdapter
                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(OracleCmd);

                //5.Salida de los resultados y cerrar la conexión.
                DataTable DT = new DataTable();
                da.Fill(DT);
                DropDownEmpresa.DataSource = DT;
                DropDownEmpresa.DataValueField = "Codigo";
                DropDownEmpresa.DataTextField = "Descripcion";
                DropDownEmpresa.DataBind();
                DropDownEmpresa.SelectedValue = "Seleccione";
                DropDownEmpresa.Text = "Seleccione";
            }
            catch (Exception ex)
            {
                if (login == "admin")
                {
                    Mensaje.Text = ex.ToString();
                }
                else
                {
                    Mensaje.Text = "Error al cargar empresas. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Traer_Areas()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Areas";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. Parámetros con valores 
                OracleCmd.Parameters.Add("Cursor_Traer_Areas", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(OracleCmd);

                //5. Salida de los resultados y cerrar la conexión.
                DataTable DT = new DataTable();
                da.Fill(DT);
                DropDownArea.DataSource = DT;
                DropDownArea.DataValueField = "Codigo";
                DropDownArea.DataTextField = "Descripcion";
                DropDownArea.DataBind();
                DropDownArea.SelectedValue = "Seleccione";
            }
            catch (Exception ex)
            {
                if (login == "admin")
                {
                    Mensaje.Text = ex.ToString();
                }
                else
                {
                    Mensaje.Text = "Error al cargar las áreas. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Actualizar_Parametros()
        {
            try
            {
                //1. Establecer la cadena de conexión
                con.ConnectionString = Funciones.ConexionOra(ruta);

                // Evalúa índices a actualizar
                if (DropDownEmpresa.SelectedValue != "0")
                    Lb_Cod_Empresa.Text = DropDownEmpresa.SelectedValue;
                
                if (DropDownArea.SelectedValue != "0")
                    Lb_Cod_Area.Text = DropDownArea.SelectedValue;
                
                int val_canasta;
                if (CheckCanasta.Checked == true)
                    val_canasta = 1;
                else
                    val_canasta = 0;

                int val_autoescala;
                if (CheckAutoescala.Checked == true)
                    val_autoescala = 1;
                else
                    val_autoescala = 0;
                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Actualizar_Parametros";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. Parámetros con valores 
                OracleCmd.Parameters.Add("nCodigo", OracleType.Number).Value = "" + Lb_Cod_Tecnico.Text + "";
                OracleCmd.Parameters.Add("nEmpresa", OracleType.Number).Value = "" + Lb_Cod_Empresa.Text + "";
                OracleCmd.Parameters.Add("nArea", OracleType.Number).Value = "" + Lb_Cod_Area.Text + "";
                OracleCmd.Parameters.Add("nCanasta", OracleType.Number).Value = "" + val_canasta + "";
                OracleCmd.Parameters.Add("nAutoescala", OracleType.Number).Value = "" + val_autoescala + "";
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();
                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

                if (datodevuelto == "1")
                {
                    Mensaje.Text = "Registro actualizado correctamente.";
                }
                else
                {
                    Mensaje.Text = "Error actualizando el registro. Consulte al administrador del sistema";
                }

            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["perfil"]) == "1")
                {
                    Mensaje.Text = ex.ToString();
                }
                else
                {
                    Mensaje.Text = "Error actualizando el registro. Consulte al administrador del sistema";
                }
                con.Close();
            }
            con.Close();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Traer_Tecnicos();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = GridView1.SelectedIndex;
            Session["Seleccion"] = GridView1.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Cod_Tecnico' 
            if (e.CommandName == "Cod_Tecnico")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridView1.Rows[index];
                TableCell ColCodigo = FilaSeleccionada.Cells[1];
                TableCell ColNombre = FilaSeleccionada.Cells[4];
                TableCell ColApellido = FilaSeleccionada.Cells[5];
                TableCell ColLogin = FilaSeleccionada.Cells[6];
                TableCell ColEmpresa = FilaSeleccionada.Cells[2];
                TableCell ColArea = FilaSeleccionada.Cells[3];
                TableCell ColCanasta = FilaSeleccionada.Cells[7];
                TableCell ColAutoescala = FilaSeleccionada.Cells[8];

                Lb_Cod_Tecnico.Text = ColCodigo.Text;
                Nombre_Tecnico.Text = ColNombre.Text + " " + ColApellido.Text + " (" + ColLogin.Text + ")";

                DropDownEmpresa.SelectedItem.Text = ColEmpresa.Text;
                Lb_Cod_Empresa.Text = "0";
                DropDownArea.SelectedItem.Text = ColArea.Text;
                Lb_Cod_Area.Text = "0";

                if (ColCanasta.Text == "Habilitada")
                {
                    CheckCanasta.Checked = true;
                }
                else
                {
                    CheckCanasta.Checked = false;
                }

                if (ColAutoescala.Text == "Manual")
                {
                    CheckAutoescala.Checked = false;
                }
                else
                {
                    CheckAutoescala.Checked = true;
                }
                table_tecnico.Visible = true;
            }
        }

        protected void Guardar_Click(object sender, ImageClickEventArgs e)
        {
            Actualizar_Parametros();
            table_tecnico.Visible = false;
            // Oculta la información para que se necesite refrescar la página
            Titulo.Visible = false;
            GridView1.Visible = false;
        }

        protected void Atras_Click(object sender, ImageClickEventArgs e)
        {
            if (perfil == 1)
            {
                Response.Redirect("vistagerente.aspx");
            }
            if (perfil == 2)
            {
                Response.Redirect("vistadirector.aspx");
            }
        }

        protected void upMenu_Load(object sender, EventArgs e)
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
    }
}