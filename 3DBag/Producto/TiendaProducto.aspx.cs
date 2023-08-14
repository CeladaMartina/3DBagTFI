using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class TiendaProducto : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;        
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                //ListarProductos();
            }            
            //(Usuario)(Session["UserSession"])
        }

        #region metodos
        //void ListarProductos()
        //{
        //    try
        //    {
        //        dataListProduct.DataSource = null;
        //        dataListProduct.DataSource = GestorArticulo.Listar();
        //        dataListProduct.DataBind();


        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex);
        //    }

        //}

        #endregion



        #region botones
        
        #endregion

        protected void dataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito") // check commandname here
            {
                int index = e.Item.ItemIndex;
                Label lbl = (Label)dataList.Items[index].FindControl("NombreLabel");
                Response.Write(lbl.Text);
                // your code
            }
        }
    }
}