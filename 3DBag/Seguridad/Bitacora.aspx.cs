﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Bitacora : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                //traduccion de la pagina
                Session["UserSession"] = null;
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Ver_Bitacora)))
                {
                    divGeneral.Visible = true;
                    lblPermiso.Visible = false;

                    CargarComboUsuario();
                    ListarBitacora();
                }
                else
                {
                    divGeneral.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }
            }
        }

        #region metodos
        void ListarBitacora()
        {
            try
            {
                GridBitacora.DataSource = null;
                GridBitacora.DataSource = Seguridad.Listar();                
                GridBitacora.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }

        void CargarComboUsuario()
        {
            ListUsuarios.Items.Add("Todos");
            List<string> NickUsuarios = GestorUsuario.NickUsuario();
            foreach (var NickUs in NickUsuarios)
            {
                ListUsuarios.Items.Add(NickUs.ToString());
            }
        }

        bool ChequearFallaTxt()
        {
            bool A = false;
            if (string.IsNullOrEmpty(txtDesde.Text) || string.IsNullOrEmpty(txtHasta.Text))
            {
                A = true;
            }
            return A;
        }

        void Filtrar()
        {
            try
            {
                GridBitacora.DataSource = null;

                string strDateDesde = txtDesde.Text;
                string strDateHasta = txtHasta.Text;

                DateTime dtDesde = DateTime.ParseExact(strDateDesde, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtDesde.ToString("yyyy-MM-dd");

                DateTime dtHasta = DateTime.ParseExact(strDateHasta, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtHasta.ToString("yyyy-MM-dd");
                dtHasta.AddHours(23).AddMinutes(59).AddSeconds(59);

                string criticidad = ListCriticidiad.SelectedValue;
                string usuario = ListUsuarios.SelectedValue;
                string consultaCriticidad = "";
                string consultaUsuario = "";

                switch (usuario)
                {                    
                    case "Todos":
                        consultaUsuario = "select IdUsuario from Usuario";
                        break;
                    default:
                        consultaUsuario = "select IdUsuario from Usuario where Nick = '" + Seguridad.EncriptarAES(usuario) + "'";
                        break;
                }

                switch (criticidad)
                {
                    case "Todos":
                        consultaCriticidad = "select distinct criticidad from Bitacora";
                        break;
                    default:
                        consultaCriticidad = "select criticidad from Bitacora where criticidad = '" + criticidad + "'";
                        break;
                }

                //fixear la consulta a la base de datos
                GridBitacora.DataSource = Seguridad.ConsultarBitacora(dtDesde, dtHasta, consultaCriticidad, consultaUsuario);
                GridBitacora.DataBind();

                if (GridBitacora.DataSource == null)
                {                    
                    lblError.Visible = true;
                    lblError.Text = SiteMaster.TraducirGlobal("No hay valores para mostrar en la grilla.") ?? ("No hay valores para mostrar en la grilla.");
                }               
                else if (GridBitacora.Rows.Count == 0)
                {
                    GridBitacora.DataSource = null;
                    lblError.Visible = true;
                    lblError.Text = SiteMaster.TraducirGlobal("No hay valores para mostrar en la grilla.") ?? ("No hay valores para mostrar en la grilla.");
                }
                else
                {
                    GridBitacora.DataBind();
                    lblError.Visible = false;
                }

            }
            catch(Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                ListarBitacora();
            }
        }
        #endregion
        #region botones paginacion

        //por medio de este metodo controlamos cuantas paginas hay para presentar
        protected void GridBitacora_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerow = GridBitacora.BottomPagerRow;
            Label pagenro = (Label)pagerow.Cells[0].FindControl("Label1");
            Label totalPageNro = (Label)pagerow.Cells[0].FindControl("Label2");

            if((pagenro != null) && (totalPageNro != null))
            {
                int pagen = GridBitacora.PageIndex + 1;
                int tot = GridBitacora.PageCount;

                pagenro.Text = pagen.ToString();
                totalPageNro.Text = tot.ToString();
            }
        }

        protected void GridBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //esto hace que funcione los botones de "Anterior" y "Siguiente"
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            ListarBitacora(); 
        }
        #endregion
        #region botones calendario
        protected void imageButtonDesde_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
            Calendar1.Attributes.Add("style", "position:absolute");
        }

        protected void imageButtonHasta_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }
            Calendar2.Attributes.Add("style", "position:absolute");
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDesde.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
            Calendar1.Visible = false;
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            txtHasta.Text = Calendar2.SelectedDate.ToString("dd/MM/yyyy");
            Calendar2.Visible = false;
        }

        #endregion

        
        protected void bntFiltrar_Click(object sender, EventArgs e)
        {
            string strDateDesde = txtDesde.Text;
            string strDateHasta = txtHasta.Text;

            DateTime dtDesde = DateTime.ParseExact(strDateDesde, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            dtDesde.ToString("yyyy-MM-dd");

            DateTime dtHasta = DateTime.ParseExact(strDateHasta, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            dtHasta.ToString("yyyy-MM-dd");
            dtHasta.AddHours(23).AddMinutes(59).AddSeconds(59);

            try
            {
                if (ChequearFallaTxt() == false)
                {
                    if (dtDesde >= dtHasta)
                    {
                        lblError.Visible = true;
                        lblError.Text = SiteMaster.TraducirGlobal("La fecha Hasta no puede ser menor que Desde.") ?? ("La fecha Hasta no puede ser menor que Desde.");
                    }
                    else
                    {
                        Filtrar();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
                ListarBitacora();
            }
        }

        //exporta lo que vemos en la bitacora en xml.
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            //recorre el header de la tabla
            foreach(TableCell column in GridBitacora.HeaderRow.Cells)
            {
                dt.Columns.Add(column.Text);
            }
            
            //recorre las filas de la tabla
            foreach(GridViewRow row in GridBitacora.Rows)
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
            string filePath = Server.MapPath("~/GridBitacora.xml");

            // Write the DataSet to an XML file
            ds.WriteXml(filePath);

            // Provide a download link for the generated XML file
            Response.Clear();
            Response.ContentType = "application/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=GridBitacora.xml");
            Response.TransmitFile(filePath);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblDesde.Text = Sujeto.TraducirObserver(lblDesde.SkinID.ToString()) ?? lblDesde.SkinID.ToString();
            lblHasta.Text = Sujeto.TraducirObserver(lblHasta.SkinID.ToString()) ?? lblHasta.SkinID.ToString();
            lblCriticidad.Text = Sujeto.TraducirObserver(lblCriticidad.SkinID.ToString()) ?? lblCriticidad.SkinID.ToString();
            lblUsuarios.Text = Sujeto.TraducirObserver(lblUsuarios.SkinID.ToString()) ?? lblUsuarios.SkinID.ToString();
            bntFiltrar.Text = Sujeto.TraducirObserver(bntFiltrar.SkinID.ToString()) ?? bntFiltrar.SkinID.ToString();
            btnExportar.Text = Sujeto.TraducirObserver(btnExportar.SkinID.ToString()) ?? btnExportar.SkinID.ToString();            
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblDesde.Text = SiteMaster.TraducirGlobal(lblDesde.SkinID.ToString()) ?? lblDesde.SkinID.ToString();
            lblHasta.Text = SiteMaster.TraducirGlobal(lblHasta.SkinID.ToString()) ?? lblHasta.SkinID.ToString();
            lblCriticidad.Text = SiteMaster.TraducirGlobal(lblCriticidad.SkinID.ToString()) ?? lblCriticidad.SkinID.ToString();
            lblUsuarios.Text = SiteMaster.TraducirGlobal(lblUsuarios.SkinID.ToString()) ?? lblUsuarios.SkinID.ToString();
            bntFiltrar.Text = SiteMaster.TraducirGlobal(bntFiltrar.SkinID.ToString()) ?? bntFiltrar.SkinID.ToString();
            btnExportar.Text = SiteMaster.TraducirGlobal(btnExportar.SkinID.ToString()) ?? btnExportar.SkinID.ToString();
        }
        #endregion
    }
}