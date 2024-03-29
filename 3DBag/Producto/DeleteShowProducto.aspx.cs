﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class DeleteShowProducto : System.Web.UI.Page, IObserver
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
                //traduccion de la pagina               
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                codProd = Convert.ToInt32(Request.QueryString["producto"]);

                //pantalla borrar
                if (Request.QueryString["Funcion"] == "borrar")
                {
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Eliminar") ?? ("Eliminar");
                    lblPregunta.Visible = true;
                    lblPregunta.Text = SiteMaster.TraducirGlobal("¿Estás seguro de que quieres eliminar esto?") ?? ("¿Estás seguro de que quieres eliminar esto?");
                    btnBorrar.CssClass = "btn btn-danger";
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
            Imagen.ImageUrl = producto[0].Imagen.ToString();
        }

        void Baja(int IdArticulo)
        {
           if(GestorArticulo.Baja(IdArticulo) != 1)
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Error de baja producto", "Alta", 0);
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Baja de Producto exitosamente") ?? ("Baja de Producto exitosamente");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Baja Articulo", "Baja", 0);
                
            }
            

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
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
               
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
            btnBorrar.Text = Sujeto.TraducirObserver(btnBorrar.SkinID.ToString()) ?? btnBorrar.SkinID.ToString();
            LinkRedireccion.Text = Sujeto.TraducirObserver(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
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
            btnBorrar.Text = SiteMaster.TraducirGlobal(btnBorrar.SkinID.ToString()) ?? btnBorrar.SkinID.ToString();
            LinkRedireccion.Text = SiteMaster.TraducirGlobal(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
        }
        #endregion
    }
}