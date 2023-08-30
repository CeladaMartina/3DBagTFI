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
        int IdVenta = -1;

        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                
                //IdVenta = Convert.ToInt32(Request.QueryString["IdVenta"]);
                IdVenta = Convert.ToInt32(Session["IdVenta"]);

                if (IdVenta == 0)
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

        public void ModificarDV(int IdDetalle, int IdArticulo, decimal PUnit, int Cantidad, int DVH, int IdVenta)
        {
            int CantidadChequeoStock = GestorArticulo.VerificarCantStock(IdArticulo);
            if (Cantidad <= CantidadChequeoStock && GestorDV.ChequearStock(IdArticulo, IdVenta, Cantidad, IdDetalle) <= CantidadChequeoStock)
            {
                GestorDV.ModificarDV(IdDetalle, IdArticulo, PUnit, Cantidad, DVH, IdVenta);
                lblMensaje.Visible = true;                
                lblMensaje.Text = "Cantidad modificada correctamente.";
            }
            else
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No hay Stock suficiente.";
                //MessageBox.Show(Cambiar_Idioma.TraducirGlobal("No hay Stock suficiente") ?? "No hay Stock suficiente");
            }
        }

        public void BajaDV(int IdDetalle, int IdArticulo)
        {
            GestorDV.BajaDV(IdDetalle, IdArticulo);
            TraerDetalleVenta(Convert.ToInt32(Session["IdVenta"]));            
        }
        #endregion

        #region botones
        protected void gridDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //hagarramos la fila a editar               
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridDetalleVenta.Rows[index];

            //buscamos los valores de la tabla y el valor del txt modificado
            Label IdVenta = (Label)row.FindControl("IdVenta");
            Label CodProd = (Label)row.FindControl("CodProd");
            Label IdDetalle = (Label)row.FindControl("IdDetalle");
            TextBox txtCantidad = (TextBox)row.FindControl("txtCant");
            string precioU = row.Cells[3].Text;

            if (e.CommandName == "editar")
            {               
                ModificarDV(Convert.ToInt32(IdDetalle.Text), GestorArticulo.SeleccionarIdArticulo(int.Parse(CodProd.Text)), decimal.Parse(precioU), int.Parse(txtCantidad.Text), 0, Convert.ToInt32(IdVenta.Text));
            }
            else if(e.CommandName == "borrar")
            {
                BajaDV(Convert.ToInt32(IdDetalle.Text), GestorArticulo.SeleccionarIdArticulo(int.Parse(CodProd.Text)));
            }
        }
        #endregion




    }
}