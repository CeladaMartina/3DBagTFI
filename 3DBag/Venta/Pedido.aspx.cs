using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Pedido : System.Web.UI.Page
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

            //GestorVenta.Vender(Convert.ToInt32(Session["IdVenta"]));
            //string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/Factura");
            string ruta = "../Factura/";
            string lblSubtotal = GestorDV.SubTotal(int.Parse(Session["IdVenta"].ToString())).ToString();
            PDF(ruta, Convert.ToInt32(Session["IdVenta"]), GestorDV.SeleccionarNick(Propiedades_BE.SingletonLogin.GlobalIdUsuario), DateTime.Now.ToShortDateString(), decimal.Parse(lblSubtotal));
            
            Response.Redirect("../Venta/FinalizarVenta.aspx?IdVenta=" + Session["IdVenta"] + "&Factura=" + Session["NombreFactura"]);
        }

        void PDF(string ruta, int NumVenta, string Cliente, string Fecha, decimal Total)
        {

            // Create a new PDF document
            var pdfPath = Server.MapPath("~/Factura/Nombre");
            var pdfDocument = new PdfDocument(new PdfWriter(pdfPath));
            var pdf = new Document(pdfDocument);

            // Create a table with the same number of columns as the GridView
            var table = new Table(gridDetalleVenta.HeaderRow.Cells.Count);

            // Add header row cells to the PDF table
            foreach (TableCell headerCell in gridDetalleVenta.HeaderRow.Cells)
            {
                table.AddCell(new Paragraph(headerCell.Text));
            }

            // Add data rows to the PDF table
            foreach (GridViewRow row in gridDetalleVenta.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    table.AddCell(new Paragraph(cell.Text));
                }
            }

            // Add the table to the PDF document
            pdf.Add(table);

            // Close the PDF document
            pdf.Close();

            // Provide the generated PDF file for download or display
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", $"attachment; filename=GridViewToPdf.pdf");
            Response.TransmitFile(pdfPath);
            Response.End();
        }
    }
}