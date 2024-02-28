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
    public partial class vistaInicio : System.Web.UI.Page
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

            //if (Session["login"].ToString() == ("admin").ToUpper())
            //{
            //    Modificar.Visible = true;
            //}
            perfil = Convert.ToInt32(Session["perfil"]);
            cod_usuario = Convert.ToInt32(Session["cod_usuario"]);

            if (perfil != 1)
            {
                btnCrearInventario.Visible = false;
            }

            Mensaje.Text = Session["mensaje"].ToString();
            Session["mensaje"] = "";
       }

        //Código que maneja la barra de herramientas +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region Barra_Herramientas

        protected void Inicio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaInicio.aspx");
        }        

        #endregion
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected void btnCrearClienteObra_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaCrearClienteObra.aspx");
        }

        protected void btnCrearInventario_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaCrearInventario.aspx");
        }

        protected void btnNuevaRemision_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaRemision.aspx");
        }

        protected void btnReporteRemision_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaReporteRemision.aspx");
        }
        protected void btnReporteDia_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaReporteDia.aspx");
        }
        protected void btnReporteHistorico_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaReporteHistorico.aspx");
        }
        protected void btnRecordatorio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vistaRecordatorio.aspx");
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

