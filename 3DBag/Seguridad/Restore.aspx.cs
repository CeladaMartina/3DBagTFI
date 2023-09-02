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
            Traducir();
        }

        #region metodos
        void RealizarRestore()
        {
            string restore = Seguridad.Restaurar(txtRuta.Text);
            if (restore == "ok")
            {
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
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Restore fallido", "Alta", 0);
                lblResultado.Visible = true;
                lblResultado.Text = (SiteMaster.TraducirGlobal("Error al realizar el restore") ?? "Error al realizar el restore");
            }
            
        }
        #endregion


        #region Traduccion
        public void Update(ISubject Sujeto)
        {
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            btnRestaurar.Text = Sujeto.TraducirObserver(btnRestaurar.SkinID.ToString()) ?? btnRestaurar.SkinID.ToString();
        }

        public void Traducir()
        {
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            btnRestaurar.Text = SiteMaster.TraducirGlobal(btnRestaurar.SkinID.ToString()) ?? btnRestaurar.SkinID.ToString();
        }

        #endregion
        protected void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (txtRuta.Text == "")
            {
                lblResultado.Visible = true;
                lblResultado.Text = (SiteMaster.TraducirGlobal("Seleccione un .bak para poder continuar.") ?? "Seleccione un .bak para poder continuar.");
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
                    lblResultado.Text = (SiteMaster.TraducirGlobal("Error") ?? "Error");
                }
            }
        }
    }
}