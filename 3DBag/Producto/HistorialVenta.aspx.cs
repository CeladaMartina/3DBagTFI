using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class HistorialVenta : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();        
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarProductos();
            }
        }

        #region metodos
        void ListarProductos()
        {
            try
            {
                gridVentas.DataSource = null;
                gridVentas.DataSource = GestorVenta.Listar();
                gridVentas.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion

        #region boton
        #endregion
    }
}