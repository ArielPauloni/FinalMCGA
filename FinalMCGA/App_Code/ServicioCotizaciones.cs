using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI_Server;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Descripción breve de ServicioCotizaciones
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class ServicioCotizaciones : System.Web.Services.WebService
{
    private SqlConnection myConnection = new SqlConnection();
    private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    public ServicioCotizaciones()
    {
    }

    [WebMethod]
    public decimal CotizacionDolar(DateTime fecha)
    {
        decimal ret = 0;
        Cotizacion cotizacion = new Cotizacion();
        string queryString = "http://localhost:53550/api/Cotizaciones?fecha=" + fecha.ToString("yyyy-MM-dd");

        HttpClient client = new HttpClient();
        var response = client.GetAsync(queryString);

        if ((response.Result != null) && (response.Result.IsSuccessStatusCode))
        {
            var jsonResp = response.Result.Content.ReadAsStringAsync();

            cotizacion = JsonConvert.DeserializeObject<Cotizacion>(jsonResp.Result);

            if (cotizacion != null) { ret = cotizacion.Valor; }
        }
        return ret;
    }

    [WebMethod]
    public int ActualizarCotizaciones(string filePath)
    {
        int ret = 0;
        myConnection.ConnectionString = ConnectionString;
        myConnection.Open();

        SqlCommand sqlCmd = new SqlCommand("dbo.pr_ActualizarCotizaciones ", myConnection);
        sqlCmd.Parameters.Add("@path", SqlDbType.VarChar).Value = filePath;
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.ExecuteNonQuery();

        if (myConnection != null && myConnection.State == ConnectionState.Open)
            myConnection.Close();

        return ret;
    }
}
