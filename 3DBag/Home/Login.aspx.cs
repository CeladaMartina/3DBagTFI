using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Propiedades_BE.Usuario Usuario = new Propiedades_BE.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}