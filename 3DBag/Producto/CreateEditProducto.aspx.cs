﻿using System;
using System.Collections.Generic;
using System.IO;
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

        void Alta(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material,int Stock, decimal PUnit,byte[] Imagen, int DVH)
        {
            GestorProducto.Alta(IdArticulo, CodProd, Nombre, Descripcion, Material, Stock, PUnit, Imagen, DVH);
            lblRespuesta.Text = SiteMaster.TraducirGlobal("Alta de Producto exitosamente") ?? ("Alta de Producto exitosamente");
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Alta Articulo", "Media", 0);

        }

        void Modificar(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int Stock, decimal PUnit, int DVH)
        {
            GestorProducto.Modificar(IdArticulo, CodProd, Nombre, Descripcion, Material, Stock, PUnit, DVH);
            lblRespuesta.Text = SiteMaster.TraducirGlobal("Modificación de Producto exitosamente") ?? ("Modificación de Producto exitosamente");
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Modificar Articulo", "Baja", 0);

        }

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
            if(Request.QueryString["producto"] != null)
            {
                if (ChequearFallaTxt() == false)
                {
                    try
                    {
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
            else
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
    }
}