using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Producto
    {
        Acceso_BD Acceso = new Acceso_BD();

        #region Seguridad

        public void EjecutarConsulta(string Consulta)
        {
            Acceso.AbrirConexion();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Acceso.Conexion;
            Cmd.CommandText = Consulta;
            Cmd.ExecuteNonQuery();
            Acceso.CerrarConexion();
        }

        public int SeleccionarIdArticulo(int CodProd)
        {
            int ID = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select IdArticulo from Articulo where CodProd = " + CodProd + "";
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

        public int SeleccionarCodArticulo(string DescripcionProducto)
        {
            int Cod = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select CodProd from Articulo where Descripcion = '" + DescripcionProducto + "'";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Cod = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Cod;
        }

        public string SeleccionarNombreArt(int CodProd)
        {
            string Descripcion = "";
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Descripcion from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Descripcion = Lector.GetString(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Descripcion;
        }

        public int VerificarCantStock(int IdArticulo)
        {
            int Cant = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Stock from Articulo where IdArticulo = " + IdArticulo + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Cant = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Cant;
        }

        public decimal SeleccionPUnit(int CodProd)
        {
            decimal PUnit = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select PUnit from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            PUnit = decimal.Parse(Lector.GetValue(0).ToString());
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return PUnit;
        }

        public List<string> CodProdArticulo()
        {
            List<string> CodProd = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select CodProd from Articulo where BajaLogica != 1";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            CodProd.Add(Lector.GetInt32(0).ToString());
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return CodProd;
        }

        public List<Propiedades_BE.Articulo> consultarCodProd(int CodProd)
        {
            List<Propiedades_BE.Articulo> codProdList = new List<Propiedades_BE.Articulo>();
            Acceso.AbrirConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from Articulo where CodProd = '" + CodProd + "'";

            cmd.Connection = Acceso.Conexion;
            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Propiedades_BE.Articulo A = new Propiedades_BE.Articulo();
                A.IdArticulo = int.Parse(lector["IdArticulo"].ToString());
                A.CodProd = int.Parse(lector["CodProd"].ToString());
                A.Nombre = lector["Nombre"].ToString();
                A.Descripcion = lector["Descripcion"].ToString();
                A.Material = lector["Material"].ToString();
                A.Stock = int.Parse(lector["Stock"].ToString());
                A.PUnit = decimal.Parse(lector["PUnit"].ToString());
                codProdList.Add(A);
            }
            lector.Close();
            Acceso.CerrarConexion();
            return codProdList;
        }

        public List<string> DescripcionProd()
        {
            List<string> DescProd = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string query = "Select Descripcion From Articulo where BajaLogica != 1";
                using (SqlCommand cmd = new SqlCommand(query, Acceso.Conexion))
                {
                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            DescProd.Add(lector.GetString(0));
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return DescProd;
        }

        public int SeleccionarStock(int CodProd)
        {
            int Stock = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Stock from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Stock = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Stock;
        }

        #endregion

        #region ABML

        public List<Propiedades_BE.Articulo> Listar()
        {
            List<Propiedades_BE.Articulo> ListarArticulo = new List<Propiedades_BE.Articulo>();
            DataTable Tabla = Acceso.Leer("ListarArticulo", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Articulo A = new Propiedades_BE.Articulo();
                A.IdArticulo = int.Parse(R["IdArticulo"].ToString());
                A.CodProd = int.Parse(R["CodProd"].ToString());
                A.Nombre = (R["Nombre"].ToString());
                A.Descripcion = (R["Descripcion"].ToString());
                A.Material = (R["Material"].ToString());                
                A.NombreLocalidad = (R["NombreLocalidad"].ToString());
                A.Stock = int.Parse(R["Stock"].ToString());
                A.PUnit = decimal.Parse(R["PUnit"].ToString());
                //se convierte la imagen  de byte a base64 y despues a img
                byte[] bytes = (byte[])R["Imagen"];
                string str = Convert.ToBase64String(bytes);
                string URL = "data:Image/png;base64," + str;                
                A.Imagen = URL;
                //fin de imagen
                A.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                ListarArticulo.Add(A);
            }
            return ListarArticulo;
        }

        public int Alta(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[9];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);
            P[1] = new SqlParameter("@IdLocalidad", A.IdLocalidad);
            P[2] = new SqlParameter("@CodProd", A.CodProd);
            P[3] = new SqlParameter("@Nombre", A.Nombre);
            P[4] = new SqlParameter("@Descripcion", A.Descripcion);
            P[5] = new SqlParameter("@Material", A.Material);
            P[6] = new SqlParameter("@Talle", A.Talle);
            P[7] = new SqlParameter("@Stock", A.Stock);
            P[8] = new SqlParameter("@PUnit", A.PUnit);
            fa = Acceso.Escribir("AltaArticulo", P);
            return fa;
        }

        public int Modificar(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[8];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);
            P[1] = new SqlParameter("@IdLocalidad", A.IdLocalidad);
            P[2] = new SqlParameter("@CodProd", A.CodProd);
            P[3] = new SqlParameter("@Nombre", A.Nombre);
            P[4] = new SqlParameter("@Descripcion", A.Descripcion);
            P[5] = new SqlParameter("@Material", A.Material);
            P[6] = new SqlParameter("@Talle", A.Talle);
            P[7] = new SqlParameter("@PUnit", A.PUnit);
            fa = Acceso.Escribir("ModificarArticulo", P);
            return fa;
        }

        public int Baja(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[1];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);
            fa = Acceso.Escribir("BajaArticulo", P);
            return fa;
        }

        public List<Propiedades_BE.Articulo> ListarTopProductos()
        {
            List<Propiedades_BE.Articulo> ListarTopProductos = new List<Propiedades_BE.Articulo>();
            DataTable Tabla = Acceso.Leer("ListarTopProductos", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Articulo A = new Propiedades_BE.Articulo();
                A.CodProd = int.Parse(R["CodProd"].ToString());
                A.Nombre = R["Nombre"].ToString();
                A.Descripcion = R["Descripcion"].ToString();
                A.PUnit = decimal.Parse(R["Precio"].ToString());
                ListarTopProductos.Add(A);
            }
            return ListarTopProductos;
        }
        #endregion
    }
}
