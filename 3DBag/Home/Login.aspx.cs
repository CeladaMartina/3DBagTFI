using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace _3DBag
{
    public partial class Login : System.Web.UI.Page
    {
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Propiedades_BE.Usuario Usuario = new Propiedades_BE.Usuario();

        private static Login _instancia;

       private ContentPlaceHolder contentPlace;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        }

        public void VerificarIntegridadGeneral()
        {
            string ProblemaUsuario = GestorUsuario.VerificarIntegridadUsuario(Propiedades_BE.SingletonLogin.GlobalIdUsuario);

            string ProblemaDefinitivo = ProblemaUsuario;

            if (ProblemaDefinitivo == "")
            {
                //sistema correcto
                Propiedades_BE.SingletonLogin.SumarIntegridadGeneral(0);
            }
            else
            {
                //error administrador
                Propiedades_BE.SingletonLogin.SumarIntegridadGeneral(1);
            }

            if(Propiedades_BE.SingletonLogin.GlobalIntegridad == 0)
            {
                //Login();
            }
            else
            {
                if (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Recalcular_Digitos))
                {
                    //mostrar en pantalla el problema definitivo
                }
                else
                {
                    //Falla de integridad: No tiene los permisos necesarios
                }
            }
        }

        public void logguear(object sender, EventArgs e)
        {
            if (Propiedades_BE.SingletonLogin.GlobalIntegridad == 0)
            {

                if (GestorUsuario.VerificarUsuarioContraseña(txtNick.Text, txtContraseña.Text, Propiedades_BE.SingletonLogin.GlobalIntegridad) == 1)
                {
                    if (GestorUsuario.VerificarEstado(txtNick.Text) == false)
                    {
                        GestorUsuario.ReiniciarIntentos(txtNick.Text);

                        try
                        {
                            GestorUsuario.LogIn(Usuario);
                            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Login", "Baja", 0);
                                                        
                            Response.Redirect("Home.aspx");
                            //this.Hide();
                            //menu.Show();

                        }
                        catch (Exception EX)
                        {
                            //MessageBox.Show(EX.Message);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("El usuario se encuentra bloqueado");
                    }
                }
                else if (GestorUsuario.VerificarEstado(txtNick.Text) == true)
                {
                    //MessageBox.Show("No se puede acceder, usuario bloqueado");
                }
                else if (GestorUsuario.VerificarContador(txtNick.Text) < 3)
                {
                    //MessageBox.Show("Usuarios y/o contraseña incorrectos");
                    Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Falla de LogIn", "Alta", 0);
                }
                else if (GestorUsuario.VerificarContador(txtNick.Text) >= 3)
                {
                    //MessageBox.Show("El usuario se encuentra bloqueado");
                    Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Bloqueo de usuario", "Alta", 0);
                    GestorUsuario.BloquearUsuario(txtNick.Text);
                }
            }
            else
            {
                if (GestorUsuario.VerificarUsuarioContraseña(txtNick.Text, txtContraseña.Text, Propiedades_BE.SingletonLogin.GlobalIntegridad) == 1)
                {
                    if (GestorUsuario.VerificarEstado(txtNick.Text) == false)
                    {
                        if (GestorUsuario.VerificarContador(txtNick.Text) < 3)
                        {
                            //MessageBox.Show("Ingreso correctamente. Error de integridad en la base de datos");

                            GestorUsuario.LogIn(Usuario);

                            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "LogIn. Falla de integridad", "Alta", 0);

                            Menu M = new Menu();
                            //this.Hide();
                            //M.Show();
                        }
                    }
                }
            }
        }

        public void MetodoPrueba(object sender, EventArgs e)
        {
            
        }
    }
}