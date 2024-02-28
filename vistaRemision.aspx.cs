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
    public partial class vistaRemision : System.Web.UI.Page
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
        public int datodevuelto;
        public string login;
        public int perfil;
        public int cod_usuario;
        public string cod_soporte;
        public string nom_tot_anexo;
        public int estado_remision; // 1: ACTIVO 2: ANULADO
        
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

                if (Session["tipo_remision"] != null)
                {
                    listTipoRemision.SelectedValue = Convert.ToString(Session["tipo_remision"]);
                }
                
                TraerClienteObra();
                TraerMaterial();
                //Session["DT_Material"] = null;
                Session["tipo_reg"] = "Guardar";                

                if (Session["cod_remision"] != null)
                {
                    txtRemision.Text = Convert.ToString(Session["cod_remision"]);
                    Session["cod_remision"] = null;
                    ProcesoBuscarRemision();
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
            Response.Redirect("vistaRemision.aspx");
        }

        #endregion
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void TraerClienteObra()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerClienteObra", conn);
                cmd.CommandType = CommandType.StoredProcedure;

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
               
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public void TraerMaterial()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerMaterial", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);

                listMaterial.Items.Clear();
                listMaterial.DataSource = DT;
                listMaterial.Items.Add("Seleccione"); // Funciona con AppendDataBoundItems="true"
                listMaterial.DataValueField = "IdInventario";
                listMaterial.DataTextField = "Material";
                listMaterial.DataBind();
                conn.Close();

            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        protected void listMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMaterial.SelectedValue != "Seleccione" && txtCantidad.Text.Length != 0)
            {
                RegistrarMaterial(listMaterial.SelectedValue, txtCantidad.Text);
                ManejoBotones();
            }
        }

        public void ManejoBotones()
        {
            if (Session["tipo_reg"] == "Actualizar")
            {
                lbActualizarRemision.Visible = true;
                btnActualizarRemision.Visible = true;
                lbGuardarRemision.Visible = false;
                btnGuardarRemision.Visible = false;                    
            }
            else
            {
                lbGuardarRemision.Visible = true;
                btnGuardarRemision.Visible = true;
                lbActualizarRemision.Visible = false;
                btnActualizarRemision.Visible = false;
            } 
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (listMaterial.SelectedValue != "Seleccione" && txtCantidad.Text.Length != 0)
            {
                RegistrarMaterial(listMaterial.SelectedValue, txtCantidad.Text);
                ManejoBotones();  
            }
        }

        public bool RegistrarMaterial(string s_inventario, string s_cantidad) 
        {
            try
            {
                #region RegistrarMaterial

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_RegistrarMaterial", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@IdRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);
                cmd.Parameters.AddWithValue("@IdInventario", s_inventario);
                cmd.Parameters.AddWithValue("@nCantidad", s_cantidad);                

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);

                //if (DT.Columns.Count == 1)
                //{
                //    Mensaje.Text = "La cantidad disponible para este material es de " + DT.Rows[0].ItemArray[0] + " unidad(es).";
                //}

                if (Session["DT_Material"] == null)
                {
                    // Primero llena la grilla con la información de material nuevo
                    if (listTipoRemision.SelectedValue == "1")
                    {
                        // Registra el material ENTREGA
                        GridViewRemisionInventario.DataSource = DT;
                        GridViewRemisionInventario.DataBind();
                        Session["DT_Material"] = DT;
                    }
                    else
                    {
                        // Registra el material RETIRO
                        GridViewRemisionRetiro.DataSource = DT;
                        GridViewRemisionRetiro.DataBind();
                        Session["DT_Material"] = DT;
                    }
                }
                else
                {
                    // Borra el registro si ya existe para actualizarlo con el nuevo
                    DataTable DT_Material = (Session["DT_Material"]) as DataTable;
                    foreach (DataRow dr in DT_Material.Rows)
                    {
                        if (dr["Codigo"].ToString() == listMaterial.SelectedValue.ToString())
                        {
                            //dr.Delete();
                            DT_Material.Rows.Remove(dr);
                            break;
                        }
                    }

                    // Enumera los ítems
                    int NumReg = 0;
                    foreach (DataRow dr in DT_Material.Rows)
                    {
                        dr["NumReg"] = NumReg + 1;
                        NumReg++;
                    }

                    if (listTipoRemision.SelectedValue == "1")
                    {
                        // Registra el material ENTREGA
                        DataRow Row1;
                        Row1 = DT_Material.NewRow();
                        Row1["Codigo"] = DT.Rows[0].ItemArray[0];
                        Row1["NumReg"] = NumReg + 1;
                        Row1[2] = DT.Rows[0].ItemArray[2]; // Cantidad
                        Row1["Material"] = DT.Rows[0].ItemArray[3];
                        DT_Material.Rows.Add(Row1);
                        GridViewRemisionInventario.DataSource = DT_Material;
                        GridViewRemisionInventario.DataBind();
                        Session["DT_Material"] = DT_Material;
                    }
                    else
                    {
                        // Registra el material RETIRO
                        DataRow Row1;
                        Row1 = DT_Material.NewRow();
                        Row1["Codigo"] = DT.Rows[0].ItemArray[0];
                        Row1["NumReg"] = NumReg + 1;
                        Row1[2] = DT.Rows[0].ItemArray[2]; // Retiro
                        Row1["Pendiente"] = DT.Rows[0].ItemArray[3]; 
                        Row1["Material"] = DT.Rows[0].ItemArray[4];
                        DT_Material.Rows.Add(Row1);
                        GridViewRemisionRetiro.DataSource = DT_Material;
                        GridViewRemisionRetiro.DataBind();
                        Session["DT_Material"] = DT_Material;
                    }
                }

                LimpiarMaterialCantidad();
                conn.Close();

                #endregion
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al insertar.", ex);
            }
        }           

        protected void btnGuardarRemision_Click(object sender, ImageClickEventArgs e)
        {
            if (txtRemision.Text.Length == 0 || txtFechaRegistro.Text.Length == 0 || listClienteObra.SelectedValue == "Seleccione")
            {
                Mensaje.Text = "Faltan campos por registrar. Favor verificar.";
            }
            else
            {
                if (GuardarRemision())
                {
                    ModalPopupOK.Show();
                    ModalPopupOK.Enabled = true;
                }
                LimpiarMaterialCantidad();
                lbGuardarRemision.Visible = false;
                btnGuardarRemision.Visible = false;                 
            }
        }

        protected void btnActualizarRemision_Click(object sender, ImageClickEventArgs e)
        {
            if (txtRemision.Text.Length == 0 || txtFechaRegistro.Text.Length == 0 || listClienteObra.SelectedValue == "Seleccione")
            {
                Mensaje.Text = "Faltan campos por registrar. Favor verificar.";
            }
            else
            {
                if(ActualizarRemision())
                {
                    ModalPopupOK.Show();
                    ModalPopupOK.Enabled = true;
                }
                LimpiarMaterialCantidad();
                lbActualizarRemision.Visible = false;
                btnActualizarRemision.Visible = false;
            }
        }

        protected void btnBuscarRemision_Click(object sender, ImageClickEventArgs e)
        {
            ProcesoBuscarRemision();            
        }

        public void ProcesoBuscarRemision()
        {
            if (BuscarRemision())
            {
                lbNuevaRemision.Visible = true;
                btnNuevaRemision.Visible = true;

                if (estado_remision == 1) // ACTIVO
                {
                    Session["tipo_reg"] = "Actualizar";
                    lbActualizarRemision.Visible = true;
                    lbGuardarRemision.Visible = false;
                    btnActualizarRemision.Visible = true;
                    btnGuardarRemision.Visible = false;
                    LabelAnular.Visible = true;
                    btnAnular.Visible = true;

                    if (listTipoRemision.SelectedValue == "2") // Si es 'Remisión de Retiro' trae el 'Reporte al día'
                    {                        
                        if (TraerReporteAlDia())
                        {
                            LabelReporte.Visible = true;
                            lbCrearRemision.Visible = true;
                            btnCrearRemision.Visible = true;
                        }
                        else
                        {
                            LabelReporte.Visible = false;
                            lbCrearRemision.Visible = false;
                            btnCrearRemision.Visible = false;
                        }
                    }
                }
                else // ANULADO
                {
                    lbGuardarRemision.Visible = false;
                    btnGuardarRemision.Visible = false;
                    LabelAnulado.Visible = true;
                    txtRemision.Enabled = false;
                    listTipoRemision.Enabled = false;
                    txtFechaRegistro.Enabled = false;
                    listClienteObra.Enabled = false;
                    listMaterial.Enabled = false;
                    txtCantidad.Enabled = false;
                    LabelAnular.Visible = false;
                    btnAnular.Visible = false;
                    btnBuscarRemision.Visible = false;
                    lbActualizarRemision.Visible = false;
                    btnActualizarRemision.Visible = false;
                    LabelReporte.Visible = false;
                    if (listTipoRemision.SelectedValue == "1") // Remisión Inventario
                    {
                        GridViewRemisionInventario.Columns[4].Visible = false;
                    }
                    else // Remisión Retiro
                    {
                        GridViewRemisionRetiro.Columns[5].Visible = false;
                    }                    
                }                
            }
        }

        public void LimpiarMaterialCantidad()
        {
            TraerMaterial();
            txtCantidad.Text = null;
            Session["cod_remision"] = null;
        }

        public void LimpiarGridViews()
        {
            GridViewRemisionInventario.DataSource = null;
            GridViewRemisionInventario.DataBind();
            GridViewRemisionRetiro.DataSource = null;
            GridViewRemisionRetiro.DataBind();
            Session["DT_Material"] = null;
        }

        public bool GuardarRemision()
        {
            try
            {
                DataTable DT_Material = (Session["DT_Material"]) as DataTable;
                
                #region GuardarRemision
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_GuardarRemision", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdObra", listClienteObra.SelectedValue);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);
                cmd.Parameters.AddWithValue("@dFechaRegistro", txtFechaRegistro.Text);
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    datodevuelto = dr.GetInt32(0);
                }
                conn.Close();

                if (datodevuelto == -1) // Ya no se usa esta opción debido a que se puede actualizar la información
                {
                    Mensaje.Text = "Ya existe remisión con este número y tipo. Favor verificar.";
                }
                #endregion
                #region GuardarRemisionInventario
                else
                {
                    BorrarRemisionInventario();
                    foreach (DataRow dtRow in DT_Material.Rows)
                    {

                        conn.Open();
                        SqlCommand cmd2 = new SqlCommand("SP_GuardarRemisionInventario", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;

                        cmd2.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                        cmd2.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);
                        cmd2.Parameters.AddWithValue("@IdInventario", Convert.ToInt32(dtRow[0]));
                        cmd2.Parameters.AddWithValue("@nCantidad", Convert.ToInt32(dtRow[2]));   
                        cmd2.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                        SqlDataReader dr2 = cmd2.ExecuteReader();

                        if (dr2.Read())
                        {
                            datodevuelto = dr2.GetInt32(0);
                        }
                        conn.Close();
                    }

                    Mensaje.Text = "Información guardada correctamente.";
                }
                #endregion
                // Se limpia la tabla
                DT_Material = null;
                Session["DT_Material"] = null;
                GridViewRemisionInventario.DataSource = DT_Material;
                GridViewRemisionInventario.DataBind();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public bool ActualizarRemision()
        {
            try
            {
                DataTable DT_Material = (Session["DT_Material"]) as DataTable;

                #region ActualizarRemision
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ActualizarRemision", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdObra", listClienteObra.SelectedValue);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);
                cmd.Parameters.AddWithValue("@dFechaRegistro", txtFechaRegistro.Text);
                cmd.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    datodevuelto = dr.GetInt32(0);
                }
                conn.Close();

                #endregion

                #region GuardarRemisionInventario

                // Borra para guardar actualizados los nuevos materiales
                if (DT_Material != null)
                {
                    BorrarRemisionInventario();
                    foreach (DataRow dtRow in DT_Material.Rows)
                    {

                        conn.Open();
                        SqlCommand cmd2 = new SqlCommand("SP_GuardarRemisionInventario", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;

                        cmd2.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                        cmd2.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);
                        cmd2.Parameters.AddWithValue("@IdInventario", Convert.ToInt32(dtRow[0]));
                        cmd2.Parameters.AddWithValue("@nCantidad", Convert.ToInt32(dtRow[2]));
                        cmd2.Parameters.AddWithValue("@vUsuario", Session["login"].ToString());

                        SqlDataReader dr2 = cmd2.ExecuteReader();

                        if (dr2.Read())
                        {
                            datodevuelto = dr2.GetInt32(0);
                        }
                        conn.Close();
                    }
                }                

                Mensaje.Text = "Información guardada correctamente.";
                
                #endregion
                // Se limpia la tabla
                DT_Material = null;
                Session["DT_Material"] = null;
                GridViewRemisionInventario.DataSource = DT_Material;
                GridViewRemisionInventario.DataBind();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al insertar.", ex);
            }
        }

        public bool BuscarRemision()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BuscarRemision", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtFechaRegistro.Text = dr.GetString(0);
                        listClienteObra.SelectedValue = dr.GetString(1);
                        estado_remision = dr.GetInt32(2);
                        
                        TraerRemisionInventario();
                        LimpiarMaterialCantidad();
                    }                    
                }
                else
                {
                    Mensaje.Text = "No existe remisión con este número y tipo.";
                    LimpiarMaterialCantidad();
                    return false;
                }
                            
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public void TraerRemisionInventario()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_TraerRemisionInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);

                DA = new SqlDataAdapter(cmd);

                DataTable DT = new DataTable();
                DA.Fill(DT);
                if (listTipoRemision.SelectedValue == "1") // Remisión de Entrega
                {
                    GridViewRemisionInventario.DataSource = DT;
                    GridViewRemisionInventario.DataBind();
                    GridViewRemisionRetiro.DataSource = null;
                    GridViewRemisionRetiro.DataBind();
                    LabelReporte.Visible = false;
                    GridViewReporte.DataSource = null;
                    GridViewReporte.DataBind();
                }
                else // Remisión de Retiro
                {
                    GridViewRemisionRetiro.DataSource = DT;
                    GridViewRemisionRetiro.DataBind();
                    GridViewRemisionInventario.DataSource = null;
                    GridViewRemisionInventario.DataBind();
                }
                
                Session["DT_Material"] = DT;
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

        protected void GridViewRemisionInventario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewRemisionInventario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRemisionInventario.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }

        protected void GridViewRemisionRetiro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRemisionRetiro.PageIndex = e.NewPageIndex;
            //Traer_Listado();
        }

        protected void ListTipoRemision_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (listTipoRemision.SelectedValue == "1")
            {
                Session["tipo_remision"] = "1";
                Response.Redirect("vistaRemision.aspx");
            }
            else
            {
                Session["tipo_remision"] = "2";
                Response.Redirect("vistaRemision.aspx");
            }
        }

        protected void listClienteObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTipoRemision.SelectedValue == "2") // Si es 'Remisión de Retiro' trae el 'Reporte al día'
            {
                if (listClienteObra.SelectedValue != "Seleccione")
                {
                    if (TraerReporteAlDia())
                    {
                        LabelReporte.Visible = true;
                        lbCrearRemision.Visible = true;
                        btnCrearRemision.Visible = true;
                    }
                    else
                    {
                        LabelReporte.Visible = false;
                        lbCrearRemision.Visible = false;
                        btnCrearRemision.Visible = false;
                    }
                    LimpiarGridViews();
                } 
            }
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

        protected void GridViewRemisionInventario_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Remover")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewRemisionInventario.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                int reg = Convert.ToInt32(ColumnaCodigo.Text);

                BorrarRegistro(reg);
                LimpiarMaterialCantidad();
                if (GridViewRemisionInventario.Rows.Count > 0)
                {
                    if (Session["tipo_reg"] == "Actualizar")
                    {
                        lbActualizarRemision.Visible = true;
                        btnActualizarRemision.Visible = true;
                        lbGuardarRemision.Visible = false;
                        btnGuardarRemision.Visible = false;
                    }
                    else
                    {
                        lbGuardarRemision.Visible = true;
                        btnGuardarRemision.Visible = true;
                        lbActualizarRemision.Visible = false;
                        btnActualizarRemision.Visible = false;
                    }
                }
                else
                {
                    lbGuardarRemision.Visible = false;
                    btnGuardarRemision.Visible = false;
                    lbActualizarRemision.Visible = false;
                    btnActualizarRemision.Visible = false;
                }
                //BuscarRemision();
            }
        }

        protected void GridViewRemisionRetiro_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Remover")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridViewRemisionRetiro.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[0];
                int reg = Convert.ToInt32(ColumnaCodigo.Text);

                BorrarRegistro(reg);
                LimpiarMaterialCantidad();
                if (GridViewRemisionRetiro.Rows.Count > 0)
                {
                    if (Session["tipo_reg"] == "Actualizar")
                    {
                        lbActualizarRemision.Visible = true;
                        btnActualizarRemision.Visible = true;
                        lbGuardarRemision.Visible = false;
                        btnGuardarRemision.Visible = false;
                    }
                    else
                    {
                        lbGuardarRemision.Visible = true;
                        btnGuardarRemision.Visible = true;
                        lbActualizarRemision.Visible = false;
                        btnActualizarRemision.Visible = false;
                    }
                }
                else
                {
                    lbGuardarRemision.Visible = false;
                    btnGuardarRemision.Visible = false;
                    lbActualizarRemision.Visible = false;
                    btnActualizarRemision.Visible = false;
                }
                //BuscarRemision();
            }
        }


        public void BorrarRegistro(int reg) // Borra el registro del DataTable
        {
            try
            {
                DataTable DT_Material = (Session["DT_Material"]) as DataTable;
                foreach (DataRow dr in DT_Material.Rows)
                {
                    if (dr["Codigo"].ToString() == reg.ToString())
                    {
                        //dr.Delete();
                        DT_Material.Rows.Remove(dr);
                        break;
                    }
                }

                // Enumera los ítems
                int NumReg = 0;
                foreach (DataRow dr in DT_Material.Rows)
                {
                    dr["NumReg"] = NumReg + 1;
                    NumReg++;
                }

                // Acutaliza GridView correspondiente
                if (listTipoRemision.SelectedValue == "1")
                {
                    GridViewRemisionInventario.DataSource = DT_Material;
                    GridViewRemisionInventario.DataBind();
                }
                else 
                {
                    GridViewRemisionRetiro.DataSource = DT_Material;
                    GridViewRemisionRetiro.DataBind();
                }
                Session["DT_Material"] = DT_Material;
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public void BorrarRemisionInventario() //Elimina el inventario total de la remisión para actualizar la información
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_BorrarRemisionInventario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);

                SqlDataReader dr = cmd.ExecuteReader();

                conn.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al buscar.", ex);
            }
        }

        public bool TraerReporteAlDia()
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

                foreach (GridViewRow row in GridViewReporte.Rows) // Si trae registros retorna TRUE
                {
                    return true;
                }
                return false;                
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error al buscar.", ex);
            }
        }

        protected void GridViewReporte_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
        }
        
        protected void GridViewReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewReporte.PageIndex = e.NewPageIndex;
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

        protected void btnAnular_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupConfirmar.Show();
            ModalPopupConfirmar.Enabled = true;
        }

        protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
        {
            if (AnularRemision())
            {
                MensajeModal.Text = "Remisión anulada correctamente.";
            }
            else
            {
                MensajeModal.Text = "La remisión no puede ser anulada. Favor verificar.";
            }
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
            ProcesoBuscarRemision();
        }

        protected void btnSalirModalOK_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupOK.Hide();
            ModalPopupOK.Enabled = false;
            Response.Redirect("vistaRemision.aspx");
        }

        public bool AnularRemision()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_AnularRemision", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nRemision", txtRemision.Text);
                cmd.Parameters.AddWithValue("@IdTipoRemision", listTipoRemision.SelectedValue);
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

        protected void btnCrearRemision_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarGridViews();
            foreach (GridViewRow row in GridViewReporte.Rows)
            {
                TableCell txtIdInventario = row.Cells[0];
                TextBox txtCantRetiro = row.Cells[3].FindControl("txtColCantRetiro") as TextBox;
                if (txtCantRetiro.Text.Length != 0 && Convert.ToInt32(txtCantRetiro.Text) > 0)
                {
                    if (!RegistrarMaterial(txtIdInventario.Text, txtCantRetiro.Text))
                    {
                        Mensaje.Text = "Error registrando material. Favor consultar al administrador del sistema.";
                        return;
                    }
                }
            }
            ManejoBotones();
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
        
    }
}

