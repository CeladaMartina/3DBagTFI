using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Acceso_BD
    {
        public SqlConnection Conexion = new SqlConnection();

        static string _confinal = "";

        public string GlobalConexion
        {
            get { return _confinal; }
            set { _confinal = value; }
        }


        #region conexion

        public void GenerarConexion(string Conexion)
        {
            GlobalConexion = Conexion;
        }
        public void AbrirConexion()
        {
            try
            {
                Conexion.ConnectionString = GlobalConexion;
                Conexion.Open();
            }
            catch (Exception)
            {
                CerrarConexion();
                Conexion.ConnectionString = GlobalConexion;
                Conexion.Open();
            }
        }

        public void CerrarConexion()
        {
            Conexion.Close();
        }
        #endregion

        public DataTable Leer(string S, SqlParameter[] P)
        {
            AbrirConexion();
            DataTable Tabla = new DataTable();
            SqlDataAdapter Adaptador = new SqlDataAdapter();
            Adaptador.SelectCommand = new SqlCommand();
            Adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            Adaptador.SelectCommand.CommandText = S;
            Adaptador.SelectCommand.Connection = Conexion;

            if (P != null)
            {
                Adaptador.SelectCommand.Parameters.AddRange(P);
            }

            Adaptador.Fill(Tabla);
            CerrarConexion();
            return Tabla;
        }

        public int Escribir(string S, SqlParameter[] P)
        {
            int FA = 0;
            AbrirConexion();
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = S;
            Cmd.Connection = Conexion;
            Cmd.Parameters.AddRange(P);

            try
            {
                FA = Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                FA = -1;
            }

            CerrarConexion();
            return FA;
        }

        public void EjecutarConsulta(string Consulta)
        {
            AbrirConexion();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Conexion;
            Cmd.CommandText = Consulta;
            Cmd.ExecuteNonQuery();
            CerrarConexion();
        }
    }
}
