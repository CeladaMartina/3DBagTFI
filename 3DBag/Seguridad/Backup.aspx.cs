﻿using System;
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
            
            //Traducir();
            if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Realizar_BackUp)))
            {                
                divBackup.Visible = true;
                lblPermiso.Visible = false;
            }
            else
            {
                divBackup.Visible = false;
                lblPermiso.Text = "No tiene los permisos para realizar esta accion";
                lblPermiso.Visible = true;
            }
            
        }

        #region metodos
        
        public void Generar(object sender, EventArgs e)
        {
            try
            {
                if(ChequearFallaTxt() == false)
                {
                    RealizarBackUp();
                }
            }catch(Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Complete los datos";
                lblResultado.CssClass = "alert alert-warning";
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
                lblResultado.Text = "Backup realizado correctamente.";
                lblResultado.CssClass = "alert alert-success";
            }
            else
            {
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Backup error", "Alta", 0);
                lblResultado.Visible = true;
                lblResultado.Text = "Se produjo error al realizar el backup";
                lblResultado.CssClass = "alert alert-warning";
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
            lblRuta.Text = Subject.TraducirObserver(lblRuta.SkinID.ToString()) ?? lblRuta.SkinID.ToString();
            lblNombre.Text = Subject.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
        }

        public void Traducir()
        {            
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblRuta.Text = SiteMaster.TraducirGlobal(lblRuta.SkinID.ToString()) ?? lblRuta.SkinID.ToString();
        }
        #endregion
    }
}