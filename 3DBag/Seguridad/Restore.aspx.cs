using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Restore : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

            //traduccion de la pagina
           
            if (Session["IdiomaSelect"] != null)
            {
                DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                Traducir();
            }

            if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Realizar_Restore)))
            {
                divGeneral.Visible = true;
                lblPermiso.Visible = false;
            }
            else
            {
                divGeneral.Visible = false;
                lblPermiso.CssClass = "alert alert-danger";
                lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                lblPermiso.Visible = true;
            }

        }

        protected void Generar(object sender, EventArgs e)
        {
            if (txtRuta.Text == "")
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Seleccione un archivo .bak para poder continuar.") ?? ("Seleccione un archivo .bak para poder continuar.");
            }
            else
            {
                try
                {
                    RealizarRestore();

                }
                catch (Exception)
                {
                    lblResultado.Visible = true;
                    lblResultado.CssClass = "alert alert-danger";
                    lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                }
            }
        }

        void RealizarRestore()
        {
            string restore = Seguridad.Restaurar(txtRuta.Text);
            if (restore == "ok")
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Restore realizado correctamente") ?? ("Restore realizado correctamente");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Restore exitoso", "Alta", 0);
                Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
                GestorUsuario.LogOut();

                //eliminamos la sesion del usuario y el problema definitivo
                Session["UserSession"] = null;
                Session["ProblemaDefinitivo"] = null;

                if (Request.Url.ToString() == "https://localhost:44388/Home/Home.aspx")
                {
                    //si esta en la home, tiene que recargar asi la pagina, porque sino rompe
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    //si no esta en la home, te lleva ahi
                    Response.Redirect("../Home/Home.aspx",false);
                }

            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Restore fallido", "Alta", 0);                
            }
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblRuta.Text = Sujeto.TraducirObserver(lblRuta.SkinID.ToString()) ?? lblRuta.SkinID.ToString();
            btnRestore.Text = Sujeto.TraducirObserver(btnRestore.SkinID.ToString()) ?? btnRestore.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblRuta.Text = SiteMaster.TraducirGlobal(lblRuta.SkinID.ToString()) ?? lblRuta.SkinID.ToString();
            btnRestore.Text = SiteMaster.TraducirGlobal(btnRestore.SkinID.ToString()) ?? btnRestore.SkinID.ToString();            
        }
        #endregion

        protected void linkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Seguridad.aspx");
        }
    }
}