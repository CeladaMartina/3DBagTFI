﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
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

        public static Login ObtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Login();
            }
            //_instancia.BringToFront();
            return _instancia;
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        }

        #region boton

        public void logguear(object sender, EventArgs e)
        {
            try
            {
                Usuario.IdUsuario = GestorUsuario.SeleccionarIDNick(txtNick.Text);
                Propiedades_BE.SingletonLogin.SetIdUsuario(Usuario);
                GestorUsuario.LogIn(Usuario);
                VerificarIntegridadGeneral();
            }
            catch(Exception ex)
            {

            }
        }       

        #endregion

        #region metodos
        public void VerificarIntegridadGeneral()
        {
            string ProblemaUsuario = GestorUsuario.VerificarIntegridadUsuario(Propiedades_BE.SingletonLogin.GlobalIdUsuario);

            string ProblemaDefinitivo = ProblemaUsuario;

            if (ProblemaDefinitivo == "")
            {
                lblError.Visible = true;
                lblError.Text = "Sistema Correcto.";
                lblError.CssClass = "alert alert-success";
                Propiedades_BE.SingletonLogin.SumarIntegridadGeneral(0);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Error -> Contacte al administrador.";
                lblError.CssClass = "alert alert-warning";
                Propiedades_BE.SingletonLogin.SumarIntegridadGeneral(1);
            }

            if(Propiedades_BE.SingletonLogin.GlobalIntegridad == 0)
            {
                LogIn();
            }
            else
            {
                if (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Recalcular_Digitos))
                {
                    //mostrar en pantalla el problema definitivo
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Falla de integridad: No tiene los permisos necesarios.";                    
                }
            }
        }

        void LogIn()
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
                        }
                        catch (Exception EX)
                        {
                            //MessageBox.Show(EX.Message);
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "El usuario se encuentra bloqueado.";
                    }
                }
                else if (GestorUsuario.VerificarEstado(txtNick.Text) == true)
                {
                    lblError.Visible = true;
                    lblError.Text = "No se puede acceder, usuario bloqueado.";
                }
                else if (GestorUsuario.VerificarContador(txtNick.Text) < 3)
                {
                    lblError.Visible = true;
                    lblError.Text = "Usuarios y/o contraseña incorrectos.";
                    Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Falla de LogIn", "Alta", 0);
                }
                else if (GestorUsuario.VerificarContador(txtNick.Text) >= 3)
                {
                    lblError.Visible = true;
                    lblError.Text = "El usuario se encuentra bloqueado.";
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

                            lblError.Visible = true;
                            lblError.Text = "Ingreso correctamente. Error de integridad en la base de datos.";
                            lblError.CssClass = "alert alert-success";

                            GestorUsuario.LogIn(Usuario);

                            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "LogIn. Falla de integridad", "Alta", 0);

                            Response.Redirect("Home.aspx");
                        }
                    }
                }
            }
        }
        #endregion



    }
}