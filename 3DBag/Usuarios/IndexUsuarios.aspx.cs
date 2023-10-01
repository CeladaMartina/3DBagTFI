using System;
using System.Collections.Generic;
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

                ListarUsuarios();
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
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            btnAltaUsuario.Text = SiteMaster.TraducirGlobal(btnAltaUsuario.SkinID.ToString()) ?? btnAltaUsuario.SkinID.ToString();
        }
        #endregion
    }
}