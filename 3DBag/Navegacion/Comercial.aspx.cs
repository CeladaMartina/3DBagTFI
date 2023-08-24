using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Propiedades_BE;

namespace _3DBag
{
    public partial class Comercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //si el usuario no esta logueado, lleva a la pagina de login
            if (!Page.IsPostBack)
            {
                if ((Usuario)(Session["UserSession"]) == null)
                {
                    Response.Redirect("/Home/Login.aspx");
                }
                else
                {
                    Permisos();
                }
            }
        }

        public void Permisos()
        {
            if (Propiedades_BE.SingletonLogin.GetInstance.IsLoggedIn())
            {
                //ocultará o mostrará las redirecciones segun dependa
                LinkProductos.Visible = (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Articulo));
                LinkTienda.Visible = (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Venta));
                LinkVerVentas.Visible = (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Articulo));
                LinkPedido.Visible = (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Venta));
            }
        }

        #region redirecciones
        protected void LinkProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/IndexProducto.aspx");
        }

        protected void LinkTienda_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/TiendaProducto.aspx");
        }

        protected void LinkVerVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/HistorialVenta.aspx");
        }

        protected void LinkPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Venta/Pedido.aspx");
        }

        #endregion
    }
}