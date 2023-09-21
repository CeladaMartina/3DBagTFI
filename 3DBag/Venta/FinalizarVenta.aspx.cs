using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class FinalizarVenta : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        
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
            viewPDF.Src = "https://localhost:44388/" + Session["NombreFactura"];
        }
        #endregion
    }
}