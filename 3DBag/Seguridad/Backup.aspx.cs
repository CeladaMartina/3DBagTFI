using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace _3DBag
{
    public partial class Backup : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        }

        protected void browse(object sender, EventArgs e)
        {
            Thread thdSyncRead = new Thread(new ThreadStart(openfolder));
            thdSyncRead.SetApartmentState(ApartmentState.STA);
            thdSyncRead.Start();
        }

        public void openfolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            //fixear que el txt se llene con el path
            txtRuta.Text = fbd.SelectedPath;
        }

        public void Generar(object sender, EventArgs e)
        {
            Directory.CreateDirectory(txtRuta.Text);
            DirectorySecurity sec = Directory.GetAccessControl(txtRuta.Text);

            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(txtRuta.Text, sec);

            string Backup = Seguridad.GenerarBackUp(txtNombre.Text, txtRuta.Text);
            if(Backup == "ok")
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Backup realizado correctamente.";
                lblResultado.CssClass = "alert alert-success";
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Se produjo error al realizar el backup";
                lblResultado.CssClass = "alert alert-warning";
            }
        }



    }
}