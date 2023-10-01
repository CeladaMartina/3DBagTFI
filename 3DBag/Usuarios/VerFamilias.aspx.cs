using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class VerFamilias : System.Web.UI.Page, IObserver
    {
        private ContentPlaceHolder contentPlace;
        Propiedades_BE.Familia FamTemp;
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


                FamTemp = new Propiedades_BE.Familia();
                FamTemp.Id = Convert.ToInt32(Request.QueryString["id"]);
                FamTemp.Nombre = Request.QueryString["nombre"];
                lblNombreResp.Text = FamTemp.Nombre;
                MostrarFamiliaPermisos(true);
            }
        }

        void MostrarFamiliaPermisos(bool Fam)
        {
            if (FamTemp == null)
            {
                return;
            }
            IList<Propiedades_BE.Componente> Familia = null;
            if (Fam)
            {
                Familia = GestorPermisos.GetAll("=" + FamTemp.Id);
                foreach (var i in Familia)
                {
                    FamTemp.AgregarHijo(i);

                }
            }
            else
            {
                Familia = FamTemp.Hijos;
            }
            this.TreeView1.Nodes.Clear();
            TreeNode root = new TreeNode(FamTemp.Nombre);
            root.Value = FamTemp.ToString();
            this.TreeView1.Nodes.Add(root);
            foreach (var item in Familia)
            {
                LlenarTreeView(root, item);
            }
            TreeView1.ExpandAll();
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
            Response.Redirect("../Usuarios/IndexFamilias.aspx");
        }

        #region traduccion
        public void Update(ISubject Sujeto)
        {
            lblTitulo.Text = Sujeto.TraducirObserver(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = Sujeto.TraducirObserver(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDetalles.Text = Sujeto.TraducirObserver(lblDetalles.SkinID.ToString()) ?? lblDetalles.SkinID.ToString();
            LinkRedireccion.Text = Sujeto.TraducirObserver(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
        }

        public void Traducir()
        {
            lblTitulo.Text = SiteMaster.TraducirGlobal(lblTitulo.SkinID.ToString()) ?? lblTitulo.SkinID.ToString();
            lblNombre.Text = SiteMaster.TraducirGlobal(lblNombre.SkinID.ToString()) ?? lblNombre.SkinID.ToString();
            lblDetalles.Text = SiteMaster.TraducirGlobal(lblDetalles.SkinID.ToString()) ?? lblDetalles.SkinID.ToString();
            LinkRedireccion.Text = SiteMaster.TraducirGlobal(LinkRedireccion.SkinID.ToString()) ?? LinkRedireccion.SkinID.ToString();
        }
        #endregion
    }
}