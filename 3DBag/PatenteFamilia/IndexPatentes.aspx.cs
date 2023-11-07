using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class IndexPatentes : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Familia FamTemp;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();
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

                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Patentes)))
                {
                    divPatentes.Visible = true;
                    lblPermiso.Visible = false;
                    ListarPatentes();
                }
                else
                {
                    divPatentes.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }

                
            }
        }

        void ListarPatentes()
        {
            try
            {
                gridPatentes.DataSource = null;
                gridPatentes.DataSource = GestorPermisos.GetAllPatentes();
                gridPatentes.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAltaPatente.Text = Sujeto.TraducirObserver(btnAltaPatente.SkinID.ToString()) ?? btnAltaPatente.SkinID.ToString();
            linkVolver.Text = Sujeto.TraducirObserver(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAltaPatente.Text = SiteMaster.TraducirGlobal(btnAltaPatente.SkinID.ToString()) ?? btnAltaPatente.SkinID.ToString();
            linkVolver.Text = SiteMaster.TraducirGlobal(linkVolver.SkinID.ToString()) ?? linkVolver.SkinID.ToString();
            TraducirGridview();
        }
        #endregion

        protected void btnAltaPatente_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEditPatente.aspx?Funcion=alta");
        }

        protected void gridPatentes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int crow;
            crow = Convert.ToInt32(e.CommandArgument.ToString());
            string id = gridPatentes.Rows[crow].Cells[0].Text;
            string nombre = gridPatentes.Rows[crow].Cells[1].Text;

            if (e.CommandName == "editar")
            {
                //enviamos el nick del usuario
                Response.Redirect("CreateEditPatente.aspx?patente=" + id + "&nombre="+ nombre + "&Funcion=editar");

            }
            else if (e.CommandName == "select")
            {
                Response.Redirect("VerPatente.aspx?patente=" + id + "&nombre=" + nombre);
            }            
        }

        protected void linkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Navegacion/Administracion.aspx");
        }

        void TraducirGridview()
        {
            foreach (DataControlField column in gridPatentes.Columns)
            {
                //buttons
                if (column is ButtonField buttonField)
                {
                    string headerText = buttonField.Text;

                    if (Session["IdiomaSelect"].ToString() == "Ingles")
                    {
                        switch (headerText)
                        {
                            case "Editar":
                                buttonField.Text = "Edit";
                                break;
                            case "Ver":
                                buttonField.Text = "Show";
                                break;                           
                        }
                    }
                    else if (Session["IdiomaSelect"].ToString() == "Español")
                    {
                        switch (headerText)
                        {
                            case "Editar":
                                buttonField.Text = "Editar";
                                break;
                            case "Ver":
                                buttonField.Text = "Ver";
                                break;                            
                        }
                    }
                }

                //columna header
                if (column is BoundField boundField)
                {
                    string dataField = boundField.HeaderText;

                    if (Session["IdiomaSelect"].ToString() == "Ingles")
                    {
                        switch (dataField)
                        {
                            case "Nombre":
                                boundField.HeaderText = "Name";
                                break;                            
                        }
                    }
                    else
                    {
                        switch (dataField)
                        {
                            case "Nombre":
                                boundField.HeaderText = "Nombre";
                                break;                            
                        }
                    }
                }
            }
        }
    }
}