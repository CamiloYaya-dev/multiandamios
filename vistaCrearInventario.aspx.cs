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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Net;
using WinSCP;
//using System.Windows.Forms;

namespace HelpDesk
{
    public partial class vistaCrearInventario : System.Web.UI.Page
    {
        tec_user.funciones Funciones = new tec_user.funciones(); // Instancia la conexión Oracle
        tec_user.DataBaseOra DataBaseOra = new tec_user.DataBaseOra(); // Instancia para uso FTP
        //tec_user.funciones Convertida = new tec_user.funciones(); // Instancia para grabar logs de errores

        // Cadena de conexión
        string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public DataRow DR;
        public SqlDataAdapter DA;
        public DataTable DT = new DataTable();
        public string seleccion;
        public string datodevuelto;
        public string login;
        public int perfil;
        public int cod_usuario;
        public string cod_soporte;
        public string nom_tot_anexo;
        public int id_registro;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            login = Convert.ToString(Session["login"]);
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }

            //if (Session["login"].ToString() == ("admin").ToUpper())
            //{
            //    Modificar.Visible = true;
            //}

            perfil = Convert.ToInt32(Session["perfil"]);
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

            if (perfil != 1)
            {
                Response.Redirect("vistaInicio.aspx");
            }

            if (Page.IsPostBack == false)
            {
                //Guardar.Visible = true;
                //Traer_TipoSoporte();
                //Traer_SubTipoSoporte();
                if (perfil != 5)
                {
                    table_atras.Visible = true;
                    //table_listado.Visible = false;
                }
                else
                {
                    table_atras.Visible = false;
                    //table_listado.Visible = true;
                }
                Session["id_registro"] = null;
                TraerInventario();
            }
            else
            {
                TraerInventarioBusqueda();
            }
            table_listado.Visible = true;
            //Traer_Listado();
            

            Mensaje.Text = Session["mensaje"].ToString();
            Session["mensaje"] = "";
       }

        //Código que maneja la barra de herramientas +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        #region Barra_Herramientas

        protected void Inicio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaInicio.aspx");
        }

        protected void CrearClienteObra_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaCrearClienteObra.aspx");
        }

        protected void CrearInventario_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaCrearInventario.aspx");
        }

        protected void CrearRemision_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaRemision.aspx");
        }

        protected void Reporte_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaReporte.aspx");
        }

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaCrearInventario.aspx");
        }

        #endregion

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected void btnBuscarCliente_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    SqlConnection conn = new SqlConnection(connectionString);
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SP_BuscarCliente", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@vNombre", txtNombre.Text);

            //    SqlDataReader dr = cmd.ExecuteReader();

            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            Session["IdCliente"] = dr.GetString(0);
            //            txtIdentificacion.Text = dr.GetString(1);
            //            //listTipoIdentificacion.Text = dr.GetString(2);
            //            txtNombre.Text = dr.GetString(3);
            //            txtTelefono.Text = dr.GetString(4);
            //            txtDireccionOficina.Text = dr.GetString(5);
            //            txtCorreo.Text = dr.GetString(6);                
            //        }
            //    }
            //    else
            //    {

            //        Mensaje.Text = "No hay registros.";
            //    }
            //    conn.Close();

            //    TraerObra();
            //}
            //catch (Exception ex)
            //{
            //    //throw new Exception("Error al buscar.", ex);
            //}
        }

        protected void btnCrearInventario_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCantidad.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtFechaRegistro.Text.Length == 0)
            {
                Mensaje.Text = "Faltan campos por registrar. Favor verificar.";
            }
            else
            {
                if (lbCrearInventario.Text == "Actualizar registro")
                {
                    if(ActualizarInventario())
                    {
                        ModalPopupOK.Show();
                        ModalPopupOK.Enabled = true;
                    }
                }
                else
                {
                    if(CrearInventario())
                    {
                        ModalPopupOK.Show();
                        ModalPopupOK.Enabled = true;
                    }
                }
            }                     
        }

        public bool ActualizarInventario()
        {
            try
            {
                int res = 0;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizarInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdInventario", Session["id_registro"].ToString());
                cmd.Parameters.AddWithValue("@nCantidad", txtCantidad.Text);
                cmd.Parameters.AddWithValue("@vDescripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@dFechaRegistro", txtFechaRegistro.Text);
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    res = dr.GetInt32(0);
                }

                conn.Close();

                Mensaje.Text = "Información actualizada correctamente.";
                lbCrearInventario.Text = "Crear registro de inventario";
                Session["id_registro"] = null;
                return true;
                //TraerInventario();
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public bool CrearInventario()
        {
            try
            {
                int res = 0;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_CrearInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nCantidad", txtCantidad.Text);
                cmd.Parameters.AddWithValue("@vDescripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@dFechaRegistro", txtFechaRegistro.Text);
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    res = dr.GetInt32(0);
                }

                conn.Close();


                Mensaje.Text = "Información guardada correctamente.";
                return true;

                //TraerInventario();
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al insertar.", ex);
            }
        }
        public void TraerInventario()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "Inventario");
                GridViewInventario.DataSource = ds.Tables["Inventario"];
                GridViewInventario.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }
        public void TraerDetalleInventario()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerDetalleInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nInventario", Session["id_registro"].ToString());

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "DetalleInventario");
                GridViewDetalleInventario.DataSource = ds.Tables["DetalleInventario"];
                GridViewDetalleInventario.DataBind();

                //Limpia grilla principal
                DataTable dt = new DataTable();
                GridViewInventario.DataSource = dt;
                GridViewInventario.DataBind();

                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public void TraerDetallePerdida()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerDetallePerdida", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nInventario", Session["id_registro"].ToString());

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "DetallePerdida");
                GridViewDetallePerdida.DataSource = ds.Tables["DetallePerdida"];
                GridViewDetallePerdida.DataBind();

                //Limpia grilla principal
                DataTable dt = new DataTable();
                GridViewInventario.DataSource = dt;
                GridViewInventario.DataBind();

                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        //public void Traer_TipoSoporte()
        //{
        //    try
        //    {
        //        //1. Establecer la cadena de conexión
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        //2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Traer_TipoSoporte";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        // 3. Parámetros con valores   
        //        //OracleCmd.Parameters.Add("iClase", OracleType.Number).Value = Convert.ToInt32(RadioButtonClase.Text);
        //        OracleCmd.Parameters.Add("Cursor_Traer_TipoSoporte", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        // 4. Abrir la conexión y crear el DataAdapter
        //        con.Open();
        //        DA = new OracleDataAdapter(OracleCmd);

        //        //5. Salida de los resultados y cerrar la conexión.
        //        DataTable DT = new DataTable();
        //        DA.Fill(DT);
        //        DropDownTipo.Items.Clear();
        //        DropDownTipo.DataSource = DT;
        //        DropDownTipo.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
        //        DropDownTipo.DataValueField = "Id_Tiposoporte";
        //        DropDownTipo.DataTextField = "Nombre";
        //        DropDownTipo.DataBind();
                
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Error:" + ex.ToString());
        //        con.Close();
        //    }
        //    con.Close();
        //}

        //public void Traer_SubTipoSoporte()
        //{
        //    try
        //    {
        //        //1. Establecer la cadena de conexión
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        //2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Traer_SubTipoSoporte";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        // 3. Parámetros con valores   
        //        //OracleCmd.Parameters.Add("iClase", OracleType.Number).Value = Convert.ToInt32(RadioButtonClase.Text);
        //        OracleCmd.Parameters.Add("iTipo", OracleType.Number).Value = Convert.ToInt32(DropDownTipo.SelectedIndex);
        //        OracleCmd.Parameters.Add("Cursor_Traer_SubTipoSoporte", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        // 4. Abrir la conexión y crear el DataAdapter
        //        con.Open();
        //        DA = new OracleDataAdapter(OracleCmd);

        //        //5. Salida de los resultados y cerrar la conexión.
        //        DataTable DT = new DataTable();
        //        DA.Fill(DT);
        //        DropDownSubtipo.Items.Clear();
        //        DropDownSubtipo.DataSource = DT;
        //        DropDownSubtipo.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
        //        DropDownSubtipo.DataValueField = "Id_Subtiposoporte";
        //        DropDownSubtipo.DataTextField = "Nombre";
        //        DropDownSubtipo.DataBind();

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToString(Session["perfil"]) == "0")
        //        {
        //            Mensaje.Text = ex.ToString();
        //        }
        //        else
        //        {
        //            Mensaje.Text = "Error al cargar información de subtipos de soporte.";
        //        }
        //        con.Close();
        //    }
        //    con.Close();
        //}

        //public void Traer_Listado()
        //{
        //    try
        //    {
        //        // 1. Establecer la cadena de conexión
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        // 2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Traer_Soporte_Usuario";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        // 3. Parámetros con valores
        //        OracleCmd.Parameters.Add("iUsuario", OracleType.Number).Value = cod_usuario;
        //        OracleCmd.Parameters.Add("Cursor_Traer_Soporte_Usuario", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        // 4. Abrir la conexión y crear el DataAdapter
        //        con.Open();
        //        DA = new OracleDataAdapter(OracleCmd);

        //        // 5. Salida de los resultados y cerrar la conexión
        //        DataSet ds = new DataSet();
        //        DA.Fill(ds, "Soporte");
        //        GridViewSoporte.DataSource = ds.Tables["Soporte"];
        //        GridViewSoporte.DataBind();
        //        if (GridViewSoporte.Rows.Count == 0)
        //        {
        //            Mensaje2.Text = "No existen registros.";
        //        }
        //        else
        //        {
        //            Mensaje2.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToString(Session["perfil"]) == "0")
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

        //public void Guardar_Soporte()
        //{
        //    try
        //    {
        //        // 1. Establecer la cadena de conexión
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        // 2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Guardar_Soporte";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        // 3. Parámetros con valores
        //        //OracleCmd.Parameters.Add("iClase", OracleType.Number).Value = RadioButtonClase.SelectedValue;
        //        OracleCmd.Parameters.Add("iTipo", OracleType.Number).Value = DropDownTipo.SelectedValue;
        //        OracleCmd.Parameters.Add("iSubtipo", OracleType.Number).Value = DropDownSubtipo.SelectedValue;
        //        OracleCmd.Parameters.Add("iUsuario", OracleType.Number).Value = cod_usuario;
        //        OracleCmd.Parameters.Add("vDetalle", OracleType.VarChar).Value = DetalleTicket.Text;
        //        OracleCmd.Parameters.Add("vAnexo", OracleType.VarChar).Value = NomAnexo.Text;
        //        OracleCmd.Parameters.Add("nCodSoporte", OracleType.Number).Direction = ParameterDirection.Output;
                
        //        // 4. Abrir la conexión y crear el DataAdapter
        //        OracleString rowld;
        //        con.Open();
        //        OracleCmd.ExecuteOracleNonQuery(out rowld);
        //        con.Close();
        //        cod_soporte = Convert.ToString(OracleCmd.Parameters["nCodSoporte"].Value);

        //        if (cod_soporte == "0")
        //        {
        //            Mensaje.Text = "No se pudo crear el registro. Consulte al administrador del sistema.";
        //        }
        //        else
        //        {
        //            // Verifica si se ha registrado anexo para concatenar nombre de archivo y guardar en FTP
        //            if (NomAnexo.Text == "") 
        //            {
        //                nom_tot_anexo = "";
        //                // Según la clase de soporte se muestra el mensaje
        //                //if (RadioButtonClase.SelectedValue == "1")
        //                //{
        //                //    Mensaje.Text = "Registro creado correctamente. Ticket No. " + cod_soporte;
        //                //}
        //                //else
        //                //{
        //                //    Mensaje.Text = "Registro creado correctamente. Solicitud No. " + cod_soporte;
        //                //}
        //            }
        //            else
        //            {
        //                nom_tot_anexo = cod_soporte + "_" + NomAnexo.Text;
        //                btnUploadFile(NomAnexo.Text);
        //                // Según la clase de soporte se muestra el mensaje
        //                // El servidor FTP es demorado en cargar. Por eso los mensajes en cada 'if'
        //                //if (RadioButtonClase.SelectedValue == "1")
        //                //{
        //                //    Mensaje.Text = "Registro creado correctamente. Ticket No. " + cod_soporte + ". " + Mensaje.Text;
        //                //}
        //                //else
        //                //{
        //                //    Mensaje.Text = "Registro creado correctamente. Solicitud No. " + cod_soporte + ". " + Mensaje.Text;
        //                //}
        //            }
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToString(Session["perfil"]) == "0")
        //        {
        //            Response.Write("Error:" + ex.ToString());
        //        }
        //        else
        //        {
        //            Mensaje.Text = "Error al intentar actualizar el registro.";
        //        }
        //        con.Close();
        //    }
        //    con.Close();
        //}

        protected void GridViewInventario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewInventario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewInventario.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }
        // Todos los Command
        protected void GridViewInventario_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Remover'
            if (e.CommandName == "Remover")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewInventario.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                //TableCell ColumnaCantidad = FilaSeleccionada.Cells[2];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[7];
                Session["id_registro"] = ColumnaCodigo.Text;

                //// Mensaje que muestra el artículo del inventario que se va a eliminar
                //Mensaje.Text = "Eliminando registro: " + ColumnaDescripcion.Text + ".\n ¿Realmente desea eliminar el registro del inventario?";
                //btnAceptar.Visible = true;
                //btnCancelar.Visible = true;

                //// El ModalPopup funciona con los controles GridView fuera de UpdatePanel
                ModalPopupConfirmar.Show();
                ModalPopupConfirmar.Enabled = true;


                ////-------------------------------------------------------------------------------------------
                //if (BorrarRegistro(Convert.ToInt32(ColumnaCodigo.Text)))
                //{
                //    Mensaje.Text = "Registro borrado correctamente.";
                //    Session["id_registro"] = null;
                //}
                //else
                //{
                //    Mensaje.Text = "El registro no puede ser eliminado del inventario. Favor verificar.";
                //}
                //TraerInventario(); 
                ////--------------------------------------------------------------------------------------------

                //ModalPopupConfirmar.Show();
                //ModalPopupConfirmar.Enabled = true;

                //this.btnBuscarMaterial.Attributes.Add("onClick", "if(!confirm('Esta seguro de resetar su clave?')){return false;};");

                // Mensaje que muestra el artículo del inventario que se va a eliminar
                //Mensaje.Text = "Eliminando registro: " + ColumnaDescripcion.Text + ".\n ¿Realmente desea eliminar el registro del inventario?";
                //btnAceptar.Visible = true;
                //btnCancelar.Visible = true;

                //if (MessageBox.Show("¿Realmente desea eliminar el registro del inventario?", "Advertencia", MessageBoxButtons.YesNo,
                //    MessageBoxIcon.Warning) == DialogResult.Yes)
                //{
                //    BorrarRegistro(id_registro);
                //}
                //Mensaje.Text = "";
            }

            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Detalle")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewInventario.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaCantidad = FilaSeleccionada.Cells[2];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[7];
                TableCell ColumnaFecha = FilaSeleccionada.Cells[8];

                // Trae la información guardada para editar
                Session["id_registro"] = ColumnaCodigo.Text;
                lbCrearInventario.Visible = false;
                btnCrearInventario.Visible = false;
                txtCantidad.Text = ColumnaCantidad.Text;
                txtDescripcion.Text = ColumnaDescripcion.Text;
                LabelBodega.Text = "Detalle Alquilado";
                //txtFechaRegistro.Text = ColumnaFecha.Text;

                TraerDetalleInventario();
            }

            // Determina el evento del botón 'DetallePerdida'
            if (e.CommandName == "DetallePerdida")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewInventario.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaCantidad = FilaSeleccionada.Cells[2];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[7];
                TableCell ColumnaFecha = FilaSeleccionada.Cells[8];

                // Trae la información guardada para editar
                Session["id_registro"] = ColumnaCodigo.Text;
                lbCrearInventario.Visible = false;
                btnCrearInventario.Visible = false;
                txtCantidad.Text = ColumnaCantidad.Text;
                txtDescripcion.Text = ColumnaDescripcion.Text;
                LabelBodega.Text = "Detalle de Pérdida";
                //txtFechaRegistro.Text = ColumnaFecha.Text;

                TraerDetallePerdida();
            }

            // Determina el evento del botón 'Editar'
            if (e.CommandName == "Editar")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewInventario.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaCantidad = FilaSeleccionada.Cells[2];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[7];
                TableCell ColumnaFecha = FilaSeleccionada.Cells[8];

                // Trae la información guardada para editar
                Session["id_registro"] = ColumnaCodigo.Text;
                lbCrearInventario.Text = "Actualizar registro";
                lbCrearInventario.Visible = true;
                btnCrearInventario.Visible = true; 
                txtCantidad.Text = ColumnaCantidad.Text;
                txtDescripcion.Text = ColumnaDescripcion.Text;
                txtFechaRegistro.Text = "";
            }
        }

        // DetalleInventario
        protected void GridViewDetalleInventario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewDetalleInventario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDetalleInventario.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }

        protected void GridViewDetalleInventario_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

        }

        //DetallePerdida
        protected void GridViewDetallePerdida_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewDetallePerdida_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDetallePerdida.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }

        protected void GridViewDetallePerdida_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

        }
        //
        public bool BorrarRegistro(int reg)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BorrarInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdInventario", reg.ToString());
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr.GetInt32(0) == -1)
                    {
                        conn.Close();
                        return false;
                    }
                    else
                    {
                        conn.Close();
                        return true;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }               
                
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al buscar.", ex);
            }
        }

        protected void ListTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Traer_TipoSoporte();
            //Traer_SubTipoSoporte();
        }

        //protected void DropDownTipo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Traer_SubTipoSoporte();
        //}

        ///*protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    int fileSize = FileUpload1.PostedFile.ContentLength;
        //    if (fileSize < 1100000)
        //    {
        //        try
        //        {
        //            ImageButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            NomAnexo.Text = Convert.ToString(FileUpload1.FileName);
        //            FileUpload1.SaveAs(MapPath("~/Anexos/") + NomAnexo.Text);
        //            NomAnexo.Visible = true;
        //            ImageButton2.Visible = true;
        //            Mensaje.Text = "";
        //        }
        //        catch (Exception ex)
        //        {
        //            if (Convert.ToString(Session["perfil"]) == "0")
        //            {
        //                Mensaje.Text = "ERROR: " + ex.Message.ToString();
        //            }
        //            else
        //            {
        //                Mensaje.Text = "Debe seleccionar un archivo";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Mensaje.Text = "El tamaño del archivo no debe superar 1MB.";
        //    }
        //}*/

        ///*protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    Mensaje.Text = "";
        //    NomAnexo.Text = "";
        //    ImageButton1.Visible = true;
        //    FileUpload1.Visible = true;
        //    ImageButton2.Visible = false;
        //    NomAnexo.Visible = false;
        //}*/
        
        //// Guarda el archivos por ruta HttpContext
        //protected void btnUploadFile_Click(object sender, EventArgs e)
        //{
        //    if (FileUpload1.HasFile)
        //    {
        //        try
        //        {
        //            ImageButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            NomAnexo.Text = Convert.ToString(FileUpload1.FileName);
        //            NomAnexo.Visible = true;
        //            ImageButton2.Visible = true;
                    
        //            HttpPostedFile mifichero = FileUpload1.PostedFile;
        //            string ubicacion = HttpContext.Current.Server.MapPath(".").ToString() + "\\Downloader\\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
        //            // Save the file.
        //            mifichero.SaveAs(ubicacion);
        //        }
                
        //        catch (Exception ex)
        //        {
        //            Mensaje.Text = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        Mensaje.Text = "No se ha especificado archivo a cargar. Verifique la información.";
        //    }
            
        //}

        //// Elimina el archivo de la ruta HttpContex
        //protected void btnUploadFileDelete_Click(object sender, EventArgs e)
        //{
        //    FileInfo TheFile = new FileInfo(HttpContext.Current.Server.MapPath(".").ToString() + "\\Downloader\\" + NomAnexo.Text);
        //    if (TheFile.Exists)
        //    {
        //        File.Delete(HttpContext.Current.Server.MapPath(".").ToString() + "\\Downloader\\" + NomAnexo.Text);
        //    }

        //    NomAnexo.Text = "";
        //    ImageButton1.Visible = true;
        //    FileUpload1.Visible = true;
        //    ImageButton2.Visible = false;
        //    NomAnexo.Visible = false;
        //}

        //// Carga de archivos por FTP
        //protected void btnUploadFile(string archivo)
        //{
        //    try
        //    {

        //        string ubicaarchivo = HttpContext.Current.Server.MapPath(".").ToString();
        //        ubicaarchivo += "\\Downloader\\" + archivo;

        //        string CnxFtp = DataBaseOra.ConexionFTP(HttpContext.Current.Server.MapPath(".").ToString());
        //        string[] words = CnxFtp.Split(',');
        //        // Setup session options
        //        SessionOptions sessionOptions = new SessionOptions
        //        {
        //            Protocol = Protocol.Sftp,
        //            HostName = words[0].ToString(),
        //            UserName = words[1].ToString(),
        //            Password = words[2].ToString(),
        //            PortNumber = Convert.ToInt32(words[3]),
        //            SshHostKey = words[4].ToString()
        //        };

        //        using (Session session = new Session())
        //        {
        //            string ubicacion = HttpContext.Current.Server.MapPath(".").ToString();

        //            ubicacion += "\\Bin\\WinSCP.exe";
        //            session.ExecutablePath = ubicacion;

        //            // Connect
        //            session.Open(sessionOptions);

        //            // Upload files
        //            TransferOptions transferOptions = new TransferOptions();
        //            transferOptions.TransferMode = TransferMode.Binary;

        //            // Se cambió porque no es necesario crear el directorio. Ya existe.
        //            string CreateDirect = @"/home/coonal/HelpDesk/" + nom_tot_anexo;

        //            /*string CreateDirect = @"/home/coonal/HelpDesk";
        //            try
        //            {
        //                session.CreateDirectory(CreateDirect);
        //            }
        //            catch
        //            { }
        //            CreateDirect = CreateDirect + @"/" + nom_tot_anexo;*/

        //            TransferOperationResult transferResult;
        //            transferResult = null;

        //            // Transfiere el archivo al servidor FTP
        //            transferResult = session.PutFiles(ubicaarchivo, CreateDirect, true, transferOptions);

        //            // Throw on any error
        //            transferResult.Check();

        //            foreach (TransferEventArgs transfer in transferResult.Transfers)
        //            {
        //                Mensaje.Text = "Archivo cargado correctamente."; //  + transfer.FileName.ToString()
        //            }
        //        }

        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Mensaje.Text = Convert.ToString(ex);
        //        //Convertida.GrabarErroresLog(ex.Message, HttpContext.Current.Request.MapPath("~"), login, ex.Source, ex.StackTrace);
        //        //return;
        //    }
        //}

        protected void Atras_Click(object sender, ImageClickEventArgs e)
        {
            //if (perfil == 1)
            //{
            //    Response.Redirect("vistagerente.aspx");
            //}
            //if (perfil == 2)
            //{
            //    Response.Redirect("vistadirector.aspx");
            //}
            //if (perfil == 4)
            //{
            //    Response.Redirect("vistatecnico.aspx");
            //}
            Response.Redirect("vistaInicio.aspx");
        }

        protected void ManualPDF_Click(object sender, ImageClickEventArgs e)
        {
            //string nom_archivo_pdf = "HelpDesk_ManualUsuario.pdf";
            //string archivo_pdf = HttpContext.Current.Server.MapPath(".").ToString();
            //archivo_pdf += "\\Manuales\\" + nom_archivo_pdf;
            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = ContentType;
            //Response.AddHeader("Content-Disposition", "attachment; filename=\"" + nom_archivo_pdf + "\"");
            //Response.TransmitFile(archivo_pdf);
            //Response.Flush();
            //Response.Close();
        }


        protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
        {
            //btnAceptar.Visible = false;
            //btnCancelar.Visible = false;
            id_registro = Convert.ToInt32(Session["id_registro"]);
            if (BorrarRegistro(id_registro))
            {
                MensajeModal.Text = "Registro borrado correctamente.";
                Session["id_registro"] = null;
            }
            else
            {
                MensajeModal.Text = "El registro no puede ser eliminado del inventario. Favor verificar.";
            }
            btnAceptarModal.Visible = false;
            btnCancelarModal.Visible = false;
            btnSalirModal.Visible = true;
            ModalPopupConfirmar.Show();
            ModalPopupConfirmar.Enabled = true;
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            //btnAceptar.Visible = false;
            //btnCancelar.Visible = false;
            btnAceptarModal.Visible = true;
            btnCancelarModal.Visible = true;
            btnSalirModal.Visible = false;
            MensajeModal.Text = "";
            ModalPopupConfirmar.Hide();
            ModalPopupConfirmar.Enabled = false;
            TraerInventario();          
        }

        protected void btnBuscarMaterial_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscar.Text.Length == 0)
            {
                Mensaje.Text = "Favor indique el material del inventario para buscar.";
            }
            else
            {
                TraerInventarioBusqueda();
            }
            lbCrearInventario.Visible = false;
            btnCrearInventario.Visible = false; 
        }

        public bool TraerInventarioBusqueda()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerInventarioBusqueda", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vDescripcion", txtBuscar.Text);

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "Inventario");
                GridViewInventario.DataSource = ds.Tables["Inventario"];
                GridViewInventario.DataBind();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al buscar.", ex);
            }
        }

        protected void btnSalirModalOK_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupOK.Hide();
            ModalPopupOK.Enabled = false;
            Response.Redirect("vistaCrearInventario.aspx");
        }
    }
}

