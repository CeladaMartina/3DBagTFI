using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditUsuario : System.Web.UI.Page, IObserver
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
                //traduccion de la pagina
                
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario)))
                {
                    divUsuarios.Visible = true;
                    lblPermiso.Visible = false;

                    // si el cod viene null, mostrara pantalla Alta
                if (Request.QueryString["usuario"] == null)
                    {
                        lblTitulo.Text = SiteMaster.TraducirGlobal("Nuevo Usuario") ?? ("Nuevo Usuario");
                        btnFunction.Text = SiteMaster.TraducirGlobal("Agregar") ?? ("Agregar");
                        btnFunction.CssClass = "btn btn-success";
                        TraerPatentes();
                        TrearFamilias();

                    }
                    else
                    {
                        //si el cod viene, mostrara pantalla Editar
                        nick = Request.QueryString["usuario"];
                        lblTitulo.Text = SiteMaster.TraducirGlobal("Editar Usuario") ?? ("Editar Usuario");
                        btnFunction.Text = SiteMaster.TraducirGlobal("Editar") ?? ("Editar");
                        btnFunction.CssClass = "btn btn-primary";
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

        void Modificar(int Id, string Nick, string Nombre, string Mail, bool Estado, int Contador, string Idioma, bool Baja,  int DVH)
        {
            try
            {
                GestorUsuario.Modificar(Id, Nick, Nombre, Mail, Estado, Contador, Idioma, Baja, DVH);
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Usuario modificado correctamente") ?? ("Usuario modificado correctamente");

            }
            catch (Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");

            }
        }

        void Alta(string Nick, string Contraseña, string Nombre, string Mail, bool Estado, int Contador, string Idioma,  int DVH)
        {
            try
            {
                GestorUsuario.AltaUsuario(Nick, Contraseña, Nombre, Mail, Estado, Contador, Idioma, DVH);
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Usuario alta correctamente") ?? ("Usuario alta correctamente");

            }
            catch (Exception)
            {
                lblResultado.Visible = true;
                lblResultado.CssClass = "alert alert-danger";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }

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

            //recorro lista patente
            foreach (ListItem item in PAsig.Items)
            {
                c1 = new Propiedades_BE.Patente();
                string permiso = item.Text.Replace(" ", "_");                
                c1.Nombre = item.ToString();                

                c1.Id = GestorPermisos.traerIDPermiso(c1.Nombre);
                c1.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permiso);

                TempUs.Permisos.Add(c1);
            }
            //recorro lista familia
            foreach (ListItem item in FAsig.Items)
            {
                c1 = new Propiedades_BE.Familia();
                string permiso = item.Text.Replace(" ", "_");
                c1.Nombre = item.ToString();

                c1.Id = GestorPermisos.traerIDFamilia(c1.Nombre);

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
                        Alta(txtNick.Text, txtContraseña.Text, txtNombre.Text, txtMail.Text, checkBloqueado.Checked, 0, txtIdioma.Text,  0);                       
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

                        Modificar(Convert.ToInt32(txtIdUsuario.Text), txtNick.Text, txtNombre.Text, txtMail.Text, checkBloqueado.Checked, 0, txtIdioma.Text, checkBaja.Checked, 0);                        
                    }

                    GuardarPatente();                    
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

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            tituloFamAsig.Text = Sujeto.TraducirObserver(tituloFamAsig.SkinID.ToString()) ?? tituloFamAsig.SkinID.ToString();
            tituloNoFamAsig.Text = Sujeto.TraducirObserver(tituloNoFamAsig.SkinID.ToString()) ?? tituloNoFamAsig.SkinID.ToString();
            tituloPatAsig.Text = Sujeto.TraducirObserver(tituloPatAsig.SkinID.ToString()) ?? tituloPatAsig.SkinID.ToString();
            tituloPatNoAsig.Text = Sujeto.TraducirObserver(tituloPatNoAsig.SkinID.ToString()) ?? tituloPatNoAsig.SkinID.ToString();
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblUsuario.Text = Sujeto.TraducirObserver(lblUsuario.SkinID.ToString()) ?? lblUsuario.SkinID.ToString();
            lblNick.Text = Sujeto.TraducirObserver(lblNick.SkinID.ToString()) ?? lblNick.SkinID.ToString();
            lblContraseña.Text = Sujeto.TraducirObserver(lblContraseña.SkinID.ToString()) ?? lblContraseña.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblMail.Text = Sujeto.TraducirObserver(lblMail.SkinID.ToString()) ?? lblMail.SkinID.ToString();
            lblIdioma.Text = Sujeto.TraducirObserver(lblIdioma.SkinID.ToString()) ?? lblIdioma.SkinID.ToString();
            lblBloqueado.Text = Sujeto.TraducirObserver(lblBloqueado.SkinID.ToString()) ?? lblBloqueado.SkinID.ToString();
            lblBaja.Text = Sujeto.TraducirObserver(lblBaja.SkinID.ToString()) ?? lblBaja.SkinID.ToString();
            lblFamilia.Text = Sujeto.TraducirObserver(lblFamilia.SkinID.ToString()) ?? lblFamilia.SkinID.ToString();
            btnAsignarF.Text = Sujeto.TraducirObserver(btnAsignarF.SkinID.ToString()) ?? btnAsignarF.SkinID.ToString();
            btnNoAsignarF.Text = Sujeto.TraducirObserver(btnNoAsignarF.SkinID.ToString()) ?? btnNoAsignarF.SkinID.ToString();
            btnAsignar.Text = Sujeto.TraducirObserver(btnAsignar.SkinID.ToString()) ?? btnAsignar.SkinID.ToString();
            btnNoAsignar.Text = Sujeto.TraducirObserver(btnNoAsignar.SkinID.ToString()) ?? btnNoAsignar.SkinID.ToString();
            btnFunction.Text = Sujeto.TraducirObserver(btnFunction.SkinID.ToString()) ?? btnFunction.SkinID.ToString();
            LinkRedirect.Text = Sujeto.TraducirObserver(LinkRedirect.SkinID.ToString()) ?? LinkRedirect.SkinID.ToString();
        }

        public void Traducir()
        {
            tituloFamAsig.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            tituloNoFamAsig.Text = SiteMaster.TraducirGlobal(tituloNoFamAsig.SkinID.ToString()) ?? tituloNoFamAsig.SkinID.ToString();
            tituloPatAsig.Text = SiteMaster.TraducirGlobal(tituloPatAsig.SkinID.ToString()) ?? tituloPatAsig.SkinID.ToString();
            tituloPatNoAsig.Text = SiteMaster.TraducirGlobal(tituloPatNoAsig.SkinID.ToString()) ?? tituloPatNoAsig.SkinID.ToString();
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblUsuario.Text = SiteMaster.TraducirGlobal(lblUsuario.SkinID.ToString()) ?? lblUsuario.SkinID.ToString();
            lblNick.Text = SiteMaster.TraducirGlobal(lblNick.SkinID.ToString()) ?? lblNick.SkinID.ToString();
            lblContraseña.Text = SiteMaster.TraducirGlobal(lblContraseña.SkinID.ToString()) ?? lblContraseña.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblMail.Text = SiteMaster.TraducirGlobal(lblMail.SkinID.ToString()) ?? lblMail.SkinID.ToString();
            lblIdioma.Text = SiteMaster.TraducirGlobal(lblIdioma.SkinID.ToString()) ?? lblIdioma.SkinID.ToString();
            lblBloqueado.Text = SiteMaster.TraducirGlobal(lblBloqueado.SkinID.ToString()) ?? lblBloqueado.SkinID.ToString();
            lblBaja.Text = SiteMaster.TraducirGlobal(lblBaja.SkinID.ToString()) ?? lblBaja.SkinID.ToString();
            lblFamilia.Text = SiteMaster.TraducirGlobal(lblFamilia.SkinID.ToString()) ?? lblFamilia.SkinID.ToString();
            btnAsignarF.Text = SiteMaster.TraducirGlobal(btnAsignarF.SkinID.ToString()) ?? btnAsignarF.SkinID.ToString();
            btnNoAsignarF.Text = SiteMaster.TraducirGlobal(btnNoAsignarF.SkinID.ToString()) ?? btnNoAsignarF.SkinID.ToString();
            lblPatente.Text = SiteMaster.TraducirGlobal(lblPatente.SkinID.ToString()) ?? lblPatente.SkinID.ToString();
            btnAsignar.Text = SiteMaster.TraducirGlobal(btnAsignar.SkinID.ToString()) ?? btnAsignar.SkinID.ToString();
            btnNoAsignar.Text = SiteMaster.TraducirGlobal(btnNoAsignar.SkinID.ToString()) ?? btnNoAsignar.SkinID.ToString();
            btnFunction.Text = SiteMaster.TraducirGlobal(btnFunction.SkinID.ToString()) ?? btnFunction.SkinID.ToString();
            LinkRedirect.Text = SiteMaster.TraducirGlobal(LinkRedirect.SkinID.ToString()) ?? LinkRedirect.SkinID.ToString();
        }
        #endregion

    }
}