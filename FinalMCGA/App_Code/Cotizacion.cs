using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Cotizacion
/// </summary>
public class Cotizacion
{

    private int idCotizacion;

    public int IdCotizacion
    {
        get { return idCotizacion; }
        set { idCotizacion = value; }
    }

    private DateTime dia;

    public DateTime Dia
    {
        get { return dia; }
        set { dia = value; }
    }

    private decimal valor;

    public decimal Valor
    {
        get { return valor; }
        set { valor = value; }
    }
}