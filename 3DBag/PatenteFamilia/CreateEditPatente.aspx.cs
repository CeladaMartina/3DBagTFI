using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditPatente : System.Web.UI.Page
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

                // si el cod viene null, mostrara pantalla Alta
                if (Request.QueryString["patente"] == null)
                {
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Nueva Patente") ?? ("Nueva Patente");
                    btnFunction.Text = SiteMaster.TraducirGlobal("Agregar") ?? ("Agregar");                    
                }
                else
                {
                    //si el cod viene, mostrara pantalla Editar                    
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Editar Patente") ?? ("Editar Patente");
                    btnFunction.Text = SiteMaster.TraducirGlobal("Editar") ?? ("Editar");

                    PatTemp = new Propiedades_BE.Patente();
                    PatTemp.Id = Convert.ToInt32(Request.QueryString["patente"]);
                    PatTemp.Nombre = Request.QueryString["nombre"];

                    txtNombre.Text = PatTemp.Nombre;
                    MostrarPatenteSelect(PatTemp.Id);
                }

            }
        }

        void MostrarPatenteSelect(int id)
        {
            txtDescripcion.Text = GestorPermisos.traerPermiso(id);
        }

        protected void LinkRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PatenteFamilia/IndexPatentes.aspx");
        }

        protected void btnFunction_Click(object sender, EventArgs e)
        {
            try
            {
                if(ChequearFallaTxt() == false)
                {
                    if(Request.QueryString["Funcion"] == "alta")
                    {
                        Alta(0, txtNombre.Text, txtDescripcion.Text);

                    }else if(Request.QueryString["Funcion"] == "editar")
                    {
                        Modificar(Convert.ToInt32(Request.QueryString["patente"]), txtNombre.Text, txtDescripcion.Text);
                    }
                }

            }catch(Exception ex)
            {

            }
        }

        void Alta(int id, string nombre, string descripcion)
        {

        }

        void Modificar(int id, string nombre, string descripcion)
        {
            GestorPermisos.ModificarPatente(id, nombre, descripcion);

        }

        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescripcion.Text))
            {
                A = true;
            }
            return A;
        }
        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDescripcion.Text = Sujeto.TraducirObserver(lblDescripcion.SkinID.ToString()) ?? lblDescripcion.SkinID.ToString();
            LinkRedirect.Text = Sujeto.TraducirObserver(LinkRedirect.SkinID.ToString()) ?? LinkRedirect.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDescripcion.Text = SiteMaster.TraducirGlobal(lblDescripcion.SkinID.ToString()) ?? lblDescripcion.SkinID.ToString();
            LinkRedirect.Text = SiteMaster.TraducirGlobal(LinkRedirect.SkinID.ToString()) ?? LinkRedirect.SkinID.ToString();
        }
        #endregion
    }
}