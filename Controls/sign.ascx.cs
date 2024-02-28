using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class sign : System.Web.UI.UserControl
{
    tec_user.DataBase BD = new tec_user.DataBase();
    private string _usuario = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {        
       
        MuestraFirma();
        
    }
    public string Usuario
    {
        set { _usuario = value; }
        get { return _usuario; }
    }
    public string Titulo
    {
        set { TituloFirma.Text = value;}
        get { return TituloFirma.Text;}
    }
    protected void btFirmar_Click(object sender, EventArgs e)
    {        
        if (Session["login"] != null && Session["login"].ToString() != string.Empty)
        {
            _usuario = Session["login"].ToString();
            MuestraFirma();
        }                
    }


    private void MuestraFirma()
    {

        string Ubicacion = HttpContext.Current.Server.MapPath(".").ToString();
        BD.BaseDatos(Ubicacion);        
        BD.CrearConsulta("select * from [Usuarios]..[tusuarios] where cod_usuario = @Usuario");
        BD.AsignarParametroCadena("Usuario", _usuario);
        BD.Conectar();
        SqlDataReader drUsuario = BD.EjecutarConsulta();
        if (drUsuario.HasRows)
        {
            PanelFirma.Visible = true;
            PanelFirmar.Visible = false;
            drUsuario.Read();
            lblNombre.Text = drUsuario["txt_nombre"].ToString() + " " + drUsuario["txt_apellido"].ToString();
            lblemail.Text = drUsuario["txt_mail"].ToString();
        }
        else
        {
            PanelFirma.Visible = false;
            PanelFirmar.Visible = true;
        }
        BD.Desconectar();       
    }
}
