using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio_BLL;

namespace _3DBag
{
    public partial class Login : System.Web.UI.Page
    {
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            Conectar();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {

        }

        public void Conectar()
        {
            SqlConnection Conexion = new SqlConnection();
            GestorUsuario.GenerarConexion("GASGANO", "BD3dbag");
            Conexion.ConnectionString = GestorUsuario.GetConexion();
            Conexion.Open();
        }
    }
}