using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class hvpagaduria_Controls_Direcciones : System.Web.UI.UserControl
{
    public event EventHandler Ocultar;
    tec_user.DataBase Funciones = new tec_user.DataBase();
    tec_user.funciones Convertida = new tec_user.funciones();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (ViewState["EsVisible"] != null)
            {
                if ((bool)ViewState["EsVisible"])
                {
                    
                        ScriptManager.RegisterClientScriptBlock(upDireccionCiudad, typeof(UpdatePanel), "MitadPantallaDireccion", "var Motivos = document.getElementById('" + divDireccionCiudad.ClientID + "'); var ScrollArriba; if (navigator.userAgent.indexOf('MSIE') >= 0) { ScrollArriba = document.documentElement.scrollTop; }else{ ScrollArriba = document.body.scrollTop; } var Derecha =  (window.screen.availWidth / 2) - (Motivos.offsetWidth / 2);  Motivos.style.left = Derecha+'px'; var Arriba =  (window.screen.availHeight / 2) + ScrollArriba - (Motivos.offsetHeight / 2); Motivos.style.top = Arriba+'px';", true);
                        //ScriptManager.RegisterClientScriptBlock(upDireccionCiudad, typeof(UpdatePanel), "OscureceFondoDireccion", "var Fondo = document.getElementById('" + FondoDireccion.ClientID + "'); Fondo.style.height = document.body.clientHeight+'px';", true);
                    
                }
            }
        }
    }
    public void Show()
    {
        Traer_Complementos();
        divDireccionCiudad.Style["display"] = "block";
        FondoDireccion.Style["display"] = "block";
        if (ViewState["EsVisible"] == null) { ViewState.Add("EsVisible", true); } else { ViewState["EsVisible"]=true;}

        ScriptManager.RegisterClientScriptBlock(upDireccionCiudad, typeof(UpdatePanel), "MitadPantallaDireccion", "var Motivos = document.getElementById('" + divDireccionCiudad.ClientID + "'); var ScrollArriba; if (navigator.userAgent.indexOf('MSIE') >= 0) { ScrollArriba = document.documentElement.scrollTop; }else{ ScrollArriba = document.body.scrollTop; } var Derecha =  (window.screen.availWidth / 2) - (Motivos.offsetWidth / 2);  Motivos.style.left = Derecha+'px'; var Arriba =  (window.screen.availHeight / 2) + ScrollArriba - (Motivos.offsetHeight / 2); Motivos.style.top = Arriba+'px'; Motivos.style.display = 'block';", true);
        //ScriptManager.RegisterClientScriptBlock(upDireccionCiudad, typeof(UpdatePanel), "OscureceFondoDireccion", "var Fondo = document.getElementById('" + FondoDireccion.ClientID + "'); Fondo.style.height = document.body.clientHeight+'px';", true);
        upDireccionCiudad.Update();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        TextBox2.Text = "";
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (TextBox1.Text != "")
        {
            TextBox2.Text = TextBox2.Text + " " + TextBox1.Text;
            TextBox1.Text = "";
            Label3.Text = "";
        }
        else
        {
            Label3.Text = "Debe ingresar datos en el campo de texto";
        }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox2.Text != "" && DropDownList3.Text != "Seleccione")
        {
            TextBox2.Text = TextBox2.Text + " " + DropDownList3.SelectedValue;
            DropDownList3.SelectedValue = "Seleccione";
        }
        else
        {
            TextBox2.Text = DropDownList3.Text;
            DropDownList3.SelectedValue = "Seleccione";
        }
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox2.Text != "" && DropDownList4.Text != "Seleccione")
        {
            TextBox2.Text = TextBox2.Text + " " + DropDownList4.SelectedValue;
            DropDownList4.SelectedValue = "Seleccione";
        }
        else
        {
            TextBox2.Text = DropDownList4.Text;
            DropDownList4.SelectedValue = "Seleccione";
        }

    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox2.Text != "" && DropDownList5.Text != "Seleccione")
        {
            TextBox2.Text = TextBox2.Text + " " + DropDownList5.SelectedValue;
            DropDownList5.SelectedValue = "Seleccione";
        }
        else
        {
            TextBox2.Text = DropDownList5.Text;
            DropDownList5.SelectedValue = "Seleccione";
        }

    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox2.Text != "" && DropDownList6.Text != "Seleccione")
        {
            TextBox2.Text = TextBox2.Text + " " + DropDownList6.SelectedValue;
            DropDownList6.SelectedValue = "Seleccione";
        }
        else
        {
            TextBox2.Text = DropDownList6.Text;
            DropDownList6.SelectedValue = "Seleccione";
        }
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox2.Text != "" && DropDownList7.Text != "Seleccione")
        {
            TextBox2.Text = TextBox2.Text + " " + DropDownList7.SelectedValue;
            DropDownList7.SelectedValue = "Seleccione";
        }
        else
        {
            TextBox2.Text = DropDownList7.Text;
            DropDownList7.SelectedValue = "Seleccione";
        }
    }

    private void Traer_Complementos()
    {
        try
        {
            string ubicacion = HttpContext.Current.Server.MapPath(".").ToString();
            string comando = "Traer_complementos";

            Funciones.BaseDatos(ubicacion);
            Funciones.Conectar();
            Funciones.CrearComando(comando);

            SqlDataAdapter da = Funciones.EjecutarConsultaAdap();

            DataTable DT = new DataTable();
            da.Fill(DT);
            Funciones.Desconectar();

            DropDownList7.DataSource = DT;
            DropDownList7.DataValueField = "Abreviatura";
            DropDownList7.DataTextField = "Nombre";
            DropDownList7.DataBind();
            DropDownList7.Items.Add("Seleccione");
            DropDownList7.SelectedValue = "Seleccione";
        }
        catch (Exception ex)
        {
            //Convertida.GrabarErroresLog(ex.Message, HttpContext.Current.Server.MapPath(".").ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ex.Source, ex.StackTrace);
        }
    }

    public string Direccion
    {
        get { return TextBox2.Text.Trim();}
    }

    protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
    {
        divDireccionCiudad.Style["display"] = "none";
        FondoDireccion.Style["display"] = "none";
        if (ViewState["EsVisible"] == null) { ViewState.Add("EsVisible", false); } else { ViewState["EsVisible"] = false; }
        OnOcultar(new EventArgs());
        upDireccionCiudad.Update();
    }
    protected void Cerrar_Click(object sender, EventArgs e)
    {
        TextBox2.Text = string.Empty;        
        FondoDireccion.Style["display"] = "none";
        divDireccionCiudad.Style["display"] = "none";
        if (ViewState["EsVisible"] == null) { ViewState.Add("EsVisible", false); } else { ViewState["EsVisible"] = false; }
        upDireccionCiudad.Update();
    }
    protected virtual void OnOcultar(EventArgs e)
    {
        if (Ocultar != null)
        {
            Ocultar(this, e);
        }
    }
        
}
