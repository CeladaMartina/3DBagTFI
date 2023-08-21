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
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
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

        protected void VerTienda(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/TiendaProducto.aspx");
        }

        public void ModificarDV(int IdDetalle, int IdArticulo, decimal PUnit, int Cantidad, int DVH)
        {
            int CantidadChequeoStock = GestorArticulo.VerificarCantStock(IdArticulo);
            if (Cantidad <= CantidadChequeoStock && GestorDV.ChequearStock(IdArticulo, IdVenta, Cantidad, IdDetalle) <= CantidadChequeoStock)
            {
                GestorDV.ModificarDV(IdDetalle, IdArticulo, PUnit, Cantidad, DVH);
                //ListarDV();
                //Lblsubtotal.Text = GestorDV.SubTotal(int.Parse(TxtIdVenta.Text)).ToString();
            }
            else
            {
                //MessageBox.Show(Cambiar_Idioma.TraducirGlobal("No hay Stock suficiente") ?? "No hay Stock suficiente");
            }
        }
        #endregion

        #region botones

        protected void gridDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                //hagarramos la fila a editar
                //int crow;
                //crow = Convert.ToInt32(e.CommandArgument.ToString());

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridDetalleVenta.Rows[index];

                string idDetalle = row.Cells[2].Text;

                //se encuentra el txt modificado y se guarda el cambio


                //string value = gridDetalleVenta.Rows[crow].Cells[0].Text;

                //int idDetalle = Convert.ToInt32(gridDetalleVenta.Rows[crow].Cells[0].Text);
                //int codArticulo = Convert.ToInt32(gridDetalleVenta.Rows[crow].Cells[1].Text);
                //TextBox txtCantidad = (TextBox)gridDetalleVenta.Rows[crow].FindControl("txtCant");                
                //int cant = Convert.ToInt32(txtCantidad.Text);                
                //string precioU = gridDetalleVenta.Rows[crow].Cells[4].Text;

                //ModificarDV(idDetalle, codArticulo, decimal.Parse(precioU), cant, 0);

            }
        }
        #endregion




    }
}