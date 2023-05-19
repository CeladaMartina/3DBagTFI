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
            if (!IsPostBack)
            {
                ListarUsuarios();
            }
           
        }
        void ListarUsuarios()
        {
            try
            {
                gridUsuarios.DataSource = null;
                gridUsuarios.DataSource = GestorUsuario.Listar();
                gridUsuarios.DataBind();

            }
            catch(Exception ex)
            {
                Response.Write(ex);
            }
           
        }

        protected void gridUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string nick = ((TextBox)gridUsuarios.Rows[e.RowIndex].Cells)
            gridUsuarios.EditIndex = e.NewEditIndex;
            ListarUsuarios();
        }

        protected void gridUsuarios_RowUpdating(object sender, GridViewEditEventArgs e)
        {
            string nick = gridUsuarios.DataKeys[0].Value.ToString();
            string nombre = gridUsuarios.DataKeys[1].Value.ToString();
            string mail = gridUsuarios.DataKeys[2].Value.ToString();          
            string idioma = gridUsuarios.DataKeys[3].Value.ToString();

            Modificar(nick, nombre, mail, idioma);
        }

        void Modificar(string nick, string nombre, string mail, string idioma)
        {
            GestorUsuario.Modificar(nick, nombre, mail, idioma);

        }








    }
}