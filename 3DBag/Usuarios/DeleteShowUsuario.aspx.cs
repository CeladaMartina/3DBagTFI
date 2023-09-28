using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class DeleteShowUsuario : System.Web.UI.Page
    {
        string nick;
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();

        Propiedades_BE.Usuario TempUs;
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                if ((Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario)))
                {
                    DivUsuarios.Visible = true;
                    lblPermiso.Visible = false;

                    nick = Request.QueryString["usuario"];

                    //pantalla EDITAR
                    if (Request.QueryString["Funcion"] == "borrar")
                    {
                        lblTitulo.Text = SiteMaster.TraducirGlobal("Eliminar") ?? ("Eliminar");
                        lblPregunta.Visible = true;
                        lblPregunta.Text = SiteMaster.TraducirGlobal("¿Estás seguro de que quieres eliminar esto?") ?? ("¿Estás seguro de que quieres eliminar esto?");
                    }
                    else
                    {
                        //pantalla VER                     
                        lblTitulo.Text = SiteMaster.TraducirGlobal("Detalles") ?? ("Detalles");
                        lblPregunta.Visible = false;
                        btnBorrar.Visible = false;
                    }

                    TraerUsuario(nick);
                    TraerPermisos();

                }
                else
                {
                    DivUsuarios.Visible = false;
                    lblPermiso.Text = SiteMaster.TraducirGlobal("No tiene los permisos necesarios para realizar esta accion") ?? ("No tiene los permisos necesarios para realizar esta accion");
                    lblPermiso.Visible = true;
                }

               
            }
        }

        void TraerUsuario(string nick)
        {
            List<Propiedades_BE.Usuario> usuario = GestorUsuario.consultarNick(nick);
            lblNickResp.Text = usuario[0].Nick;
            lblNombreResp.Text = usuario[0].Nombre;
            lblMailResp.Text = usuario[0].Mail;

            int IdIdioma = usuario[0].IdIdioma;
            if (IdIdioma == 1)
            {
                lblIdiomaResp.Text = "Español";
            }
            else
            {
                lblIdiomaResp.Text = "Ingles";
            }
        }

        void TraerPermisos()
        {            
            TempUs = new Propiedades_BE.Usuario();
            TempUs.IdUsuario = GestorUsuario.SeleccionarIDNick(lblNickResp.Text);
            TempUs.Nombre = lblNombreResp.Text;
            GestorPermisos.FillUserComponents(TempUs);
            MostrarPermisos(TempUs);
        }

        public void MostrarPermisos(Propiedades_BE.Usuario u)
        {
            this.TreeViewPermisos.Nodes.Clear();
            TreeNode Root = new TreeNode(u.Nombre);
            foreach (var item in u.Permisos)
            {
                LlenarTreeView(Root, item);
            }
            this.TreeViewPermisos.Nodes.Add(Root);
            this.TreeViewPermisos.ExpandAll();
        }

        public void LlenarTreeView(TreeNode Padre, Propiedades_BE.Componente C)
        {
            TreeNode Hijo = new TreeNode(C.Nombre);
            //Padre.Tag = C;
            Padre.ChildNodes.Add(Hijo);
            if (C.Hijos != null)
            {
                foreach (var item in C.Hijos)
                {
                    LlenarTreeView(Hijo, item);
                }
            }
        }

        protected void LinkRedireccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Usuarios/IndexUsuarios.aspx");
        }
    }
}