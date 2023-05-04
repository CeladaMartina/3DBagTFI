using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Usuario
    {
        Acceso_BD Acceso = new Acceso_BD();
        public void GenerarConexion(string Conexion)
        {
            Acceso.GenerarConexion(Conexion);
        }

        public string GetConexion()
        {
            return Acceso.GlobalConexion;
        }
    }
}
