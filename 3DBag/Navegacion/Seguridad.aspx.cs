using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Seguridad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //si el usuario no esta logueado, lleva a la pagina de login
            if (!Page.IsPostBack)
            {
                if ((Usuario)(Session["UserSession"]) == null)
                {
                    Response.Redirect("../Home/Login.aspx");
                }
            }
        }
    }
}