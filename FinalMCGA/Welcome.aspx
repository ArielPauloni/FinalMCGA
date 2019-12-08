<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="margin: 0px auto;">
                <tr style="text-align: center;">
                    <td>
                        <asp:Calendar ID="calDia" runat="server" Width="263px"></asp:Calendar>
                    </td>
                    <td></td>
                </tr>
                <tr style="text-align: center;">
                    <td>
                        <asp:Button ID="btnObtener" runat="server" Text="Cotizacion BD local" OnClick="btnObtener_Click" />
                    </td>
                    <td></td>
                </tr>
                <tr style="text-align: center;">
                    <td>
                        <asp:Button ID="btnCotizacionBCRA" runat="server" Text="Cotizacion BCRA" OnClick="btnCotizacionBCRA_Click" Width="171px" />
                    </td>
                    <td></td>
                </tr>
                <tr style="text-align: center;">
                    <td>
                        <asp:Button ID="btnActualizarCotizaciones" runat="server" Text="Actualizar cotizaciones desde BCRA" OnClick="btnActualizarCotizaciones_Click" Width="254px" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
