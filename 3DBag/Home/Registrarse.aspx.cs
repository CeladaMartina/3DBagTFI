using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Registrarse : System.Web.UI.Page
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
                    lblError.Text = "Registrado exitosamente.";
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = "Error.";
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Complete todos los campos.";
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
    }
}