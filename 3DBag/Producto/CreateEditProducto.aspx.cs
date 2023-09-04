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
        int IdArticulo = -1;
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

        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtCodProd.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescripcion.Text) ||
                string.IsNullOrEmpty(txtMaterial.Text) ||string.IsNullOrEmpty(txtPUnit.Text))
            {
                A = true;
            }
            return A;
        }

        void Alta(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int IdLocalidad, int Stock, decimal PUnit, int DVH)
        {
            GestorProducto.Alta(IdArticulo, CodProd, Nombre, Descripcion, Material, IdLocalidad, Stock, PUnit, DVH);
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Alta Articulo", "Media", 0);

        }

        void Modificar(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int IdLocalidad, int Stock, decimal PUnit, int DVH)
        {
            GestorProducto.Modificar(IdArticulo, CodProd, Nombre, Descripcion, Material, IdLocalidad, Stock, PUnit, DVH);
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Modificar Articulo", "Baja", 0);

        }

        protected void LinkRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/IndexProducto.aspx");
        }

        protected void btnFunction_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["producto"] != null)
            {
                if (ChequearFallaTxt() == false)
                {
                    try
                    {
                        Modificar(GestorProducto.SeleccionarIdArticulo(Convert.ToInt32(txtCodProd.Text)), int.Parse(txtCodProd.Text), txtNombre.Text, txtDescripcion.Text, txtMaterial.Text, 8, int.Parse(txtStock.Text), decimal.Parse(txtPUnit.Text), 0);
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(CambiarIdioma.TraducirGlobal("Error") ?? "Error");
                    }
                }
                else
                {
                    //MessageBox.Show(CambiarIdioma.TraducirGlobal("Complete todos los campos") ?? "Complete todos los campos");
                }
            }
            else
            {
                if (ChequearFallaTxt() == false)
                {
                    try
                    {
                        Alta(IdArticulo, int.Parse(txtCodProd.Text), txtNombre.Text, txtDescripcion.Text, txtMaterial.Text, 8, int.Parse(txtStock.Text), decimal.Parse(txtPUnit.Text), 0);
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(CambiarIdioma.TraducirGlobal("Error") ?? "Error");
                    }
                }
                else
                {
                    //MessageBox.Show(CambiarIdioma.TraducirGlobal("Complete todos los campos") ?? "Complete todos los campos");
                }
            }
        }
    }
}