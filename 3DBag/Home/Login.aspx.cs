﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Text;
using System.Web.Security;

namespace _3DBag
{
    public partial class Login : System.Web.UI.Page, IObserver
    {
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();        
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad(); //bitacora
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();
        Negocio_BLL.Producto GestorProducto = new Negocio_BLL.Producto();
        Negocio_BLL.Idioma GestorIdioma = new Negocio_BLL.Idioma();
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();

        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Usuario Usuario = new Propiedades_BE.Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                Session["UserSession"] = null;               
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();                   
                }
            }

            contentPlace = (ContentPlaceHolder)Master.FindControl("LoginUser");
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
                Console.WriteLine(ex.ToString());
            }
        }       

        #endregion

        #region metodos
        public void VerificarIntegridadGeneral()
        {
            string ProblemaUsuario = GestorUsuario.VerificarIntegridadUsuario(Propiedades_BE.SingletonLogin.GlobalIdUsuario);            
            string ProblemaPermiso = GestorPermisos.VerificarIntegridadPermiso(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            string ProblemaProducto = GestorProducto.VerificarIntegridadProducto(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            string ProblemaIdioma = GestorIdioma.VerificarIntegridadIdioma(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            string ProblemaVenta = GestorVenta.VerificarIntegridadVenta(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            string ProblemaDetalleVenta = GestorDV.VerificarIntegridadDV(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            string ProblemaBitacora = Seguridad.VerificarIntegridadBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario);

            string ProblemaDefinitivo = ProblemaUsuario + ProblemaPermiso + ProblemaProducto + ProblemaIdioma + ProblemaVenta + ProblemaDetalleVenta + ProblemaBitacora;
           
            if (ProblemaDefinitivo == "")
            {
                lblError.Visible = true;              
                Propiedades_BE.SingletonLogin.SumarIntegridadGeneral(0);
            }
            else
            {
                lblError.Visible = true;
                //lblError.Text = "Error -> Contacte al administrador.";
                //lblError.CssClass = "alert alert-warning";
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
                    //guardamos en sesion el problema de la base
                    Session["ProblemaDefinitivo"] = ProblemaDefinitivo;
                    Response.Redirect("../Home/Home.aspx");                    ;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = SiteMaster.TraducirGlobal("Falla de integridad: No tiene los permisos necesarios.") ?? ("Falla de integridad: No tiene los permisos necesarios.");                    
                }
            }
        }

        void LogIn()
        {
            if (Propiedades_BE.SingletonLogin.GlobalIntegridad == 0)
            {
                if(ChequearFallaTxt() == false)
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

                                //guarda la sesion del usuario, para comprobarlo en las otras paginas                            
                                HttpContext.Current.Session["UserSession"] = Usuario;
                                //enviamos el ID para presentarlo en la home como mensaje de bienvenida
                                Response.Redirect("../Home/Home.aspx?usuario=" + Usuario.IdUsuario, false);
                            }
                            catch (Exception EX)
                            {
                                lblError.Visible = true;
                                lblError.CssClass = "alert alert-danger";
                                lblError.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.CssClass = "alert alert-danger";
                            lblError.Text = SiteMaster.TraducirGlobal("El usuario se encuentra bloqueado") ?? ("El usuario se encuentra bloqueado");
                        }
                    }
                    else if (GestorUsuario.VerificarEstado(txtNick.Text) == true)
                    {
                        lblError.Visible = true;
                        lblError.CssClass = "alert alert-danger";
                        lblError.Text = SiteMaster.TraducirGlobal("No se puede acceder, usuario bloqueado") ?? ("No se puede acceder, usuario bloqueado");
                    }
                    else if (GestorUsuario.VerificarContador(txtNick.Text) < 3)
                    {
                        lblError.Visible = true;
                        lblError.CssClass = "alert alert-danger";
                        lblError.Text = SiteMaster.TraducirGlobal("Usuarios y/o contraseña incorrecto") ?? ("Usuarios y/o contraseña incorrecto");
                        Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Falla de LogIn", "Alta", 0);
                    }
                    else if (GestorUsuario.VerificarContador(txtNick.Text) >= 3)
                    {
                        lblError.Visible = true;
                        lblError.CssClass = "alert alert-danger";
                        lblError.Text = SiteMaster.TraducirGlobal("El usuario se encuentra bloqueado") ?? ("El usuario se encuentra bloqueado");
                        Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Bloqueo de usuario", "Alta", 0);
                        GestorUsuario.BloquearUsuario(txtNick.Text);
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.CssClass = "alert alert-danger";
                    lblError.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }                
            }
            else
            {
                if(ChequearFallaTxt() == false)
                {
                    if (GestorUsuario.VerificarUsuarioContraseña(txtNick.Text, txtContraseña.Text, Propiedades_BE.SingletonLogin.GlobalIntegridad) == 1)
                    {
                        if (GestorUsuario.VerificarEstado(txtNick.Text) == false)
                        {
                            if (GestorUsuario.VerificarContador(txtNick.Text) < 3)
                            {

                                //lblError.Visible = true;
                                //lblError.Text = "Ingreso correctamente. Error de integridad en la base de datos.";
                                //lblError.CssClass = "alert alert-success";

                                GestorUsuario.LogIn(Usuario);

                                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "LogIn. Falla de integridad", "Alta", 0);

                                Response.Redirect("../Home/Home.aspx");
                            }
                        }
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");
                }
            }
        }

        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtNick.Text) || string.IsNullOrEmpty(txtContraseña.Text))
            {
                A = true;
            }
            return A;
        }

        #endregion

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            MemberId.Text = Sujeto.TraducirObserver(MemberId.SkinID.ToString()) ?? MemberId.SkinID.ToString();
            nickId.Text = Sujeto.TraducirObserver(nickId.SkinID.ToString()) ?? nickId.SkinID.ToString();
            contraseñaId.Text = Sujeto.TraducirObserver(contraseñaId.SkinID.ToString()) ?? contraseñaId.SkinID.ToString();
            btnLogin.Text = Sujeto.TraducirObserver(btnLogin.SkinID.ToString()) ?? btnLogin.SkinID.ToString();
            Olvidaste.Text = Sujeto.TraducirObserver(Olvidaste.SkinID.ToString()) ?? Olvidaste.SkinID.ToString();
            linkVolver.Text = Sujeto.TraducirObserver(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }

        public void Traducir()
        {
            MemberId.Text = SiteMaster.TraducirGlobal(MemberId.SkinID.ToString()) ?? MemberId.SkinID.ToString();
            nickId.Text = SiteMaster.TraducirGlobal(nickId.SkinID.ToString()) ?? nickId.SkinID.ToString();
            contraseñaId.Text = SiteMaster.TraducirGlobal(contraseñaId.SkinID.ToString()) ?? contraseñaId.SkinID.ToString();
            btnLogin.Text = SiteMaster.TraducirGlobal(btnLogin.SkinID.ToString()) ?? btnLogin.SkinID.ToString();
            Olvidaste.Text = SiteMaster.TraducirGlobal(Olvidaste.SkinID.ToString()) ?? Olvidaste.SkinID.ToString();
            linkVolver.Text = SiteMaster.TraducirGlobal(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }
        #endregion

        protected void Olvidaste_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/OlvidoContraseña.aspx");
        }

        protected void linkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx");
        }
    }
}