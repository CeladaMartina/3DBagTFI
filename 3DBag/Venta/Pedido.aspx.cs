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
                if(IdVenta == 0)
                {
                    gridDetalleVenta.Visible = false;
                    lblMensaje.Visible = true;
                    linkRedirect.Visible = true;
                    lblMensaje.Text = "Primero debe agregar un producto al carrito de compras.";                  
                    
                }
                else
                {
                    lblMensaje.Visible = false;
                    gridDetalleVenta.Visible = true;
                    TraerDetalleVenta(IdVenta);                    
                }
                
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

        protected void VerTienda(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/TiendaProducto.aspx");
        }

        protected void gridDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                //hagarramos la fila a editar
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());

                GridViewRow Row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox txtCant = Row.FindControl("txtCant") as TextBox;             
                
                //se lo asignamos al txt para poder modificarlo
                txtCant.Text = gridDetalleVenta.Rows[crow].Cells[1].Text;
                txtCant.Visible = true;

                //ocultamos esa fila con el valor que venia antes
                gridDetalleVenta.Rows[crow].Cells[1].Visible = false;



            }
        }
    }
}