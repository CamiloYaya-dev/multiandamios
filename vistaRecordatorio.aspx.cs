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
    public partial class vistaRecordatorio : System.Web.UI.Page
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
                TraerRecordatorio();
            }
            //else
            //{
            //    TraerRecordatorio();
            //}
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
            Response.Redirect("vistaRecordatorio.aspx");
        }

        #endregion

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        protected void btnGuardarRecordatorio_Click(object sender, ImageClickEventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtFechaRegistro.Text.Length == 0)
            {
                Mensaje.Text = "Faltan campos por registrar. Favor verificar.";
            }
            else
            {
                if (LabelRecordatorio.Text == "Actualizar registro")
                {
                    ActualizarRecordatorio();
                }
                else
                {
                    GuardarRecordatorio();
                }
                Limpiar();
            }
        }

        public void GuardarRecordatorio()
        {
            try
            {
                int res = 0;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_GuardarRecordatorio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dFechaRegistro", txtFechaRegistro.Text);
                cmd.Parameters.AddWithValue("@vTitulo", txtTitulo.Text);
                cmd.Parameters.AddWithValue("@vDescripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    res = dr.GetInt32(0);
                }

                conn.Close();

                Mensaje.Text = "Información guardada correctamente.";

                TraerRecordatorio();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public void ActualizarRecordatorio()
        {
            try
            {
                int res = 0;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizarRecordatorio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nRecordatorio", Session["id_registro"].ToString());
                cmd.Parameters.AddWithValue("@dFechaRegistro", txtFechaRegistro.Text);
                cmd.Parameters.AddWithValue("@vTitulo", txtTitulo.Text);
                cmd.Parameters.AddWithValue("@vDescripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    res = dr.GetInt32(0);
                }

                conn.Close();

                Mensaje.Text = "Información actualizada correctamente.";

                TraerRecordatorio();
                LabelRecordatorio.Text = "Recordatorio";
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public void TraerRecordatorio()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerRecordatorio", conn);
                cmd.CommandType = CommandType.StoredProcedure;                

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "Recordatorio");

                GridViewBuscarRecordatorio.DataSource = null;
                GridViewBuscarRecordatorio.DataBind();

                GridViewRecordatorio.DataSource = ds.Tables["Recordatorio"];
                GridViewRecordatorio.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }


        protected void GridViewRecordatorio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewRecordatorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRecordatorio.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }
        // Todos los Command
        protected void GridViewRecordatorio_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Remover'
            if (e.CommandName == "Remover")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewRecordatorio.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaTitulo = FilaSeleccionada.Cells[2];
                Session["id_registro"] = ColumnaCodigo.Text;

                // Mensaje que muestra el artículo del inventario que se va a eliminar
                Mensaje.Text = "Eliminando registro: " + ColumnaTitulo.Text + ".\n ¿Realmente desea eliminar el registro del recordatorio?";
                btnAceptar.Visible = true;
                btnCancelar.Visible = true;
                //if (MessageBox.Show("¿Realmente desea eliminar el registro del inventario?", "Advertencia", MessageBoxButtons.YesNo,
                //    MessageBoxIcon.Warning) == DialogResult.Yes)
                //{
                //    BorrarRegistro(id_registro);
                //}
                //Mensaje.Text = "";     
            }

            // Determina el evento del botón 'Editar'
            if (e.CommandName == "Editar")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewRecordatorio.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaFecha = FilaSeleccionada.Cells[1];
                TableCell ColumnaTitulo = FilaSeleccionada.Cells[2];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[3];

                // Trae la información guardada para editar
                LabelRecordatorio.Text = "Actualizar registro";
                Session["id_registro"] = ColumnaCodigo.Text;
                txtFechaRegistro.Text = "";
                txtTitulo.Text = ColumnaTitulo.Text;
                txtDescripcion.Text = ColumnaDescripcion.Text;
            }
        }

        protected void GridViewBuscarRecordatorio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewBuscarRecordatorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBuscarRecordatorio.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }
        // Todos los Command
        protected void GridViewBuscarRecordatorio_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Remover'
            if (e.CommandName == "Remover")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewBuscarRecordatorio.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaTitulo = FilaSeleccionada.Cells[2];
                Session["id_registro"] = ColumnaCodigo.Text;

                // Mensaje que muestra el artículo del inventario que se va a eliminar
                Mensaje.Text = "Eliminando registro: " + ColumnaTitulo.Text + ".\n ¿Realmente desea eliminar el registro del recordatorio?";
                btnAceptar.Visible = true;
                btnCancelar.Visible = true;
                //if (MessageBox.Show("¿Realmente desea eliminar el registro del inventario?", "Advertencia", MessageBoxButtons.YesNo,
                //    MessageBoxIcon.Warning) == DialogResult.Yes)
                //{
                //    BorrarRegistro(id_registro);
                //}
                //Mensaje.Text = "";     
            }

            // Determina el evento del botón 'Editar'
            if (e.CommandName == "Editar")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewBuscarRecordatorio.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                TableCell ColumnaFecha = FilaSeleccionada.Cells[1];
                TableCell ColumnaTitulo = FilaSeleccionada.Cells[2];
                TableCell ColumnaDescripcion = FilaSeleccionada.Cells[3];

                // Trae la información guardada para editar
                LabelRecordatorio.Text = "Actualizar registro";
                Session["id_registro"] = ColumnaCodigo.Text;
                txtFechaRegistro.Text = "";
                txtTitulo.Text = ColumnaTitulo.Text;
                txtDescripcion.Text = ColumnaDescripcion.Text;
            }
        }

        public bool BorrarRegistro(int reg)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BorrarRecordatorio", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdRecordatorio", reg.ToString());

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


        protected void Atras_Click(object sender, ImageClickEventArgs e)
        {
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
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            id_registro = Convert.ToInt32(Session["id_registro"]);
            if (BorrarRegistro(id_registro))
            {
                Mensaje.Text = "Registro borrado correctamente.";
                Session["id_registro"] = null;
            }
            else
            {
                Mensaje.Text = "El registro no puede ser eliminado. Favor verificar.";
            }
            TraerRecordatorio();
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            Mensaje.Text = "";
        }

        protected void btnBuscarRecordatorio_Click(object sender, ImageClickEventArgs e)
        {
            BuscarRecordatorio();

            //txtFechaRegistro.Enabled = false;
            //txtTitulo.Enabled = false;
            //txtDescripcion.Enabled = false;
            //lbGuardarRecordatorio.Visible = false;
            //btnGuardarRecordatorio.Visible = false;
        }

        public void BuscarRecordatorio()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BuscarRecordatorio", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vDescripcion", txtBuscarRecordatorio.Text);

                DA = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                DA.Fill(ds, "Recordatorio");
                GridViewRecordatorio.DataSource = null;
                GridViewRecordatorio.DataBind();

                GridViewBuscarRecordatorio.DataSource = ds.Tables["Recordatorio"];
                GridViewBuscarRecordatorio.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public void Limpiar() // Limpiar campos
        {
            txtFechaRegistro.Text = "";
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
        }
    }
}

