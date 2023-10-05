using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class DeleteShowUsuario : System.Web.UI.Page, IObserver
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
                //traduccion de la pagina
                
                if (Session["IdiomaSelect"] != null)
                {
                    DropDownList masterDropDownList = (DropDownList)Master.FindControl("DropDownListIdioma");
                    masterDropDownList.SelectedValue = Session["IdiomaSelect"].ToString();
                    Traducir();
                }

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

        void Baja(int IdUsuario)
        {
            if(GestorUsuario.Baja(IdUsuario) == 0)
            {
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Error de baja usuario", "Alta", 0);
                lblResultado.Visible = true;
                lblResultado.CssClass = "label-success";
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = SiteMaster.TraducirGlobal("Baja de Producto exitosamente") ?? ("Baja de Producto exitosamente");
                Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Baja Articulo", "Baja", 0);
            }
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblUsuario.Text = Sujeto.TraducirObserver(lblUsuario.SkinID.ToString()) ?? lblUsuario.SkinID.ToString();
            lblNick.Text = Sujeto.TraducirObserver(lblNick.SkinID.ToString()) ?? lblNick.SkinID.ToString();
            lblPregunta.Text = Sujeto.TraducirObserver(lblPregunta.SkinID.ToString()) ?? lblPregunta.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblMail.Text = Sujeto.TraducirObserver(lblMail.SkinID.ToString()) ?? lblMail.SkinID.ToString();
            lblIdioma.Text = Sujeto.TraducirObserver(lblIdioma.SkinID.ToString()) ?? lblIdioma.SkinID.ToString();
            lblPermisos.Text = Sujeto.TraducirObserver(lblPermisos.SkinID.ToString()) ?? lblPermisos.SkinID.ToString();
            btnBorrar.Text = Sujeto.TraducirObserver(btnBorrar.SkinID.ToString()) ?? btnBorrar.SkinID.ToString();
            LinkRedireccion.Text = Sujeto.TraducirObserver(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString(); 
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblUsuario.Text = SiteMaster.TraducirGlobal(lblUsuario.SkinID.ToString()) ?? lblUsuario.SkinID.ToString();
            lblNick.Text = SiteMaster.TraducirGlobal(lblNick.SkinID.ToString()) ?? lblNick.SkinID.ToString();
            lblPregunta.Text = SiteMaster.TraducirGlobal(lblPregunta.SkinID.ToString()) ?? lblPregunta.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblMail.Text = SiteMaster.TraducirGlobal(lblMail.SkinID.ToString()) ?? lblMail.SkinID.ToString();
            lblIdioma.Text = SiteMaster.TraducirGlobal(lblIdioma.SkinID.ToString()) ?? lblIdioma.SkinID.ToString();
            lblPermisos.Text = SiteMaster.TraducirGlobal(lblPermisos.SkinID.ToString()) ?? lblPermisos.SkinID.ToString();
            btnBorrar.Text = SiteMaster.TraducirGlobal(btnBorrar.SkinID.ToString()) ?? btnBorrar.SkinID.ToString();
            LinkRedireccion.Text = SiteMaster.TraducirGlobal(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();   
        }
        #endregion

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            nick = Request.QueryString["usuario"];

            try
            {
                Baja(GestorUsuario.SeleccionarIDNick(nick));
            }
            catch (Exception)
            {
                lblResultado.Text = SiteMaster.TraducirGlobal("Error de Servicio") ?? ("Error de Servicio");

            }
        }
    }
}