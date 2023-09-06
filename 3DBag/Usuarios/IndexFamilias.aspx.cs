using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexFamilias : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Familia FamTemp;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarFamilias();
            }
        }

        void ListarFamilias()
        {
            try
            {
                gridFamilias.DataSource = null;
                gridFamilias.DataSource = GestorPermisos.GetAllFamilias();
                gridFamilias.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
    }
}