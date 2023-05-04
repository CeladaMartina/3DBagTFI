using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_DAL;

namespace Negocio_BLL
{
    public class Usuario
    {
        Acceso_DAL.Usuario Mapper = new Acceso_DAL.Usuario();
        public void GenerarConexion(string usuario, string basedatos)
        {
            string Conexion = "";
            string DataSourceRaw = "";
            string DataSourceRaw2 = "";
            DataSourceRaw = usuario;
            DataSourceRaw2 = basedatos;           
            Conexion = @"Data Source=" + DataSourceRaw + ";Initial Catalog=" + DataSourceRaw2 + ";Integrated Security=True; MultipleActiveResultSets=true";
            Mapper.GenerarConexion(Conexion);
        }

        public string GetConexion()
        {
            return Mapper.GetConexion();
        }
    }
}
