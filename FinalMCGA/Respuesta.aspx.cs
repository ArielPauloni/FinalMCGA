using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Respuesta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblInformacion.Text = String.Format("Cotización del dolar en la fecha {0}: ${1}", Session["fecha"].ToString().Substring(0,10), Session["cotizacion"].ToString());
    }
}