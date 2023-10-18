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

        long DV = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            
        }

        #region metodos

        void TraerPatentes()
        {
            FamTemp.Id = Convert.ToInt32(Request.QueryString["id"]);
            FamTemp.Nombre = Request.QueryString["nombre"];
            txtNombre.Text = FamTemp.Nombre;

            PAsig.DataSource = GestorPermisos.TraerPatentesDeFamilia(FamTemp);    
            PAsig.DataBind();

            PNoAsig.DataSource = GestorPermisos.GetAllPatentes();
            PNoAsig.DataBind();

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
        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                A = true;
            }
            return A;
        }
        void GuardarFamilia()
        {
            Propiedades_BE.Componente c1;

            FamTemp = new Propiedades_BE.Familia();
            FamTemp.Id = Convert.ToInt32(Request.QueryString["id"]);

            //recorro lista patente
            foreach (ListItem item in PAsig.Items)
            {
                c1 = new Propiedades_BE.Patente();
                string permiso = item.Text.Replace(" ", "_");
                c1.Nombre = item.ToString();

                c1.Id = GestorPermisos.traerIDPermiso(c1.Nombre);
                c1.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permiso);

                FamTemp.AgregarHijo(c1);
            }

            GestorPermisos.GuardarFamilia(FamTemp);

        }

        void AltaFamilia(string nombre)
        {
            FamTemp = new Propiedades_BE.Familia();
            FamTemp.Nombre = nombre;            
            GestorPermisos.GuardarComponente(FamTemp, true);
        }

        void ModificarFamilia(int id, string nombre)
        {
            GestorPermisos.ModificarFamilia(id, nombre);
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

        protected void btnFunction_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChequearFallaTxt() == false)
                {
                    if (Request.QueryString["Funcion"] == "alta")
                    {
                        //sacar dvh xq la familia no tiene dvh
                        AltaFamilia(txtNombre.Text);

                    }
                    else if (Request.QueryString["Funcion"] == "editar")
                    {
                        //sacar dvh xq la familia no tiene dvh
                        ModificarFamilia(Convert.ToInt32(Request.QueryString["id"]), txtNombre.Text);
                    }

                    GuardarFamilia();
                }
                else
                {
                    lblResultado.Visible = true;
                    lblResultado.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
            }
            catch (Exception)
            {
                lblResultado.Visible = true;
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
        }

        protected void LinkRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexFamilias.aspx");
        }

        #endregion


    }
}