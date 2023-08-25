using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Bitacora : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;

        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        //Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                ListarBitacora();
            }
        }

        #region metodos
        void ListarBitacora()
        {
            try
            {
                GridBitacora.DataSource = null;
                GridBitacora.DataSource = Seguridad.Listar();                
                GridBitacora.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion

        //por medio de este metodo controlamos cuantas paginas hay para presentar
        protected void GridBitacora_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerow = GridBitacora.BottomPagerRow;
            Label pagenro = (Label)pagerow.Cells[0].FindControl("Label1");
            Label totalPageNro = (Label)pagerow.Cells[0].FindControl("Label2");

            if((pagenro != null) && (totalPageNro != null))
            {
                int pagen = GridBitacora.PageIndex + 1;
                int tot = GridBitacora.PageCount;

                pagenro.Text = pagen.ToString();
                totalPageNro.Text = tot.ToString();
            }
        }

        protected void GridBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //esto hace que funcione los botones de "Anterior" y "Siguiente"
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            ListarBitacora(); 
        }
    }
}