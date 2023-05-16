using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexUsuarios : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            ListarUsuarios();
        }
        void ListarUsuarios()
        {
            gridUsuarios.DataSource = null;
            gridUsuarios.DataSource = GestorUsuario.Listar();
            gridUsuarios.DataBind(); 
        }










    }
}