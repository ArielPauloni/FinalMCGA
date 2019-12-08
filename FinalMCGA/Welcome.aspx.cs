using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

public partial class Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            calDia.SelectedDate = DateTime.Now;
        }
    }

    protected void btnObtener_Click(object sender, EventArgs e)
    {
        if (calDia.SelectedDate != null)
        {
            if (calDia.SelectedDate > DateTime.Now)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + " No puede obtener cotizaciones futuras, no somos adivinos" + "');", true);
            }
            else
            {
                ServicioCotizaciones miServicio = new ServicioCotizaciones();
                decimal cotizacion = miServicio.CotizacionDolar(calDia.SelectedDate);
                if (cotizacion > 0)
                {
                    Session["fecha"] = calDia.SelectedDate.ToString();
                    Session["cotizacion"] = cotizacion.ToString("0.##");
                    Response.Redirect("Respuesta.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se tiene la cotización para esa fecha" + "');", true);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Seleccione una fecha por favor" + "');", true);
        }
    }

    protected void btnCotizacionBCRA_Click(object sender, EventArgs e)
    {
        if (calDia.SelectedDate != null)
        {
            if (calDia.SelectedDate > DateTime.Now)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + " No puede obtener cotizaciones futuras, no somos adivinos" + "');", true);
            }
            else
            {
                List<CotizacionBCRA> cotizaciones = new List<CotizacionBCRA>();
                string text = System.IO.File.ReadAllText(Server.MapPath("AuthenticationBCRA_Service.txt"));

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", text);

                string jsonResp = client.GetStringAsync("https://api.estadisticasbcra.com/usd").Result;
                cotizaciones = JsonConvert.DeserializeObject<List<CotizacionBCRA>>(jsonResp);

                var query = from a in cotizaciones where a.D == calDia.SelectedDate.ToString("yyyy-MM-dd").Substring(0, 10) select a;
                CotizacionBCRA cotizacion = query.FirstOrDefault();

                if (cotizacion != null)
                {
                    Session["fecha"] = calDia.SelectedDate.ToString();
                    Session["cotizacion"] = cotizacion.V.ToString("0.##");
                    Response.Redirect("Respuesta.aspx");
                }
                else { ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "No se tiene la cotización para esa fecha" + "');", true); }
            }
        }
    }

    protected void btnActualizarCotizaciones_Click(object sender, EventArgs e)
    {
        string text = System.IO.File.ReadAllText(Server.MapPath("AuthenticationBCRA_Service.txt"));

        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", text);

        string jsonResp = client.GetStringAsync("https://api.estadisticasbcra.com/usd").Result;

        if (!string.IsNullOrWhiteSpace(jsonResp))
        {
            System.IO.File.WriteAllText(Server.MapPath("CotizacionesBCRA.json"), jsonResp);

            ServicioCotizaciones miServicio = new ServicioCotizaciones();
            int resp = miServicio.ActualizarCotizaciones(Server.MapPath("CotizacionesBCRA.json"));
        }
    }
}