using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class hvpagaduria_Controls_MessageBox : System.Web.UI.UserControl
{
    public string ELMensaje;
    public event EventHandler Ocultar;
    public enum MessageOptions
    {
        SiNo,
        SiNoCancelar,
        AceptarCancelar,
        Aceptar
    }
    public enum MessageValues
    {
        Si=1,
        No=2,
        Aceptar=3,
        Cancelar=4
    }
    public MessageValues Respuesta;
    public void Show(string Mensaje, string Titulo, MessageOptions Opciones)
    {
        ELMensaje= Mensaje;
        lblTitulo.Text = Titulo;
        switch (Opciones)
        {
            case MessageOptions.Aceptar: 
                btAceptar.Visible = true;
                btCancelar.Visible = false;
                btSi.Visible = false;
                btNo.Visible = false;
                break;
            case MessageOptions.AceptarCancelar:
                btAceptar.Visible = true;
                btCancelar.Visible = true;
                btSi.Visible = false;
                btNo.Visible = false;
                break;
            case MessageOptions.SiNo:
                btAceptar.Visible = false;
                btCancelar.Visible = false;
                btSi.Visible = true;
                btNo.Visible = true;
                break;
            case MessageOptions.SiNoCancelar:
                btAceptar.Visible = false;
                btCancelar.Visible = true;
                btSi.Visible = true;
                btNo.Visible = true;
                break;
        }
        divMensaje.Style["display"] = "block";        
        FondoMensaje.Style["display"] = "block";

        ScriptManager.RegisterClientScriptBlock(upMessagebox, typeof(UpdatePanel), "MitadPantallaMensaje", "var Motivos = document.getElementById('" + divMensaje.ClientID + "'); var ScrollArriba; if (navigator.userAgent.indexOf('MSIE') >= 0) { ScrollArriba = document.documentElement.scrollTop; }else{ ScrollArriba = document.body.scrollTop; } var Derecha =  (window.screen.availWidth / 2) - (Motivos.offsetWidth / 2);  Motivos.style.left = Derecha+'px'; var Arriba =  (window.screen.availHeight / 2) + ScrollArriba - (Motivos.offsetHeight / 2); Motivos.style.top = Arriba+'px'; Motivos.style.display = 'block';", true);
        //ScriptManager.RegisterClientScriptBlock(upMessagebox, typeof(UpdatePanel), "OscureceFondoMensaje", "var Fondo = document.getElementById('" + FondoMensaje.ClientID + "'); Fondo.style.height = document.body.clientHeight+'px';", true);
        upMessagebox.Update();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btSi_Click(object sender, EventArgs e)
    {
        Respuesta = MessageValues.Si;
        divMensaje.Style["display"] = "none";
        FondoMensaje.Style["display"] = "none";
        OnOcultar(new EventArgs());
    }
    protected void btNo_Click(object sender, EventArgs e)
    {
        Respuesta = MessageValues.No;
        divMensaje.Style["display"] = "none";
        FondoMensaje.Style["display"] = "none";
        OnOcultar(new EventArgs());
    }
    protected void btAceptar_Click(object sender, EventArgs e)
    {
        Respuesta = MessageValues.Aceptar;
        divMensaje.Style["display"] = "none";
        FondoMensaje.Style["display"] = "none";
        OnOcultar(new EventArgs());
    }
    protected void btCancelar_Click(object sender, EventArgs e)
    {
        Respuesta = MessageValues.Cancelar;
        divMensaje.Style["display"] = "none";
        FondoMensaje.Style["display"] = "none";
        OnOcultar(new EventArgs());
    }
    protected virtual void OnOcultar(EventArgs e)
    {
        if (Ocultar != null)
        {
            Ocultar(this, e);
        }
    }
    protected void Cerrar_Click(object sender, EventArgs e)
    {
        Respuesta = MessageValues.Cancelar;
        divMensaje.Style["display"] = "none";
        FondoMensaje.Style["display"] = "none";
        OnOcultar(new EventArgs());
    }
}
