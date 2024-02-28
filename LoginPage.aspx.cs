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

namespace HelpDesk
{

    public partial class _Default : System.Web.UI.Page
    {
        tec_user.funciones Funciones = new tec_user.funciones();
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public string datodevuelto;
        public int perfil, area, cod_usuario, cod_aplicacion, autoescala;
        // Cadena de conexión
        string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["cod_aplicacion"] = 1; // (1) CÓDIGO APLICACIÓN HELP DESK 
            cod_aplicacion = Convert.ToInt32(Session["cod_aplicacion"]); // VERIFICAR TABLA (APLICACION) INSTANCIA (USUARIOS) DBLINK (@DBL_USUARIO)
            Session["login"] = null;
            Session["perfil"] = null;
            Session["area"] = null;
            Session["cod_usuario"] = null;
            Session["redirect"] = null;
            Session["seleccione"] = -1;
            Session["mensaje"] = "";

            if (Page.IsPostBack == false)
            {
                Usuario.Focus();
            }
        }

        protected void Login_Click(object sender, ImageClickEventArgs e)
        {
            Validar_Login_Original();
        }

        public void Login_TextChanged(object sender, EventArgs e)
        {

        }

        public void Validar_Login_Original()
        {
            #region codigo
            
            try
            {
                // Encripta la clave --------------------------------------------------
                string s_clave_encriptada;
                if (Clave.Text.Length != 0)
                {
                    s_clave_encriptada = tec_user.DTCautenticacion.Encrypt(Clave.Text);
                }
                else
                {
                    s_clave_encriptada = null;
                }
                //---------------------------------------------------------------------
                int perfil = 0;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_LoginUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vUsuario", Usuario.Text);
                cmd.Parameters.AddWithValue("@vClave", s_clave_encriptada);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    perfil = dr.GetInt32(0);
                }

                conn.Close();

                Session["login"] = Usuario.Text;
                Session["perfil"] = perfil;                

                if (perfil == -1)
                {
                    Mensaje.Text = "Usuario o clave incorrecta.";
                    Usuario.Text = "";
                    Clave.Text = "";
                    Usuario.Focus();
                }
                else
                {
                    Response.Redirect("vistaInicio.aspx");
                }
                //else if (perfil == -2)
                //{
                //    Mensaje.Text = "Usuario incorrecto."; 
                //    Usuario.Focus();
                //}

                
            }
            catch (Exception ex)
            {
                if (Usuario.Text.ToUpper() == "ADMIN")
                {
                    Mensaje.Text = ex.ToString();
                    Usuario.Focus();
                }
                else
                {
                    Mensaje.Text = "Error de acceso. Consulte al administrador del sistema.";
                    Usuario.Focus();
                }
                con.Close();
            }
            
            #endregion
        }

        public void Validar_Login()
        {
            #region codigo

            try
            {
                if (Usuario.Text.ToUpper() == "ADMIN" && Clave.Text == "admin")
                {
                    perfil = 1;
                    Session["login"] = "ADMIN"; //Usuario.Text;
                }
                else if (Usuario.Text.ToUpper() == "USER" && Clave.Text == "user")
                {
                    perfil = 5;
                    Session["login"] = "USER"; //Usuario.Text;
                }
                else
                {
                    perfil = 0;
                }
                
                if (perfil == 1)
                {
                    cod_usuario = 1; //Convert.ToInt32(OracleCmd.Parameters["nUsuario"].Value);
                    perfil = 1; //Convert.ToInt32(OracleCmd.Parameters["nPerfil"].Value);
                    area = 1; //Convert.ToInt32(OracleCmd.Parameters["nArea"].Value);
                    autoescala = 1;//Convert.ToInt32(OracleCmd.Parameters["nAutoescala"].Value);
                    Session["perfil"] = 1;  //perfil;
                    Session["area"] = 1; //area;
                    Session["cod_usuario"] = 1; //cod_usuario;
                    Session["autoescala"] = 1;//autoescala;

                    Response.Redirect("vistaInicio.aspx");
                    
                }
                else if (perfil == 5)
                {
                    cod_usuario = 5; //Convert.ToInt32(OracleCmd.Parameters["nUsuario"].Value);
                    perfil = 5; //Convert.ToInt32(OracleCmd.Parameters["nPerfil"].Value);
                    area = 5; //Convert.ToInt32(OracleCmd.Parameters["nArea"].Value);
                    autoescala = 5;//Convert.ToInt32(OracleCmd.Parameters["nAutoescala"].Value);
                    Session["perfil"] = 5;  //perfil;
                    Session["area"] = 5; //area;
                    Session["cod_usuario"] = 5; //cod_usuario;
                    Session["autoescala"] = 5;//autoescala;

                    Response.Redirect("vistaInicio.aspx");
                }
                else
                {
                    Mensaje.Text = "Usuario o clave incorrecta. Verifique la información."; // Error de usuario (verificar perfil, aplicación o estado de usuario)
                    Usuario.Text = "";
                    Clave.Text = "";
                    Usuario.Focus();
                }
            }
            catch (Exception ex)
            {
                //if (Usuario.Text == "admin" || Usuario.Text == "ADMIN")
                //{
                //    Mensaje.Text = ex.ToString();
                //    // Label1.Text = "Error de acceso a la base de datos. Verifique la conexión o consulte al administrador de la red.";
                //    Usuario.Focus();
                //}
                //else
                //{
                //    Mensaje.Text = "Error. Consulte al administrador del sistema.";
                //    Usuario.Focus();
                //}
            }

            #endregion
        }
    }

}