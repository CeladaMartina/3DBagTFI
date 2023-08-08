using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Propiedades_BE.Cliente> Listar()
        {
            List<Propiedades_BE.Cliente> ListarCliente = new List<Propiedades_BE.Cliente>();
            DataTable Tabla = Acceso.Leer("ListarCliente", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Cliente C = new Propiedades_BE.Cliente();
                C.IdCliente = int.Parse(R["IdCliente"].ToString());
                C.DNI = Seguridad.Desencriptar(R["DNI"].ToString());
                C.Nombre = R["Nombre"].ToString();
                C.Apellido = R["Apellido"].ToString();
                C.FechaNac = DateTime.Parse(R["FechaNac"].ToString());
                C.Tel = int.Parse(R["Tel"].ToString());
                C.Mail = R["Mail"].ToString();
                C.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                ListarCliente.Add(C);
            }
            return ListarCliente;
        }
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

        public int Modificar(Propiedades_BE.Cliente C)
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
            fa = Acceso.Escribir("ModificarCliente", P);
            return fa;
        }

        public int Baja(Propiedades_BE.Cliente C)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[1];
            P[0] = new SqlParameter("@IdCliente", C.IdCliente);
            fa = Acceso.Escribir("BajaCliente", P);
            return fa;
        }

        public int SeleccionarID(string DNI)
        {
            int ID = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "Select IdCliente from Cliente where DNI = '" + Seguridad.EncriptarAES(DNI) + "'";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            ID = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return ID;
        }

        public string SeleccionarNombreCliente(string DNI)
        {
            string NomCli = "";
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "Select Nombre from Cliente where DNI = '" + Seguridad.EncriptarAES(DNI) + "'";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            NomCli = Lector.GetString(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return NomCli;
        }

        public int SeleccionarDNICliente(string NombreCliente)
        {
            int DNI = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "Select DNI from Cliente where Nombre = '" + NombreCliente + "'";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            DNI = int.Parse(Seguridad.Desencriptar(Lector.GetString(0)));
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return DNI;
        }

        public List<string> NombresClientes()
        {
            List<string> NomCli = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "Select Nombre from Cliente";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            NomCli.Add(Lector.GetString(0));
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return NomCli;
        }

        public List<string> DNIsClientes()
        {
            List<string> DNICli = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryDNIs = "select DNI from Cliente";
                using (SqlCommand Cmd = new SqlCommand(QueryDNIs, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            DNICli.Add(Seguridad.Desencriptar(Lector.GetString(0).ToString()));
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return DNICli;
        }
    }
}
