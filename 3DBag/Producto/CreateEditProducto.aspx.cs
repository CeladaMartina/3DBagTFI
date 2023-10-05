using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditProducto : System.Web.UI.Page, IObserver
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
                //traduccion de la pagina                
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                //si el cod viene null, mostrara pantalla Alta
                if (Request.QueryString["producto"] == null)
                {
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Nuevo Producto") ?? ("Nuevo Producto");
                    btnFunction.Text = SiteMaster.TraducirGlobal("Agregar") ?? ("Agregar");
                    Imagen.Visible = false;
                }
                else
                {
                    //si el cod viene, mostrara pantalla Editar
                    codProd = Convert.ToInt32(Request.QueryString["producto"]);
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Editar Producto") ?? ("Editar Producto");
                    btnFunction.Text = SiteMaster.TraducirGlobal("Editar") ?? ("Editar");
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
            Imagen.Visible = true;
            Imagen.ImageUrl = producto[0].Imagen.ToString();
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

        void Alta(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material,int Stock, decimal PUnit,byte[] Imagen, int DVH)
        {
            if(GestorProducto.Alta(IdArticulo, CodProd, Nombre, Descripcion, Material, Stock, PUnit, Imagen, DVH) == 0)
            {
                lblRespuesta.Visible = true;
                lblRespuesta.CssClass = "label-success";
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Error de alta producto", "Alta", 0);
            }
            else
            {
                lblRespuesta.Visible = true;
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Alta de Producto exitosamente") ?? ("Alta de Producto exitosamente");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Alta Articulo", "Media", 0);
            }           

        }

        void Modificar(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int Stock, decimal PUnit, int DVH)
        {
            if(GestorProducto.Modificar(IdArticulo, CodProd, Nombre, Descripcion, Material, Stock, PUnit, DVH)== 0)
            {
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Error de alta producto", "Alta", 0);
                lblRespuesta.Visible = true;
                lblRespuesta.CssClass = "label-success";
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
            else
            {
                lblRespuesta.Visible = true;
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Modificación de Producto exitosamente") ?? ("Modificación de Producto exitosamente");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Modificar Articulo", "Baja", 0);
            }         

        }

        void GuardarImagenProd(int IdArticulo, byte[] ImagenProd)
        {
            if(GestorProducto.GuardarImagenProd(IdArticulo, ImagenProd) == 0)
            {
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Error de alta producto", "Alta", 0);
                lblRespuesta.Visible = true;
                lblRespuesta.CssClass = "label-success";
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
        }

        //la imagen la crea en bytes para subirlo a la base de datos
        byte[] ObtenerImagen()
        {
            byte[] resultado;

            using (Stream s = FileUploadImagen.PostedFile.InputStream)
            {
                using(BinaryReader br = new BinaryReader(s))
                {
                    byte[] Databytes = br.ReadBytes((Int32)s.Length);
                    resultado = Databytes;
                }
            }    
            
            return resultado;
        }

        protected void LinkRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/IndexProducto.aspx");
        }

        protected void btnFunction_Click(object sender, EventArgs e)
        {
            //metodo editar
            if(Request.QueryString["producto"] != null)
            {
                if (ChequearFallaTxt() == false)
                {
                    try
                    {
                        string prodFileName = Path.GetFileName(FileUploadImagen.PostedFile.FileName);
                        if (prodFileName != "")
                        {
                            byte[] ImagenProd = ObtenerImagen();
                            GuardarImagenProd(GestorProducto.SeleccionarIdArticulo(Convert.ToInt32(txtCodProd.Text)), ImagenProd);                            
                        }                        
                        
                        Modificar(GestorProducto.SeleccionarIdArticulo(Convert.ToInt32(txtCodProd.Text)), int.Parse(txtCodProd.Text), txtNombre.Text, txtDescripcion.Text, txtMaterial.Text, int.Parse(txtStock.Text), decimal.Parse(txtPUnit.Text), 0);

                    }
                    catch (Exception)
                    {
                        lblRespuesta.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");                        
                    }
                }
                else
                {
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
            }
            else //metodo alta
            {
                if (ChequearFallaTxt() == false)
                {
                    try
                    {
                        string prodFileName = Path.GetFileName(FileUploadImagen.PostedFile.FileName);
                        if(prodFileName == "")
                        {
                            lblRespuesta.Text = SiteMaster.TraducirGlobal("Cargue una Imagen") ?? ("Cargue una Imagen");
                        }
                        else
                        {
                            byte[] ImagenProd = ObtenerImagen();
                            Alta(IdArticulo, int.Parse(txtCodProd.Text), txtNombre.Text, txtDescripcion.Text, txtMaterial.Text, int.Parse(txtStock.Text), decimal.Parse(txtPUnit.Text), ImagenProd, 0);

                        }
                    }
                    catch (Exception)
                    {
                        lblRespuesta.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                    }
                }
                else
                {
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
            }
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblCodProd.Text = Sujeto.TraducirObserver(lblCodProd.SkinID.ToString()) ?? lblCodProd.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDescripcion.Text = Sujeto.TraducirObserver(lblDescripcion.SkinID.ToString()) ?? lblDescripcion.SkinID.ToString();
            lblMaterial.Text = Sujeto.TraducirObserver(lblMaterial.SkinID.ToString()) ?? lblMaterial.SkinID.ToString();
            lblStock.Text = Sujeto.TraducirObserver(lblStock.SkinID.ToString()) ?? lblStock.SkinID.ToString();
            lblPUnit.Text = Sujeto.TraducirObserver(lblPUnit.SkinID.ToString()) ?? lblPUnit.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblCodProd.Text = SiteMaster.TraducirGlobal(lblCodProd.SkinID.ToString()) ?? lblCodProd.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDescripcion.Text = SiteMaster.TraducirGlobal(lblDescripcion.SkinID.ToString()) ?? lblDescripcion.SkinID.ToString();
            lblMaterial.Text = SiteMaster.TraducirGlobal(lblMaterial.SkinID.ToString()) ?? lblMaterial.SkinID.ToString();
            lblStock.Text = SiteMaster.TraducirGlobal(lblStock.SkinID.ToString()) ?? lblStock.SkinID.ToString();
            lblPUnit.Text = SiteMaster.TraducirGlobal(lblPUnit.SkinID.ToString()) ?? lblPUnit.SkinID.ToString();            

        }
        #endregion
    }
}