using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio_BLL;

namespace _3DBag
{
    public partial class IndexProducto : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();        
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarProductos();
            }
        }

        #region metodos
        void ListarProductos()
        {
            try
            {
                gridProducto.DataSource = null;
                gridProducto.DataSource = GestorArticulo.Listar();
                
                gridProducto.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion

        #region boton

        protected void gridProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                string v = gridProducto.Rows[crow].Cells[0].Text;


                //enviamos el nick del usuario
                Response.Redirect("Edit.aspx?usuario=" + v);


            }
        }

        #endregion
    }
}