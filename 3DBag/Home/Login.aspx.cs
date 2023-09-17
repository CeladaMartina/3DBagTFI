using System;
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
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Propiedades_BE.Usuario Usuario = new Propiedades_BE.Usuario();

        private static Login _instancia;

        SiteMaster masterData;
        private ISubject SubjectData;

        private ContentPlaceHolder contentPlace;

        //singletonLogin
        //public static Login ObtenerInstancia()
        //{
        //    if (_instancia == null)
        //    {
        //        _instancia = new Login();
        //    }

        //    return _instancia;
        //}

        //public Login(ISubject subject)
        //{
        //    this.SubjectData = subject;
        //    this.SubjectData.Attach(this);
        //}

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
                    //Update()
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
            //falta agregar 2 problemas mas

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
                    //guardamos en sesion el problema de la base
                    Session["ProblemaDefinitivo"] = ProblemaDefinitivo;
                    Response.Redirect("../Home/Home.aspx");                    ;
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
                if(ChequearFallaTxt() == false)
                {
                    if (GestorUsuario.VerificarUsuarioContraseña(txtNick.Text, txtContraseña.Text, Propiedades_BE.SingletonLogin.GlobalIntegridad) == 1)
                    {
                        if (GestorUsuario.VerificarEstado(txtNick.Text) == false)
                        {
                            //ingreso correctamente
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
                    //MessageBox.Show(Cambiar_Idioma.TraducirGlobal("Complete todos los campos") ?? "Complete todos los campos");
                    lblError.Visible = true;
                    lblError.Text = "Complete todos los campos.";
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

                                lblError.Visible = true;
                                lblError.Text = "Ingreso correctamente. Error de integridad en la base de datos.";
                                lblError.CssClass = "alert alert-success";

                                GestorUsuario.LogIn(Usuario);

                                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "LogIn. Falla de integridad", "Alta", 0);

                                Response.Redirect("../Home/Home.aspx");
                            }
                        }
                    }
                }
                else
                {
                    //MessageBox.Show(Cambiar_Idioma.TraducirGlobal("Complete todos los campos") ?? "Complete todos los campos");
                    lblError.Visible = true;
                    lblError.Text = "Complete todos los campos.";
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

        }

        protected void linkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx");
        }
    }
}