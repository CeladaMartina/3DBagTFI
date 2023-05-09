using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Cliente
    {
        Acceso_BD Acceso = new Acceso_BD();
        Seguridad Seguridad = new Seguridad();

        public int Alta(Propiedades_BE.Cliente C)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[7];
            P[0] = new SqlParameter("@IdCliente", C.IdCliente);
            P[1] = new SqlParameter("@DNI", C.DNI);
            P[2] = new SqlParameter("@Nombre", C.Nombre);
            P[3] = new SqlParameter("@Apellido", C.Apellido);
            P[4] = new SqlParameter("@FechaNac", C.FechaNac);
            P[5] = new SqlParameter("@Tel", C.Tel);
            P[6] = new SqlParameter("@Mail", C.Mail);
            fa = Acceso.Escribir("AltaCliente", P);
            return fa;
        }
    }
}
