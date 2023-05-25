using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Edit : System.Web.UI.Page
    {
        int IdUsuario = -1;
        string nick;

        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                nick = Request.QueryString["usuario"];
                TraerUsuario();
            }           
        }

        void TraerUsuario()
        {
            List<Propiedades_BE.Usuario> usuario = GestorUsuario.consultarNick(nick);
            txtNick.Text = usuario[0].Nick;
            txtNombre.Text = usuario[0].Nombre;
            txtMail.Text = usuario[0].Mail;

            if(Convert.ToString(usuario[0].Estado) == "true")
            {
                rdbBloqueado.Checked = true;
            }
            else
            {
                rdbBloqueado.Checked = false;
            }

            if(Convert.ToString(usuario[0].IdIdioma) == "1")
            {
                txtIdioma.Text = "Español";
            }
            else
            {
                txtIdioma.Text = "Ingles";
            }            

            if (Convert.ToString(usuario[0].BajaLogica) == "true")
            {
                rdbBaja.Checked = true;
            }
            else
            {
                rdbBaja.Checked = false;
            }
        }
    }
}