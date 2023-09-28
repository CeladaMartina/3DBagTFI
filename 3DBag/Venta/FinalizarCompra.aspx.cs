using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class FinalizarCompra : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();

        int IdVenta = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                IdVenta = Convert.ToInt32(Session["IdVenta"]);
                TraerDetalleVenta();
            }
        }

        #region metodos


        void TraerDetalleVenta()
        {
            lblClienteResp.Text = GestorDV.SeleccionarNick(Propiedades_BE.SingletonLogin.GlobalIdUsuario);
            lblIdVentaResp.Text = Session["IdVenta"].ToString();
            TraerPDF();
        }

        //traer el nombre del pdf creado x medio de la url
        //llenar el iframe con ese pdf
        void TraerPDF()
        {
            viewPDF.Src = "https://localhost:44388/Facturas/" + Session["NombreFactura"];
        }

        void EnviarMail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("martina.celada@gmail.com", "3DBag 2023");
            mail.Subject = "Factura de Compra en 3DBag";
            mail.Body = "<h1>¡Muchas gracias por comprar en 3DBag!</h1>";
            mail.IsBodyHtml = true;

            //a quien le mandamos el mail, el mail del usuario logueado
            mail.To.Add(new MailAddress(GestorUsuario.TraerMail(Propiedades_BE.SingletonLogin.GlobalIdUsuario)));

            //adjuntamos el pdf
            string ruta = "C:\\Users\\mcelada\\Desktop\\SAP - TFI - Auditoria 2023\\TFI\\3DBag\\3DBag\\3DBag\\Facturas\\";
            mail.Attachments.Add(new Attachment(ruta + Session["NombreFactura"]));

            using(SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new System.Net.NetworkCredential("martina.celada@gmail.com", "ljrshyhsmelygaed");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }


        }
        #endregion

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            GestorVenta.Vender(Convert.ToInt32(Session["IdVenta"]));
            EnviarMail();
            lblResultado.Visible = true;
            lblResultado.Text = SiteMaster.TraducirGlobal("Compra finalizada correctamente. Factura enviada por email.") ?? ("Compra finalizada correctamente. Factura enviada por email.");
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Venta Realizada", "Baja", 0);
            Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));
        }
    }
}