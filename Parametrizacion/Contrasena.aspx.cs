using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Configuration;


namespace ControlVentas
{

    public partial class Contrasena : System.Web.UI.Page
    {
        tec_user.funciones Funciones = new tec_user.funciones();
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public DataRow DR;
        public int datodevuelto;
        public string seleccion;
        public string login;
        public string cod_usuario;
        string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
        public SqlDataAdapter DA;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            login = Convert.ToString(Session["login"]);
            if (Session["login"] == null)
            {
                Response.Redirect("SesionKill.aspx");
            }

            if (Page.IsPostBack == false)
            {
                TxtClave.Text = "";
                TxtConfirmar.Text = "";
            }

            if (Page.IsPostBack == true)
            {
                Mensaje.Text = "";
            }
        }

        protected void Guardar_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtClaveActual.Text.Length != 0 && TxtClave.Text.Length != 0 && TxtConfirmar.Text.Length != 0)
            {
                if (TxtClave.Text == TxtConfirmar.Text)
                {
                    Actualizar_Clave();
                    TxtClaveActual.Text = "";
                    TxtClave.Text = "";
                    TxtConfirmar.Text = ""; 
                    TxtClaveActual.Focus();
                }
                else
                {
                    Mensaje.Text = "Las claves escritas no coinciden. Digite de nuevo.";
                    TxtClave.Text = "";
                    TxtConfirmar.Text = "";
                    TxtClaveActual.Focus();
                }
            }
            else
            {
                Mensaje.Text = "Debe diligenciar totalmente el formulario.";
            }
        }

        public void Actualizar_Clave()
        {
            try
            {
                // Encripta la clave actual--------------------------------------------
                string s_clave_encriptada_actual;
                if (TxtClaveActual.Text.Length != 0)
                    s_clave_encriptada_actual = tec_user.DTCautenticacion.Encrypt(TxtClaveActual.Text);
                else
                    s_clave_encriptada_actual = null;
                // Encripta la clave --------------------------------------------------
                string s_clave_encriptada;
                if (TxtClave.Text.Length != 0)
                    s_clave_encriptada = tec_user.DTCautenticacion.Encrypt(TxtClave.Text);
                else
                    s_clave_encriptada = null;
                //---------------------------------------------------------------------

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizarClave", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());
                cmd.Parameters.AddWithValue("@vClaveActual", s_clave_encriptada_actual);
                cmd.Parameters.AddWithValue("@vClave", s_clave_encriptada);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    datodevuelto = dr.GetInt32(0);
                }

                conn.Close();

                
                if (datodevuelto == 1)
                {
                    Mensaje.Text = "Registro actualizado correctamente.";
                }
                else if (datodevuelto == -1)
                {
                    Mensaje.Text = "Las clave actual no es correcta. Digite de nuevo.";
                }
            }
            catch (Exception ex)
            {
                if (login == "ADMIN")
                {
                    Mensaje.Text = "Error al actualizar la clave. Consulte al administrador del sistema.";
                }
                else
                {
                    Session["Label1"] = "Error al actualizar la clave. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }
    }
    
}
