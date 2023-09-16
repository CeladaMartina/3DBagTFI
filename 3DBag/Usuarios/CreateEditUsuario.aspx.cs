using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3DBag
{
    public partial class CreateEditUsuario : System.Web.UI.Page
    {        
        string nick;
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();        

        Propiedades_BE.Usuario TempUs;        
        protected void Page_Load(object sender, EventArgs e)
        {            
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (!IsPostBack)
            {
                nick = Request.QueryString["usuario"];
                TraerUsuario();
                TraerPermisos();               
            }           
        }

        #region metodos
        void TraerUsuario()
        {
            List<Propiedades_BE.Usuario> usuario = GestorUsuario.consultarNick(nick);
            txtNick.Text = usuario[0].Nick;
            txtNombre.Text = usuario[0].Nombre;
            txtMail.Text = usuario[0].Mail;
            //txtIdUsuario.Text =  usuario[0].IdUsuario.ToString();

            if (Convert.ToString(usuario[0].Estado) == "true")
            {
                //rdbBloqueado.Checked = true;
            }
            else
            {
                //rdbBloqueado.Checked = false;
            }

            if(Convert.ToString(usuario[0].IdIdioma) == "1")
            {
                txtIdioma.Text = "Español";
            }
            else
            {
                txtIdioma.Text = "Ingles";
            }            

            if (Convert.ToString(usuario[0].BajaLogica) == "true")
            {
                //rdbBaja.Checked = true;
            }
            else
            {
                //rdbBaja.Checked = false;
            }           
        }

        void Modificar(int Id, string Nick, string Nombre, string Mail, bool Estado, int Contador, string Idioma, int DVH)
        {
            if (GestorUsuario.Modificar(Id, Nick, Nombre, Mail, Estado, Contador, Idioma, DVH) == 0)
            {
                //lblResultado.Visible = true;
                //lblResultado.CssClass = "label-success";
                //lblResultado.Text = "Usuario modificado correctamente";
            }

            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Modificar usuario", "Alta", 0);           
            LimpiarTxt();
            
        }

        void LimpiarTxt()
        {
            txtNick.Text = "";
            txtNombre.Text = "";
            txtMail.Text = "";
            txtIdioma.Text = "";
            //rdbBaja.Checked = false;
            //rdbBloqueado.Checked = false;

        }

        //trae permisos de ese usuario
        void TraerPermisos()
        {
            TempUs = new Propiedades_BE.Usuario();
            TempUs.IdUsuario = GestorUsuario.SeleccionarIDNick(txtNick.Text);
            TempUs.Nombre = txtNombre.Text;

            //trae las patentes de ese usuario especifico
            PAsig.DataSource = GestorPermisos.FillUserComponentsList(TempUs);
            PAsig.DataBind();

            //trae todas las patentes
            PNoAsig.DataSource = GestorPermisos.GetAllPatentes();
            PNoAsig.DataBind();

            //elimina de patente no asignada, permisos que ya tiene el usuario
            foreach(ListItem item1 in PAsig.Items)
            {
                bool existe = false;

                foreach(ListItem item2 in PNoAsig.Items)
                {
                    if(item1.Text == item2.Text && item1.Value == item2.Value)
                    {
                        existe = true;
                        PNoAsig.Items.Remove(item2);
                        break;
                    }
                }                
            }
        }
        
        void AgregarPatente()
        {
            TempUs = new Propiedades_BE.Usuario();
            if (TempUs != null)
            {
                string ope = PNoAsig.SelectedItem.Value;
                PAsig.Items.Add(ope);
                PNoAsig.Items.Remove(ope);
            }
        }

        void QuitarPatente()
        {
            TempUs = new Propiedades_BE.Usuario();

            if (TempUs != null)
            {
                string ope = PAsig.SelectedItem.Value;
                PNoAsig.Items.Add(ope);
                PAsig.Items.Remove(ope);
            }
        }

        void AgregarFamilia()
        {

        }

        void QuitarFamilia()
        {

        }

        void Guardar()
        {            
            Propiedades_BE.Componente c1;
            
            TempUs = new Propiedades_BE.Usuario();            
            TempUs.IdUsuario = GestorUsuario.SeleccionarIDNick(txtNick.Text);

            foreach (ListItem item in PAsig.Items)
            {
                c1 = new Propiedades_BE.Patente();
                string permiso = item.Text.Replace(" ", "_");                
                c1.Nombre = item.ToString();                

                c1.Id = GestorPermisos.traerIDPermiso(c1.Nombre);
                c1.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permiso);

                TempUs.Permisos.Add(c1);
            }
            
            GestorUsuario.GuardarPermisos(TempUs);

        }
        #endregion
        #region boton
        public void ModificarUsuario(object sender, EventArgs e)
        {
            try
            {
                Modificar(Convert.ToInt32(txtIdUsuario.Text), txtNick.Text, txtNombre.Text, txtMail.Text, false, 0, txtIdioma.Text, 0);
            }
            catch (Exception)
            {
                //MessageBox.Show(CambiarIdioma.TraducirGlobal("Error") ?? "Error");
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                AgregarPatente();
            }
            catch (Exception)
            {
                //error
            }

        }

        protected void btnNoAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                QuitarPatente();
            }
            catch (Exception)
            {
                //error
            }
        }

        protected void btnFunction_Click(object sender, EventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception)
            {
                //error
            }
        }
        #endregion


    }
}