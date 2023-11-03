using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class VerPatente : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Patente PatTemp;
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


                PatTemp = new Propiedades_BE.Patente();
                PatTemp.Id = Convert.ToInt32(Request.QueryString["patente"]);
                PatTemp.Nombre = Request.QueryString["nombre"];

                lblNombreResp.Text = PatTemp.Nombre;
                MostrarPatenteSelect(PatTemp.Id);
            }
        }

        void MostrarPatenteSelect(int id)
        {
            lblDescripcionResp.Text = GestorPermisos.traerPermiso(id);
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDescripcion.Text = Sujeto.TraducirObserver(lblDescripcion.SkinID.ToString()) ?? lblDescripcion.SkinID.ToString();
            LinkRedireccion.Text = Sujeto.TraducirObserver(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDescripcion.Text = SiteMaster.TraducirGlobal(lblDescripcion.SkinID.ToString()) ?? lblDescripcion.SkinID.ToString();
            LinkRedireccion.Text = SiteMaster.TraducirGlobal(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
        }
        #endregion

        protected void LinkRedireccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PatenteFamilia/IndexPatentes.aspx");
        }
    }
}