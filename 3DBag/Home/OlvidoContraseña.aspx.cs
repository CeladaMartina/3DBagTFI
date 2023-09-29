using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class OlvidoContraseña : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();

        public int Contador = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        }

        #region metodos
        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtNick.Text) || string.IsNullOrEmpty(txtMail.Text))
            {
                A = true;
            }
            return A;
        }

        void Verificar()
        {
            if (Contador >= 3)
            {
                GestorUsuario.BloquearUsuario(txtNick.Text);
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Su usuario ha sido bloqueado. Contacte al administrador del sistema") ?? ("Su usuario ha sido bloqueado. Contacte al administrador del sistema");
            }
            else
            {
                if (GestorUsuario.VerificarNickMail(Seguridad.EncriptarAES(txtNick.Text), txtMail.Text) == true)
                {
                    GestorUsuario.ReiniciarIntentos(txtNick.Text);
                    string contraseñaNueva = Seguridad.GenerarClave();
                    ConfirmarCambio(contraseñaNueva);                             
                }
                else
                {
                    Contador += 1;
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Datos mal cargados") ?? ("Datos mal cargados");                   
                }
            }
        }

        void ConfirmarCambio(string contraseñaNueva)
        {
            try
            {
                if (Seguridad.ValidarClave(contraseñaNueva) == true)
                {
                    GestorUsuario.ConfirmarCambioContraseña(txtNick.Text, contraseñaNueva, txtMail.Text);
                    Seguridad.CargarBitacora(GestorUsuario.SeleccionarIDNick(txtNick.Text), DateTime.Now, "Contraseña cambiada", "Alta", 0);
                    EnviarMail(contraseñaNueva, txtMail.Text);
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Contraseña cambiada con exito") ?? ("Contraseña cambiada con exito");                    
                }
                else
                {
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Ingrese otra contraseña") ?? ("Ingrese otra contraseña");                    
                }
            }
            catch (Exception)
            {
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Error cambiando contraseña, intente nuevamente") ?? ("Error cambiando contraseña, intente nuevamente");               
            }

        }

        void EnviarMail(string contraseñaNueva, string Mail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("martina.celada@gmail.com", "3DBag 2023");
            mail.Subject = "Cambio de Contraseña 3DBag";
            mail.Body = "<h2>Aqui esta tu nueva contraseña</h2><br/><p>No la compartas con nadie: "+ contraseñaNueva + "</p>";
            mail.IsBodyHtml = true;

            mail.To.Add(new MailAddress(Mail));

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new System.Net.NetworkCredential("martina.celada@gmail.com", "ljrshyhsmelygaed");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }

        #endregion
        protected void btnActualizarContra_Click(object sender, EventArgs e)
        {
            if (ChequearFallaTxt() == false)
            {
                try
                {
                    Verificar();

                }
                catch (Exception ex)
                {
                    lblRespuesta.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                }
            }
            else
            {
                lblRespuesta.Text = SiteMaster.TraducirGlobal("Complete todos los campos") ?? ("Complete todos los campos");               
            }
        }
    }
}