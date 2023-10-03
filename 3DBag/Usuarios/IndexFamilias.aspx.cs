using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexFamilias : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Familia FamTemp;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                //traduccion de la pagina
                
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Permisos)))
                {
                    divFamilias.Visible = true;
                    lblPermiso.Visible = false;
                    ListarFamilias();
                }
                else
                {
                    divFamilias.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }

                
            }
        }

        void ListarFamilias()
        {
            try
            {
                gridFamilias.DataSource = null;
                gridFamilias.DataSource = GestorPermisos.GetAllFamilias();
                gridFamilias.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }

        protected void gridFamilias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int crow;
            crow = Convert.ToInt32(e.CommandArgument.ToString());
            string v = gridFamilias.Rows[crow].Cells[0].Text;

            if (e.CommandName == "select")
            {
                //enviamos los valores de la familia                
                FamTemp = new Propiedades_BE.Familia();
                FamTemp.Id = Convert.ToInt32(v);
                FamTemp.Nombre = gridFamilias.Rows[crow].Cells[1].Text;

                Response.Redirect("VerFamilias.aspx?id=" + FamTemp.Id + "&nombre=" + FamTemp.Nombre);

            }
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();            
        }
        #endregion
    }
}