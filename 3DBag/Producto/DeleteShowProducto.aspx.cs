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
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                codProd = Convert.ToInt32(Request.QueryString["producto"]);

                //pantalla EDITAR
                if (Request.QueryString["Funcion"] == "borrar")
                {
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Eliminar") ?? ("Eliminar");
                    lblPregunta.Visible = true;
                    lblPregunta.Text = SiteMaster.TraducirGlobal("¿Estás seguro de que quieres eliminar esto?") ?? ("¿Estás seguro de que quieres eliminar esto?");                   
                }
                else
                {
                    //pantalla VER                     
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Detalles") ?? ("Detalles");
                    lblPregunta.Visible = false;
                    btnBorrar.Visible = false;
                }

                TraerProducto(codProd);
            }
        }

        void TraerProducto(int codProd)
        {
            List<Propiedades_BE.Articulo> producto = GestorArticulo.consultarCodProd(codProd);
            lblCodProdResp.Text = producto[0].CodProd.ToString();
            lblNombreResp.Text = producto[0].Nombre;
            lblDescripcionResp.Text = producto[0].Descripcion;
            lblMaterialResp.Text = producto[0].Material;
            lblStockResp.Text = producto[0].Stock.ToString();
            lblPUnitResp.Text = producto[0].PUnit.ToString();
        }

        void Baja(int IdArticulo)
        {
            GestorArticulo.Baja(IdArticulo);
            lblResultado.Visible = true;
            lblResultado.Text = SiteMaster.TraducirGlobal("Baja de Producto exitosamente") ?? ("Baja de Producto exitosamente");
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Baja Articulo", "Baja", 0);

        }

        protected void LinkRedireccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/IndexProducto.aspx");
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                Baja(GestorArticulo.SeleccionarIdArticulo(codProd));
            }
            catch (Exception)
            {
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
               
            }
        }

    }
}