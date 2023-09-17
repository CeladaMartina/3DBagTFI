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

        #region redirecciones
        protected void LinkBackup_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Backup.aspx");
        }

        protected void LinkBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Bitacora.aspx");
        }

        protected void LinkRestore_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Restore.aspx");
        }

        #endregion
    }
}