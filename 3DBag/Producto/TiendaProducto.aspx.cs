using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class TiendaProducto : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;        
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarProductos();
            }            
            //(Usuario)(Session["UserSession"])
        }

        #region metodos
        void ListarProductos()
        {
            try
            {
                dataListProducto.DataSource = null;
                dataListProducto.DataSource = GestorArticulo.Listar();
                dataListProducto.DataBind();
                

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion

        #region botones
        public void dataListProducto_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                Response.Write("pedido agregado");
            }
        }
        #endregion


    }
}