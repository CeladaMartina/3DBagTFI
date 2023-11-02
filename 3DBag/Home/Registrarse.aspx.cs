using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Registrarse : System.Web.UI.Page,IObserver
    {
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Cliente GestorCliente = new Negocio_BLL.Cliente();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();

        int IdUsuario = -1;
        private ContentPlaceHolder contentPlace;
        bool Estado = false;
        int Contador = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

            if (!Page.IsPostBack)
            {
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }
            }          
        }

        #region Boton
        public void Registrar(object sender, EventArgs e)
        {
            if (ChequearFallaTxt() == false)
            {
                try
                {
                    AltaUsuario(txtNick.Text, txtPassword.Text, txtNombre.Text, txtEmail.Text, false, 0,DropDownList1.SelectedValue, 0);
                    AltaCliente(IdUsuario, txtNombre.Text, txtApellido.Text, Seguridad.EncriptarAES(txtDNI.Text), txtEmail.Text, int.Parse(txtTelefono.Text), DateTime.Parse(txtFecha.Text));
                    LimpiarTxt();
                    IdUsuario = -1;
                    lblError.Visible = true;
                    lblError.Text = SiteMaster.TraducirGlobal("Registrado correctamente") ?? ("Registrado correctamente");
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
            }
        }
        #endregion

        #region metodos

        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtPassword.Text) ||  string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtNick.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                A = true;
            }
            return A;
        }

        void AltaCliente(int IdCliente, string nombre, string apellido, string dni, string email, int telefono, DateTime fechaNac)
        {
            GestorCliente.Alta(IdCliente, nombre, apellido, dni, email, telefono, fechaNac);
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Alta Cliente", "Media", 0);
            //LimpiarTxt();
            //IdUsuario = -1;
        }
        void AltaUsuario(string nick, string contraseña, string nombre, string email, bool Estado, int contador, string idioma, int dvh)
        {
            GestorUsuario.AltaUsuario(nick, contraseña, nombre, email, Estado, contador, idioma, dvh);
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Alta Usuario", "Alta", 0);
            //LimpiarTxt();
            //IdUsuario = -1;
        }

        void LimpiarTxt()
        {
            txtNick.Text = "";
            txtPassword.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtFecha.Text = "";
            DropDownList1.SelectedValue = "0";                
        }

        #endregion

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblNick.Text = Sujeto.TraducirObserver(lblNick.SkinID.ToString()) ?? lblNick.SkinID.ToString();
            Registracion.Text = Sujeto.TraducirObserver(Registracion.SkinID.ToString()) ?? Registracion.SkinID.ToString();
            Contraseña.Text = Sujeto.TraducirObserver(Contraseña.SkinID.ToString()) ?? Contraseña.SkinID.ToString();
            Nombre.Text = Sujeto.TraducirObserver(Nombre.SkinID.ToString()) ?? Nombre.SkinID.ToString();
            Apellido.Text = Sujeto.TraducirObserver(Apellido.SkinID.ToString()) ?? Apellido.SkinID.ToString();
            DNI.Text = Sujeto.TraducirObserver(DNI.SkinID.ToString()) ?? DNI.SkinID.ToString();
            Email.Text = Sujeto.TraducirObserver(Email.SkinID.ToString()) ?? Email.SkinID.ToString();
            Telefono.Text = Sujeto.TraducirObserver(Telefono.SkinID.ToString()) ?? Telefono.SkinID.ToString();
            Fecha_de_Nacimiento.Text = Sujeto.TraducirObserver(Fecha_de_Nacimiento.SkinID.ToString()) ?? Fecha_de_Nacimiento.SkinID.ToString();
            Idioma.Text = Sujeto.TraducirObserver(Idioma.SkinID.ToString()) ?? Idioma.SkinID.ToString();
            btnRegistrar.Text = Sujeto.TraducirObserver(btnRegistrar.SkinID.ToString()) ?? btnRegistrar.SkinID.ToString();
            linkVolver.Text = Sujeto.TraducirObserver(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }

        public void Traducir()
        {
            lblNick.Text = SiteMaster.TraducirGlobal(lblNick.SkinID.ToString()) ?? lblNick.SkinID.ToString();
            Registracion.Text = SiteMaster.TraducirGlobal(Registracion.SkinID.ToString()) ?? Registracion.SkinID.ToString();
            Contraseña.Text = SiteMaster.TraducirGlobal(Contraseña.SkinID.ToString()) ?? Contraseña.SkinID.ToString();
            Nombre.Text = SiteMaster.TraducirGlobal(Nombre.SkinID.ToString()) ?? Nombre.SkinID.ToString();
            Apellido.Text = SiteMaster.TraducirGlobal(Apellido.SkinID.ToString()) ?? Apellido.SkinID.ToString();
            DNI.Text = SiteMaster.TraducirGlobal(DNI.SkinID.ToString()) ?? DNI.SkinID.ToString();
            Email.Text = SiteMaster.TraducirGlobal(Email.SkinID.ToString()) ?? Email.SkinID.ToString();
            Telefono.Text = SiteMaster.TraducirGlobal(Telefono.SkinID.ToString()) ?? Telefono.SkinID.ToString();
            Fecha_de_Nacimiento.Text = SiteMaster.TraducirGlobal(Fecha_de_Nacimiento.SkinID.ToString()) ?? Fecha_de_Nacimiento.SkinID.ToString();
            Idioma.Text = SiteMaster.TraducirGlobal(Idioma.SkinID.ToString()) ?? Idioma.SkinID.ToString();
            btnRegistrar.Text = SiteMaster.TraducirGlobal(btnRegistrar.SkinID.ToString()) ?? btnRegistrar.SkinID.ToString();
            linkVolver.Text = SiteMaster.TraducirGlobal(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }
        #endregion

        protected void linkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx");
        }
    }
}