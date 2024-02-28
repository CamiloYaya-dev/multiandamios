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
    public partial class Usuarios : System.Web.UI.Page
    {
        tec_user.funciones Funciones = new tec_user.funciones();
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public DataRow DR;
        public int datodevuelto;
        public string login, cod_usuario_modif;
        public int cod_aplicacion;
        public int perfil, area;
        // Cadena de conexión
        string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
        public SqlDataAdapter DA;

        protected void Page_Load(object sender, EventArgs e)
        {
            //cod_aplicacion = Convert.ToInt32(Session["cod_aplicacion"]); 
            login = Convert.ToString(Session["login"]);
            if (Session["login"] == null)
            {
                Response.Redirect("SesionKill.aspx");
            }

            perfil = Convert.ToInt32(Session["perfil"]);
            area = Convert.ToInt32(Session["area"]);

            // Verifica si el ingreso es por Modificación de Usuario
            cod_usuario_modif = Convert.ToString(Session["cod_usuario_modif"]); 
            
            if (Page.IsPostBack == false)
            {
                
                Traer_Perfiles();
                //Traer_Empresas();
                //if (perfil == 1)
                //{
                //    //DropDownArea.Enabled = true;
                //    Traer_Areas();
                //}
                //else
                //{
                //    //DropDownArea.Enabled = false;
                //    Traer_Areas();
                //    //DropDownArea.SelectedValue = Convert.ToString(area);
                //}
                //table_canasta.Visible = false;

                if (cod_usuario_modif == null || cod_usuario_modif == "")
                {
                    Titulo.Text = "Ingreso de Usuarios";
                    TxtCodigo.Enabled = true;
                    TxtCodigo.Text = "";
                }
                else
                {
                        Titulo.Text = "Modificación de Usuarios";
                    Login.Enabled = false;
                    TxtCodigo.Text = cod_usuario_modif;

                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SP_TraerUsuarioCodigo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nCodigo", cod_usuario_modif);                    

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        TxtCodigo.Text = dr.GetDecimal(0).ToString();
                        Nombre.Text = dr.GetString(1);
                        Email.Text = dr.GetString(3);
                        Login.Text = dr.GetString(4);
                    }
                    conn.Close();
                    
                }
            }
            if (Page.IsPostBack == true)
            {
                Mensaje.Text = "";
            }
        }

        //protected void TxtCodigo_TextChanged(object sender, EventArgs e)
        //{
        //    if (TxtCodigo.Text.Length != 0)
        //    {
        //        try
        //        {
        //            // Se asegura que se ingresen sólo números
        //            string VerificaError1 = Convert.ToString(Convert.ToDouble(TxtCodigo.Text) * Convert.ToDouble(TxtCodigo.Text));
        //            // Parámetro cod_filtro: 1. Busca usuario de la instancia USUARIOS que no está habilitado para esta aplicación
        //            Traer_Info_Usuario(1); // (Modo Ingreso)
        //            Nombre.Focus();
        //        }
        //        catch
        //        {
        //            TxtCodigo.Text = "";
        //            TxtCodigo.Focus();
        //        }
        //    }
        //}

        protected void Guardar_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtCodigo.Text.Length != 0 && Nombre.Text.Length != 0 &&  Email.Text.Length != 0 && Login.Text.Length != 0 && DropDownPerfil.Text != "Seleccione")
            {
                if (Session["cod_usuario_modif"] == null) // Modo ingreso de usuarios
                {
                    if (Clave.Text.Length == 0)
                    {
                        Mensaje.Text = "Es necesario ingresar la clave de usuario.";
                    }
                    else
                    {
                        Ingresar_Usuario();
                        TxtCodigo.Text = "";
                        Nombre.Text = "";
                        //Apellidos.Text = "";
                        Email.Text = "";
                        Login.Text = "";
                        Clave.Text = "";
                        //DropDownPerfil.SelectedValue = "0";
                        //DropDownEmpresa.SelectedValue = "0";
                        //DropDownArea.SelectedValue = "0";
                        RadioButtonEstado.SelectedValue = "1";
                        //table_canasta.Visible = false;
                    }
                }
                else // Modo modificación de usuarios
                {
                    Actualizar_Usuario();
                    Response.Redirect("ModificarUsuarios.aspx");
                }
            }
            else
            {
                Mensaje.Text = "Debe diligenciar totalmente el formulario.";
            }

        }

        public void Traer_Perfiles()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerPerfiles", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);

                DropDownPerfil.Items.Clear();
                DropDownPerfil.DataSource = DT;
                DropDownPerfil.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
                DropDownPerfil.DataValueField = "IdPerfil";
                DropDownPerfil.DataTextField = "Nombre";
                DropDownPerfil.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (login == "ADMIN")
                {
                    Mensaje.Text = ex.ToString();
                }
                else
                {
                    Mensaje.Text = "Error al cargar perfiles de usuario. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }
        
        //public void Traer_Empresas()
        //{
        //    try
        //    {
        //        //1. Establecer la cadena de conexion
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        string var = "Pro_Traer_Empresas";

        //        //2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = var;//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        //3.parametros con valores 
        //        OracleCmd.Parameters.Add("Cursor_Traer_Empresas", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        //4.Abrir la conexión y crear el DataAdapter
        //        con.Open();
        //        OracleDataAdapter da = new OracleDataAdapter(OracleCmd);

        //        //5.Salida de los resultados y cerrar la conexión.
        //        DataTable DT = new DataTable();
        //        da.Fill(DT);
        //        //DropDownEmpresa.DataSource = DT;
        //        //DropDownEmpresa.DataValueField = "Codigo";
        //        //DropDownEmpresa.DataTextField = "Descripcion";
        //        //DropDownEmpresa.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (login == "admin")
        //        {
        //            Mensaje.Text = ex.ToString();
        //        }
        //        else
        //        {
        //            Mensaje.Text = "Error al cargar empresas. Consulte al administrador del sistema.";
        //        }
        //        con.Close();
        //    }
        //    con.Close();
        //}

        //public void Traer_Areas()
        //{
        //    try
        //    {
        //        //1. Establecer la cadena de conexion
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        //2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Traer_Areas";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        //3. Parámetros con valores 
        //        OracleCmd.Parameters.Add("Cursor_Traer_Areas", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        //4. Abrir la conexión y crear el DataAdapter
        //        con.Open();
        //        OracleDataAdapter da = new OracleDataAdapter(OracleCmd);

        //        //5. Salida de los resultados y cerrar la conexión.
        //        DataTable DT = new DataTable();
        //        da.Fill(DT);
        //        //DropDownArea.DataSource = DT;
        //        //DropDownArea.DataValueField = "Codigo";
        //        //DropDownArea.DataTextField = "Descripcion";
        //        //DropDownArea.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        if (login == "admin")
        //        {
        //            Mensaje.Text = ex.ToString();
        //        }
        //        else
        //        {
        //            Mensaje.Text = "Error al cargar las áreas. Consulte al administrador del sistema.";
        //        }
        //        con.Close();
        //    }
        //    con.Close();
        //}

        //public void Traer_Info_Usuario(int cod_filtro)
        //{
        //    try
        //    {
        //        // 1. Establecer la cadena de conexion
        //        con.ConnectionString = Funciones.ConexionOra(ruta);

        //        // 2. Establecer el comando
        //        OracleCommand OracleCmd = new OracleCommand();
        //        OracleCmd.Connection = con;//La conexion que va a usar el comando
        //        OracleCmd.CommandText = "Pro_Traer_Info_Usuario";//El comando a ejecutar
        //        OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

        //        // 3. Parámetros con valores
        //        OracleCmd.Parameters.Add("nFiltro", OracleType.Number).Value = cod_filtro;
        //        OracleCmd.Parameters.Add("nCodigo", OracleType.Number).Value = Convert.ToInt32(TxtCodigo.Text);
        //        OracleCmd.Parameters.Add("vNombres", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("vApellidos", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("nPerfil", OracleType.Number).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("nEmpresa", OracleType.Number).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("nArea", OracleType.Number).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("vEmail", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("vLogin", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("fFechaIng", OracleType.DateTime).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("vCanasta", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("vAutoescala", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("vEstado", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
        //        OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

        //        // 4. Abrir la conexión 
        //        OracleString rowld;
        //        con.Open();
        //        OracleCmd.ExecuteOracleNonQuery(out rowld);
        //        con.Close();

        //        datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

        //        // Sólo busca usuario de la instancia USUARIOS (Modo Ingreso)
        //        if (cod_filtro == 1)
        //        {
        //            if (datodevuelto == "1")
        //            {
        //                Nombre.Text = Convert.ToString(OracleCmd.Parameters["vNombres"].Value);
        //                //Apellidos.Text = Convert.ToString(OracleCmd.Parameters["vApellidos"].Value);
        //                DropDownPerfil.SelectedValue = "0";
        //                //DropDownEmpresa.SelectedValue = Convert.ToString(OracleCmd.Parameters["nEmpresa"].Value);
        //                //DropDownArea.SelectedValue = "0";
        //                Email.Text = Convert.ToString(OracleCmd.Parameters["vEmail"].Value);
        //                Login.Text = Convert.ToString(OracleCmd.Parameters["vLogin"].Value);
        //                Clave.Text = "";

        //                RadioButtonEstado.SelectedValue = "2"; // Inactivo por defecto
        //                //CheckCanasta.Checked = false;
        //                //CheckAutoescala.Checked = false;
        //            }
        //            else
        //            {
        //                Nombre.Text = "";
        //                //Apellidos.Text = "";
        //                DropDownPerfil.SelectedValue = "0";
        //                //DropDownEmpresa.SelectedValue = "0";
        //                //DropDownArea.SelectedValue = "0";
        //                Email.Text = "";
        //                Login.Text = "";
        //                Clave.Text = "";
        //                RadioButtonEstado.SelectedValue = "1";
        //                //CheckCanasta.Checked = false;
        //                //CheckAutoescala.Checked = false;
        //            }
        //        } 
        //        // Busca usuario a modificar (Modo Modificación)
        //        if (cod_filtro == 2) 
        //        {
        //            if (datodevuelto == "1") 
        //            {
        //                Nombre.Text = Convert.ToString(OracleCmd.Parameters["vNombres"].Value);
        //                //Apellidos.Text = Convert.ToString(OracleCmd.Parameters["vApellidos"].Value);
        //                DropDownPerfil.SelectedValue = Convert.ToString(OracleCmd.Parameters["nPerfil"].Value);
        //                //DropDownEmpresa.SelectedValue = Convert.ToString(OracleCmd.Parameters["nEmpresa"].Value);
        //                //DropDownArea.SelectedValue = Convert.ToString(OracleCmd.Parameters["nArea"].Value);
        //                Email.Text = Convert.ToString(OracleCmd.Parameters["vEmail"].Value);
        //                Login.Text = Convert.ToString(OracleCmd.Parameters["vLogin"].Value);
        //                Clave.Text = "";

        //                if (Convert.ToString(OracleCmd.Parameters["vEstado"].Value) == "Activo")
        //                    RadioButtonEstado.SelectedValue = "1";
        //                else
        //                    RadioButtonEstado.SelectedValue = "2";
                        
        //                // Perfil técnico habilita los parámetros de tecnología

        //                if (DropDownPerfil.SelectedItem.Text == "TECNICO")
        //                    table_canasta.Visible = true;
                        
        //                if (Convert.ToString(OracleCmd.Parameters["vCanasta"].Value) == "Habilitada")
        //                    CheckCanasta.Checked = true;
        //                else
        //                    CheckCanasta.Checked = false;
                        
        //                if (Convert.ToString(OracleCmd.Parameters["vAutoescala"].Value) == "Manual")
        //                    CheckAutoescala.Checked = false;
        //                else
        //                    CheckAutoescala.Checked = true;
                        
        //            }
        //            else
        //            {
        //                Session["mensaje"] = "Error al cargar informacion de usuario. Consulte al administrador del sistema.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToString(Session["login"]) == "admin")
        //        {
        //            Mensaje.Text = ex.ToString();
        //        }
        //        else
        //        {
        //            Mensaje.Text = "Error al cargar informacion de usuario. Consulte al administrador del sistema.";
        //        }
        //        con.Close();
        //    }
        //    con.Close();
        //}

        public void Ingresar_Usuario()
        {
            try
            {
                // Encripta la clave --------------------------------------------------
                string s_clave_encriptada;
                if (Clave.Text.Length != 0)
                    s_clave_encriptada = tec_user.DTCautenticacion.Encrypt(Clave.Text);
                else
                    s_clave_encriptada = null;
                //---------------------------------------------------------------------

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_IngresarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vUsuario", Login.Text);
                cmd.Parameters.AddWithValue("@vClave", s_clave_encriptada);
                cmd.Parameters.AddWithValue("@nIdentificacion", TxtCodigo.Text);
                cmd.Parameters.AddWithValue("@vNombre", Nombre.Text);
                cmd.Parameters.AddWithValue("@vCorreo", Email.Text);
                cmd.Parameters.AddWithValue("@IdPerfil", DropDownPerfil.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@vUsuarioCreacion", Session["login"].ToString());


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    datodevuelto = dr.GetInt32(0);
                }

                conn.Close();

                if (datodevuelto == -1)
                {
                    Mensaje.Text = "El login ya existe. Ingrese otro login de usuario.";
                    Login.Text = "";
                }
                else if (datodevuelto > 0)
                {
                    Mensaje.Text = "Información almacenada correctamente.";
                    Email.Text = "";
                    Nombre.Text = "";
                    Session["cod_usuario_modif"] = null;
                }
                else
                {
                    Mensaje.Text = "Se ha producido un error guardando el usuario. Consulte al administrador del sistema.";
                }

            }
            catch (Exception ex)
            {
                if (login == "ADMIN")
                {
                    Mensaje.Text = ex.ToString();
                }
                else
                {
                    Mensaje.Text = "Se ha producido un error guardando el usuario. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Actualizar_Usuario()
        {
            try
            {
                // Encripta la clave --------------------------------------------------
                string s_clave_encriptada;
                if (Clave.Text.Length != 0)
                    s_clave_encriptada = tec_user.DTCautenticacion.Encrypt(Clave.Text);
                else
                    s_clave_encriptada = null;
                //---------------------------------------------------------------------

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vUsuario", Login.Text);
                cmd.Parameters.AddWithValue("@vClave", s_clave_encriptada);
                cmd.Parameters.AddWithValue("@nIdentificacion", TxtCodigo.Text);
                cmd.Parameters.AddWithValue("@vNombre", Nombre.Text);
                cmd.Parameters.AddWithValue("@vCorreo", Email.Text);
                cmd.Parameters.AddWithValue("@IdPerfil", DropDownPerfil.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@bEstado", RadioButtonEstado.SelectedValue);


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    datodevuelto = dr.GetInt32(0);
                }

                conn.Close();

                if (datodevuelto == 1)
                {
                    Session["Mensaje"] = "Registro actualizado correctamente.";
                    Session["cod_usuario_modif"] = null;
                }
                else if (datodevuelto == -1)
                {
                    Session["Mensaje"] = "La identificación ya existe. Favor verificar.";
                    Login.Text = "";
                }
                else
                {
                    Session["Mensaje"] = "Se ha producido un error actualizando el usuario. Consulte al administrador del sistema.";
                }
            }
            catch (Exception ex)
            {
                if (login == "admin")
                {
                    Session["Mensaje"] = ex.ToString();
                }
                else
                {
                    Session["Mensaje"] = "Se ha producido un error actualizando el usuario. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        //protected void DropDownPerfil_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DropDownPerfil.SelectedItem.Text == "TECNICO")
        //    {
        //        table_canasta.Visible = true;
        //    }
        //    else
        //    {
        //        table_canasta.Visible = false;
        //        CheckCanasta.Checked = false;
        //        CheckAutoescala.Checked = false;
        //    }
        //}

    }
}