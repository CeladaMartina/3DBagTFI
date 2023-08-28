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
        protected void Page_Load(object sender, EventArgs e)
        {
            contentPlace = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //Primero se conecta con la BD
            Conectar();
            //Si el usuario se logueo, controla que mensaje de bienvenida dar
            if(Session["ProblemaDefinitivo"] == null)
            {
                if ((Propiedades_BE.Usuario)(Session["UserSession"]) != null)
                {
                    id = Convert.ToInt32(Request.QueryString["usuario"]);
                    if (id == 17)
                    {
                        lblUsuario.Text = "Bienvenido WEBMASTER";
                    }
                    lblUsuario.Visible = true;
                }
                else
                {
                    lblUsuario.Visible = false;
                }
            }
            else
            {
                //si hay un problema en la base lo mostrara aca
                lblUsuario.Visible = true;
                lblUsuario.Text = Session["ProblemaDefinitivo"].ToString();
            }
            
        }
        public void Conectar()
        {
            SqlConnection Conexion = new SqlConnection();
            GestorUsuario.GenerarConexion("GASGANO", "Diploma_Empresa");
            Conexion.ConnectionString = GestorUsuario.GetConexion();
            Conexion.Open();
        }
    }
}