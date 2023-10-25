using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class HistorialVenta : System.Web.UI.Page,IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();        
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

                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Venta)))
                {
                    divHistorialVentas.Visible = true;
                    lblPermiso.Visible = false;
                    ListarProductos();
                }
                else
                {
                    divHistorialVentas.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }
            }
        }

        #region metodos
        void ListarProductos()
        {
            try
            {
                gridVentas.DataSource = null;
                gridVentas.DataSource = GestorVenta.Listar();
                gridVentas.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }
        #endregion

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
        }
        #endregion

        protected void btnWebServiceCliente_Click(object sender, EventArgs e)
        {
            gridClientes.Visible = true;

            WebService webService = new WebService();
            gridClientes.DataSource = null;
            gridClientes.DataSource = webService.ClientesMasVendido();
            gridClientes.DataBind();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            //recorre el header de la tabla
            foreach (TableCell column in gridVentas.HeaderRow.Cells)
            {
                dt.Columns.Add(column.Text);
            }

            //recorre las filas de la tabla
            foreach (GridViewRow row in gridVentas.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dt.Rows[row.RowIndex][i] = row.Cells[i].Text;
                }
            }

            //guardamos la tabla
            ds.Tables.Add(dt);
            // Specify the file path for the XML file
            string filePath = Server.MapPath("~/GridHistorialVentas.xml");

            // Write the DataSet to an XML file
            ds.WriteXml(filePath);

            // Provide a download link for the generated XML file
            Response.Clear();
            Response.ContentType = "application/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=HistorialVentas.xml");
            Response.TransmitFile(filePath);
            Response.End();
        }
    }
}