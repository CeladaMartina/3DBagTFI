using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Administracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //si el usuario no esta logueado, lleva a la pagina de login
            if (!Page.IsPostBack)
            {
                if ((Usuario)(Session["UserSession"]) == null)
                {
                    Response.Redirect("/Home/Login.aspx");
                }
                else
                {
                    Permisos();
                }
            }
                
        }

        public void Permisos()
        {
            if (Propiedades_BE.SingletonLogin.GetInstance.IsLoggedIn())
            {
                LinkGestionUsuarios.Visible=(Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario));                
            }
        }

        #region redirecciones
        protected void LinkGestionUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Usuarios/IndexUsuarios.aspx");
        }

        protected void LinkGestionFamilias_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Usuarios/IndexFamilias.aspx");
        }

        protected void LinkGestionPatentes_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Usuarios/IndexPatentes.aspx");
        }
        #endregion


    }
}