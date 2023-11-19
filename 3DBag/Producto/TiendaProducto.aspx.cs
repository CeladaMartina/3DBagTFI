using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class TiendaProducto : System.Web.UI.Page,IObserver
    {
        private ContentPlaceHolder contentPlace;
        int IdVenta = 0;
        Label IdVentaUsuario;

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
                //traduccion de la pagina                
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                ListarProductos();
            }
            
        }

        #region metodos        
        public void AltaVenta(int IdUsuario, DateTime Fecha, int DVH)
        {
            GestorVenta.Alta(IdUsuario, Fecha, DVH);
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
                VerCarrito.Visible = true;
                VerCarrito.Text = SiteMaster.TraducirGlobal("No hay Stock suficiente") ?? ("No hay Stock suficiente");               
            }
        }

       void ListarProductos()
        {
            try
            {
                dataList.DataSource = null;
                dataList.DataSource = GestorArticulo.Listar();
                dataList.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion



        #region botones
        protected void dataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito")
            {
                int index = e.Item.ItemIndex;
                IdVentaUsuario = (Label)dataList.Items[index].FindControl("IdVentaUsuario");
                Label IdArticulo = (Label)dataList.Items[index].FindControl("IdArticuloLabel");
                Label NombreLabel = (Label)dataList.Items[index].FindControl("NombreLabel");
                Label PUnit = (Label)dataList.Items[index].FindControl("PUnitLabel");
                TextBox txtCantidad = (TextBox)dataList.Items[index].FindControl("TxtCantidad");

                if(txtCantidad.Text == "")
                {
                    lblRespuesta.Visible = true;
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
                else
                {
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
                            //si no existe el producto en lo que ya se eligio, se agrega al listado
                            AltaDV(IdVenta, Convert.ToInt32(IdArticulo.Text), NombreLabel.Text, decimal.Parse(PUnit.Text), int.Parse(txtCantidad.Text), 0);
                        }
                    }
                    else
                    {
                        //si no existe, lo creamos
                        AltaVenta(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Today, 0);
                        //traemos el ultimo IDVenta realizado
                        IdVenta = GestorVenta.TraerIdVenta();
                        //creamos el alta detalle 
                        AltaDV(IdVenta, Convert.ToInt32(IdArticulo.Text), NombreLabel.Text, decimal.Parse(PUnit.Text), int.Parse(txtCantidad.Text), 0);
                    }

                    //guardamos el valor del IDVenta
                    Session["IdVenta"] = IdVenta;
                    //mostramos el link para ver el pedido
                    VerCarrito.Visible = true;
                }
            }            
        }

        #endregion

        protected void VerCarrito_Click(object sender, EventArgs e)
        {            
            Response.Redirect("../Venta/Pedido.aspx?IdVenta=" + Session["IdVenta"]);
        }

        #region traduccion
        
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();            
            //BtnAgregar.Text = Sujeto.TraducirObserver(BtnAgregar.SkinID.ToString()) ?? BtnAgregar.SkinID.ToString();
            VerCarrito.Text = Sujeto.TraducirObserver(VerCarrito.SkinID.ToString()) ?? VerCarrito.SkinID.ToString();
            LinkRedireccion.Text = Sujeto.TraducirObserver(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();          
            VerCarrito.Text = SiteMaster.TraducirGlobal(VerCarrito.SkinID.ToString()) ?? VerCarrito.SkinID.ToString();  
            LinkRedireccion.Text = SiteMaster.TraducirGlobal(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString(); 
        }

        protected void LinkRedireccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Comercial.aspx");
        }

        #endregion

        protected void dataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label lblNombre = (Label)e.Item.FindControl("lblNombre");
            Label lblDesc = (Label)e.Item.FindControl("lblDescripcion");
            Label lblPrecio = (Label)e.Item.FindControl("lblPrecio");
            Label lblCantidad = (Label)e.Item.FindControl("lblCantidad");
            Button bntAgregar = (Button)e.Item.FindControl("BtnAgregar");

            //idioma null = espanol, asi hace la traduccion.
            if(Session["IdiomaSelect"] == null)
            {
                string idioma;
                idioma = "Español";
                Session["IdiomaSelect"] = idioma;
            }

            if (Session["IdiomaSelect"].ToString() == "Ingles" || Session["IdiomaSelect"].ToString() == "Español")
            {
                lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
                lblDesc.Text = SiteMaster.TraducirGlobal(lblDesc.SkinID.ToString()) ?? lblDesc.SkinID.ToString();
                lblPrecio.Text = SiteMaster.TraducirGlobal(lblPrecio.SkinID.ToString()) ?? lblPrecio.SkinID.ToString();
                lblCantidad.Text = SiteMaster.TraducirGlobal(lblCantidad.SkinID.ToString()) ?? lblCantidad.SkinID.ToString();
                bntAgregar.Text = SiteMaster.TraducirGlobal(bntAgregar.SkinID.ToString()) ?? bntAgregar.SkinID.ToString();
            }

        }
    }
}