using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Administracion : System.Web.UI.Page, IObserver
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //si el usuario no esta logueado, lleva a la pagina de login
            if (!Page.IsPostBack)
            {
                if ((Usuario)(Session["UserSession"]) == null)
                {
                    Response.Redirect("/Home/Login.aspx");
                }
                else
                {                    
                    if (Session["IdiomaSelect"] != null)
                    {
                        DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                        masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                        Traducir();                        
                    }
                }             
                
            }
                
        }
        #region redirecciones
        protected void LinkGestionUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Usuarios/IndexUsuarios.aspx");
        }

        protected void LinkGestionFamilias_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PatenteFamilia/IndexFamilias.aspx");
        }

        protected void LinkGestionPatentes_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PatenteFamilia/IndexPatentes.aspx");
        }

        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            LinkGestionUsuarios.Text = Sujeto.TraducirObserver(LinkGestionUsuarios.SkinID.ToString()) ?? LinkGestionUsuarios.SkinID.ToString();
            LinkGestionFamilias.Text = Sujeto.TraducirObserver(LinkGestionFamilias.SkinID.ToString()) ?? LinkGestionFamilias.SkinID.ToString();
            LinkGestionPatentes.Text = Sujeto.TraducirObserver(LinkGestionPatentes.SkinID.ToString()) ?? LinkGestionPatentes.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            LinkGestionUsuarios.Text = SiteMaster.TraducirGlobal(LinkGestionUsuarios.SkinID.ToString()) ?? LinkGestionUsuarios.SkinID.ToString();
            LinkGestionFamilias.Text = SiteMaster.TraducirGlobal(LinkGestionFamilias.SkinID.ToString()) ?? LinkGestionFamilias.SkinID.ToString();
            LinkGestionPatentes.Text = SiteMaster.TraducirGlobal(LinkGestionPatentes.SkinID.ToString()) ?? LinkGestionPatentes.SkinID.ToString();
        }
        #endregion


    }
}