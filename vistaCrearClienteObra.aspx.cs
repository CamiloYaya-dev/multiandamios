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
    public partial class vistaCrearClienteObra : System.Web.UI.Page
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

            //if (Session["login"].ToString() == ("admin").ToUpper())
            //{
            //    Modificar.Visible = true;
            //}
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
            Response.Redirect("vistaCrearClienteObra.aspx");
        }

        #endregion
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected void btnBuscarCliente_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscarCliente.Text.Length == 0)
            {
                Mensaje.Text = "Por favor indique el cliente a buscar.";
            }
            else
            {
                if (TraerClientesBusqueda())
                {
                    btnBuscarCliente.Visible = false;
                    txtBuscarCliente.Visible = false;
                    listCliente.Visible = true;
                    Mensaje.Text = "Por favor seleccione el cliente a buscar.";
                }
            }
        }

        public bool TraerClientesBusqueda()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vNombre", txtBuscarCliente.Text);

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);
                conn.Close();

                if (DT.Rows.Count == 0)
                {
                    Mensaje.Text = "No se encontraron clientes con el nombre indicado. Favor verificar.";
                    return false;
                }
                else
                {
                    listCliente.Items.Clear();
                    listCliente.DataSource = DT;
                    listCliente.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
                    listCliente.DataValueField = "IdCliente";
                    listCliente.DataTextField = "Cliente";
                    listCliente.DataBind();
                    return true;
                }     
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
                return false;
            }
        }

        protected void listCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCliente.SelectedValue == "Seleccione")
            {
                Mensaje.Text = "Favor seleccione el cliente a buscar.";
            }
            else
            {
                BuscarCliente(1);
                btnBuscarCliente.Visible = true;
                txtBuscarCliente.Visible = true;
                txtBuscarCliente.Text = "";
                listCliente.Visible = false;
                lbCrearCliente.Text = "Actualizar Cliente";
                lbNuevoCliente.Visible = true;
                btnNuevoCliente.Visible = true;
            }
        }

        public void BuscarCliente(int i_TipoBuscar)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BuscarCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (i_TipoBuscar == 1)
                {
                    cmd.Parameters.AddWithValue("@IdCliente", listCliente.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdCliente", listClienteObra.SelectedValue);
                }
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Session["IdCliente"] = dr.GetString(0);
                        txtIdentificacion.Text = dr.GetString(1);
                        //listTipoIdentificacion.Text = dr.GetString(2);
                        txtNombre.Text = dr.GetString(3);
                        txtTelefono.Text = dr.GetString(4);
                        txtDireccionOficina.Text = dr.GetString(5);
                        txtCorreo.Text = dr.GetString(6);
                    }
                }
                else
                {

                    Mensaje.Text = "No hay registros.";
                }
                conn.Close();

                TraerObra();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }
        protected void btnCrearCliente_Click(object sender, ImageClickEventArgs e)
        {
            if(lbCrearCliente.Text == "Crear Cliente")
            {
                CrearCliente();
            }
            if (lbCrearCliente.Text == "Actualizar Cliente")
            {
                ActualizarCliente();
            }            
        }

        public void CrearCliente()
        {
            int res = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_CrearCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nIdentificacion", txtIdentificacion.Text);
                cmd.Parameters.AddWithValue("@IdTipoIdentificacion", listTipoIdentificacion.Text);
                cmd.Parameters.AddWithValue("@vNombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@nTelefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@vDireccion", txtDireccionOficina.Text);
                cmd.Parameters.AddWithValue("@vCorreo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@vUsuarioCreacion", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    res = dr.GetInt32(0);
                    Session["IdCliente"] = res.ToString();
                }

                conn.Close();


                Mensaje.Text = "Información guardada correctamente.";

            }
            catch (Exception ex)
            {
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public void ActualizarCliente()
        {
            string res = "0";
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizarCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCliente", listCliente.SelectedValue);
                cmd.Parameters.AddWithValue("@nIdentificacion", txtIdentificacion.Text);
                cmd.Parameters.AddWithValue("@IdTipoIdentificacion", listTipoIdentificacion.Text);
                cmd.Parameters.AddWithValue("@vNombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@nTelefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@vDireccion", txtDireccionOficina.Text);
                cmd.Parameters.AddWithValue("@vCorreo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@vUsuarioModificacion", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    res = dr.GetString(0);
                    Session["IdCliente"] = res;
                }

                conn.Close();

                Mensaje.Text = "Información actualizada correctamente.";

            }
            catch (Exception ex)
            {
                //throw new Exception("Error al insertar.", ex);
            }
        }
        protected void btnCrearObra_Click(object sender, ImageClickEventArgs e)
        {
            int res = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_CrearObra", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vObra", txtObra.Text);
                cmd.Parameters.AddWithValue("@nTelefonoObra", txtTelefonoObra.Text);
                cmd.Parameters.AddWithValue("@vDireccionObra", txtDireccionObra.Text);
                cmd.Parameters.AddWithValue("@vContacto", txtContacto.Text);
                cmd.Parameters.AddWithValue("@nTelefonoContacto", txtTelefonoContacto.Text);
                cmd.Parameters.AddWithValue("@IdCliente", Session["IdCliente"].ToString());
                cmd.Parameters.AddWithValue("@vUsuarioCreacion", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    res = dr.GetInt32(0);
                }

                if (res == -1)
                {
                    Mensaje.Text = "Ya existe la Obra " + txtObra.Text + " para el Cliente " + txtNombre.Text + ". Favor verificar.";
                }
                else
                {
                    Mensaje.Text = "Información guardada correctamente.";
                }
                conn.Close();

                TraerObra();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public void TraerObra()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerObra", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCliente", Session["IdCliente"].ToString());

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "Obra");
                GridViewObra.DataSource = ds.Tables["Obra"];
                GridViewObra.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        protected void GridViewObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewObra_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewObra.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }
        
        protected void ListTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Traer_TipoSoporte();
            //Traer_SubTipoSoporte();
        }

        protected void Atras_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaInicio.aspx");
        }

        protected void GridViewObra_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Remover")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewObra.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                int reg = Convert.ToInt32(ColumnaCodigo.Text);

                BorrarRegistro(reg);
            }
        }

        public void BorrarRegistro(int reg)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BorrarObra", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdObra", reg.ToString());

                SqlDataReader dr = cmd.ExecuteReader();
                int res = 0;
                if (dr.Read())
                {
                    res = dr.GetInt32(0);
                }

                if (res == -1)
                {
                    Mensaje.Text = "Ya existe Remisión asignada a la Obra. Favor verificar.";
                }
                else
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DataTable DTb = new DataTable();
                            DTb = null;
                            GridViewObra.DataSource = DTb;
                            GridViewObra.DataBind();
                        }
                    }
                }
                conn.Close();

                TraerObra();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
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

        protected void btnBuscarObra_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscarObra.Text.Length == 0)
            {
                Mensaje.Text = "Por favor indique la obra a buscar.";
            }
            else
            {
                if (TraerClientesBusqueda())
                {
                    btnBuscarCliente.Visible = false;
                    txtBuscarCliente.Visible = false;
                    listCliente.Visible = true;
                    Mensaje.Text = "Por favor seleccione el cliente a buscar.";
                }
            }
        }

        protected void listClienteObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCliente.SelectedValue == "Seleccione")
            {
                Mensaje.Text = "Favor seleccione la obra a buscar.";
            }
            else
            {
                BuscarCliente(1);
                btnBuscarObra.Visible = true;
                btnBuscarObra.Visible = true;
                txtBuscarObra.Text = "";
                listClienteObra.Visible = false;
            }
        }
    }
}

