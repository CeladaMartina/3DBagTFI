using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexProducto : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Producto GestorProducto = new Negocio_BLL.Producto();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarProductos();
            }
        }

        void ListarProductos()
        {
            try
            {
                gridProducto.DataSource = null;
                gridProducto.DataSource = GestorProducto.Listar();
                gridProducto.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEditProducto.aspx");
        }

        protected void gridProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                string v = gridProducto.Rows[crow].Cells[0].Text;


                //enviamos el codProd del Prod
                Response.Redirect("CreateEditProducto.aspx?producto=" + v);
            }
        }
    }
}