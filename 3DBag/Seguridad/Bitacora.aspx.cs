using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class Bitacora : System.Web.UI.Page
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                CargarComboUsuario();
                ListarBitacora();

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
                    lblError.Text = "No hay valores para mostrar en la grilla.";
                }               
                else if (GridBitacora.Rows.Count == 0)
                {
                    GridBitacora.DataSource = null;
                    lblError.Visible = true;
                    lblError.Text = "No hay valores para mostrar en la grilla.";
                }
                else
                {
                    GridBitacora.DataBind();
                    lblError.Visible = false;
                }

            }
            catch(Exception ex)
            {
                //lblError.Visible = true;
                //lblError.Text = ex.ToString();
                //ListarBitacora();
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
                        lblError.Text = "La fecha Hasta no puede ser menor que Desde.";
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
                lblError.Text = "Error filtrando";
                ListarBitacora();
            }
        }
    }
}