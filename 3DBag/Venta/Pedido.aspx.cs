using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Pedido : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        int IdVenta = -1;

        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();
        Negocio_BLL.Detalle_Venta GestorDV = new Negocio_BLL.Detalle_Venta();
        Negocio_BLL.Producto GestorArticulo = new Negocio_BLL.Producto();
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

                if (Session["TipoUsuario"].ToString() == "Cliente")
                {
                    IdVenta = Convert.ToInt32(Session["IdVenta"]);

                    if (IdVenta == 0)
                    {
                        gridDetalleVenta.Visible = false;
                        btnComprarAhora.Visible = false;
                    }
                    else
                    {
                        //lblMensaje.Visible = false;
                        gridDetalleVenta.Visible = true;
                        TraerDetalleVenta(IdVenta);
                    }
                }
                else
                {
                    divPedido.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }              
                           
            }
        }

        #region metodos
        void TraerDetalleVenta(int IdVenta)
        {
            gridDetalleVenta.DataSource = null;
            gridDetalleVenta.DataSource = GestorDV.ListarDV(IdVenta);
            gridDetalleVenta.DataBind();
            
            if(gridDetalleVenta.DataSource == null || gridDetalleVenta.Rows.Count == 0)
            {
                btnComprarAhora.Visible = false;
            }
            else
            {
                btnComprarAhora.Visible = true;
                gridDetalleVenta.DataBind();
            }
            
        }

        protected void VerTienda(object sender, EventArgs e)
        {
            Response.Redirect("../Producto/TiendaProducto.aspx");
        }

        public void BajaDV(int IdDetalle, int IdArticulo)
        {
            GestorDV.BajaDV(IdDetalle, IdArticulo);
            //lista nuevamente la tabla con los valores de la venta
            TraerDetalleVenta(Convert.ToInt32(Session["IdVenta"]));            
        }
        #endregion

        #region botones
        protected void gridDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //hagarramos la fila a editar               
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridDetalleVenta.Rows[index];

            //buscamos los valores de la tabla             
            Label CodProd = (Label)row.FindControl("CodProd");
            Label IdDetalle = (Label)row.FindControl("IdDetalle");            
            
            if(e.CommandName == "borrar")
            {
                BajaDV(Convert.ToInt32(IdDetalle.Text), GestorArticulo.SeleccionarIdArticulo(int.Parse(CodProd.Text)));
            }
        }

        #endregion

        protected void btnComprarAhora_Click(object sender, EventArgs e)
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory  + "\\Facturas\\";             
            //string ruta = Server.MapPath("~/");
            string lblSubtotal = GestorDV.SubTotal(int.Parse(Session["IdVenta"].ToString())).ToString();
            PDF(ruta, Convert.ToInt32(Session["IdVenta"]), GestorDV.SeleccionarNick(Propiedades_BE.SingletonLogin.GlobalIdUsuario), DateTime.Now.ToShortDateString(), decimal.Parse(lblSubtotal));
            
            Response.Redirect("../Venta/FinalizarCompra.aspx?IdVenta=" + Session["IdVenta"] + "&Factura=" + Session["NombreFactura"]);
        }

        void PDF(string ruta, int NumVenta, string Cliente, string Fecha, decimal Total)
        {
            Directory.CreateDirectory(ruta);
            DirectorySecurity sec = Directory.GetAccessControl(ruta);

            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(ruta, sec);

            string fecha_final = Fecha.Replace("/", "-");
            string ruta_final = "" + ruta + "Venta" + NumVenta + "__" + fecha_final + ".pdf";
            string rutaViewPDF= "Venta" + NumVenta + "__" + fecha_final + ".pdf";

            System.IO.FileStream fs = new FileStream(ruta_final, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40f, 40f, 40f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            BaseFont Fuente0 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, true);
            iTextSharp.text.Font Titulo = new iTextSharp.text.Font(Fuente0, 26f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);


            BaseFont Fuente1 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, true);
            iTextSharp.text.Font Titulo2 = new iTextSharp.text.Font(Fuente1, 18f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            BaseFont Fuente2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
            iTextSharp.text.Font TablasTitulo = new iTextSharp.text.Font(Fuente2, 14f, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);

            BaseFont Fuente3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
            iTextSharp.text.Font TablasTexto = new iTextSharp.text.Font(Fuente3, 12f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            Paragraph Venta = new Paragraph("Venta", Titulo);
            Venta.Alignment = Element.ALIGN_CENTER;
            doc.Add(Venta);
            doc.Add(new Chunk("\n"));

            Paragraph NVenta = new Paragraph("Numero de venta: " + NumVenta + "", Titulo2);
            NVenta.Alignment = Element.ALIGN_LEFT;
            doc.Add(NVenta);
            doc.Add(new Chunk("\n"));

            Paragraph ClienteN = new Paragraph("Cliente: " + Cliente + "", Titulo2);
            ClienteN.Alignment = Element.ALIGN_LEFT;
            doc.Add(ClienteN);
            doc.Add(new Chunk("\n"));

            Paragraph FechaP = new Paragraph("Fecha: " + Fecha + "", Titulo2);
            FechaP.Alignment = Element.ALIGN_LEFT;
            doc.Add(FechaP);
            doc.Add(new Chunk("\n"));

            PdfPTable table = new PdfPTable(3);
            //table.AddCell(new PdfPCell(new Phrase("Codigo Producto", TablasTitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Descripcion", TablasTitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Precio", TablasTitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Cantidad", TablasTitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });

            table.SpacingBefore = 10;
            foreach(GridViewRow row in gridDetalleVenta.Rows)
            {
                try
                {
                    string Descripcion = row.Cells[1].Text;
                    decimal PrecioU = decimal.Parse(row.Cells[2].Text);
                    int Cantidad = int.Parse(row.Cells[3].Text);

                    table.AddCell(new PdfPCell(new Phrase(Descripcion.ToString(), TablasTexto)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(PrecioU.ToString(), TablasTexto)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(Cantidad.ToString(), TablasTexto)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                }
                catch(Exception ex)
                {
                    //error
                }
            }
            doc.Add(table);

            Paragraph Linea = new Paragraph("----------------------------", Titulo2);
            Linea.Alignment = Element.ALIGN_RIGHT;
            doc.Add(Linea);

            Paragraph TotalF = new Paragraph("El total es: " + Total + "", Titulo2);
            TotalF.Alignment = Element.ALIGN_RIGHT;
            doc.Add(TotalF);
            doc.Add(new Chunk("\n"));

            Session["NombreFactura"] = rutaViewPDF;
            doc.Close();
            writer.Close();
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblPedido.Text = Sujeto.TraducirObserver(lblPedido.SkinID.ToString()) ?? lblPedido.SkinID.ToString();
            linkRedirect.Text = Sujeto.TraducirObserver(linkRedirect.SkinID.ToString()) ?? linkRedirect.SkinID.ToString();
            btnComprarAhora.Text = Sujeto.TraducirObserver(btnComprarAhora.SkinID.ToString()) ?? btnComprarAhora.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblPedido.Text = SiteMaster.TraducirGlobal(lblPedido.SkinID.ToString()) ?? lblPedido.SkinID.ToString();
            linkRedirect.Text = SiteMaster.TraducirGlobal(linkRedirect.SkinID.ToString()) ?? linkRedirect.SkinID.ToString();
            btnComprarAhora.Text = SiteMaster.TraducirGlobal(btnComprarAhora.SkinID.ToString()) ?? btnComprarAhora.SkinID.ToString();
        }
        #endregion
    }
}