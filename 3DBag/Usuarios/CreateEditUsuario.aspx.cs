using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditUsuario : System.Web.UI.Page
    {        
        string nick;
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();        

        Propiedades_BE.Usuario TempUs;        
        protected void Page_Load(object sender, EventArgs e)
        {            
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario)))
                {
                    divUsuarios.Visible = true;
                    lblPermiso.Visible = false;

                    // si el cod viene null, mostrara pantalla Alta
                if (Request.QueryString["usuario"] == null)
                    {
                        lblTitulo.Text = SiteMaster.TraducirGlobal("Nuevo Usuario") ?? ("Nuevo Usuario");
                        btnFunction.Text = SiteMaster.TraducirGlobal("Agregar") ?? ("Agregar");
                        TraerPatentes();
                        TrearFamilias();
                    }
                    else
                    {
                        //si el cod viene, mostrara pantalla Editar
                        nick = Request.QueryString["usuario"];
                        lblTitulo.Text = SiteMaster.TraducirGlobal("Editar Usuario") ?? ("Editar Usuario");
                        btnFunction.Text = SiteMaster.TraducirGlobal("Editar") ?? ("Editar");
                        TraerUsuario();
                        TraerPermisos();
                    }
                }
                else
                {
                    divUsuarios.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }
                
            }           
        }

        #region metodos
        void TraerUsuario()
        {
            List<Propiedades_BE.Usuario> usuario = GestorUsuario.consultarNick(nick);
            txtIdUsuario.Text = usuario[0].IdUsuario.ToString();
            txtNick.Text = usuario[0].Nick;            
            txtNombre.Text = usuario[0].Nombre;
            txtMail.Text = usuario[0].Mail;            

            if (usuario[0].Estado == true)
            {
                checkBloqueado.Checked = true;
            }
            else
            {
                checkBloqueado.Checked = false;
            }

            if(Convert.ToString(usuario[0].IdIdioma) == "1")
            {
                txtIdioma.Text = "Español";
            }
            else
            {
                txtIdioma.Text = "Ingles";
            }            

            if (usuario[0].BajaLogica == true)
            {
                checkBaja.Checked = true;
            }
            else
            {
                checkBaja.Checked = false;
            }           
        }

        void Modificar(int Id, string Nick, string Nombre, string Mail, bool Estado, int Contador, string Idioma, int DVH)
        {
            if (GestorUsuario.Modificar(Id, Nick, Nombre, Mail, Estado, Contador, Idioma, DVH) == 0)
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "label-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Usuario modificado correctamente") ?? ("Usuario modificado correctamente");
            }

            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Modificar usuario", "Alta", 0);           
            //LimpiarTxt();
            
        }

        void Alta(string Nick, string Contraseña, string Nombre, string Mail, bool Estado, int Contador, string Idioma, int DVH)
        {
            GestorUsuario.AltaUsuario(Nick, Contraseña, Nombre, Mail, Estado, Contador, Idioma, DVH);
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Alta usuario", "Alta", 0);
            //LimpiarTxt();            
        }

        void LimpiarTxt()
        {
            txtNick.Text = "";
            txtNombre.Text = "";
            txtMail.Text = "";
            txtIdioma.Text = "";
            txtContraseña.Text = "";
            checkBaja.Checked = false;
            checkBloqueado.Checked = false;

        }

        //trae permisos de ese usuario
        void TraerPermisos()
        {
            TempUs = new Propiedades_BE.Usuario();
            TempUs.IdUsuario = GestorUsuario.SeleccionarIDNick(txtNick.Text);
            TempUs.Nombre = txtNombre.Text;

            //trae las familias de ese usuario especifico
            FAsig.DataSource = GestorPermisos.FillUserComponentsListF(TempUs);
            FAsig.DataBind();            

            //trae las patentes de ese usuario especifico
            PAsig.DataSource = GestorPermisos.FillUserComponentsList(TempUs);
            PAsig.DataBind();

            TraerPatentes();
            TrearFamilias();

            //elimina de patente no asignada, permisos que ya tiene el usuario
            foreach (ListItem item1 in PAsig.Items)
            {
                bool existe = false;

                foreach(ListItem item2 in PNoAsig.Items)
                {
                    if(item1.Text == item2.Text && item1.Value == item2.Value)
                    {
                        existe = true;
                        PNoAsig.Items.Remove(item2);
                        break;
                    }
                }                
            }

            //elimina de familia No asignada, familias que ya tiene el usuario
            foreach (ListItem item1 in FAsig.Items)
            {
                bool existe = false;

                foreach (ListItem item2 in FNoAsig.Items)
                {
                    if (item1.Text == item2.Text && item1.Value == item2.Value)
                    {
                        existe = true;
                        FNoAsig.Items.Remove(item2);
                        break;
                    }
                }
            }
        }

        void TraerPatentes()
        {
            //trae todas las patentes
            PNoAsig.DataSource = GestorPermisos.GetAllPatentes();
            PNoAsig.DataBind();
        }

        void TrearFamilias()
        {
            //trae todas las familias
            FNoAsig.DataSource = GestorPermisos.GetAllFamilias();
            FNoAsig.DataBind();
        }

        void AgregarPatente()
        {
            TempUs = new Propiedades_BE.Usuario();
            if (TempUs != null)
            {
                string ope = PNoAsig.SelectedItem.Value;
                PAsig.Items.Add(ope);
                PNoAsig.Items.Remove(ope);
            }
        }

        void QuitarPatente()
        {
            TempUs = new Propiedades_BE.Usuario();

            if (TempUs != null)
            {
                string ope = PAsig.SelectedItem.Value;
                PNoAsig.Items.Add(ope);
                PAsig.Items.Remove(ope);
            }
        }

        void AgregarFamilia()
        {
            TempUs = new Propiedades_BE.Usuario();

            if (TempUs != null)
            {
                string ope = FNoAsig.SelectedItem.Value;
                FAsig.Items.Add(ope);
                FNoAsig.Items.Remove(ope);
            }
        }

        void QuitarFamilia()
        {
            TempUs = new Propiedades_BE.Usuario();

            if (TempUs != null)
            {
                string ope = FAsig.SelectedItem.Value;
                FNoAsig.Items.Add(ope);
                FAsig.Items.Remove(ope);
            }
        }

        void GuardarPatente()
        {            
            Propiedades_BE.Componente c1;
            
            TempUs = new Propiedades_BE.Usuario();            
            TempUs.IdUsuario = GestorUsuario.SeleccionarIDNick(txtNick.Text);

            foreach (ListItem item in PAsig.Items)
            {
                c1 = new Propiedades_BE.Patente();
                string permiso = item.Text.Replace(" ", "_");                
                c1.Nombre = item.ToString();                

                c1.Id = GestorPermisos.traerIDPermiso(c1.Nombre);
                c1.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permiso);

                TempUs.Permisos.Add(c1);
            }
            
            GestorUsuario.GuardarPermisos(TempUs);

        }

        void GuardarFamilia()
        {
            Propiedades_BE.Componente c1;

            TempUs = new Propiedades_BE.Usuario();
            TempUs.IdUsuario = GestorUsuario.SeleccionarIDNick(txtNick.Text);

            foreach (ListItem item in FAsig.Items)
            {
                c1 = new Propiedades_BE.Familia();
                string permiso = item.Text.Replace(" ", "_");
                c1.Nombre = item.ToString();

                c1.Id = GestorPermisos.traerIDPermiso(c1.Nombre);               

                TempUs.Permisos.Add(c1);
            }

            GestorUsuario.GuardarPermisos(TempUs);
        }

        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtNick.Text) || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                A = true;
            }
            return A;
        }

        
        #endregion

        #region boton       

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
                    if (Request.QueryString["Funcion"] == "alta")
                    {
                        Alta(txtNick.Text, txtContraseña.Text, txtNombre.Text, txtMail.Text, checkBloqueado.Checked, 0, txtIdioma.Text, 0);
                    }
                    else if(Request.QueryString["Funcion"] == "editar")
                    {
                        if (txtContraseña.Text != "")
                        {
                            if (Seguridad.ValidarClave(txtContraseña.Text) == true)
                            {
                                GestorUsuario.ConfirmarCambioContraseña(txtNick.Text, txtContraseña.Text, txtMail.Text);
                                Seguridad.CargarBitacora(GestorUsuario.SeleccionarIDNick(txtNick.Text), DateTime.Now, "Contraseña cambiada", "Alta", 0);
                            }
                            else
                            {
                                lblResultado.Visible = true;
                                lblResultado.Text = SiteMaster.TraducirGlobal("Ingrese otra contraseña") ?? ("Ingrese otra contraseña");
                            }
                        }

                        Modificar(Convert.ToInt32(txtIdUsuario.Text), txtNick.Text, txtNombre.Text, txtMail.Text, checkBloqueado.Checked, 0, txtIdioma.Text, 0);
                    }

                    GuardarPatente();
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
            Response.Redirect("../Usuarios/IndexUsuarios.aspx");
        }

        protected void btnAsignarF_Click(object sender, EventArgs e)
        {
            try
            {
                AgregarFamilia();
            }
            catch (Exception)
            {
                lblResultado.Visible = true;
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
        }

        protected void btnNoAsignarF_Click(object sender, EventArgs e)
        {
            try
            {
                QuitarFamilia();
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