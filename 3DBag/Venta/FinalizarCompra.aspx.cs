using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class FinalizarCompra : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();

        int IdVenta = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                IdVenta = Convert.ToInt32(Session["IdVenta"]);
                TraerDetalleVenta();
            }
        }

        #region metodos


        void TraerDetalleVenta()
        {
            lblClienteResp.Text = GestorDV.SeleccionarNick(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            lblIdVentaResp.Text = Session["IdVenta"].ToString();
            TraerPDF();
        }

        //traer el nombre del pdf creado x medio de la url
        //llenar el iframe con ese pdf
        void TraerPDF()
        {
            viewPDF.Src = "https://localhost:44388/Facturas/" + Session["NombreFactura"];
        }
        #endregion

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            GestorVenta.Vender(Convert.ToInt32(Session["IdVenta"]));
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Venta Realizada", "Baja", 0);
            Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));
        }
    }
}