using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Restore : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

            //Traducir();
            if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Realizar_Restore)))
            {
                divGeneral.Visible = true;
                lblPermiso.Visible = false;
            }
            else
            {
                divGeneral.Visible = false;
                lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                lblPermiso.Visible = true;
            }

        }

        protected void Generar(object sender, EventArgs e)
        {
            if (txtRuta.Text == "")
            {
                lblResultado.Visible = true;
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
                lblResultado.Text = SiteMaster.TraducirGlobal("Restore realizado correctamente") ?? ("Restore realizado correctamente");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Restore exitoso", "Alta", 0);
                Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
                GestorUsuario.LogOut();

                //eliminamos la sesion del usuario
                Session["UserSession"] = null;

                if (Request.Url.ToString() == "https://localhost:44388/Home/Home.aspx")
                {
                    //si esta en la home, tiene que recargar asi la pagina, porque sino rompe
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    //si no esta en la home, te lleva ahi
                    Response.Redirect("../Home/Home.aspx");
                }

            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Restore fallido", "Alta", 0);                
            }
        }
    }
}