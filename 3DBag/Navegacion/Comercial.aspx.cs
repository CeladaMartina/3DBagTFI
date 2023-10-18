using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Propiedades_BE;

namespace _3DBag
{
    public partial class Comercial : System.Web.UI.Page, IObserver
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
                    if (Session["IdiomaSelect"] != null)
                    {
                        DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                        masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                        Traducir();                        
                    }
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

        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            LinkProductos.Text = Sujeto.TraducirObserver(LinkProductos.SkinID.ToString()) ?? LinkProductos.SkinID.ToString();
            LinkVerVentas.Text = Sujeto.TraducirObserver(LinkVerVentas.SkinID.ToString()) ?? LinkVerVentas.SkinID.ToString();
            LinkTienda.Text = Sujeto.TraducirObserver(LinkTienda.SkinID.ToString()) ?? LinkTienda.SkinID.ToString();
            LinkPedido.Text = Sujeto.TraducirObserver(LinkPedido.SkinID.ToString()) ?? LinkPedido.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            LinkProductos.Text = SiteMaster.TraducirGlobal(LinkProductos.SkinID.ToString()) ?? LinkProductos.SkinID.ToString();
            LinkVerVentas.Text = SiteMaster.TraducirGlobal(LinkVerVentas.SkinID.ToString()) ?? LinkVerVentas.SkinID.ToString();
            LinkTienda.Text = SiteMaster.TraducirGlobal(LinkTienda.SkinID.ToString()) ?? LinkTienda.SkinID.ToString();
            LinkPedido.Text = SiteMaster.TraducirGlobal(LinkPedido.SkinID.ToString()) ?? LinkPedido.SkinID.ToString();
        }
        #endregion
    }
}