using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexCliente : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Cliente GestorCliente = new Negocio_BLL.Cliente();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarClientes();
            }
        }

        #region metodos
        void ListarClientes()
        {
            try
            {
                gridCliente.DataSource = null;
                gridCliente.DataSource = GestorCliente.Listar();
                gridCliente.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion

        #region botones
        protected void gridCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                string v = gridCliente.Rows[crow].Cells[0].Text;


                //enviamos el nick del usuario
                Response.Redirect("Edit.aspx?usuario=" + v);


            }
        }
        #endregion
    }
}