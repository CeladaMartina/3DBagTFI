using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Idioma
    {
        Acceso_BD Acceso = new Acceso_BD();

        public List<Propiedades_BE.Idioma> Listar()
        {
            List<Propiedades_BE.Idioma> ListarIdioma = new List<Propiedades_BE.Idioma>();
            DataTable tabla = Acceso.Leer("ListarIdioma", null);

            foreach (DataRow R in tabla.Rows)
            {
                Propiedades_BE.Idioma I = new Propiedades_BE.Idioma();
                I.IdIdioma = int.Parse(R["IdIdioma"].ToString());
                I.NombreIdioma = R["NombreIdioma"].ToString();
                I.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                ListarIdioma.Add(I);
            }
            return ListarIdioma;
        }

        public List<string> NombreIdioma()
        {
            List<string> NomIdioma = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string query = "select NombreIdioma from Idioma where BajaLogica <> 1";
                using (SqlCommand command = new SqlCommand(query, Acceso.Conexion))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            NomIdioma.Add(lector.GetString(0).ToString());
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return NomIdioma;
        }

        public int Alta(Propiedades_BE.Idioma I)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@IdIdioma", I.IdIdioma);
            Param[1] = new SqlParameter("@NombreIdioma", I.NombreIdioma);
            fa = Acceso.Escribir("AltaIdioma", Param);
            return fa;
        }

        public int Baja(Propiedades_BE.Idioma I)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@IdIdioma", I.IdIdioma);
            fa = Acceso.Escribir("BajaIdioma", Param);
            return fa;
        }

        public int Modificar(Propiedades_BE.Idioma I)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@IdIdioma", I.IdIdioma);
            Param[1] = new SqlParameter("@NombreIdioma", I.NombreIdioma);
            fa = Acceso.Escribir("ModificarIdioma", Param);
            return fa;
        }
    }
}
