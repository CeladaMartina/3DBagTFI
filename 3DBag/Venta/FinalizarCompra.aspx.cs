using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class FinalizarCompra : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();

        int IdVenta = -1;
        private System.Threading.Timer timer;

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
            //string pdfPath = AppDomain.CurrentDomain.BaseDirectory + "Facturas\\" + Session["NombreFactura"];
            string pdfPath = "https://localhost:44388/" + "\\Facturas\\" + Session["NombreFactura"]; 
            viewPDF.Attributes["src"] = pdfPath;
            
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
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\Facturas\\";
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

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblCliente.Text = Sujeto.TraducirObserver(lblCliente.SkinID.ToString()) ?? lblCliente.SkinID.ToString();
            lblIdVenta.Text = Sujeto.TraducirObserver(lblIdVenta.SkinID.ToString()) ?? lblIdVenta.SkinID.ToString();
            lblTipoFactura.Text = Sujeto.TraducirObserver(lblTipoFactura.SkinID.ToString()) ?? lblTipoFactura.SkinID.ToString();
            btnFinalizarCompra.Text = Sujeto.TraducirObserver(btnFinalizarCompra.SkinID.ToString()) ?? btnFinalizarCompra.SkinID.ToString();
        }

        public void Traducir()
        {
            lblCliente.Text = SiteMaster.TraducirGlobal(lblCliente.SkinID.ToString()) ?? lblCliente.SkinID.ToString();
            lblIdVenta.Text = SiteMaster.TraducirGlobal(lblIdVenta.SkinID.ToString()) ?? lblIdVenta.SkinID.ToString();
            lblTipoFactura.Text = SiteMaster.TraducirGlobal(lblTipoFactura.SkinID.ToString()) ?? lblTipoFactura.SkinID.ToString();
            btnFinalizarCompra.Text = SiteMaster.TraducirGlobal(btnFinalizarCompra.SkinID.ToString()) ?? btnFinalizarCompra.SkinID.ToString();
        }
        #endregion
    }
}