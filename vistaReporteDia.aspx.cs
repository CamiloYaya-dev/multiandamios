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

namespace HelpDesk
{
    public partial class vistaReporteDia : System.Web.UI.Page
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
        

        protected void Page_Load(object sender, EventArgs e)
        {
            login = Convert.ToString(Session["login"]);
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            
            perfil = Convert.ToInt32(Session["perfil"]);
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

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
                TraerClienteObra();               
            }
            table_listado.Visible = true;
            

            Mensaje.Text = Session["mensaje"].ToString();
            Session["mensaje"] = "";
       }

        //Código que maneja la barra de herramientas +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region Barra_Herramientas

        protected void Inicio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaInicio.aspx");
            Session["cod_material"] = null;
        }

        protected void CrearClienteObra_Click(object sender, ImageClickEventArgs e)
        {
            Session["cod_material"] = null;
            Response.Redirect("vistaCrearClienteObra.aspx");
        }

        protected void CrearInventario_Click(object sender, ImageClickEventArgs e)
        {
            Session["cod_material"] = null;
            Response.Redirect("vistaCrearInventario.aspx");
        }

        protected void CrearRemision_Click(object sender, ImageClickEventArgs e)
        {
            Session["cod_material"] = null;
            Response.Redirect("vistaRemision.aspx");
        }

        protected void Reporte_Click(object sender, ImageClickEventArgs e)
        {
            Session["cod_material"] = null;
            Response.Redirect("vistaReporte.aspx");
        }

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            Session["cod_material"] = null;
            Response.Redirect("vistaReporteDia.aspx");
        }

        #endregion
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // Se elimina el campo y el botón de búsqueda
        //protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (txtBuscar.Text.Length == 0)
        //    {
        //        Mensaje.Text = "Favor indique el cliente a buscar.";
        //    }
        //    else
        //    {
        //        if (TraerClienteObra())
        //        {
        //            btnBuscar.Visible = false;
        //            txtBuscar.Visible = false;
        //            listClienteObra.Visible = true;
        //            Mensaje.Text = "Favor seleccione de la lista el cliente a buscar.";
        //        }
        //    }
        //}

        #region Manejo_Buscar

        protected void btnBuscarClienteObra_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscarCliente.Text.Length == 0)
            {
                Mensaje.Text = "Por favor indique el cliente a buscar.";
            }
            else
            {
                TraerClienteObraBusqueda();
            }
        }

        public bool TraerClienteObraBusqueda()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerClienteObraBusqueda", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vDescripcion", txtBuscarCliente.Text);

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);
                conn.Close();

                if (DT.Rows.Count == 0)
                {
                    Mensaje.Text = "No se encontraron registros en la búsqueda. Favor verificar.";
                    return false;
                }
                else
                {
                    listClienteObra.Items.Clear();
                    listClienteObra.DataSource = DT;
                    listClienteObra.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
                    listClienteObra.DataValueField = "IdObra";
                    listClienteObra.DataTextField = "ClienteObra";
                    listClienteObra.DataBind();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
                return false;
            }
        }

        #endregion

        public bool TraerReporte()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerReporteDia", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nObra", listClienteObra.SelectedValue);

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "Reporte");
                GridViewReporte.DataSource = ds.Tables["Reporte"];
                GridViewReporte.DataBind();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public bool TraerClienteObra()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //SqlCommand cmd = new SqlCommand("SP_TraerClienteObraBusqueda", conn);
                SqlCommand cmd = new SqlCommand("SP_TraerClienteObraActivo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@vDescripcion", txtBuscar.Text); 

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);

                listClienteObra.Items.Clear();
                listClienteObra.DataSource = DT;
                listClienteObra.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
                listClienteObra.DataValueField = "IdObra";
                listClienteObra.DataTextField = "ClienteObra";
                listClienteObra.DataBind();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al buscar.", ex);
            }
        }

        protected void listClienteObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listClienteObra.SelectedValue != "Seleccione")
            {
                TraerReporte();
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

        protected void GridViewReporte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewReporte.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }

        protected void GridViewReporte_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Perdida")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewReporte.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[2];

                Mensaje2.Visible = true;
                txtPerdida.Visible = true;
                btnGuardarInventarioReposicion.Visible = true;
                Session["cod_material"] = Convert.ToInt32(ColumnaCodigo.Text);
                Mensaje2.Text = "Registro de pérdida de material: " + ColumnaDescripcion.Text + ": ";

            }
        }

        protected void ListTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnGuardarInventarioReposicion_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupConfirmar.Show();
            ModalPopupConfirmar.Enabled = true;                    
        }

        protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
        {
            int cod_material = Convert.ToInt32(Session["cod_material"]);

            if (GuardarInventarioReposicion(cod_material))
            {
                MensajeModal.Text = "Registro guardado correctamente.";
            }
            else
            {
                MensajeModal.Text = "El registro no se puede guardar. Consulte al administrador del sistema.";
            }

            Mensaje2.Text = "";
            Mensaje2.Visible = false;
            txtPerdida.Visible = false;
            btnGuardarInventarioReposicion.Visible = false;
            Session["cod_material"] = null;

            TraerReporte();

            btnAceptarModal.Visible = false;
            btnCancelarModal.Visible = false;
            btnSalirModal.Visible = true;
            ModalPopupConfirmar.Show();
            ModalPopupConfirmar.Enabled = true;
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            btnAceptarModal.Visible = true;
            btnCancelarModal.Visible = true;
            btnSalirModal.Visible = false;
            MensajeModal.Text = "";
            ModalPopupConfirmar.Hide();
            ModalPopupConfirmar.Enabled = false;
        }

        protected void btnSalir_Click(object sender, ImageClickEventArgs e)
        {
            btnAceptarModal.Visible = true;
            btnCancelarModal.Visible = true;
            btnSalirModal.Visible = false;
            MensajeModal.Text = "";
            ModalPopupConfirmar.Hide();
            ModalPopupConfirmar.Enabled = false;
            TraerReporte();
        }

        protected bool GuardarInventarioReposicion(int reg)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_GuardarInventarioReposicion", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdInventario", reg.ToString());
                cmd.Parameters.AddWithValue("@IdObra", listClienteObra.SelectedValue);
                cmd.Parameters.AddWithValue("@nCantidad", txtPerdida.Text);
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


        
    }
}

