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
            foreach (ListItem i in PNoAsig.Items)
            {
                if(i.Selected == true)
                {
                    string ope = i.Value;
                }
            }
          
            //string ope = PNoAsig.SelectedItem.Value;
            //PAsig.Items.Add(ope);
            //PNoAsig.Items.Remove(ope);
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

        void AltaFamilia(string nombre, int DVH)
        {
            FamTemp = new Propiedades_BE.Familia();
            FamTemp.Nombre = nombre;
            FamTemp.DVH = DVH;
            GestorPermisos.GuardarComponente(FamTemp, true);

            DV = Seguridad.CalcularDVH("select * from Permiso where id=(select TOP 1 id from Permiso order by id desc)", "Permiso");
            Seguridad.EjecutarConsulta("Update Permiso set DVH= '" + DV + "' where id=(select TOP 1 id from Permiso order by id desc)");
            Seguridad.ActualizarDVV("Permiso", Seguridad.SumaDVV("Permiso"));
        }

        void ModificarFamilia(int id, string nombre, int dvh)
        {            
            ModificarFamilia(id, nombre, dvh);
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
                if(ChequearFallaTxt() == false)
                {
                    if(Request.QueryString["Funcion"] == "alta")
                    {
                        AltaFamilia(txtNombre.Text, 0);

                    }else if(Request.QueryString["Funcion"] == "editar")
                    {
                        ModificarFamilia(Convert.ToInt32(Request.QueryString["id"]), txtNombre.Text, 0);
                    }

                    GuardarFamilia();
                }
                else
                {
                    lblResultado.Visible = true;
                    lblResultado.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
            }catch(Exception)
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