using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Propiedades_BE;

namespace _3DBag
{
    public partial class SiteMaster : MasterPage, ISubject
    {
        Negocio_BLL.Idioma GestorIdioma = new Negocio_BLL.Idioma();
        Negocio_BLL.Traduccion GestorTraduccion = new Negocio_BLL.Traduccion();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();

        List<IObserver> ListaObservadores = new List<IObserver>();
        public static Dictionary<string, string> DiccionarioTraduccionGlobal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //si el usuario no esta logueado
                if ((Usuario)(Session["UserSession"]) == null)
                {
                    SalirId.Visible = false;                    
                }
                else
                {
                    //si el usuario esta logueado
                    LoginId.Visible = false;
                    RegistrarId.Visible = false;
                    SalirId.Visible = true;
                }

                if (Session["IdiomaSelect"] != null)
                {
                    DropDownListIdioma.SelectedValue = Session["IdiomaSelect"].ToString();
                    //DropDownListIdioma.SelectedValue = "Español";
                    Traducir();
                    //Update()
                }
            }            
        }

        
        #region Metodos traduccion

        public void Attach(IObserver observer)
        {
            ListaObservadores.Add(observer);
        }

        public void NotificarCambio()
        {
            ////var numerator = Application.OpenForms.GetEnumerator();
            
            //var numerator = Application.GetEnumerator();
            //while (numerator.MoveNext())
            //{
            //    if (numerator.Current is IObserver)
            //    {
            //        this.Attach((IObserver)numerator.Current);
                    
            //    }
            //}            
            //ListaObservadores.ForEach(item => item.Update(this));

            foreach(IObserver observer in ListaObservadores)
            {
                observer.Update(this);
            }
            
        }

        public string TraducirObserver(string variable)
        {
            string trad = "";
            try
            {
                trad = DiccionarioTraduccionGlobal[variable];
            }
            catch (Exception)
            {
                trad = null;
            }
            return trad;
        }

        public static string TraducirGlobal(string variable)
        {
            string trad = "";
            try
            {
                trad = DiccionarioTraduccionGlobal[variable];
            }
            catch (Exception)
            {
                trad = null;
            }
            return trad;
        }
        #endregion

        #region botones

        //cuando se selecciona un valor del combo idioma, cambiara
        protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["IdiomaSelect"] = DropDownListIdioma.SelectedItem.ToString();
            try
            {
                DiccionarioTraduccionGlobal = new Dictionary<string, string>();
                foreach (var item in GestorTraduccion.ListarTraduccionDicionario(DropDownListIdioma.SelectedItem.ToString()))
                {
                    DiccionarioTraduccionGlobal.Add(item.Original, item.Traducido);
                }
                this.NotificarCambio();
                Traducir();
            }
            catch (Exception)
            {
                //MessageBox.Show(TraducirGlobal("Error") ?? "Error");
            }
        }

        protected void onClickSalir(object sender, EventArgs e)
        {
            Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "LogOut", "Baja", 0);
            GestorUsuario.LogOut();
            //eliminamos la sesion del usuario
            Session["UserSession"] = null;
            
            if(Request.Url.ToString() == "https://localhost:44388/Home/Home.aspx")
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
        #endregion

        #region metodos
        void Traducir()
        {
            HomeId.Text = TraducirGlobal(HomeId.SkinID.ToString()) ?? HomeId.SkinID.ToString();
            AdminId.Text = TraducirGlobal(AdminId.SkinID.ToString()) ?? AdminId.SkinID.ToString();
            SeguridadId.Text = TraducirGlobal(SeguridadId.SkinID.ToString()) ?? SeguridadId.SkinID.ToString();
            ComercialId.Text = TraducirGlobal(ComercialId.SkinID.ToString()) ?? ComercialId.SkinID.ToString();
            LoginId.Text = TraducirGlobal(LoginId.SkinID.ToString()) ?? LoginId.SkinID.ToString();
            RegistrarId.Text = TraducirGlobal(RegistrarId.SkinID.ToString()) ?? RegistrarId.SkinID.ToString();
            SalirId.Text = TraducirGlobal(SalirId.SkinID.ToString()) ?? SalirId.SkinID.ToString();
        }
        #endregion

        #region redirecciones
        protected void hrefRegistracion(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Registrarse.aspx");
        }

        protected void hrefLogin(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Login.aspx");
        }
        protected void HomeId_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx");
        }

        protected void AdminId_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Administracion.aspx");
        }

        protected void SeguridadId_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Seguridad.aspx");
        }

        protected void ComercialId_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Comercial.aspx");
        }

        #endregion
    }
}