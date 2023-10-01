using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Seguridad : System.Web.UI.Page, IObserver
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //si el usuario no esta logueado, lleva a la pagina de login
            if (!Page.IsPostBack)
            {
                if ((Usuario)(Session["UserSession"]) == null)
                {
                    Response.Redirect("../Home/Login.aspx");
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
        protected void LinkBackup_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Backup.aspx");
        }

        protected void LinkBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Bitacora.aspx");
        }

        protected void LinkRestore_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Restore.aspx");
        }

        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            LinkBackup.Text = Sujeto.TraducirObserver(LinkBackup.SkinID.ToString()) ?? LinkBackup.SkinID.ToString();
            LinkBitacora.Text = Sujeto.TraducirObserver(LinkBitacora.SkinID.ToString()) ?? LinkBitacora.SkinID.ToString();
            LinkRestore.Text = Sujeto.TraducirObserver(LinkRestore.SkinID.ToString()) ?? LinkRestore.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            LinkBackup.Text = SiteMaster.TraducirGlobal(LinkBackup.SkinID.ToString()) ?? LinkBackup.SkinID.ToString();
            LinkBitacora.Text = SiteMaster.TraducirGlobal(LinkBitacora.SkinID.ToString()) ?? LinkBitacora.SkinID.ToString();
            LinkRestore.Text = SiteMaster.TraducirGlobal(LinkRestore.SkinID.ToString()) ?? LinkRestore.SkinID.ToString();
        }

        #endregion
    }
}