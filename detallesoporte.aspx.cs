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
using WinSCP;
using System.Net.Mail;

namespace HelpDesk
{
    public partial class detallesoporte : System.Web.UI.Page
    {
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        tec_user.DataBaseOra DataBaseOra = new tec_user.DataBaseOra(); // Instancia para uso FTP
        tec_user.funciones Funciones = new tec_user.funciones();

        OracleConnection con = new OracleConnection();
        public OracleDataAdapter DA;
        public DataRow DR;
        public string datodevuelto;
        public string seleccion;
        int index;
        public int perfil;
        public string login;
        public int cod_usuario; 
        public int cod_soporte;

        // Enviar correo
        MailMessage objMail;
        public string Email_enviar;
        public string Email_enviar_copia;
        public string Email_enviar_oculto;
        public string Email_enviar_admin;
        public string Usuario_Solicitante;
        public string Usuario_Asignado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }

            BtDescarga.Attributes.Add("onclick", "javascript:document.getElementById('" + divCargando.ClientID + "').style.display='block'");
            
            perfil = Convert.ToInt32(Session["perfil"]);
            login = Convert.ToString(Session["login"]);
            
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);;
            cod_soporte = Convert.ToInt32(Session["cod_soporte"]);
            
            Escalar.Ocultar += new EventHandler(Escalar_onOcultar);

            Traer_Detalle();
            Traer_Seguimiento();
            Habilitar_Objetos();

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
            Response.Redirect("detallesoporte.aspx");
        }

        public void Habilitar_Objetos()
        {
            // Si el caso está 'Cerrado' no permite hacer seguimiento
            Guardar.Visible = false;
            LbCerrar.Visible = false;
            BtCerrar.Visible = false;
            LbDevolver.Visible = false;
            BtDevolver.Visible = false;
            table_seg.Visible = false;

            if (Cod_Estado.Text != "5") // Estado 'Cerrado'
            {
                // Si el caso está 'Solucionado' no permite realizar seguimiento del técnico
                if (Cod_Estado.Text == "3")  // Estado 'Solucionado'
                {
                    if (perfil == 5 || Usuario.Text.ToUpper() == login.ToUpper())
                    {
                        LbCerrar.Visible = true;
                        LbCerrar.Text = "Cerrar";
                        BtCerrar.Visible = true;
                        LbDevolver.Visible = true; // Muestra el botón para devolver el soporte solucionado
                        BtDevolver.Visible = true;
                    }
                }
                else // Si el caso se encuentra en 'Proceso de solución' se permite hacer seguimiento del técnico
                {
                    if (perfil == 5) // Sólo el usuario analista puede cerrar el caso
                    {
                        LbCerrar.Visible = true;
                        LbCerrar.Text = "Cerrar";
                        BtCerrar.Visible = true;
                    }
                    else // Otros usuarios pueden hacer seguimiento
                    {
                        LbCerrar.Visible = true;
                        LbCerrar.Text = "Caso Solucionado";
                        BtCerrar.Visible = true;
                        table_seg.Visible = true;
                        Guardar.Visible = true;
                    }
                }
            }
        }

        // Función que carga la lista principal de detalles
        public void Traer_Detalle()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Detalle";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. Parámetros con valores   
                OracleCmd.Parameters.Add("iSoporte", OracleType.Number).Value = cod_soporte;
                OracleCmd.Parameters.Add("vUsuario", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vEmail", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vClase", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vTipo", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vSubTipo", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vFecha", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("iEstado", OracleType.Number).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vEstado", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vEscalado", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vDetalle", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("vAnexo", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();

                Soporte.Text = Convert.ToString(cod_soporte);
                Usuario.Text = Convert.ToString(OracleCmd.Parameters["vUsuario"].Value);
                Email.Text = "(" + Convert.ToString(OracleCmd.Parameters["vEmail"].Value) + ")";
                Clase.Text = Convert.ToString(OracleCmd.Parameters["vClase"].Value);
                TipoSoporte.Text = Convert.ToString(OracleCmd.Parameters["vTipo"].Value);
                SubTipoSoporte.Text = Convert.ToString(OracleCmd.Parameters["vSubTipo"].Value);
                DateTime fecha_soporte = Convert.ToDateTime(OracleCmd.Parameters["vFecha"].Value);
                Fecha.Text = string.Format("{0:dd/MM/yyyy}", fecha_soporte);
                Cod_Estado.Text = Convert.ToString(OracleCmd.Parameters["iEstado"].Value);
                Estado.Text = Convert.ToString(OracleCmd.Parameters["vEstado"].Value);
                Escalado.Text = Convert.ToString(OracleCmd.Parameters["vEscalado"].Value);
                Detalle.Text = Convert.ToString(OracleCmd.Parameters["vDetalle"].Value);
                LbAnexo.Text = Convert.ToString(OracleCmd.Parameters["vAnexo"].Value);
                if (LbAnexo.Text == "")
                {
                    LbMsjAnexo.Text = "No hay archivo.";
                    BtDescarga.Visible = false;
                }
                else
                {
                    LbMsjAnexo.Text = "";
                    BtDescarga.Visible = true;
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
                    Mensaje.Text = "Error al cargar información de detalle.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Traer_Seguimiento()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Seguimiento";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores
                OracleCmd.Parameters.Add("iSoporte", OracleType.Number).Value = cod_soporte;
                OracleCmd.Parameters.Add("Cursor_Traer_Seguimiento", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4.Abrir la conexión y crear el DataAdapter
                con.Open();
                DA = new OracleDataAdapter(OracleCmd);

                ////5.Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Seguimiento");
                GridView1.DataSource = ds.Tables["Seguimiento"];
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    Mensaje2.Text = "No existen registros de seguimiento.";
                }
                else
                {
                    Mensaje2.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (Convert.ToString(Session["perfil"]) == "0")
                {
                    Response.Write("Error:" + ex.ToString());
                }
                else
                {
                    Mensaje.Text = "Error al cargar información de seguimiento.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Guardar_Seguimiento(int cod_estado_seg) // cod_estado_seg: Estado del seguimiento
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
                OracleCmd.Parameters.Add("vObservacion", OracleType.VarChar).Value = Seguimiento.Text;
                OracleCmd.Parameters.Add("iUsuario", OracleType.Number).Value = cod_usuario;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();
                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

                if (datodevuelto == "1")
                {
                    Mensaje.Text = "Información registrada correctamente.";
                    Cambiar_Estado(cod_estado_seg); // Actualiza el estado del soporte
                }
                else
                {
                    Mensaje.Text = "No se pudo registrar la información de seguimiento.";
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
                    Mensaje.Text = "Error al registrar información de seguimiento. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Cambiar_Estado(int cod_estado_seg) // cod_estado_seg: Estado del seguimiento
        {
            try
            {
                //1. Establecer la cadena de conexión
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con; // La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Cambiar_Estado"; // El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure; // Decirle al comando que va a ejecutar una sentencia SQL

                //3. Parámetros con valores
                OracleCmd.Parameters.Add("iSoporte", OracleType.Number).Value = cod_soporte;
                OracleCmd.Parameters.Add("iEstado", OracleType.VarChar).Value = cod_estado_seg;
                OracleCmd.Parameters.Add("vNomEstado", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();

                Cod_Estado.Text = Convert.ToString(cod_estado_seg);
                Estado.Text = Convert.ToString(OracleCmd.Parameters["vNomEstado"].Value);
                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);

                if (datodevuelto == "1")
                {
                    table_seg.Visible = false;
                }
                else
                {
                    Mensaje.Text = Mensaje.Text + " Error al actualizar el estado de seguimiento.";
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
                    Mensaje.Text = Mensaje.Text + " Error al actualizar el estado de seguimiento. Consulte al administrador del sistema.";
                }
                con.Close();
            }
            con.Close();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = GridView1.SelectedIndex;
            Session["Seleccion"] = GridView1.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Escalar_Click(object sender, ImageClickEventArgs e)
        {
            Session["usuario_solicitante_sop"] = Usuario.Text;
            Session["cod_soporte"] = cod_soporte;
            Escalar.Show(2); // Escalamiento manual únicamente
        }

        protected void Guardar_Click(object sender, ImageClickEventArgs e)
        {
            if (Seguimiento.Text != "")
            {
                if (perfil == 5) // Usuario Perfil Registro guarda seguimiento de devolución
                {
                    Guardar_Seguimiento(4); // Seguimiento del usuario Perfil Registro. Estado 'Devuelto'(5)
                    Enviar_Notificacion("DEVUELTO");
                }
                else
                {
                    Guardar_Seguimiento(1); // Seguimiento de técnicos y directores. Estado 'Proceso de solución'(5)
                }
                Response.Redirect("detallesoporte.aspx");
            }
            else
            {
                Mensaje.Text = "Es necesario indicar el respectivo seguimiento.";
            }
        }

        protected void BtDescarga_Click(object sender, ImageClickEventArgs e)
        {
            if (LbAnexo.Text != "")
            {
                btnUploadFile(LbAnexo.Text);
                divCargando.Style["display"] = "none";
            }
        }

        // Función que descarga el archivo
        protected void btnUploadFile(string archivo)
        {
            try
            {
                string ubicaarchivo = HttpContext.Current.Server.MapPath(".").ToString();
                ubicaarchivo += "\\Downloader\\" + archivo;

                string CnxFtp = DataBaseOra.ConexionFTP(HttpContext.Current.Server.MapPath(".").ToString());
                string[] words = CnxFtp.Split(',');
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = words[0].ToString(),
                    UserName = words[1].ToString(),
                    Password = words[2].ToString(),
                    PortNumber = Convert.ToInt32(words[3]),
                    SshHostKey = words[4].ToString()
                };

                using (Session session = new Session())
                {
                    string ubicacion = HttpContext.Current.Server.MapPath(".").ToString();

                    ubicacion += "\\Bin\\WinSCP.exe";
                    session.ExecutablePath = ubicacion;

                    // Connect
                    session.Open(sessionOptions);


                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    // Se cambió porque no es necesario crear el directorio. Ya existe.
                    string CreateDirect = @"/home/coonal/HelpDesk/" + LbAnexo.Text;

                    /*string CreateDirect = @"/home/coonal/HelpDesk";
                    try
                    {
                        session.CreateDirectory(CreateDirect);
                    }
                    catch
                    { }
                    CreateDirect = CreateDirect + @"/" + LbAnexo.Text;*/

                    TransferOperationResult transferResult;
                    transferResult = null;

                    // Carga el archivo en el directorio temporal
                    transferResult = session.GetFiles(CreateDirect, ubicaarchivo, false, transferOptions);

                    // Verifica si hay error
                    transferResult.Check();

                    // Oculta el UpdatePanel 'Espere la carga del archivo' -- Aún no funciona
                    ScriptManager.RegisterClientScriptBlock(upCargando, typeof(UpdatePanel), "OcultaCargando", "document.getElementById('" + divCargando.ClientID + "').style.display='none';", true);
                    upCargando.Update();

                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        //Response.Clear();
                        //Response.ContentType = ContentType;
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Anexo.Text);
                        //Response.WriteFile(Server.MapPath("~/Downloader/" + Anexo.Text));
                        //Response.Flush();
                        //System.IO.File.Delete(Server.MapPath("~/Downloader/" + Anexo.Text));
                        //Response.End();

                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = ContentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + LbAnexo.Text + "\"");
                        Response.TransmitFile(ubicaarchivo);
                        Response.Flush();
                        System.IO.File.Delete(ubicaarchivo);
                        Response.Close();
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                Mensaje.Text = "Error descargando el archivo.";

                //Mensaje.Text = Convert.ToString(ex);

                //Convertida.GrabarErroresLog(ex.Message, HttpContext.Current.Request.MapPath("~"), login, ex.Source, ex.StackTrace);
                //return;
            }
        }

        protected void Escalar_onOcultar(object sender, EventArgs e)
        {
            //txtDireccionInfoBasic.Text = dirCiudadInfoBasica.Direccion;
        }

        protected void Atras_Click(object sender, ImageClickEventArgs e)
        {
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

        protected void BtCerrar_Click(object sender, ImageClickEventArgs e)
        {
            if (perfil == 5 || Usuario.Text.ToUpper() == login.ToUpper())// Usuario Perfil Registro
            {
                Seguimiento.Text = "Cerrado por el usuario " + login.ToUpper() + ".";
                Guardar_Seguimiento(5); // 'Cerrado'(5) por el usuario Perfil Registro
                Enviar_Notificacion("CERRADO");
            }
            else
            {
                Seguimiento.Text = "Solucionado por el usuario " + login.ToUpper() + ".";
                Guardar_Seguimiento(3); // Se cambia a estado 'Solucionado'(3), a falta de cerrar por el usuario Perfil Registro
            }
            
            BtCerrar.Visible = false;
            LbCerrar.Visible = false;
            BtDevolver.Visible = false;
            LbDevolver.Visible = false;
            Guardar.Visible = false;
        }
        
        protected void BtDevolver_Click(object sender, ImageClickEventArgs e)
        {
            Seguimiento.Text = "";
            table_seg.Visible = true;
            Guardar.Visible = true;
            LbEscalar.Visible = false; // No habilitado para usuario analista (perfil 5)
            BtEscalar.Visible = false;
            BtDevolver.Visible = false;
            LbDevolver.Visible = false;
            Mensaje.Text = "Favor indicar seguimiento para devolver el caso.";
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
        
        public void Enviar_Notificacion(string nom_estado)
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

            objMail.Subject = "Servicio Help Desk: Soporte en estado " + nom_estado + ".";
            objMail.Body = "<br> Le recordamos que esta direcci&oacute;n de e-mail es utilizada solamente para los env&iacute;os de la informaci&oacute;n solicitada de manera autom&aacute;tica.";
            objMail.Body += "<br> Por favor no responda con consultas personales ya que no podr&aacute;n ser respondidas.";
            objMail.Body += "<br> <br> Estimado usuario: <b></b> <br>";
            //objMail.Body += "<br> Le informamos que ha sido asignado el soporte solicitado.<br>";
            //objMail.Body += "<br> Descripci&oacute;n: <br>";
            objMail.Body += "<br> El soporte con n&uacute;mero de referencia <b>" + cod_soporte + "</b> se encuentra en estado <b>" + nom_estado + ".</b><br>";
            objMail.Body += "<br> Fecha: <b>" + DateTime.Now.ToString("dd/MM/yyyy") + "</b> <br>";
            if (nom_estado == "CERRADO")
                objMail.Body += "<br> Solucionado por: <b>" + Usuario_Asignado.ToUpper() + "</b> <br>";
            else
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
            OracleCmd.Parameters.Add("vUserSolicitante", OracleType.VarChar, 50).Value = "" + Usuario.Text.ToUpper() + "";
            OracleCmd.Parameters.Add("vUserEscalado", OracleType.VarChar, 50).Value = "" + Escalado.Text.ToUpper() + "";
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