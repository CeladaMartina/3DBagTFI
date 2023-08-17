using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Bitacora : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;

        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        //Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarBitacora();
            }
        }

        #region metodos
        void ListarBitacora()
        {
            try
            {
                GridBitacora.DataSource = null;
                GridBitacora.DataSource = Seguridad.Listar();                
                GridBitacora.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion
    }
}