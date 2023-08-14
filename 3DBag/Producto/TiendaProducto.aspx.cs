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
        int IdVenta = 0;

        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        //Propiedades_BE.Usuario Usuario = new Propiedades_BE.Usuario();
        //venta
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        //Negocio_BLL.Cliente GestorCliente = new Negocio_BLL.Cliente();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                //ListarProductos();
                //HttpContext.Current.Session["UserSession"] = Usuario;
            }
            
        }

        #region metodos
        //void ListarProductos()
        //{
        //    try
        //    {
        //        dataListProduct.DataSource = null;
        //        dataListProduct.DataSource = GestorArticulo.Listar();
        //        dataListProduct.DataBind();


        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex);
        //    }

        //}


        public void AltaVenta(int IdUsuario, DateTime Fecha)
        {
            GestorVenta.Alta(IdUsuario, Fecha);
        }

        public void AltaDV(int IdVenta, int IdArticulo, string Descripcion, decimal PUnit, int Cantidad, int DVH)
        {
            int CantidadChequeoStock = GestorArticulo.VerificarCantStock(IdArticulo);
            if (Cantidad <= CantidadChequeoStock && GestorDV.ChequearStock(IdArticulo, IdVenta, Cantidad, 0) <= CantidadChequeoStock)
            {
                GestorDV.AltaDV(PUnit, IdArticulo, Descripcion, DVH, Cantidad, IdVenta);                
            }
            else
            {
                //MessageBox.Show(Cambiar_Idioma.TraducirGlobal("No hay Stock suficiente") ?? "No hay Stock suficiente");
            }
        }

        #endregion



        #region botones
        protected void dataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito")
            {
                int index = e.Item.ItemIndex;
                Label IdArticulo = (Label)dataList.Items[index].FindControl("IdArticuloLabel");
                Label NombreLabel = (Label)dataList.Items[index].FindControl("NombreLabel");
                Label PUnit = (Label)dataList.Items[index].FindControl("PUnitLabel");
                TextBox txtCantidad = (TextBox)dataList.Items[index].FindControl("TxtCantidad");


                //verificamos si ya existe una venta de este usuario en este momento.
                IdVenta = GestorVenta.ExisteVenta(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Today);

                if (IdVenta != 0)
                {
                    //si existe el producto, lo unificamos,
                    if (GestorDV.ExisteProducto(IdVenta, Convert.ToInt32(IdArticulo.Text)) == true)
                    {
                        GestorDV.UnificarArticulos(IdVenta, Convert.ToInt32(IdArticulo.Text), Convert.ToInt32(txtCantidad.Text));
                    }
                    else
                    {
                        //si no existe el producto en lo que ya se elijio, se agrega al listado
                        AltaDV(IdVenta, Convert.ToInt32(IdArticulo.Text), NombreLabel.Text, decimal.Parse(PUnit.Text), int.Parse(txtCantidad.Text), 0);
                    }                   
                }
                else
                {
                    //si no existe, lo creamos
                    AltaVenta(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Today);
                    //traemos el ultimo IDVenta realizado
                    IdVenta = GestorVenta.TraerIdVenta();
                    //creamos el alta detalle 
                    AltaDV(IdVenta, Convert.ToInt32(IdArticulo.Text), NombreLabel.Text, decimal.Parse(PUnit.Text), int.Parse(txtCantidad.Text), 0);
                }

            }


            
        }

        #endregion
    
    }
}