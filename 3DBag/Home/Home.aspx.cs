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
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        Negocio_BLL.Usuario GestorUsuario = new Negocio_BLL.Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            Conectar();
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