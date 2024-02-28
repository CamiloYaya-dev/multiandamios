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

namespace HelpDesk
{
    public partial class vistausuario : System.Web.UI.Page
    {
        tec_user.funciones Funciones = new tec_user.funciones(); // Instancia la conexión Oracle
        tec_user.DataBaseOra DataBaseOra = new tec_user.DataBaseOra(); // Instancia para uso FTP
        //tec_user.funciones Convertida = new tec_user.funciones(); // Instancia para grabar logs de errores

        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public DataRow DR;
        public OracleDataAdapter DA;
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

            if (Session["login"].ToString() == ("admin").ToUpper())
            {
                Modificar.Visible = true;
            }
            perfil = Convert.ToInt32(Session["perfil"]);
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

            if (Page.IsPostBack == false)
            {
                Guardar.Visible = true;
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

        protected void Nuevo_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Modificar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistagerente.aspx");
        }

        protected void Eliminar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Guardar_Click(object sender, ImageClickEventArgs e)
        {
            //if (DropDownTipo.Text != "Seleccione" && DropDownSubtipo.Text != "Seleccione" && DetalleTicket.Text.Length != 0)
            //{
            //    Guardar_Soporte();
            //    Guardar.Visible = false;
            //    ImageButton2.Visible = false;
            //    DropDownTipo.Enabled = false;
            //    DropDownSubtipo.Enabled = false;
            //    DetalleTicket.Enabled = false;
            //    //RadioButtonClase.Enabled = false;

            //    upPagina.Update();
            //}
            //else
            //{
            //    Mensaje.Visible = true;
            //    Mensaje.Text = "Faltan campos por registrar. Por favor verifique la información.";

            //    upPagina.Update();
            //}
            Session["mensaje"] = "Información guardada correctamente.";
            Response.Redirect("vistausuario.aspx");
        }

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            //btnUploadFileDelete_Click(sender, e);
            Response.Redirect("vistausuario.aspx");
        }

        protected void Copiar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void InformePDF_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ExportarExcel_Click(object sender, ImageClickEventArgs e)
        {

        }

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            //Creamos el dataTable
            DataTable DT = new DataTable();
            DT.Columns.Add("Codigo");
            DT.Columns.Add("Cantidad");
            DT.Columns.Add("Descripcion");
            DT.Columns.Add("ValUnitario");
            DT.Columns.Add("ValTotal");

            DataRow Fila = DT.NewRow();
            Fila[0] = 1;
            Fila[1] = txtCantidad.Text;
            Fila[2] = txtDescripcion.Text;
            Fila[3] = txtValUnitario.Text;
            Fila[4] = txtValTotal.Text;

            DT.Rows.Add(Fila);

            //Guardamos la información en una variable de sesión
            Session["DT"] = DT;

            //Asignamos el DT al gridview (en este momento el DT esta vacio
            GridViewObra.DataSource = DT;
            GridViewObra.DataBind();



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

        protected void GridViewObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewObra_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewObra.PageIndex = e.NewPageIndex;
            //Traer_Listado();
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
                //TableCell ColumnaClase = FilaSeleccionada.Cells[3];
                //Session["cod_soporte"] = ColumnaCodigo.Text;

                //btnUploadFileDelete_Click(sender, e);
                Response.Redirect("detallesoporte.aspx");
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
            Response.Redirect("LoginPage.aspx");
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

