using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio_BLL;
using Propiedades_BE;

namespace _3DBag
{
    public partial class Home : System.Web.UI.Page, IObserver
    {       
        int id;
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        Negocio_BLL.Permisos GestorPermisos = new Negocio_BLL.Permisos();
        Negocio_BLL.Producto GestorProducto = new Negocio_BLL.Producto();
        Negocio_BLL.Venta GestorVenta = new Negocio_BLL.Venta();
        Negocio_BLL.Detalle_Venta GestorDetalleVenta = new Negocio_BLL.Detalle_Venta();


        Propiedades_BE.Usuario usuario;

        #region patron
        PatternChain.Cliente cliente = new PatternChain.Cliente();
        PatternChain.Empleado empleado = new PatternChain.Empleado();
        PatternChain.Administrador administrador = new PatternChain.Administrador();
        PatternChain.Webmaster webmaster = new PatternChain.Webmaster();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //Primero se conecta con la BD
            Conectar();

            //patron chain
            webmaster.AgregarSiguiente(administrador);
            administrador.AgregarSiguiente(empleado);
            empleado.AgregarSiguiente(cliente);                    

            //Si el usuario se logueo, controla que mensaje de bienvenida dar
            if(Session["ProblemaDefinitivo"] != null)
            {
                containerHome.Visible = false;
                containerError.Visible = true;
                lblError.Text = Session["ProblemaDefinitivo"].ToString();
            }
            else
            {
                if((Propiedades_BE.Usuario)(Session["UserSession"]) != null)
                {
                    containerError.Visible = false;
                    containerHome.Visible = true;
                    lblUsuario.Visible = true;
                    lblUsuario.Text = webmaster.Procesar(usuario);                    
                }
            }
            
        }

        #region metodos
        public void Conectar()
        {
            SqlConnection Conexion = new SqlConnection();
            GestorUsuario.GenerarConexion("GASGANO", "TFI_Empresa");
            Conexion.ConnectionString = GestorUsuario.GetConexion();
            Conexion.Open();
        }

        //recalcular dvh 
        void RUsuario()
        {
            GestorUsuario.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Usuario recalculado", "Alta", 0);
        }

        void RPermiso()
        {
            GestorPermisos.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Permisos recalculado", "Alta", 0);
        }

        void RArticulo()
        {
            GestorProducto.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Articulo recalculado", "Alta", 0);
        }

        void RIdioma()
        {
            GestorProducto.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Idioma recalculado", "Alta", 0);
        }

        void RVenta()
        {
            GestorVenta.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Venta recalculado", "Alta", 0);
        }

        void RBitacora()
        {
            Seguridad.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Bitacora recalculado", "Alta", 0);
        }

        void RDetalleVenta()
        {
            GestorDetalleVenta.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Bitacora recalculado", "Alta", 0);
        }

        void RDVV()
        {
            Seguridad.RecalcularDVV();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "Digitos DVV recalculados", "Alta", 0);     
        }

        #endregion
        #region botones 
        protected void hrefRestaurar(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Restore.aspx");
           
        }

        protected void hrefRecalcular(object sender, EventArgs e)
        {
            RecalcularTablas();              
            Session["ProblemaDefinitivo"] = null;
            Propiedades_BE.SingletonLogin.GlobalIntegridad = 0;
            Response.Redirect("../Home/Login.aspx");
        }

        public void Update(ISubject Sujeto)
        {
            throw new NotImplementedException();
        }

        void RecalcularTablas()
        {
            //RUsuario(); //recalculamos la tabla Usuario
            //RPermiso(); //recalculamos la tabla Permiso
            //RArticulo(); //recalculamos la tabla articulo
            //RIdioma(); //recalculamos la tabla idioma
            RDetalleVenta(); //recalculamos la tabla detalle venta
            RVenta(); //recalculamos la tabla Venta
            RBitacora();
            RDVV();     //recalculamos la tabla Digitos Verticales  
        }
        #endregion
    }
}