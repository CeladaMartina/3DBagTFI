using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexProducto : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Producto GestorProducto = new Negocio_BLL.Producto();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                //traduccion de la pagina
                Session["UserSession"] = null;
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

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
            int crow;
            crow = Convert.ToInt32(e.CommandArgument.ToString());
            string v = gridProducto.Rows[crow].Cells[0].Text;

            if (e.CommandName == "editar")
            {
                //enviamos el codProd del Prod
                Response.Redirect("CreateEditProducto.aspx?producto=" + v);
            }
            //enviamos el codProd del Prod y la funcion a realizar
            else if (e.CommandName == "select")
            {

                Response.Redirect("DeleteShowProducto.aspx?producto=" + v + "&Funcion=Ver");

            }
            else if(e.CommandName == "borrar")
            {
                Response.Redirect("DeleteShowProducto.aspx?producto=" + v + "&Funcion=borrar");
            }
        }

        protected void btnWebService_Click(object sender, EventArgs e)
        {
            WebService webService = new WebService();
            lblRespuesta.Text = webService.TopListaProd();           
            lblRespuesta.Visible = true;
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAlta.Text = Sujeto.TraducirObserver(btnAlta.SkinID.ToString()) ?? btnAlta.SkinID.ToString();
            btnWebService.Text = Sujeto.TraducirObserver(btnWebService.SkinID.ToString()) ?? btnWebService.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAlta.Text = SiteMaster.TraducirGlobal(btnAlta.SkinID.ToString()) ?? btnAlta.SkinID.ToString();
            btnWebService.Text = SiteMaster.TraducirGlobal(btnWebService.SkinID.ToString()) ?? btnWebService.SkinID.ToString();
        }
        #endregion
    }
}