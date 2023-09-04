using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class DeleteShowProducto : System.Web.UI.Page
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
                codProd = Convert.ToInt32(Request.QueryString["producto"]);

                //pantalla EDITAR
                if (Request.QueryString["Funcion"] == "borrar")
                {
                    lblTitulo.Text = "Eliminar";
                    lblPregunta.Visible = true;
                    lblPregunta.Text = "¿Estás seguro de que quieres eliminar esto?";
                    btnFuncion.Text = "Eliminar";
                }
                else
                {
                    //pantalla VER                     
                    lblTitulo.Text = "Detalles";
                    lblPregunta.Visible = false;
                    btnFuncion.Visible = false;
                }

                TraerProducto(codProd);
            }
        }

        void TraerProducto(int codProd)
        {
            List<Propiedades_BE.Articulo> producto = GestorProducto.consultarCodProd(codProd);
            lblCodProdResp.Text = producto[0].CodProd.ToString();
            lblNombreResp.Text = producto[0].Nombre;
            lblDescripcionResp.Text = producto[0].Descripcion;
            lblMaterialResp.Text = producto[0].Material;
            lblStockResp.Text = producto[0].Stock.ToString();
            lblPUnitResp.Text = producto[0].PUnit.ToString();
        }

        protected void LinkRedireccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/IndexProducto.aspx");
        }
    }
}