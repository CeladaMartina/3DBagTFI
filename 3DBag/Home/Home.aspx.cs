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
    public partial class Home : System.Web.UI.Page
    {       
        int id;
        private ContentPlaceHolder contentPlace;
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //Primero se conecta con la BD
            Conectar();
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
                    id = Convert.ToInt32(Request.QueryString["usuario"]);
                    if (id == 17)
                    {
                        containerError.Visible = false;
                        containerHome.Visible = true;
                        lblUsuario.Text = "Bienvenido WEBMASTER";
                    }
                    lblUsuario.Visible = true;
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

        void RUsuario()
        {
            GestorUsuario.RecalcularDVH();
            Seguridad.CargarBitacora(Propiedades_BE.SingletonLogin.GlobalIdUsuario, DateTime.Now, "DVH Usuario recalculado", "Alta", 0);
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
            RUsuario(); //recalculamos la tabla Usuario
            RDVV();     //recalculamos la tabla Digitos Verticales    
            Session["ProblemaDefinitivo"] = null;
            Propiedades_BE.SingletonLogin.GlobalIntegridad = 0;
            Response.Redirect("../Home/Login.aspx");
        }

        #endregion
    }
}