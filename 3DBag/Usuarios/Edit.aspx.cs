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
            List<string> x = GestorUsuario.NickIdUsuario(nick);
            Label1.Text = x[0];
        }
    }
}