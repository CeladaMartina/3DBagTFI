using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Traduccion
    {
        Acceso_BD Acceso = new Acceso_BD();

        public List<Propiedades_BE.Traduccion> Listar(Propiedades_BE.Traduccion Traduccion)
        {
            List<Propiedades_BE.Traduccion> ListaTraduccion = new List<Propiedades_BE.Traduccion>();
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@NombreIdioma", Traduccion.NombreIdioma);
            DataTable tabla = Acceso.Leer("ListarTraduccion", Param);

            foreach (DataRow R in tabla.Rows)
            {
                Propiedades_BE.Traduccion T = new Propiedades_BE.Traduccion();
                T.IdTraduccion = int.Parse(R["IdTraduccion"].ToString());
                T.NombreIdioma = R["NombreIdioma"].ToString();
                T.Original = R["Original"].ToString();
                T.Traducido = R["Traducido"].ToString();
                ListaTraduccion.Add(T);
            }
            return ListaTraduccion;
        }

        public List<Propiedades_BE.Traduccion> ListarTraduccionDiccionario(Propiedades_BE.Traduccion Traduccion)
        {
            List<Propiedades_BE.Traduccion> Lista = new List<Propiedades_BE.Traduccion>();
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@NombreIdioma", Traduccion.NombreIdioma);
            DataTable tabla = Acceso.Leer("ListarTraduccionDiccionario", Param);
            foreach (DataRow R in tabla.Rows)
            {
                Propiedades_BE.Traduccion T = new Propiedades_BE.Traduccion();
                T.Original = R["Original"].ToString();
                T.Traducido = R["Traducido"].ToString();
                Lista.Add(T);
            }
            return Lista;
        }

        public List<Propiedades_BE.Traduccion> IdiomaDefault()
        {
            List<Propiedades_BE.Traduccion> TraduccionDefault = new List<Propiedades_BE.Traduccion>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string query = "select Original from IdiomaOrigin";
                using (SqlCommand command = new SqlCommand(query, Acceso.Conexion))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Propiedades_BE.Traduccion T = new Propiedades_BE.Traduccion();
                            T.Original = lector["Original"].ToString();
                            TraduccionDefault.Add(T);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return TraduccionDefault;
        }

        public List<Propiedades_BE.Traduccion> FiltrarIdiomaDefault(string Original)
        {
            List<Propiedades_BE.Traduccion> TraduccionDefault = new List<Propiedades_BE.Traduccion>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string query = "select Original from IdiomaOrigin where Original like '%" + Original + "%'";
                using (SqlCommand command = new SqlCommand(query, Acceso.Conexion))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Propiedades_BE.Traduccion T = new Propiedades_BE.Traduccion();
                            T.Original = lector["Original"].ToString();
                            TraduccionDefault.Add(T);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return TraduccionDefault;
        }

        public int Alta(Propiedades_BE.Traduccion T)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@NombreIdioma", T.NombreIdioma);
            Param[1] = new SqlParameter("@Original", T.Original);
            Param[2] = new SqlParameter("@Traducido", T.Traducido);
            fa = Acceso.Escribir("AltaTraduccion", Param);
            return fa;
        }

        public int Baja(Propiedades_BE.Traduccion T)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@IdTraduccion", T.IdTraduccion);
            fa = Acceso.Escribir("BajaTraduccion", Param);
            return fa;
        }

        public int Modificar(Propiedades_BE.Traduccion T)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@IdTraduccion", T.IdTraduccion);
            Param[1] = new SqlParameter("@NombreIdioma", T.NombreIdioma);
            Param[2] = new SqlParameter("@Original", T.Original);
            Param[3] = new SqlParameter("@Traducido", T.Traducido);
            fa = Acceso.Escribir("ModificarTraduccion", Param);
            return fa;
        }
    }
}
