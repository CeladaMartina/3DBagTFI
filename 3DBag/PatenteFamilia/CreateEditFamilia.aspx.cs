using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditFamilia : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Familia FamTemp = new Propiedades_BE.Familia();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                lblTitulo.Text = SiteMaster.TraducirGlobal("Nueva Familia") ?? ("Nueva Familia");
                btnFunction.Text = SiteMaster.TraducirGlobal("Agregar") ?? ("Agregar");                
            }
            else
            {
                //si el id viene, mostrara pantalla Editar                
                lblTitulo.Text = SiteMaster.TraducirGlobal("Editar Familia") ?? ("Editar Familia");
                btnFunction.Text = SiteMaster.TraducirGlobal("Editar") ?? ("Editar");

                TraerPatentes();                
            }
        }

        #region metodos
        
        void TraerPatentes()
        {
            FamTemp.Id = Convert.ToInt32(Request.QueryString["id"]);
            FamTemp.Nombre = Request.QueryString["nombre"];
            txtNombre.Text = FamTemp.Nombre;

            PAsig.DataSource = GestorPermisos.TraerPatentesDeFamilia(FamTemp);
            PAsig.DataBind();

            TraerTodasPatentes();

            //elimina de patente no asignada, permisos que ya tiene la familia
            foreach (ListItem item1 in PAsig.Items)
            {
                bool existe = false;

                foreach (ListItem item2 in PNoAsig.Items)
                {
                    if (item1.Text == item2.Text && item1.Value == item2.Value)
                    {
                        existe = true;
                        PNoAsig.Items.Remove(item2);
                        break;
                    }
                }
            }
        }
        
        void AgregarPatente()
        {
            string ope = PNoAsig.SelectedItem.Value;
            PAsig.Items.Add(ope);
            PNoAsig.Items.Remove(ope);
        }

        void QuitarPatente()
        {
            string ope = PAsig.SelectedItem.Value;
            PNoAsig.Items.Add(ope);
            PAsig.Items.Remove(ope);
        }

        void TraerTodasPatentes()
        {            
            PNoAsig.DataSource = GestorPermisos.GetAllPatentes();
            PNoAsig.DataBind();
        }
        #endregion

        #region botones
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                AgregarPatente();
            }
            catch (Exception)
            {
                lblResultado.Visible = true;
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }

        }

        protected void btnNoAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                QuitarPatente();
            }
            catch (Exception)
            {
                lblResultado.Visible = true;
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
        }
        #endregion
    }
}