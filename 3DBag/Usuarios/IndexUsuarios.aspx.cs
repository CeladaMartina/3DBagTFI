using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexUsuarios : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
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

                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario)))
                {
                    divUsuarios.Visible = true;
                    lblPermiso.Visible = false;
                    ListarUsuarios();
                }
                else
                {
                    divUsuarios.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }

               
            }
           
        }
        void ListarUsuarios()
        {
            try
            {
                gridUsuarios.DataSource = null;
                gridUsuarios.DataSource = GestorUsuario.Listar();
                gridUsuarios.DataBind();

            }
            catch(Exception ex)
            {
                Response.Write(ex);
            }
           
        }

        protected void gridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int crow;
            crow = Convert.ToInt32(e.CommandArgument.ToString());
            string v = gridUsuarios.Rows[crow].Cells[0].Text;

            if (e.CommandName == "editar")
            {
                //enviamos el nick del usuario
                Response.Redirect("CreateEditUsuario.aspx?usuario="+ v + "&Funcion=editar");
                
            }
            else if (e.CommandName == "select")            {

                Response.Redirect("DeleteShowUsuario.aspx?usuario=" + v + "&Funcion=Ver");
            }
            else if (e.CommandName == "borrar")
            {
                Response.Redirect("DeleteShowUsuario.aspx?usuario=" + v + "&Funcion=borrar");
            }
        }

        protected void btnAltaUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEditUsuario.aspx?Funcion=alta");
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAltaUsuario.Text = Sujeto.TraducirObserver(btnAltaUsuario.SkinID.ToString()) ?? btnAltaUsuario.SkinID.ToString();
            btnExportar.Text = Sujeto.TraducirObserver(btnExportar.SkinID.ToString()) ?? btnExportar.SkinID.ToString();
            linkVolver.Text = Sujeto.TraducirObserver(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAltaUsuario.Text = SiteMaster.TraducirGlobal(btnAltaUsuario.SkinID.ToString()) ?? btnAltaUsuario.SkinID.ToString();
            btnExportar.Text = SiteMaster.TraducirGlobal(btnExportar.SkinID.ToString()) ?? btnExportar.SkinID.ToString();
            linkVolver.Text = SiteMaster.TraducirGlobal(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }
        #endregion

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            //evitamos las ultimas 3 columnas porque son botones

            //recorro el header de las columnas
            int totalColumns = gridUsuarios.HeaderRow.Cells.Count;
            int columnsToSkip = 3;

            for (int i = 0; i < totalColumns - columnsToSkip; i++)
            {                
                TableCell cell = gridUsuarios.HeaderRow.Cells[i];
                dt.Columns.Add(cell.Text);             

            }

            //recorro las filas, excepto las ultimas 3
            int totalRows = gridUsuarios.Rows[0].Cells.Count;
            int rowsToSkip = 3;

            foreach (GridViewRow row in gridUsuarios.Rows)
            {
                for (int i = 0; i < totalRows - rowsToSkip; i++)
                {
                    dt.Rows.Add(row.Cells[i].Text);
                }
            }

            //guardamos la tabla
            ds.Tables.Add(dt);
            // Specify the file path for the XML file
            string filePath = Server.MapPath("~/GridUsuarios.xml");

            // Write the DataSet to an XML file
            ds.WriteXml(filePath);

            // Provide a download link for the generated XML file
            Response.Clear();
            Response.ContentType = "application/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Usuarios.xml");
            Response.TransmitFile(filePath);
            Response.End();
        }

        protected void linkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Administracion.aspx");
        }
    }
}