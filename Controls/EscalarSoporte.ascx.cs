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
using System.Reflection;
using System.Net.Mail;

namespace HelpDesk
{
    public partial class EscalarSoporte : System.Web.UI.UserControl
    {
        tec_user.funciones Funciones = new tec_user.funciones();
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public OracleDataAdapter DA;
        public DataRow DR;
        public string datodevuelto;
        int index;
        public int perfil;
        public string login;
        public int cod_usuario;
        public int cod_area; // Variable que se usa para el escalamiento automático por área
        public int cod_escalado; // Variable que se usa para el escalamiento manual por técnico
        public string usuario_esc; // Variable que guarda el login de usuario ya escalado 
        public int cod_soporte; // Variable que recibe el código de soporte a escalar
        public string usuario_solicitante_sop; // Variable que recibe el usuario que hace el ticket (Para enviar correo de notificación)

        // Enviar correo
        MailMessage objMail;
        public string Email_enviar;
        public string Email_enviar_copia;
        public string Email_enviar_oculto;
        public string Email_enviar_admin;
        public string Usuario_Solicitante;
        public string Usuario_Asignado;
        
        public event EventHandler Ocultar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            perfil = Convert.ToInt32(Session["perfil"]);
            login = Convert.ToString(Session["login"]);
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);
            cod_soporte = Convert.ToInt32(Session["cod_soporte"]);
            usuario_solicitante_sop = Convert.ToString(Session["usuario_solicitante_sop"]);
        }
        
        public void Traer_Areas()
        {
            try
            {
                // 1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                // 2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Areas";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                // 3.parametros con valores 
                OracleCmd.Parameters.Add("Cursor_Traer_Areas", OracleType.Cursor).Direction = ParameterDirection.Output;

                // 4.Abrir la conexión y crear el DataAdapter
                con.Open();
                OracleDataAdapter DA = new OracleDataAdapter(OracleCmd);

                // 5.Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Area");
                GridView1.DataSource = ds.Tables["Area"];
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    Session["mensaje"] = "No existen registros.";
                }
                else
                {
                    Session["mensaje"] = "";
                }
            }

            catch (Exception ex)
            {
                if (Convert.ToString(Session["perfil"]) == "0")
                {
                    Session["mensaje"] = "Error:" + ex.ToString();
                }
                else
                {
                    Session["mensaje"] = "Error al cargar información.";
                }
                con.Close();
            }
            con.Close();
        }

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
                GridView2.DataSource = ds.Tables["Tecnicos"];
                GridView2.DataBind();
                if (GridView2.Rows.Count == 0)
                {
                    Session["mensaje"] = "No existen registros.";
                }
                else
                {
                    Session["mensaje"] = "";
                }
            }

            catch (Exception ex)
            {
                if (Convert.ToString(Session["perfil"]) == "0")
                {
                    Session["mensaje"] = "Error:" + ex.ToString();
                }
                else
                {
                    Session["mensaje"] = "Error al cargar información.";
                }
                con.Close();
            }
            con.Close();
        }

        public bool Escalamiento_Auto()
        {
            try
            {
                // 1. Establecer la cadena de conexion 
                con.ConnectionString = Funciones.ConexionOra(ruta);

                // 2. Establecer el comando 
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Escalamiento_Auto";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                // 3.parametros con valores 
                OracleCmd.Parameters.Add("iSoporte", OracleType.Number).Value = cod_soporte;
                OracleCmd.Parameters.Add("iArea", OracleType.Number).Value = cod_area;
                OracleCmd.Parameters.Add("vLogin", OracleType.VarChar,4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                // 4.Abrir la conexión 
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();

                usuario_esc = Convert.ToString(OracleCmd.Parameters["vLogin"].Value); 
                cod_escalado = Convert.ToInt32(OracleCmd.Parameters["nRespuesta"].Value);

                if (cod_escalado < 1)
                {
                    Session["mensaje"] = "Error al realizar escalamiento.";
                }
                else
                {
                    Session["mensaje"] = "Escalamiento exitoso.";
                    Traer_Info_Usuario();                    
                }
                con.Close();
                return true;                
            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["login"]) == "admin")
                {
                    Session["mensaje"] = ex.ToString();
                }
                else
                {
                    Session["mensaje"] = "Error al realizar escalamiento. Consulte al administrador del sistema.";
                }
                con.Close();
                return false;
            }            
        }

        public bool Escalamiento_Manual()
        {
            try
            {
                // 1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                // 2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Escalamiento_Manual";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                // 3. Parámetros con valores 
                OracleCmd.Parameters.Add("iSoporte", OracleType.Number).Value = cod_soporte;
                OracleCmd.Parameters.Add("iEscalado", OracleType.Number).Value = cod_escalado;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                // 4. Abrir la conexión 
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();

                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

                if (datodevuelto != "1")
                {
                    Session["mensaje"] = "Error al realizar escalamiento.";
                }
                else
                {
                    Session["mensaje"] = "Escalamiento exitoso.";
                }
                con.Close();
                return true; 
            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["login"]) == "admin")
                {
                    Session["mensaje"] = ex.ToString();
                }
                else
                {
                    Session["mensaje"] = "Error al realizar escalamiento. Consulte al administrador del sistema.";
                }
                con.Close();
                return false; 
            }
        }

        public void Guardar_Seguimiento(string texto_seguimiento) // Según el escalamiento se registra el seguimiento.
        {
            try
            {
                //1. Establecer la cadena de conexión
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Guardar_Seguimiento";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores
                OracleCmd.Parameters.Add("iSoporte", OracleType.Number).Value = cod_soporte;
                OracleCmd.Parameters.Add("vObservacion", OracleType.VarChar).Value = texto_seguimiento;
                OracleCmd.Parameters.Add("iUsuario", OracleType.Number).Value = cod_usuario;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();
                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

                if (datodevuelto != "1")
                {
                    Session["mensaje"] = Convert.ToString(Session["mensaje"]) + " No se pudo registrar la información de seguimiento.";
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
                    Session["mensaje"] = "Error al registrar la información de seguimiento.";
                }
                con.Close();
            }
            con.Close();
        }

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
                OracleCmd.Parameters.Add("nCodUsuario", OracleType.Number).Value = cod_escalado;
                OracleCmd.Parameters.Add("vNombres", OracleType.VarChar,100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vApellidos", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vPerfil", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nEmpresa", OracleType.Number).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nArea", OracleType.Number).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nEmail", OracleType.Number).Direction = ParameterDirection.Output;
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
                    Session["mensaje"] = Session["mensaje"] + " Error al traer información de usuario.";
                }
                else
                {
                    // Guarda el nombre del usuario escalado en variable de sesión para enviar mensaje de respuesta
                    string nombre_usuario = Convert.ToString(OracleCmd.Parameters["vNombres"].Value);
                    nombre_usuario = nombre_usuario + " " + Convert.ToString(OracleCmd.Parameters["vApellidos"].Value);
                    nombre_usuario = nombre_usuario + " (" + Convert.ToString(OracleCmd.Parameters["vLogin"].Value) + ")";
                    Session["mensaje"] = Session["mensaje"] + " Caso escalado a " + nombre_usuario;
                }
            }
            catch (Exception ex)
            {
                if (Session["login"] == "admin")
                {
                    Session["mensaje"] = ex.ToString();
                }
                else
                {
                    Session["mensaje"] = "Error al traer información de usuario. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Traer_Areas();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = GridView1.SelectedIndex;
            Session["seleccion"] = GridView1.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Cod_Area'
            if (e.CommandName == "Cod_Area" && Session["mensaje"].ToString().Contains("Escalamiento exitoso.") == false)
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridView1.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[1];
                cod_area = Convert.ToInt32(ColumnaCodigo.Text);

                if (Escalamiento_Auto() == true)
                {
                    string texto_seguimiento = "Servicio asignado a " + usuario_esc + " (a).";
                    Guardar_Seguimiento(texto_seguimiento);
                    Enviar_Notificacion();
                }
            }

            // Perfiles de usuario
            if (perfil == 1)
            {
                Response.Redirect("vistagerente.aspx");
            }
            else if (perfil == 2)
            {
                Response.Redirect("vistadirector.aspx"); 
            }
            else if (perfil == 4)
            {
                Response.Redirect("vistatecnico.aspx");
            }
            else if (perfil == 5)
            {
                Response.Redirect("vistausuario.aspx");
            }
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            Traer_Tecnicos();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = GridView2.SelectedIndex;
            Session["seleccion"] = GridView2.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Cod_Tecnico'
            if (e.CommandName == "Cod_Tecnico" && Session["mensaje"].ToString().Contains("Escalamiento exitoso.") == false)
            {                
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridView2.Rows[index];
                TableCell ColumnaCodigo1 = FilaSeleccionada.Cells[1];
                TableCell ColumnaCodigo2 = FilaSeleccionada.Cells[6];
                cod_escalado = Convert.ToInt32(ColumnaCodigo1.Text);
                usuario_esc = Convert.ToString(ColumnaCodigo2.Text);

                if (Escalamiento_Manual() == true)
                {
                    string texto_seguimiento = "Servicio asignado a " + usuario_esc + " (m).";
                    Guardar_Seguimiento(texto_seguimiento);
                    Enviar_Notificacion();
                }
            }

            // Perfiles de usuario
            if (perfil == 1)
            {
                Response.Redirect("vistagerente.aspx");
            }
            else if (perfil == 2)
            {
                Response.Redirect("vistadirector.aspx");
            }
            else if (perfil == 4)
            {
                Response.Redirect("vistatecnico.aspx");
            }
            else if (perfil == 5)
            {
                Response.Redirect("vistausuario.aspx");
            }
        }       

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected virtual void OnOcultar(EventArgs e)
        {
            if (Ocultar != null)
            {
                Ocultar(this, e);
            }
        }

        protected void Cerrar_Click(object sender, ImageClickEventArgs e)
        {
            divEncontrar.Style["display"] = "none";
            Fondo.Style["display"] = "none";
            OnOcultar(new EventArgs());
        }

        public void Show(int autoescala)
        {
            if (autoescala == 1) // Evalúa escalamiento automático
            {
                Traer_Areas();
                GridView1.Visible = true;
                GridView2.Visible = false;
            }
            else
            {
                Traer_Tecnicos();
                GridView2.Visible = true;
                GridView1.Visible = false;
            }
            divEncontrar.Style["display"] = "block";
            Fondo.Style["display"] = "block";
        }

        protected void btOk_Click(object sender, ImageClickEventArgs e)
        {
            divEncontrar.Style["display"] = "none";
            Fondo.Style["display"] = "none";
            OnOcultar(new EventArgs());
        }

        public void Enviar_Notificacion()
        {
            Traer_Datos_Email();

            string Remitente = "davidhernandez@tecnicasfinancierassa.com";
            //string Vinculo = Request.Url.ToString().Replace("ingresarsolicitud.aspx", "historicosolicitudes.aspx");
            //Email_enviar = "danielnaranjo@tecnicasfinancierassa.com";
            //Email_enviar_copia = "danielnaranjo@tecnicasfinancierassa.com";
            //Email_enviar_oculto = "danielnaranjo@tecnicasfinancierassa.com";
            
            //'Creamos el objeto del correo
            objMail = new MailMessage();
            objMail.From = new MailAddress("" + Remitente + ""); //Remitente
            objMail.To.Add("" + Email_enviar + ""); //Email a enviar 
            objMail.CC.Add("" + Email_enviar_copia + ""); //Email a enviar copia
            objMail.Bcc.Add("" + Email_enviar_oculto + ""); //Email a enviar oculto (Director)
            objMail.Bcc.Add("" + Email_enviar_admin + ""); //Email a enviar oculto (Gerente)

            objMail.Subject = "Servicio Help Desk: Soporte en proceso de solución.";
            objMail.Body = "<br> Le recordamos que esta direcci&oacute;n de e-mail es utilizada solamente para los env&iacute;os de la informaci&oacute;n solicitada de manera autom&aacute;tica.";
            objMail.Body += "<br> Por favor no responda con consultas personales ya que no podr&aacute;n ser respondidas.";
            objMail.Body += "<br> <br> Estimado usuario: <b></b> <br>";
            objMail.Body += "<br> Le informamos que ha sido asignado el soporte solicitado.<br>";
            //objMail.Body += "<br> Descripci&oacute;n: <br>";
            objMail.Body += "<br> No. de referencia: <b>" + cod_soporte + "</b> <br>";
            objMail.Body += "<br> Fecha: <b>" + DateTime.Now.ToString("dd/MM/yyyy") + "</b> <br>";
            objMail.Body += "<br> Asignado a: <b>" + Usuario_Asignado.ToUpper() + "</b> <br>";
            //objMail.Body += "<br> Fecha limite de la solicitud: <b>" + TextBox10.Text + "</b> <br>";
            //objMail.Body += "<br> Tipo de solicitud: <b>" + DropDownList1.SelectedItem.Text + "</b> <br>";
            //objMail.Body += "<br> Observaciones: <b>" + TextBox9.Text + "</b> <br>";
            //objMail.Body += "<br> Click aquí para ingresar a la aplicacion: <a href=\"" + Vinculo + "\">" + Vinculo + "</a></b> <br>";
            objMail.Body += "<br> <br> <br> Saludos cordiales.";
            objMail.Body += "<br> <b>Help Desk</b><br>";
            
            objMail.IsBodyHtml = true; //Formato Html del email
            //objMail.Attachments.Add(new Attachment(MapPath("~/archivos/") + Label1.Text));
            //objMail.Attachments.RemoveAt(Label1.Text);

            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = "mail.tecnicasfinancierassa.com"; //el nombre del servidor de correo
            //SmtpMail.Port = 587; //asignamos el numero de puerto

            SmtpMail.Credentials = new System.Net.NetworkCredential("davidhernandez@tecnicasfinancierassa.com", "cambio123");
            SmtpMail.Send(objMail); //Enviamos el correo

        }
        public void Traer_Datos_Email()
        {
            //1. Establecer la cadena de conexion
            con.ConnectionString = Funciones.ConexionOra(ruta);

            //2. Establecer el comando
            OracleCommand OracleCmd = new OracleCommand();
            OracleCmd.Connection = con;//La conexion que va a usar el comando
            OracleCmd.CommandText = "Pro_Traer_Datos_Email";//El comando a ejecutar
            OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

            //3.parametros con valores
            OracleCmd.Parameters.Add("vUserSolicitante", OracleType.VarChar, 50).Value = "" + usuario_solicitante_sop + "";
            OracleCmd.Parameters.Add("vUserEscalado", OracleType.VarChar, 50).Value = "" + usuario_esc + "";
            OracleCmd.Parameters.Add("Cursor_Traer_Datos_Email", OracleType.Cursor).Direction = ParameterDirection.Output;

            //4.Abrir la conexión y crear el DataAdapter
            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(OracleCmd);

            //5.Salida de los resultados y cerrar la conexión.
            DataTable DT = new DataTable();
            da.Fill(DT);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DR = DT.Rows[i];
                    Email_enviar = DR["Email_enviar"].ToString();
                    Email_enviar_copia = DR["Email_enviar_copia"].ToString();
                    Email_enviar_oculto = DR["Email_enviar_oculto"].ToString();
                    Email_enviar_admin = DR["Email_enviar_admin"].ToString();
                    Usuario_Solicitante = DR["Usuario_Solicitante"].ToString();
                    Usuario_Asignado = DR["Usuario_Asignado"].ToString();
                }
            }
            else
            {
                Session["mensaje"] = "Error al cargar información de notificación por correo electrónico.";
            }
        }

    }
}