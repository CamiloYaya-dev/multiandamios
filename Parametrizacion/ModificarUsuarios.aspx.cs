using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


public partial class ModificarUsuarios : System.Web.UI.Page
{
    tec_user.funciones Funciones = new tec_user.funciones(); 
    public string ruta = HttpContext.Current.Server.MapPath(".").ToString(); // Obtiene la ruta actual
    OracleConnection con = new OracleConnection();
    public DataRow DR;
    public string datodevuelto;
    public string login;
    public string cod_usuario;
    public int perfil, area;
    string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
    public SqlDataAdapter DA;

    protected void Page_Load(object sender, EventArgs e)
    {
        login = Convert.ToString(Session["login"]);
        if (Session["login"] == null)
        {
            Response.Redirect("SesionKill.aspx");
        }
        
        perfil = Convert.ToInt32(Session["perfil"]);
        area = Convert.ToInt32(Session["area"]);

        Session["cod_usuario_modif"] = null; // Variable de sesión para enviar el código del usuario que se desea modificar

        if (Page.IsPostBack == false)
        {
            Mensaje.Text = Convert.ToString(Session["Mensaje"]); // Muestra mensaje si se ha guardado o eliminado información
            Session["Mensaje"] = ""; // Limpia variable de sesión de mensaje
            Traer_Usuarios();
        }
        if (Page.IsPostBack == true)
        {
            Mensaje.Text = "";
        }
    }

    public void Traer_Usuarios()
    {
        try
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_TraerUsuarios", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            DA = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            DA.Fill(ds, "Reporte");
            GridView1.DataSource = ds.Tables["Reporte"];
            GridView1.DataBind();
            conn.Close();

        }
        catch (Exception ex)
        {
            Mensaje.Text = "Error. Consulte al administrador del sistema.";
            con.Close();
        }
        con.Close();
    }

    protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        // Determina el evento del botón 'Modificar'
        if (e.CommandName == "Modificar")
        {
            // Convierte el índice de la fila almacenada en la propiedad CommandArgument en un entero
            int index = Convert.ToInt32(e.CommandArgument);
            
            // Obtiene el código del negocio y redirige la página
            GridViewRow FilaSeleccionada = GridView1.Rows[index];

            // Evita que el usuario Administrador sea modificado por otro usuario
            if (FilaSeleccionada.Cells[4].Text == "admin" || FilaSeleccionada.Cells[4].Text == "ADMIN")
            {
                Mensaje.Text = "No es posible modificar el usuario Administrador.";
                Session["cod_usuario_modif"] = null;
            }
            else
            {
                Session["cod_usuario_modif"] = FilaSeleccionada.Cells[1].Text;
                Response.Redirect("Usuarios.aspx");
            }         
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Traer_Usuarios();
    }
}
