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

        protected void gridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "editar")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                string v = gridUsuarios.Rows[crow].Cells[0].Text;


                //enviamos el nick del usuario
                Response.Redirect("Edit.aspx?usuario="+ v);

                
            }
        }










    }
}