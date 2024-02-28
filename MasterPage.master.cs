using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Obtener_Fecha();
        Label2.Text = Convert.ToString(Session["login"]);
    }

    public string Obtener_Fecha()
    {
        string fecha_Dia = DateTime.Now.ToString("MMMM/dd/yyyy");
        return fecha_Dia;
    }
}
