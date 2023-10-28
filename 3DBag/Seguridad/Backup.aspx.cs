using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace _3DBag
{
    public partial class Backup : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();

        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

            //traduccion de la pagina
            
            if (Session["IdiomaSelect"] != null)
            {
                DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                Traducir();
            }

            if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Realizar_BackUp)))
            {                
                divBackup.Visible = true;
                lblPermiso.Visible = false;
            }
            else
            {
                divBackup.Visible = false;
                lblResultado.CssClass = "alert alert-danger";
                lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                lblPermiso.Visible = true;
            }
            
        }

        #region metodos
        
        public void Generar(object sender, EventArgs e)
        {
            if(ChequearFallaTxt() == false)
            {
                try
                {
                    RealizarBackUp();
                }
                catch (Exception ex)
                {
                    lblResultado.Visible = true;
                    lblResultado.CssClass = "alert alert-danger";
                    lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                    Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Error de backup", "Alta", 0);
                }
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
            }
                
        }

        void RealizarBackUp()
        {
            Directory.CreateDirectory(txtRuta.Text);
            DirectorySecurity sec = Directory.GetAccessControl(txtRuta.Text);

            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(txtRuta.Text, sec);

            string Backup = Seguridad.GenerarBackUp(txtNombre.Text, txtRuta.Text);
            if (Backup == "ok")
            {
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Backup exitoso", "Alta", 0);
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Backup realizado correctamente") ?? ("Backup realizado correctamente");                
            }
            else
            {
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Backup error", "Alta", 0);
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Se produjo error al realizar el Backup") ?? ("Se produjo error al realizar el Backup");                
            }
        }
        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtRuta.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                A = true;
            }
            return A;
        }
        #endregion

        #region traduccion
        public void Update(ISubject Subject)
        {
            lblTitulo.Text = Subject.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblRuta.Text = Subject.TraducirObserver(lblRuta.SkinID.ToString()) ?? lblRuta.SkinID.ToString();
            lblNombre.Text = Subject.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
        }

        public void Traducir()
        {           
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblRuta.Text = SiteMaster.TraducirGlobal(lblRuta.SkinID.ToString()) ?? lblRuta.SkinID.ToString();
        }
        #endregion
    }
}