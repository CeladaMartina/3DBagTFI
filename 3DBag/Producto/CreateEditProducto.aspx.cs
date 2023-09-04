using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditProducto : System.Web.UI.Page
    {
        int codProd;
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Producto GestorProducto = new Negocio_BLL.Producto();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                //si el cod viene null, mostrara pantalla Alta
                if(Request.QueryString["producto"] == null)
                {
                    lblTitulo.Text = "Nuevo Producto";
                    btnFunction.Text = "Agregar";
                }
                else
                {
                    //si el cod viene, mostrara pantalla Editar
                    codProd = Convert.ToInt32(Request.QueryString["producto"]);
                    lblTitulo.Text = "Editar Producto";
                    btnFunction.Text = "Editar";
                    TraerProducto(codProd);
                }
            }
        }

        void TraerProducto(int codProd)
        {
            List<Propiedades_BE.Articulo> producto = GestorProducto.consultarCodProd(codProd);
            txtCodProd.Text = producto[0].CodProd.ToString();
            txtNombre.Text = producto[0].Nombre;
            txtDescripcion.Text = producto[0].Descripcion;
            txtMaterial.Text = producto[0].Material;
            txtStock.Text = producto[0].Stock.ToString();
            txtPUnit.Text = producto[0].PUnit.ToString();
        }

        protected void LinkRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/IndexProducto.aspx");
        }
    }
}