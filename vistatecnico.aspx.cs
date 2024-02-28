﻿using System;
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

namespace HelpDesk
{
    public partial class vistatecnico : System.Web.UI.Page
    {
        tec_user.funciones Funciones = new tec_user.funciones(); // Instancia la conexión Oracle
        public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
        OracleConnection con = new OracleConnection();
        public DataRow DR;
        public OracleDataAdapter DA;
        public DataTable DT = new DataTable();
        public string seleccion;
        int index;
        public string datodevuelto;
        public string login;
        public int perfil;
        public int cod_usuario;

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
                Escalar.Ocultar += new EventHandler(Escalar_onOcultar);
                TituloCanasta.Visible = false;
                GridCanasta.Visible = false;
                Ver_Canasta();
                
                Traer_Soporte();
            }
            Mensaje.Text = Convert.ToString(Session["mensaje"]);
            Session["mensaje"] = null;
       }

        //Código que maneja la barra de herramientas +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected void Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistausuario.aspx");
        }

        protected void Modificar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Eliminar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Guardar_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void Refrescar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistatecnico.aspx");
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
        
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void Ver_Canasta()
        {
            try
            {
                //1. Establecer la cadena de conexión
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Ver_Canasta";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores
                OracleCmd.Parameters.Add("iTecnico", OracleType.Number).Value = cod_usuario;
                OracleCmd.Parameters.Add("nRespuesta", OracleType.Number).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                OracleString rowld;
                con.Open();
                OracleCmd.ExecuteOracleNonQuery(out rowld);
                con.Close();
                datodevuelto = Convert.ToString(OracleCmd.Parameters["nRespuesta"].Value);
                if (datodevuelto == "1")
                {
                    TituloCanasta.Visible = true;
                    GridCanasta.Visible = true;
                    Traer_Soporte_Canasta();
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
                    Mensaje.Text = "Error al intentar actualizar el usuario.";
                }
                con.Close();
            }
            con.Close();
        }

        public void Traer_Soporte_Canasta()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Soporte";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. Parámetros con valores
                OracleCmd.Parameters.Add("Cursor_Traer_Soporte", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4. Abrir la conexión y crear el DataAdapter
                con.Open();
                OracleDataAdapter DA = new OracleDataAdapter(OracleCmd);

                //5. Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Soporte");
                GridCanasta.DataSource = ds.Tables["Soporte"];
                GridCanasta.DataBind();
                if (GridCanasta.Rows.Count == 0)
                {
                    Label2.Text = "No existen registros de soporte.";
                }
                else
                {
                    Label2.Text = "";
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
                    Mensaje.Text = "Error al cargar información.";
                }
                con.Close();
            }
            con.Close();
        }

        protected void GridCanasta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCanasta.PageIndex = e.NewPageIndex;
            Traer_Soporte_Canasta();
        }

        protected void GridCanasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = GridCanasta.SelectedIndex;
            Session["Seleccion"] = GridCanasta.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void GridCanasta_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Escalar'
            if (e.CommandName == "Escalar")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridCanasta.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[1];
                TableCell ColumnaCodigo1 = FilaSeleccionada.Cells[2];
                Session["cod_soporte"] = ColumnaCodigo.Text;
                Session["usuario_solicitante_sop"] = ColumnaCodigo1.Text; // Variable de sesión para el control EscalarSoporte (Usuario que solicita)
                
                // Variable de sesión para habilitar o deshabilitar escalamiento automático
                Escalar.Show(Convert.ToInt32(Session["autoescala"]));
            }
        }

        protected void GridCanasta_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Escalar_onOcultar(object sender, EventArgs e)
        {
            //txtDireccionInfoBasic.Text = dirCiudadInfoBasica.Direccion;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        public void Traer_Soporte()
        {
            try
            {
                //1. Establecer la cadena de conexion
                con.ConnectionString = Funciones.ConexionOra(ruta);

                //2. Establecer el comando
                OracleCommand OracleCmd = new OracleCommand();
                OracleCmd.Connection = con;//La conexion que va a usar el comando
                OracleCmd.CommandText = "Pro_Traer_Soporte_Tecnico";//El comando a ejecutar
                OracleCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3.parametros con valores
                OracleCmd.Parameters.Add("iUsuario", OracleType.Number).Value = cod_usuario;
                OracleCmd.Parameters.Add("Cursor_Traer_Soporte_Tecnico", OracleType.Cursor).Direction = ParameterDirection.Output;

                //4.Abrir la conexión y crear el DataAdapter
                con.Open();
                DA = new OracleDataAdapter(OracleCmd);

                ////5.Salida de los resultados y cerrar la conexión.
                DataSet ds = new DataSet();
                DA.Fill(ds, "Soporte");
                GridView1.DataSource = ds.Tables["Soporte"];
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    Label2.Text = "No existen registros.";
                }
                else
                {
                    Label2.Text = "";
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
                    Mensaje.Text = "Error al cargar información.";
                }
                con.Close();
            }
            con.Close();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Traer_Soporte();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = GridView1.SelectedIndex;
            Session["Seleccion"] = GridView1.DataKeys[index].Value.ToString();
            Mensaje.Text = "";
        }

        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // Determina el evento del botón 'Detalle'
            if (e.CommandName == "Detalle")
            {
                // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtiene el código del registro
                GridViewRow FilaSeleccionada = GridView1.Rows[index];
                TableCell ColumnaCodigo = FilaSeleccionada.Cells[1];
                TableCell ColumnaClase = FilaSeleccionada.Cells[3];
                Session["cod_soporte"] = ColumnaCodigo.Text;
                
                Response.Redirect("detallesoporte.aspx");
            }
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

        
    }
}

