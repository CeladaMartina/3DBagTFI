using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditFamilia : System.Web.UI.Page, IObserver
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
                //traduccion de la pagina
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                if (Request.QueryString["id"] == null)
                {
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Nueva Familia") ?? ("Nueva Familia");
                    btnFunction.Text = SiteMaster.TraducirGlobal("Agregar") ?? ("Agregar");
                    btnFunction.CssClass = "btn btn-success";

                    TraerTodasPatentes();
                }
                else
                {
                    //si el id viene, mostrara pantalla Editar                
                    lblTitulo.Text = SiteMaster.TraducirGlobal("Editar Familia") ?? ("Editar Familia");
                    btnFunction.Text = SiteMaster.TraducirGlobal("Editar") ?? ("Editar");
                    btnFunction.CssClass = "btn btn-primary";

                    TraerPatentes();
                }
            }
            
        }

        #region metodos
        void TraerTodasPatentes()
        {
            PNoAsig.DataSource = GestorPermisos.GetAllPatentes();
            PNoAsig.DataBind();
        }
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
                lblResultado.CssClass = "alert alert-danger";
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
                lblResultado.CssClass = "alert alert-danger";
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
                        try
                        {
                            //sacar dvh xq la familia no tiene dvh
                            AltaFamilia(txtNombre.Text);
                            lblResultado.Visible = true;
                            lblResultado.CssClass = "alert alert-success";
                            lblResultado.Text = SiteMaster.TraducirGlobal("Alta Familia correctamente") ?? ("Alta Familia correctamente");

                        }
                        catch(Exception ex)
                        {
                            lblResultado.Visible = true;
                            lblResultado.CssClass = "alert alert-danger";
                            lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                        }

                    }
                    else if (Request.QueryString["Funcion"] == "editar")
                    {
                        try
                        {
                            //sacar dvh xq la familia no tiene dvh
                            ModificarFamilia(Convert.ToInt32(Request.QueryString["id"]), txtNombre.Text);
                            lblResultado.Visible = true;
                            lblResultado.CssClass = "alert alert-success";
                            lblResultado.Text = SiteMaster.TraducirGlobal("Modificación Familia correctamente") ?? ("Modificación Familia correctamente");
                        }
                        catch(Exception ex)
                        {
                            lblResultado.Visible = true;
                            lblResultado.CssClass = "alert alert-danger";
                            lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                        }
                        
                    }

                    GuardarFamilia();
                }
                else
                {
                    lblResultado.Visible = true;
                    lblResultado.CssClass = "alert alert-danger";
                    lblResultado.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
            }
            catch (Exception)
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
        }

        protected void LinkRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndexFamilias.aspx");
        }

        #endregion

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblPatente.Text = Sujeto.TraducirObserver(lblPatente.SkinID.ToString()) ?? lblPatente.SkinID.ToString();
            patasig.Text = Sujeto.TraducirObserver(patasig.SkinID.ToString()) ?? patasig.SkinID.ToString();
            patnoasig.Text = Sujeto.TraducirObserver(patnoasig.SkinID.ToString()) ?? patnoasig.SkinID.ToString();
            btnAsignar.Text = Sujeto.TraducirObserver(btnAsignar.SkinID.ToString()) ?? btnAsignar.SkinID.ToString();
            btnNoAsignar.Text = Sujeto.TraducirObserver(btnNoAsignar.SkinID.ToString()) ?? btnNoAsignar.SkinID.ToString();
            LinkRedirect.Text = Sujeto.TraducirObserver(LinkRedirect.SkinID.ToString()) ?? LinkRedirect.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblPatente.Text = SiteMaster.TraducirGlobal(lblPatente.SkinID.ToString()) ?? lblPatente.SkinID.ToString();
            patasig.Text = SiteMaster.TraducirGlobal(patasig.SkinID.ToString()) ?? patasig.SkinID.ToString();
            patnoasig.Text = SiteMaster.TraducirGlobal(patnoasig.SkinID.ToString()) ?? patnoasig.SkinID.ToString();
            btnAsignar.Text = SiteMaster.TraducirGlobal(btnAsignar.SkinID.ToString()) ?? btnAsignar.SkinID.ToString();
            btnNoAsignar.Text = SiteMaster.TraducirGlobal(btnNoAsignar.SkinID.ToString()) ?? btnNoAsignar.SkinID.ToString();
            LinkRedirect.Text = SiteMaster.TraducirGlobal(LinkRedirect.SkinID.ToString()) ?? LinkRedirect.SkinID.ToString();
        }
        #endregion


    }
}