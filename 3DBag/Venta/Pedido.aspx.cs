using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Pedido : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        int IdVenta;

        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                IdVenta = Convert.ToInt32(Request.QueryString["IdVenta"]);
                TraerDetalleVenta(IdVenta);
            }
        }

        #region metodos
        void TraerDetalleVenta(int IdVenta)
        {
            gridDetalleVenta.DataSource = null;
            gridDetalleVenta.DataSource = GestorDV.ListarDV(IdVenta);
            gridDetalleVenta.DataBind();            
            
        }
        #endregion

        #region botones
        #endregion
    }
}